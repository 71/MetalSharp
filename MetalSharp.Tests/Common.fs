module Common

open MetalSharp
open Keystone

let inline raw(f: CodeBuilder -> unit) =
    use builder = new CodeBuilder()
    f builder
    builder.GetByteArray()

let inline expected(str, arch, mode) =
    use ks = new Engine(arch, mode, true)

    let addr = 0UL
    let enc = ks.Assemble(str, addr)

    enc.Buffer

module ARM =

    let inline expected32(str) = expected(str, Architecture.ARM, Mode.X32)
    let inline expected64(str) = expected(str, Architecture.ARM64, Mode.X64)

module X86 =

    let inline expected32(str) = expected(str, Architecture.X86, Mode.X32)
    let inline expected64(str) = expected(str, Architecture.X86, Mode.X64)
