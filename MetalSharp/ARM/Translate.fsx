// Translates the 'Assembly.dat' file into 'Assembly.fs', which
// translates instructions to F# functions.
module ARM

#load "../Tools/Common.fsx"

open Common

open System
open System.Collections.Generic
open System.Globalization
open System.IO
open System.Linq
open System.Text
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


let conditions =
    [|
        "z",  "is zero"
        "nz", "is non-zero"
    |]

/// Translates the instructions encoded in the given input into a
/// F# code written to the given output stream.
let translate input output =

    let printConditions name paramstr =
        for condname, descr in conditions do
            fprintfn output "  /// Emits a '%s' instruction that only executes if the last operation %s." name descr
            fprintfn output "  [<CompiledName(\"Emit%s%s\")>]" (capitalize name) (condname.ToUpper())
            fprintf output "  let %s%s %s = instr Condition.%s " name condname paramstr (condname.ToUpper())
    
        fprintfn output "  /// Emits a '%s' instruction that executes unconditionally." name
        fprintfn output "  [<CompiledName(\"Emit%s\")>]" (capitalize name)
        fprintf output "  let %s %s = instr Condition.AL " name paramstr

    for line in input do ()

    fprintf output "    ()"
