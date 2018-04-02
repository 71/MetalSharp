namespace MetalSharp.ARM

open System
open System.Runtime.CompilerServices

/// Defines the condition for an ARM instruction to be executed.
/// See ARM Architecture Reference Manual, Table 3-1 for more informations.
type Condition =
    /// Equal.
    | EQ = 0b0000uy
    /// Not equal.
    | NE = 0b0001uy
    /// Carry set.
    | CS = 0b0010uy
    /// Unsigned higher or same.
    | HS = 0b0010uy
    /// Carry clear.
    | CC = 0b0011uy
    /// Unsigned lower.
    | LO = 0b0011uy
    /// Minus / negative.
    | MI = 0b0100uy
    /// Plus / positive or zero.
    | PL = 0b0101uy
    /// Overflow.
    | VS = 0b0110uy
    /// No overflow.
    | VC = 0b0111uy
    /// Unsigned higher.
    | HI = 0b1000uy
    /// Unsigned lower or same.
    | LS = 0b1001uy
    /// Signed greater than or equal.
    | GE = 0b1010uy
    /// Signed less than.
    | LT = 0b1011uy
    /// Signed greater than.
    | GT = 0b1100uy
    /// Signed less than or equal.
    | LE = 0b1101uy
    /// Always (unconditional).
    | AL = 0b1110uy
    /// Unpredictable (ARMv4 and lower) or unconditional (ARMv5 and higher).
    | UN = 0b1111uy

/// Defines the kind of a shift.
type Shift =
    /// Logical shift left.
    | LSL = 0b00uy
    /// Logical shift right.
    | LSR = 0b01uy
    /// Arithmetic shift right.
    | ASR = 0b10uy
    /// Rotate right.
    | ROR = 0b11uy
    /// Shifted right by one bit.
    | RRX = 0b11uy

/// Defines the kind of a right rotation.
type Rotate =
    /// Rotate 8 bits to the right.
    | ROR8  = 0b01uy
    /// Rotate 16 bits to the right.
    | ROR16 = 0b10uy
    /// Rotate 24 bits to the right.
    | ROR24 = 0b11uy
    /// Do not rotate.
    | NOP   = 0b00uy

/// Defines the processor mode.
type Mode =
    /// User mode.
    | USR  = 0b10000uy
    /// FIQ (high-speed data transfer) mode.
    | FIQ = 0b10001uy
    /// IRQ (general-purpose interrupt handling) mode.
    | IRQ = 0b10010uy
    /// Supervisor mode.
    | SVC = 0b10011uy
    /// Abort mode.
    | ABT = 0b10111uy
    /// Undefined mode.
    | UND = 0b11011uy
    /// System (privileged) mode.
    | SYS = 0b11111uy

/// Defines the field mask bits.
[<Flags>]
type Field =
    /// Control field mask bit.
    | C = 0b0001uy
    /// Extension field mask bit.
    | X = 0b0010uy
    /// Status field mask bit.
    | S = 0b0100uy
    /// Flags field mask bit.
    | F = 0b1000uy

/// Defines the interrupt flags.
[<Flags>]
type InterruptFlags =
    /// Imprecise data abort bit.
    | A  = 0b100uy
    /// IRQ interrupt bit.
    | I = 0b010uy
    /// FIQ interrupt bit.
    | F   = 0b001uy


type AddressingMode =

    member __.Bits12 = 0
    member __.Bits4 = 0

/// Defines an ARM register.
[<Struct>]
type Register internal(value: byte) =
    /// Gets the value of the register.
    member __.Value = value

/// Defines an ARM coprocessor.
[<Struct>]
type Coprocessor internal(value: byte) =
    /// Gets the number of the coprocessor.
    member __.Number = value

/// Defines an ARM shifter operand.
[<RequireQualifiedAccess; Struct>]
type Operand =
    /// A right-rotated immediate.
    | Immediate      of rotate: byte * imm: byte

    /// A register shifted by a constant value.
    | ImmediateShift of shiftAmount: byte * immShift: Shift * immRm: Register

    /// A register shifted by a value stored in another register.
    | RegisterShift  of shiftRegister: Register * regShift: Shift * regRm: Register

