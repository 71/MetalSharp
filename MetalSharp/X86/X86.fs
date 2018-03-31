namespace MetalSharp.X86

open System

open MetalSharp

/// Defines a register.
[<Struct; RequireQualifiedAccess>]
type Reg<'s when 's :> OS> = N of n: byte | H of h: byte | XMM of xmm: byte | S of s: byte
with
    member this.Value = match this with
                        | Reg.N b | Reg.H b | Reg.S b | Reg.XMM b -> b

    member this.BaseValue = this.Value % 8uy

    member this.PrefixAdder = if this.Value > 7uy then 1uy else 0uy


/// Defines an operand which is either a register or a memory pointer.
[<AbstractClass>]
type RegisterOrMemory<'s when 's :> OS and 's : (new : unit -> 's)> internal() =
    inherit Operand<'s>()

/// Defines a branch condition.
type BranchCondition =
    /// Overflow.
    | O = 0uy
    /// No overflow.
    | NO = 1uy
    /// Below.
    | B = 2uy
    /// Not below.
    | NB = 3uy
    /// Zero.
    | Z = 4uy
    /// Not zero.
    | NZ = 5uy
    /// Below or equal.
    | BE = 6uy
    /// Above.
    | A = 7uy
    /// Has sign.
    | S = 8uy
    /// Does not have sign.
    | NS = 9uy
    /// Parity: even.
    | PE = 10uy
    /// Parity: odd.
    | PO = 11uy
    /// Less.
    | L = 12uy
    /// Not less.
    | NL = 13uy
    /// Less or equal.
    | LE = 14uy
    /// Greater.
    | G = 15uy


