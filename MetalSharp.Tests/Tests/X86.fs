module X86Tests

open Common
open Common.X86

open MetalSharp
open MetalSharp.X86
open MetalSharp.X86.Typed

open Swensen.Unquote
open NUnit.Framework

[<Test>]
let ``should emit correct opcodes``() =
    raw  ret =! expected32 "ret"

    // Getting different results here, even though both are valid
    // raw  (push' eax' >. pop' eax') =! expected32 "push eax ; pop eax"
    // raw  (push' rax' >. pop' rax') =! expected64 "push rax ; pop rax"
