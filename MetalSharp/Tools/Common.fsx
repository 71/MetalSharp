open System
open System.Text
open System.Text.RegularExpressions

let utf8 = new UTF8Encoding(false)

/// Defines all the mnemonics used in F# as keywords, and that thus
/// cannot be used as function names.
let mkeywords = [| "and" ; "or" |]

let (|Integer|_|) (str: string) =
   let mutable intvalue = 0
   if Int32.TryParse(str, &intvalue) then Some intvalue
   elif str.Length = 0 then Some 0
   elif str = "z" then Some -1
   else None

let (|Match|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None

let capitalize (s: string) = (s.[0].ToString().ToUpper()) + (s.Substring(1))

let printCompiledName stream m =
    fprintfn stream "  /// Emits an '%s' instruction." m
    fprintfn stream "  [<CompiledName(\"Emit%s\")>]" (capitalize m)
