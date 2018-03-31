module Program

open System
open System.Runtime.InteropServices

open MetalSharp
open Brainfuck

/// Entry point of the program, used when debugging tests through the
/// .NET Core debugger.
[<EntryPoint>]
let main args =
    use builder = new CodeBuilder()

    let start = builder.CreateLabel()
    let input = match args with
                | [||] -> Console.In.ReadToEnd()
                | [| input |] -> input
                | _ -> "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++."

    let data = Array.zeroCreate<byte> 30_000

    try
        translate input (Marshal.UnsafeAddrOfPinnedArrayElement(data, 15_000)) builder

        builder.WriteShellcode(Console.Error)
        Console.Read() |> ignore

        start.AsFunction<ExecuteDelegate>().Invoke()

        0
    with
    | ex -> eprintf "Invalid brainfuck input: %O." ex
            1