with
    /// Gets the 32-bits integer that encodes the value of the shift.
    member this.Bits =
        match this with
        | Operand.Immediate (rotate, imm) ->
            (1 <<< 25) + (int rotate <<< 8) + (int imm)
        | Operand.ImmediateShift (amount, shift, rm) ->
            (int amount <<< 7) + (int shift <<< 5) + (int rm.Value)
        | Operand.RegisterShift (reg, shift, rm) ->
            (int reg.Value <<< 8) + (int shift <<< 5) + (int rm.Value)

    /// Gets a boolean that represents whether the shifter is valid.
    member this.IsValid =
        match this with
        | Operand.Immediate (rotate, imm) ->
            (1 <<< 25) + (int rotate <<< 8) + (int imm)
        | Operand.ImmediateShift (amount, shift, rm) ->
            (int amount <<< 7) + (int shift <<< 5) + (int rm.Value)
        | Operand.RegisterShift (reg, shift, rm) ->
            (int reg.Value <<< 8) + (int shift <<< 5) + (int rm.Value)


#nowarn "25"

/// Defines all supported ARM registers.
[<AutoOpen>]
module Registers =
    /// Returns a register having the given value.
    [<CompiledName("Register")>]
    let reg v =
        if v > 15uy then
            invalidArg "v" "Registers cannot have a value higher than 15."
        else
            Register v

    let [ r0 ; r1 ; r2 ; r3 ; r4 ; r5 ; r6 ; r7 ; r8 ; r9 ; r10 ; r11 ; r12 ; r13 ; r14 ; r15 ] = List.init 16 (byte >> reg)
    let [ a1 ; a2 ; a3 ; a4 ; v1 ; v2 ; v3 ; v4 ; v5 ; v6 ; v7 ; v8 ; ip ; sp ; lr ; pc ] = List.init 16 (byte >> reg)
    let wr, sb, sl, fp = reg 7uy, reg 9uy, reg 10uy, reg 11uy

/// Defines all supported ARM coprocessors.
[<AutoOpen>]
module Coprocessors =
    /// Returns a register having the given value.
    [<CompiledName("CP")>]
    let cp n =
        if n > 15uy then
            invalidArg "n" "Coprocessors cannot havh a number higher than 15."
        else
            Coprocessor n

    let [ cp0 ; cp1 ; cp2 ; cp3 ; cp4 ; cp5 ; cp6 ; cp7 ; cp8 ; cp9 ; cp10 ; cp11 ; cp12 ; cp13 ; cp14 ; cp15 ] = List.init 16 (byte >> cp)

/// Defines all supported ARM instructions.
[<AutoOpen>]
module Instructions =

    /// Reverses a 32-bits integer from an endianness to the other.
    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let private rev i = ((i &&& 0xf000) >>> 3) + ((i &&& 0x0f00) >>> 1) + ((i &&& 0x00f0) <<< 1) + ((i &&& 0x000f) <<< 3)
    
    /// Reverses a 32-bits unsigned integer from an endianness to the other.
    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let private rev' i = ((i &&& 0xf000u) >>> 3) + ((i &&& 0x0f00u) >>> 1) + ((i &&& 0x00f0u) <<< 1) + ((i &&& 0x000fu) <<< 3)

    /// Reverses a 4-bytes array.
    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let private rev'' (arr: byte[]) = assert(arr.Length = 4) ; [| arr.[3] ; arr.[2] ; arr.[1] ; arr.[0] |]

    /// Creates an instruction that executes if the given condition is true.
    let inline instr (cond: Condition) =
        (int cond <<< 28)

    /// Creates a data-processing instruction.
    let inline dpInsr (opcode: byte) cond = instr cond + (int opcode <<< 21)

    /// Creates a software-interrupt instruction.
    let inline interrupt cond swi =
        if swi < 0 || swi > 0x00ffffff then
            invalidArg "swi" "Software-interrupt number out of bounds."
        else
            instr cond + 0xf + swi
