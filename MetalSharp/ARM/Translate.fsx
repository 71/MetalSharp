// Translates the 'Assembly.dat' file into 'Assembly.fs', which
// translates instructions to F# functions.
module ARM

#load "../Tools/Common.fsx"

open Common

open System
open System.Collections.Generic
open System.IO
open System.Linq

#nowarn "25"

let conditions =
    [|
        "eq", "compared numbers were equal"
        "ne", "compared numbers weren't equal"
        "cs", "operation set the carry"
        "cc", "operation cleared the carry"
        "hs", "unsigned comparison was higher or equal"
        "lo", "unsigned comparison was lower"
        "mi", "set number was negative"
        "pl", "set number was positive or zero"
        "vs", "operation overflowed"
        "vc", "operation did not overflow"
        "hi", "unsigned comparison was higher"
        "ls", "unsigned comparison was lower or equal"
        "ge", "signed comparison was higher or equal"
        "lt", "signed comparison was lower"
        "gt", "signed comparison was higher"
        "le", "signed comparison was lower or equal"
    |]

/// Translates the instructions encoded in the given input into a
/// F# code written to the given output stream.
let translate (input: string seq) output =

    let printConditions (name: string) (namesuffix: string) paramstr body =
        let hasHash = name.Contains("#")

        for condname, descr in conditions do
            let name =
                if hasHash then
                    name.Replace("#", condname)
                else
                    name + condname
            
            fprintfn output "  /// Emits a '%s' instruction that only executes if the last %s." name descr
            fprintfn output "  [<CompiledName(\"Emit%s%s%s\")>]" (capitalize name) (condname.ToUpper()) (namesuffix.ToUpper())
            fprintfn output "  let %s%s %s = instr Condition.%s + (%s)" name namesuffix paramstr (condname.ToUpper()) body

        let name = if hasHash then name.Replace("#", "") else name
    
        fprintfn output "  /// Emits a '%s' instruction that executes unconditionally." name
        fprintfn output "  [<CompiledName(\"Emit%s%s\")>]" (capitalize name) (namesuffix.ToUpper())
        fprintfn output "  let %s%s %s = instr Condition.AL + (%s)" (noconflict name) namesuffix paramstr body

    output.WriteLine("""
  let inline private where v cond = assert(cond) ; int v
  let inline private zeroorone v = assert(v = 0uy || v = 1uy) ; int v

  let rec private encodeRegisters = function
  | [] -> 0
  | (r : Register)::rs -> (1 <<< int r.Value) ||| encodeRegisters rs
    """)

    for line in input.Where(not << String.IsNullOrWhiteSpace) do
        let parts = line.Split(' ')
        let bodyParts = List()

        use paramstr = new StringWriter()
        
        let mutable n = 32

        let mutable switch = -1, ""
        let mutable hasCond = false

        let name = parts.[0]

        let inline addparam' size name ty value =
            n <- n - size

            fprintf paramstr "(%s : %s) " name ty
            bodyParts.Add(sprintf "(%s <<< %d)" value n)

        let inline addparam size name ty = addparam' size name ty (sprintf "(int %s)" name)

        for part in parts.Skip(1) do
            match part with
            | "cond" -> n <- n - 4 ; hasCond <- true
            | "S"    -> n <- n - 1 ; switch  <- n, "s"
            | "L"    -> n <- n - 1 ; switch  <- n, "l"
            | "N"    -> n <- n - 1 ; switch  <- n, "l"
            | "R"    -> n <- n - 1 ; switch  <- n, "r"
            | "X"    -> n <- n - 1 ; switch  <- n, "x"
            | "W"    -> n <- n - 1 ; switch  <- n, "w" // equivalent to '!'
            | "I"    -> n <- n - 1

            | "Rn" -> addparam' 4 "src"     "Register" "(int src.Value)"
            | "Rd" -> addparam' 4 "dst"     "Register" "(int dst.Value)"
            | "Rm" -> addparam' 4 "shifted" "Register" "(int shifted.Value)"
            | "Rs" -> addparam' 4 "shift"   "Register" "(int shift.Value)"
            | "RdHi" -> addparam' 4 "upper" "Register" "(int upper.Value)"
            | "RdLo" -> addparam' 4 "lower" "Register" "(int lower.Value)"
            | "CRd"  -> addparam' 4 "codst" "Register" "(int codst.Value)"
            | "CRn"  -> addparam' 4 "cosrc1" "Register" "(int cosrc1.Value)"
            | "CRm"  -> addparam' 4 "cosrc2" "Register" "(int cosrc2.Value)"

            | "shiftimm"  -> addparam 5  "shift"      "byte"
            | "rotateimm" -> addparam 4  "rotate"     "byte"
            | "satimm"    -> addparam' 4 "saturation" "byte" "(int saturation - 1)"
            | "ofs8"      -> addparam 8  "offset"     "byte"
            | "imm4"      -> addparam 4  "immediate"  "byte"
            | "imm8"      -> addparam 8  "immediate"  "byte"
            | "imm24"     -> addparam 24 "immediate"  "uint32"
            | "simm24"    -> addparam 24 "immediate"  "int32"

            | "addrmode"  -> addparam' 12 "addrmode" "AddressingMode" "addrmode.Bits12"
            | "addrmode1" -> addparam' 4 "addrmode1" "AddressingMode" "addrmode1.Bits4"
            | "addrmode2" -> addparam' 4 "addrmode2" "AddressingMode" "addrmode2.Bits4"
            | "mode"      -> addparam  5 "mode" "Mode"

            | "reglist" -> addparam' n "reglist" "Register list" "(encodeRegisters reglist)"
            | "cpnum"   -> addparam' 4 "coproc" "Coprocessor" "(int coproc.Number)"

            | "iflags" -> addparam 3 "iflags" "InterruptFlags"

            | "rotate" -> addparam 2 "rott" "Rotate"
            | "shift"  -> addparam 2 "shft" "Shift"

            | "shifter" -> addparam' n "shifter" "Operand" "shifter.Bits"

            | "fieldmask" -> addparam 4 "field" "Field"

            | "shiftimm+sh" -> addparam' 6 "shift" "byte option" "(match shift with Some b -> ((int b <<< 1) + 1) | _ -> 0)"

            | "P_U" -> ()
            | "U" -> ()
            | "H" -> addparam' 1 "h" "byte" "(zeroorone h)"

            | "opcode"  -> addparam 4 "opcode"  "byte"
            | "opcode1" -> addparam 4 "opcode1" "byte"
            | "opcode2" -> addparam 3 "opcode2" "byte"

            | "topimm" -> addparam 12 "topimm" "uint16"
            | "botimm" -> addparam 4 "bottomimm" "byte"

            | "1" -> n <- n - 1 ; bodyParts.Add(string(1 <<< n))
            | "0" -> n <- n - 1
            | ""  -> ()
            | n   -> failwithf "Unknown operand '%s' in opcode '%s'." n name

        assert(n = 0)

        let paramstr = paramstr.ToString()
        let switchindex, switchname = switch

        if hasCond then
            printConditions name "" paramstr (String.concat " ||| " bodyParts)

            if switchindex <> -1 then
                bodyParts.Add(string(1 <<< switchindex))
                printConditions name switchname paramstr (String.concat " ||| " bodyParts)
        else
            fprintfn output "  /// Emits a '%s' instruction." name
            fprintfn output "  [<CompiledName(\"Emit%s\")>]" (capitalize name)
            fprintfn output "  let %s %s = %s" (noconflict name) paramstr (String.concat " ||| " bodyParts)
