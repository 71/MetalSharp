Metal#
======

A type-safe assembler currently targeting x86 and ARM, in F#.
**This is a work in progress.**

## Quick start
```fsharp
open MetalSharp
open MetalSharp.X86
open MetalSharp.X86.Untyped

use builder = CodeBuilder()

let start = builder.CreateLabel()

builder {
    do! mov' eax' (imm32 42)
    do! ret
}

printfn "Answer: %d." start.AsFunction<Func<int>>().Invoke()
```

## License
The repository is covered by three different licenses:
- The [Keystone](https://github.com/keystone-engine/keystone) submodule in
  [MetalSharp.Tests](./MetalSharp.Tests) is covered by the [GPLv2](https://www.gnu.org/licenses/old-licenses/gpl-2.0.txt).
- The `Instructions.dat` files (for [X86](./MetalSharp/X86/Instructions.dat) and [ARM](./MetalSharp/ARM/Instructions.dat))
  are [unlicensed](http://unlicense.org/UNLICENSE).
- The rest of the repository is under the [MIT](./LICENSE.md) license.
