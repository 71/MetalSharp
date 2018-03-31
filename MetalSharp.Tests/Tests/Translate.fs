// Compares small pieces of code with pre-encoded instructions. 
module Translate

open Common
open MetalSharp
open MetalSharp.X86
open MetalSharp.X86.Typed

open Swensen.Unquote
open NUnit.Framework


[<Test>]
let ``should emit correct opcodes``() =
    raw  ret =! "\xC3"B
    raw  (pushf >. popf) =! "\x9C\x9D"B

[<Test>]
let ``should emit correct single operands``() =
    raw (inc2  eax') =! "\x40"B
    raw (inc1  bx')  =! "\x66\x43"B

    raw (dec5  (Memory edx)) =! "\xff\x0a"B

[<Test>]
let ``should emit correct modrm operands``() =
    raw (add3  eax' eax') =! "\x01\xC0"B
    raw (add2  ax' ax')   =! "\x66\x01\xC0"B

    raw (add3  eax' eax' >. ret) =! "\x01\xC0\xC3"B

[<Test>]
let ``should emit correct jumps``() =
    raw (fun b -> b {
        let! l1 = label
        do! add3 eax' ebx'
        do! jump l1
    }) =! "\x01\xD8\xEB\xFC"B

    raw (fun b -> b {
        let! l1 = label
        do! add3 eax' ebx'
        do! jump' BranchCondition.NZ l1
    }) =! "\x01\xD8\x75\xFC"B