/// Defines an immediate operand.
[<Sealed>]
type Immediate<'s when 's :> OS and 's : (new : unit -> 's)>(data: byte[]) =
    inherit Operand<'s>()

    do assert(data.Length = int (new 's()).Size / 8)

    /// Gets the data that is to be given by this immediate.
    member __.Data = data

    override __.IsImmediate = true
    override __.IsRegister = false
    override __.IsMemory = false

/// Defines a register operand.
[<Sealed>]
type Register<'s when 's :> OS and 's : (new : unit -> 's)>(r: Reg<'s>) =
    inherit RegisterOrMemory<'s>()

    /// Gets the underlying `Reg`.
    member __.Reg = r

    override __.IsImmediate = false
    override __.IsRegister = true
    override __.IsMemory = false

type Scale =
    | One   = 0b00_000_000uy
    | Two   = 0b01_000_000uy
    | Four  = 0b10_000_000uy
    | Eight = 0b11_000_000uy

[<AutoOpen>]
module Scale = type Scale with member this.Bits = byte this

/// Defines a memory operand.
type Memory<'s when 's :> OS and 's : (new : unit -> 's)> private(s: Scale option, i: Reg<'s> option, b: Reg<'s> option, disp: int) =
    inherit RegisterOrMemory<'s>()

    new(scale, base', index, ?disp) = Memory(Some scale, Some index, Some base', defaultArg disp 0)
    new(base', ?disp) = Memory(None, None, Some base', defaultArg disp 0)
    new(disp) = Memory(None, None, None, disp)

    override __.IsImmediate = false
    override __.IsRegister = false
    override __.IsMemory = true

    member __.Displacement = disp

    member __.Base = b

    member __.Index = i

    member __.Scale = s

    member __.GetSibByte() =
        match s with
        | Some s ->
            match i, b with
            | Some i, Some b -> Some(s.Bits ||| (i.Value <<< 3) ||| b.Value)
            | _ -> None
        | None -> None

    member __.GetDisplacementByte() =
        if b.IsNone then
            0b00_101_000uy
        elif disp = 0 then
            0b00_000_000uy
        elif disp <= 127 && disp >= -128 then
            0b01_000_000uy
        else
            0b10_000_000uy


/// Defines a set of utilities for emitting operands through a `CodeBuilder`.
[<AutoOpen>]
module Operands = 
    let imm v = Immediate v
    let imm8 v = Immediate [| v |] : Immediate<S8>
    let imm16 (v: int16) = imm (BitConverter.GetBytes v) : Immediate<S16>
    let imm32 (v: int32) = imm (BitConverter.GetBytes v) : Immediate<S32>
    let imm64 (v: int64) = imm (BitConverter.GetBytes v) : Immediate<S64>

    let emitImmediate (imm: Immediate<_>) (builder: CodeBuilder) =
        builder.Emit imm.Data

    let emitModrm (reg: byte) (rm: RegisterOrMemory<_>) (builder: CodeBuilder) =
        match rm with
        | :? Register<_> as src ->
            builder.Emit(0b11_000_000uy ||| (byte <| reg <<< 3) ||| src.Reg.BaseValue)
        | :? Memory<_> as src ->
            let disp = src.GetDisplacementByte()

            match src.GetSibByte() with
            | Some sib ->
                builder.Emit(disp ||| (reg <<< 3) ||| src.Base.Value.BaseValue)
                builder.Emit(sib) 
            | None ->
                assert(src.Index.IsNone)

                match src.Base with
                | Some b ->
                    builder.Emit(byte (disp ||| reg <<< 3 ||| b.BaseValue))
                | None ->
                    builder.Emit(byte (disp ||| reg <<< 3))

            if disp <> 0uy then
                builder.Emit(BitConverter.GetBytes src.Displacement)
        
        | _ -> invalidArg "rm" "Only Register and Memory arguments are accepted."

    let emitModrm' (op: Operand<_>) (rm: RegisterOrMemory<_>) ext builder =
        match op with
        | :? Register<'s> as reg -> emitModrm reg.Reg.BaseValue rm builder
        | :? Immediate<'s> as imm -> emitModrm ext rm builder
                                     emitImmediate imm builder
        | _ -> invalidArg "op" "Invalid argument."

    let emitMemory mem ext = emitModrm ext mem

    let emitMemory' mem = emitModrm 0uy mem

    let emitRegister (r: Register<_>) = emitModrm 0uy r

    let emitRegisterOpcode opcode (r: Register<_>) (builder: CodeBuilder) =
        let v = r.Reg.Value

        if v > 7uy then
            if v > 15uy then
                invalidArg "reg" "Invalid register or mod operand."
            else
                builder.Emit [| 0x44uy ; opcode + (v - 8uy) |]
        else
            builder.Emit (opcode + v)

    let emitPrefix (r: RegisterOrMemory<_>) (builder: CodeBuilder) =
        match r with
        | :? Register<_> as r ->
            let v = r.Reg.Value

            if v > 7uy then
                if v > 15uy then
                    invalidArg "reg" "Invalid register or mod operand."
                else
                    builder.Emit 0x41uy
        | _ -> ()

    let inline (!) (c: BranchCondition) = LanguagePrimitives.EnumOfValue(byte c ^^^ 1uy) : BranchCondition

    type BranchCondition with
        member this.Inverse() = !this

/// Defines a set of all supported x86 (and x86-64) registers.
[<AutoOpen>]
module Registers =

    let inline private r8' b = Reg.N (byte b) : Reg<S8>
    let inline private r16 b = Reg.N (byte b) : Reg<S16>
    let inline private r32 b = Reg.N (byte b) : Reg<S32>
    let inline private r64 b = Reg.N (byte b) : Reg<S64>
    let inline private h8 b = Reg.H (byte b) : Reg<S8>
    let inline private xmm b = Reg.XMM (byte b) : Reg<S128>
    let inline private seg b = Reg.S (byte b) : Reg<S16>

    let rax, rcx, rdx, rbx = r64 0x0, r64 0x1, r64 0x2, r64 0x3
    let rsp, rbp, rsi, rdi = r64 0x4, r64 0x5, r64 0x6, r64 0x7
    let r8,  r9,  r10, r11 = r64 0x8, r64 0x9, r64 0xA, r64 0xB
    let r12, r13, r14, r15 = r64 0xC, r64 0xD, r64 0xE, r64 0xF

    let eax, ecx, edx, ebx = r32 0x0, r32 0x1, r32 0x2, r32 0x3
    let esp, ebp, esi, edi = r32 0x4, r32 0x5, r32 0x6, r32 0x7
    let r8d,  r9d,  r10d, r11d = r32 0x8, r32 0x9, r32 0xA, r32 0xB
    let r12d, r13d, r14d, r15d = r32 0xC, r32 0xD, r32 0xE, r32 0xF

    let ax, cx, dx, bx = r16 0x0, r16 0x1, r16 0x2, r16 0x3
    let sp, bp, si, di = r16 0x4, r16 0x5, r16 0x6, r16 0x7
    let r8w,  r9w,  r10w, r11w = r16 0x8, r16 0x9, r16 0xA, r16 0xB
    let r12w, r13w, r14w, r15w = r16 0xC, r16 0xD, r16 0xE, r16 0xF

    let al, cl, dl, bl = r8' 0x0, r8' 0x1, r8' 0x2, r8' 0x3
    let spl, bpl, sil, dil = r8' 0x4, r8' 0x5, r8' 0x6, r8' 0x7
    let r8b,  r9b,  r10b, r11b = r8' 0x8, r8' 0x9, r8' 0xA, r8' 0xB
    let r12b, r13b, r14b, r15b = r8' 0xC, r8' 0xD, r8' 0xE, r8' 0xF

    let ah, ch, dh, bh = h8 0x0, h8 0x1, h8 0x2, h8 0x3

    let xmm0, xmm1, xmm2, xmm3 = xmm 0x1, xmm 0x2, xmm 0x3, xmm 0x4
    let xmm4, xmm5, xmm6, xmm7 = xmm 0x4, xmm 0x5, xmm 0x6, xmm 0x7

    let es, cs, ss, ds, fs, gs = seg 0x0, seg 0x1, seg 0x2, seg 0x3, seg 0x4, seg 0x5

    let rax', rcx', rdx', rbx' = Register rax, Register rcx, Register rdx, Register rbx
    let rsp', rbp', rsi', rdi' = Register rsp, Register rbp, Register rsi, Register rdi
    let eax', ecx', edx', ebx' = Register eax, Register ecx, Register edx, Register ebx
    let esp', ebp', esi', edi' = Register esp, Register ebp, Register esi, Register edi
    let ax', cx', dx', bx' = Register ax, Register cx, Register dx, Register bx
    let sp', bp', si', di' = Register sp, Register bp, Register si, Register di
    let al', cl', dl', bl' = Register ax, Register cx, Register dx, Register bx
    let spl', bpl', sil', dil' = Register spl, Register bpl, Register sil, Register dil
    let ah', ch', dh', bh' = Register ah, Register ch, Register dh, Register bh

[<AutoOpen>]
module Jumps =

    let rec jump (label : Label) (builder : CodeBuilder) =
        let addr = builder.Address

        if label.IsUnknown then
            builder.Emit 0xE9uy
            builder.AddPending(label, fun b -> emitBytes (BitConverter.GetBytes(int <| label.Address - addr - 7n)) b)
            builder.Emit [| 0uy ; 0uy ; 0uy ; 0uy |]
        else
            let ofs = int <| label.Address - addr

            if ofs > 125 || ofs < -128 then
                builder.Emit 0xE9uy
                builder.Emit(BitConverter.GetBytes(ofs - 5))
            else
                builder.Emit [| 0xEBuy ; byte (ofs - 2) |]

    let rec jump' (cond : BranchCondition) (label : Label) (builder : CodeBuilder) =
        let addr = builder.Address

        if label.IsUnknown then
            builder.Emit [| 0x70uy + byte !cond ; 0x5uy ; 0xE9uy |]
            builder.AddPending(label, fun b -> emitBytes (BitConverter.GetBytes(int <| label.Address - addr - 7n)) b)
            builder.Emit [| 0uy ; 0uy ; 0uy ; 0uy |]
        else
            let ofs = int <| label.Address - addr

            if ofs > 125 || ofs < -128 then
                // Cannot branch conditionally using rel32, thus we create an
                // unconditional jump that we skip if the condition is *false*.
                builder.Emit [| 0x70uy + byte !cond ; 0x5uy ; 0xE9uy |]
                builder.Emit(BitConverter.GetBytes(ofs - 5))
            else
                builder.Emit [| 0x70uy + byte cond ; byte (ofs - 2) |]
