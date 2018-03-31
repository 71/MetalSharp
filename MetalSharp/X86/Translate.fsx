// Translates the 'Assembly.dat' file into 'Assembly.fs', which
// translates instructions to F# functions.
module X86

#load "../Tools/Common.fsx"

open Common

open System
open System.Globalization
open System.IO
open System.Linq
open System.Text.RegularExpressions

#nowarn "25"

/// Represents an operand.
[<Struct>]
type Operand = Void | Imm | Reg | Mem | ModRM
with
    member this.GetCondition(size: int) =
        String.Format(
            match this with
            | Imm -> ":? Immediate<{0}>"
            | Reg -> ":? Register<{0}>"
            | Mem -> ":? Memory<{0}>"
            | ModRM -> ":? RegisterOrMemory<{0}>"
            , if size = -1 then "'s" else sprintf "S%d" size)

    static member Parse(s: string) =
        match s.Trim() with
        | "void" -> Void, 0

        | Match @"imm(\d+|z)" [ Integer sz ] -> Imm, sz
        | Match @"rel(\d+|z)" [ Integer sz ] -> Imm, sz
        | Match @"rm(\d+|z)"  [ Integer sz ] -> ModRM, sz
        | Match @"r(\d+|z)"   [ Integer sz ] -> Reg, sz
        | Match @"m(\d+|z)"   [ Integer sz ] -> Mem, sz

        | s -> failwithf "Unknown operand format %s." s



