// Compares medium pieces of code with dynamically encoded instructions. 
module Compare

open System
open System.Diagnostics
open System.IO
open System.Linq

/// Gets the expected produced byte array for the given string of
/// Intel-syntax assembly, by invoking "as tmpfile" and returning the data
/// output by "objcopy -j .text -O binary tmpfile binfile".
let getExpected s =
    let fn = Path.GetTempFileName()

    use gas = new Process()

    gas.StartInfo <- ProcessStartInfo("as", sprintf "-o %s" fn)
    gas.StartInfo.UseShellExecute <- false
    gas.StartInfo.RedirectStandardInput <- true
    gas.StartInfo.RedirectStandardOutput <- true
    gas.StartInfo.RedirectStandardError <- true

    gas.Start() |> ignore

    fprintf gas.StandardInput """
.intel_syntax noprefix

%s
"""
        s

    gas.StandardInput.Close()
    gas.WaitForExit()

    if gas.ExitCode <> 0 then
        raise <| Exception(gas.StandardError.ReadToEnd())

    let fn' = Path.GetTempFileName()
    use dmp = new Process()

    dmp.StartInfo <- ProcessStartInfo("objcopy", sprintf "-j .text -O binary \"%s\" \"%s\"" fn fn')
    dmp.StartInfo.UseShellExecute <- false
    dmp.StartInfo.RedirectStandardOutput <- true

    dmp.Start() |> ignore
    dmp.WaitForExit()

    let mutable content = File.ReadAllBytes(fn')

    File.Delete(fn)
    File.Delete(fn')

    // Filter nopes at end of .text section
    if content.[content.Length - 1] = 0x90uy then
        let i = content.Reverse().TakeWhile(fun x -> x = 0x90uy).Count()

        Array.Resize(&content, content.Length - i)

    content

let inline expected s = String.Concat((getExpected s).Select(sprintf "\\x%02x"))
let inline expected' s = String.Join(" ", (getExpected s).Select(sprintf "%02x"))
