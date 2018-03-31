module Brainfuck

open MetalSharp
open MetalSharp.X86
open MetalSharp.X86.Typed

open System
open System.Collections.Generic
open System.Runtime.InteropServices

[<UnmanagedFunctionPointer(CallingConvention.Winapi)>]
type private ReadDelegate = delegate of unit -> int
[<UnmanagedFunctionPointer(CallingConvention.Winapi)>]
type private PrintDelegate = delegate of char -> unit
[<UnmanagedFunctionPointer(CallingConvention.Cdecl)>]
type ExecuteDelegate = delegate of unit -> unit

let private readCh = ReadDelegate(Console.Read)
                     |> Marshal.GetFunctionPointerForDelegate
let private printCh = PrintDelegate(Console.Write)
                      |> Marshal.GetFunctionPointerForDelegate

let private ccall inner (builder: CodeBuilder) = builder {
    do! push5  rbp'
    do! mov4   rbp' rsp'
    do! inner
    do! mov4   rsp' rbp'
    do! pop3   rbp'
}

let private mscall inner (builder: CodeBuilder) = builder {
    do! push5  rbp'
    do! mov4   rbp' rsp'
    do! sub15  rsp' (imm8 32uy)
    do! inner
    do! add15  rsp' (imm8 32uy)
    do! pop3   rbp'
}

let translate bf (addr: nativeint) (builder: CodeBuilder) =
    let ptr, ptr', ptr'' = Register rbx, Memory rbx, Memory bh
    let labels = Stack()

    (  push5  rbx'
    >. mov12  ptr (imm64 <| int64 addr)
    >. push5  ptr

    ) builder

    let inline restorePtr() = mov8 ptr (Memory rsp) builder

    for ch in bf do
        restorePtr()

        (match ch with
        | '>' -> inc6 ptr
        | '<' -> dec6 ptr
        | '+' -> inc3 ptr''
        | '-' -> dec3 ptr''

        | '.' -> mov12   rax' (imm64 <| int64 printCh) >.
                 mscall  (mov8 rcx' ptr' >. call5 rax')

        | ',' -> mov12   rax' (imm64 <| int64 readCh) >.
                 mscall  (call5 rax') >.
                 mov4    ptr' rax'

        | '[' -> let here = builder.CreateNullLabel()
                 let there = builder.CreateNullLabel()

                 labels.Push(here, there)

                 (  cmp1   ptr'' (imm8 0uy)
                 >. jump'  BranchCondition.Z there

                 ) builder
                 
                 builder.MarkLabel(here)
                 ignore

        | ']' -> let there, here = labels.Pop()

                 (  cmp1   ptr'' (imm8 0uy)
                 >. jump'  BranchCondition.NZ there

                 ) builder
                 
                 builder.MarkLabel(here)
                 ignore

        |  _  -> ignore) builder

    (  add9  spl' (imm8 4uy)
    >. pop3  rbx'
    >. ret

    ) builder