/// Translates the instructions encoded in the given input into a
/// F# code written to the given output stream.
let translate input (typed : StreamWriter) =
    // Different functions for pattern matching:
    use ms0 = new MemoryStream()
    use fn0 = new StreamWriter(ms0, utf8)
    use ms1 = new MemoryStream()
    use fn1 = new StreamWriter(ms1, utf8)
    use ms2 = new MemoryStream()
    use fn2 = new StreamWriter(ms2, utf8)
    use ms3 = new MemoryStream()
    use fn3 = new StreamWriter(ms3, utf8)

    use msuntyped = new MemoryStream()
    use untyped = new StreamWriter(msuntyped, utf8)
    use msuntypedthr = new MemoryStream()
    use untypedthr = new StreamWriter(msuntypedthr, utf8)

    /// Duplicates functions that accept arguments of different sizes.
    let dupIfNeeded line =
        let rep c sz s = Regex.Replace(s, sprintf @"(rel|r|rm|imm)(%c)" c, fun (m: Match) -> sprintf "%s%d" m.Groups.[1].Value sz)

        if String.IsNullOrWhiteSpace(line) || line.[0] = '#' then
            Seq.empty
        else
            let hasX = Array.exists line.Contains [| "rmx" ; "rx" ; "relx" ; "immx" |]
            let hasY = Array.exists line.Contains [| "rmy" ; "ry" ; "rely" ; "immy" |]

            match hasX, hasY with
            | true, true  -> [| (rep 'x' 16 >> rep 'y' 16) line ; (rep 'x' 32 >> rep 'y' 32) line ; (rep 'x' 32 >> rep 'y' 64) line |] :> _
            | true, false -> [| rep 'x' 16 line ; rep 'x' 32 line |] :> _
            | false, true -> [| rep 'y' 16 line ; rep 'y' 32 line ; rep 'y' 64 line |] :> _
            | _           -> Seq.replicate 1 line


    let lines = seq {
        for l in input do 
            for line in dupIfNeeded(l) do 
                let m = Regex.Match(line, @"^([0-9a-fA-F ]+)\t+([a-zA-Z]+)\t+(.+?)(?:\t+(.+))?$", RegexOptions.Compiled)

                if m.Success then
                    yield m
    }

    // Individual functions
    for ms in lines.GroupBy(fun x -> x.Groups.[2].Value) do
        let mnemonic', matches = ms.Key, ms.ToArray()

        let mnemonic =
            // Rename function if it uses a F# keyword, and is thus invalid
            if mkeywords.Contains(mnemonic') then
                sprintf "%s'" mnemonic'
            else
                mnemonic'

        let mutable printedUntyped = false
        let mutable hasOperands = false

        for n, m in Seq.indexed matches do
            let opcodes   = [ for x in m.Groups.[1].Value.Split(' ') -> Int32.Parse(x, NumberStyles.HexNumber) ].ToArray()
            let operands  = match [ for x in m.Groups.[3].Value.Split(',') -> Operand.Parse(x) ].ToArray() with
                            | [||] -> [||]
                            | [| Void, _ |] -> [||]
                            | v -> v

            // Print untyped header
            if not printedUntyped then
                printCompiledName untyped mnemonic'
                fprintf untyped "  let %s" mnemonic

                fprintf untypedthr """
  /// Emits an '%s' instruction, or throws if it is unknown.
  [<CompiledName("Emit%sOrThrow")>]
  let %s'"""
                    mnemonic' (capitalize mnemonic') mnemonic

                let ps = match operands.Length with
                         | 0 -> ""
                         | 1 -> " (operand : Operand)"
                         | 2 -> " (dst : Operand) (src : Operand)"
                         | 3 -> " (op1 : Operand) (op2 : Operand) (op3 : Operand)"

                if ps.Length <> 0 then
                    untyped.Write(ps)
                    untypedthr.Write(ps)
                
                untyped.WriteLine(" (builder : CodeBuilder) =")
                untypedthr.Write(" (builder : CodeBuilder) = ")

                let m, args = match operands.Length with
                              | 0 -> "", ""
                              | 1 -> "    match (operand :> obj) with", " operand"
                              | 2 -> "    match (dst :> obj), (src :> obj) with", " dst src"
                              | 3 -> "    match (op1 :> obj), (op2 :> obj), (op3 :> obj) with", " op1 op2 op3"

                if m.Length <> 0 then
                    untyped.WriteLine(m)

                fprintfn untypedthr "if not <| %s%s builder then failwith \"Unknown instruction.\"" mnemonic args

                printedUntyped <- true
                hasOperands <- operands.Length <> 0


            let options = m.Groups.[4].Value.Split(' ').ToList()
            let inline hasOption opt = options.Contains(opt)
            let inline hasOption' opts = Array.exists hasOption opts

            if not <| options.Contains("nopr") then
                if operands.Any(fun (_, s) -> s = 16) then
                    options.Add("pre16")
                elif operands.Any(fun (_, s) -> s = 64) then
                    options.Add("pre64")

            let sra = hasOption "sra" // simple register addition
            let hasSizePre = hasOption' [| "pre16" ; "pre64" |]
            let ext = match m.Groups.[4].Value with
                      | Match @"ext(\d)" [ Integer ext ] -> ext
                      | _ -> 0

            let emitPrefix =
                let select = function
                | "pre16" -> if sra then "builder.Emit (0x66uy + op1.Reg.PrefixAdder)" else "builder.Emit 0x66uy"
                | "pre64" -> if sra then "builder.Emit (0x48uy + op1.Reg.PrefixAdder)" else "builder.Emit 0x48uy"
                | "rpre"  -> "emitPrefix op1 builder"
                | o -> printfn "Unknown option '%s'." o ; "()"

                sprintf "(%s)" <| String.Join(" ; ", options.Where(not << String.IsNullOrWhiteSpace).Select(select))

            let emitOpcode = match opcodes.Length with
                             | 1 ->
                                if sra then
                                    if hasSizePre then
                                        sprintf "(builder.Emit (0x%xuy + op1.Reg.Value %% 8uy))" opcodes.[0]
                                    else
                                        sprintf "(emitRegisterOpcode 0x%xuy op1 builder)" opcodes.[0]
                                else
                                    sprintf "(builder.Emit 0x%xuy)" opcodes.[0]

                             | 2 -> sprintf "(builder.Emit [| 0x%xuy ; 0x%xuy |])" opcodes.[0] opcodes.[1]
                             | 3 -> sprintf "(builder.Emit [| 0x%xuy ; 0x%xuy ; 0x%xuy |])" opcodes.[0] opcodes.[1] opcodes.[2]

                             | _ -> failwithf "Invalid opcode '%s'." m.Groups.[1].Value

            let emitOperands =
                let select (operand: Operand, s, i) =
                    match operand with
                    | Imm -> sprintf "emitImmediate op%d builder" i
                    | Reg -> sprintf "emitRegister op%d builder" i
                    | ModRM | Mem -> sprintf "emitMemory op%d %duy builder" i ext

                if sra then
                    if operands.Length = 1 then
                        ""
                    else
                        " ; (emitImmediate op2 builder)"
                elif operands.Length = 0 then
                    ""
                elif operands.Length = 1 then
                    let op, sz = operands.[0]
                    sprintf " ; %s" <| select(op, sz, 1)
                elif fst operands.[0] = ModRM then
                    sprintf " ; (emitModrm' op2 op1 %xuy builder)" ext
                elif fst operands.[1] = ModRM then
                    sprintf " ; (emitModrm' op1 op2 %xuy builder)" ext
                else
                    sprintf " ; (%s)" <| String.Join(" ; ", seq { for i, (x, s) in Seq.indexed operands do if x <> Void then yield select(x, s, i + 1) })

            // Get parameters string of typed function
            let operandParams =
                let select (operand: Operand, s, i) =
                    let p =  match operand with
                             | Imm   -> "Immediate"
                             | Reg   -> "Register"
                             | Mem   -> "Memory"
                             | ModRM -> "RegisterOrMemory"

                    if s = -1 then
                        sprintf "(op%d : %s<_>)" (i + 1) p
                    else
                        sprintf "(op%d : %s<S%d>)" (i + 1) p s

                String.Join(" ", seq { for i, (x, s) in Seq.indexed operands do if x <> Void then yield select(x, s, i) })


            let emit =
                if emitPrefix.IndexOf "builder" = -1 then
                    sprintf "%s%s" emitOpcode emitOperands
                else
                    sprintf "%s ; %s%s" emitPrefix emitOpcode emitOperands


            // Emit pattern matching / untyped versions
            //
            // Note: there is no need to group instructions by their name ; the F#
            //       compiler does it automatically.
            let tgt, cond =
                match operands.Length with
                | 0 -> fn0, ""
                | 1 -> let a1, b1 = operands.[0]
                       fn1,
                       sprintf "(%s as op1)" (a1.GetCondition(b1))
                | 2 -> let (a1, b1), (a2, b2) = operands.[0], operands.[1]
                       fn2,
                       sprintf "(%s as op1), (%s as op2)" (a1.GetCondition(b1)) (a2.GetCondition(b2))
                | 3 -> let (a1, b1), (a2, b2), (a3, b3) = operands.[0], operands.[1], operands.[2]
                       fn3,
                       sprintf "(%s as op1), (%s as op2), (%s as op3)" (a1.GetCondition(b1)) (a2.GetCondition(b2)) (a3.GetCondition(b3))

                | _ -> failwithf "Invalid operands '%s'." m.Groups.[3].Value

            if cond.Length = 0 then
                fprintfn tgt "    | \"%s\" -> %s ; true" mnemonic' emit
                fprintfn untyped "    %s ; true" emit
            else
                fprintfn tgt "    | \"%s\", %s -> %s ; true" mnemonic' cond emit
                fprintfn untyped "    | %s -> %s ; true" cond emit


            // Emit typed function
            let mnemonic =
                // Ensure we don't have a duplicate function name
                if matches.Length > 1 then
                    sprintf "%s%d" mnemonic (n + 1)
                else
                    mnemonic

            printCompiledName typed mnemonic'
            fprintfn typed "  let %s %s (builder : CodeBuilder) = %s" mnemonic operandParams emit
            fprintfn typed ""

        if hasOperands then
            fprintfn untyped "    | _ -> false"

        fprintfn untyped ""


    // Untyped functions
    fprintfn typed "[<AutoOpen>]"
    fprintfn typed "module Untyped ="

    untyped.Flush()
    msuntyped.WriteTo(typed.BaseStream)

    untypedthr.Flush()
    msuntypedthr.WriteTo(typed.BaseStream)


    // Pattern-maching functions
    fprintfn typed """
  /// Translates the given instruction (encoded by its mnemonic) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate0 mnemonic (builder : CodeBuilder) =
    match mnemonic with"""
    fn0.Flush()
    ms0.WriteTo(typed.BaseStream)
    fprintfn typed "    | _ -> false"

    fprintfn typed """
  /// Translates the given instruction (encoded by its mnemonic and operand) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate1<'s when 's :> OS and 's : (new : unit -> 's)> mnemonic (op1 : Operand<'s>) (builder : CodeBuilder) =
    match mnemonic, (op1 :> obj) with"""
    fn1.Flush()
    ms1.WriteTo(typed.BaseStream)
    fprintfn typed "    | _ -> false"

    fprintfn typed """
  /// Translates the given instruction (encoded by its mnemonic and two operands) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate2<'s when 's :> OS and 's : (new : unit -> 's)> mnemonic (op1 : Operand<'s>) (op2 : Operand<'s>) (builder : CodeBuilder) =
    match mnemonic, (op1 :> obj), (op2 :> obj) with"""
    fn2.Flush()
    ms2.WriteTo(typed.BaseStream)
    fprintfn typed "    | _ -> false"

    fprintfn typed """
  /// Translates the given instruction (encoded by its mnemonic and three operands) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate3<'s when 's :> OS and 's : (new : unit -> 's)> mnemonic (op1 : Operand<'s>) (op2 : Operand<'s>) (op3 : Operand<'s>) (builder : CodeBuilder) =
    match mnemonic, (op1 :> obj), (op2 :> obj), (op3 :> obj) with"""
    fn3.Flush()
    ms3.WriteTo(typed.BaseStream)
    fprintfn typed "    | _ -> false"

    fprintfn typed """
  /// Translates the given instruction (encoded by its mnemonic and optional operands) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate mnemonic operands builder =
    match operands with
    | []          -> translate0 mnemonic builder
    | [ a ]       -> translate1 mnemonic a builder
    | [ a; b ]    -> translate2 mnemonic a b builder
    | [ a; b; c ] -> translate3 mnemonic a b c builder
    | _           -> false

"""
