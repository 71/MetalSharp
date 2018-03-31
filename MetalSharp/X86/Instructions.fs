namespace MetalSharp.X86

open System

open MetalSharp

module Typed =

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add1 (op1 : RegisterOrMemory<S8>) (op2 : Register<S8>) (builder : CodeBuilder) = (builder.Emit 0x0uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add2 (op1 : RegisterOrMemory<S16>) (op2 : Register<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0x1uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add3 (op1 : RegisterOrMemory<S32>) (op2 : Register<S32>) (builder : CodeBuilder) = (builder.Emit 0x1uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add4 (op1 : RegisterOrMemory<S64>) (op2 : Register<S64>) (builder : CodeBuilder) = (builder.Emit 0x48uy) ; (builder.Emit 0x1uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add5 (op1 : Register<S8>) (op2 : RegisterOrMemory<S8>) (builder : CodeBuilder) = (builder.Emit 0x2uy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add6 (op1 : Register<S16>) (op2 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0x3uy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add7 (op1 : Register<S32>) (op2 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (builder.Emit 0x3uy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add8 (op1 : Register<S64>) (op2 : RegisterOrMemory<S64>) (builder : CodeBuilder) = (builder.Emit 0x48uy) ; (builder.Emit 0x3uy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add9 (op1 : RegisterOrMemory<S8>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x80uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add10 (op1 : RegisterOrMemory<S16>) (op2 : Immediate<S16>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add11 (op1 : RegisterOrMemory<S32>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (builder.Emit 0x81uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add12 (op1 : RegisterOrMemory<S64>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (() ; builder.Emit 0x48uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add13 (op1 : RegisterOrMemory<S16>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add14 (op1 : RegisterOrMemory<S32>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x83uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add15 (op1 : RegisterOrMemory<S64>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (() ; builder.Emit 0x48uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub1 (op1 : RegisterOrMemory<S8>) (op2 : Register<S8>) (builder : CodeBuilder) = (builder.Emit 0x28uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub2 (op1 : RegisterOrMemory<S16>) (op2 : Register<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0x29uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub3 (op1 : RegisterOrMemory<S32>) (op2 : Register<S32>) (builder : CodeBuilder) = (builder.Emit 0x29uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub4 (op1 : RegisterOrMemory<S64>) (op2 : Register<S64>) (builder : CodeBuilder) = (builder.Emit 0x48uy) ; (builder.Emit 0x29uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub5 (op1 : Register<S8>) (op2 : RegisterOrMemory<S8>) (builder : CodeBuilder) = (builder.Emit 0x2auy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub6 (op1 : Register<S16>) (op2 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0x2buy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub7 (op1 : Register<S32>) (op2 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (builder.Emit 0x2buy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub8 (op1 : Register<S64>) (op2 : RegisterOrMemory<S64>) (builder : CodeBuilder) = (builder.Emit 0x48uy) ; (builder.Emit 0x2buy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub9 (op1 : RegisterOrMemory<S8>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x80uy) ; (emitModrm' op2 op1 5uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub10 (op1 : RegisterOrMemory<S16>) (op2 : Immediate<S16>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 5uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub11 (op1 : RegisterOrMemory<S32>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (builder.Emit 0x81uy) ; (emitModrm' op2 op1 5uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub12 (op1 : RegisterOrMemory<S64>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (() ; builder.Emit 0x48uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 5uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub13 (op1 : RegisterOrMemory<S16>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 5uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub14 (op1 : RegisterOrMemory<S32>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x83uy) ; (emitModrm' op2 op1 5uy builder)

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub15 (op1 : RegisterOrMemory<S64>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (() ; builder.Emit 0x48uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 5uy builder)

  /// Emits an 'inc' instruction.
  [<CompiledName("EmitInc")>]
  let inc1 (op1 : Register<S16>) (builder : CodeBuilder) = (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0x40uy + op1.Reg.Value % 8uy))

  /// Emits an 'inc' instruction.
  [<CompiledName("EmitInc")>]
  let inc2 (op1 : Register<S32>) (builder : CodeBuilder) = (emitRegisterOpcode 0x40uy op1 builder)

  /// Emits an 'inc' instruction.
  [<CompiledName("EmitInc")>]
  let inc3 (op1 : RegisterOrMemory<S8>) (builder : CodeBuilder) = (builder.Emit 0xfeuy) ; emitMemory op1 0uy builder

  /// Emits an 'inc' instruction.
  [<CompiledName("EmitInc")>]
  let inc4 (op1 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 0uy builder

  /// Emits an 'inc' instruction.
  [<CompiledName("EmitInc")>]
  let inc5 (op1 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 0uy builder

  /// Emits an 'inc' instruction.
  [<CompiledName("EmitInc")>]
  let inc6 (op1 : RegisterOrMemory<S64>) (builder : CodeBuilder) = (() ; builder.Emit 0x48uy) ; (builder.Emit 0xffuy) ; emitMemory op1 0uy builder

  /// Emits an 'dec' instruction.
  [<CompiledName("EmitDec")>]
  let dec1 (op1 : Register<S16>) (builder : CodeBuilder) = (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0x48uy + op1.Reg.Value % 8uy))

  /// Emits an 'dec' instruction.
  [<CompiledName("EmitDec")>]
  let dec2 (op1 : Register<S32>) (builder : CodeBuilder) = (emitRegisterOpcode 0x48uy op1 builder)

  /// Emits an 'dec' instruction.
  [<CompiledName("EmitDec")>]
  let dec3 (op1 : RegisterOrMemory<S8>) (builder : CodeBuilder) = (builder.Emit 0xfeuy) ; emitMemory op1 1uy builder

  /// Emits an 'dec' instruction.
  [<CompiledName("EmitDec")>]
  let dec4 (op1 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 1uy builder

  /// Emits an 'dec' instruction.
  [<CompiledName("EmitDec")>]
  let dec5 (op1 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 1uy builder

  /// Emits an 'dec' instruction.
  [<CompiledName("EmitDec")>]
  let dec6 (op1 : RegisterOrMemory<S64>) (builder : CodeBuilder) = (() ; builder.Emit 0x48uy) ; (builder.Emit 0xffuy) ; emitMemory op1 1uy builder

  /// Emits an 'push' instruction.
  [<CompiledName("EmitPush")>]
  let push1 (op1 : Register<S16>) (builder : CodeBuilder) = (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0x50uy + op1.Reg.Value % 8uy))

  /// Emits an 'push' instruction.
  [<CompiledName("EmitPush")>]
  let push2 (op1 : Register<S32>) (builder : CodeBuilder) = (emitRegisterOpcode 0x50uy op1 builder)

  /// Emits an 'push' instruction.
  [<CompiledName("EmitPush")>]
  let push3 (op1 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 6uy builder

  /// Emits an 'push' instruction.
  [<CompiledName("EmitPush")>]
  let push4 (op1 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 6uy builder

  /// Emits an 'push' instruction.
  [<CompiledName("EmitPush")>]
  let push5 (op1 : RegisterOrMemory<S64>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 6uy builder

  /// Emits an 'pop' instruction.
  [<CompiledName("EmitPop")>]
  let pop1 (op1 : Register<S16>) (builder : CodeBuilder) = (emitRegisterOpcode 0x58uy op1 builder)

  /// Emits an 'pop' instruction.
  [<CompiledName("EmitPop")>]
  let pop2 (op1 : Register<S32>) (builder : CodeBuilder) = (emitRegisterOpcode 0x58uy op1 builder)

  /// Emits an 'pop' instruction.
  [<CompiledName("EmitPop")>]
  let pop3 (op1 : Register<S64>) (builder : CodeBuilder) = (emitRegisterOpcode 0x58uy op1 builder)

  /// Emits an 'jo' instruction.
  [<CompiledName("EmitJo")>]
  let jo (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x70uy) ; emitImmediate op1 builder

  /// Emits an 'jno' instruction.
  [<CompiledName("EmitJno")>]
  let jno (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x71uy) ; emitImmediate op1 builder

  /// Emits an 'jc' instruction.
  [<CompiledName("EmitJc")>]
  let jc (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x72uy) ; emitImmediate op1 builder

  /// Emits an 'jnc' instruction.
  [<CompiledName("EmitJnc")>]
  let jnc (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x73uy) ; emitImmediate op1 builder

  /// Emits an 'jz' instruction.
  [<CompiledName("EmitJz")>]
  let jz (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x74uy) ; emitImmediate op1 builder

  /// Emits an 'jnz' instruction.
  [<CompiledName("EmitJnz")>]
  let jnz (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x75uy) ; emitImmediate op1 builder

  /// Emits an 'je' instruction.
  [<CompiledName("EmitJe")>]
  let je (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x74uy) ; emitImmediate op1 builder

  /// Emits an 'jne' instruction.
  [<CompiledName("EmitJne")>]
  let jne (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x75uy) ; emitImmediate op1 builder

  /// Emits an 'jbe' instruction.
  [<CompiledName("EmitJbe")>]
  let jbe (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x76uy) ; emitImmediate op1 builder

  /// Emits an 'jnbe' instruction.
  [<CompiledName("EmitJnbe")>]
  let jnbe (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x77uy) ; emitImmediate op1 builder

  /// Emits an 'js' instruction.
  [<CompiledName("EmitJs")>]
  let js (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x78uy) ; emitImmediate op1 builder

  /// Emits an 'jns' instruction.
  [<CompiledName("EmitJns")>]
  let jns (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x79uy) ; emitImmediate op1 builder

  /// Emits an 'jp' instruction.
  [<CompiledName("EmitJp")>]
  let jp (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x7auy) ; emitImmediate op1 builder

  /// Emits an 'jnp' instruction.
  [<CompiledName("EmitJnp")>]
  let jnp (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x7buy) ; emitImmediate op1 builder

  /// Emits an 'jl' instruction.
  [<CompiledName("EmitJl")>]
  let jl (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x7cuy) ; emitImmediate op1 builder

  /// Emits an 'jnl' instruction.
  [<CompiledName("EmitJnl")>]
  let jnl (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x7duy) ; emitImmediate op1 builder

  /// Emits an 'jle' instruction.
  [<CompiledName("EmitJle")>]
  let jle (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x7euy) ; emitImmediate op1 builder

  /// Emits an 'jnle' instruction.
  [<CompiledName("EmitJnle")>]
  let jnle (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x7fuy) ; emitImmediate op1 builder

  /// Emits an 'cmp' instruction.
  [<CompiledName("EmitCmp")>]
  let cmp1 (op1 : RegisterOrMemory<S8>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x80uy) ; (emitModrm' op2 op1 7uy builder)

  /// Emits an 'cmp' instruction.
  [<CompiledName("EmitCmp")>]
  let cmp2 (op1 : RegisterOrMemory<S16>) (op2 : Immediate<S16>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 7uy builder)

  /// Emits an 'cmp' instruction.
  [<CompiledName("EmitCmp")>]
  let cmp3 (op1 : RegisterOrMemory<S32>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (builder.Emit 0x81uy) ; (emitModrm' op2 op1 7uy builder)

  /// Emits an 'cmp' instruction.
  [<CompiledName("EmitCmp")>]
  let cmp4 (op1 : RegisterOrMemory<S64>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (() ; builder.Emit 0x48uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 7uy builder)

  /// Emits an 'cmp' instruction.
  [<CompiledName("EmitCmp")>]
  let cmp5 (op1 : RegisterOrMemory<S16>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 7uy builder)

  /// Emits an 'cmp' instruction.
  [<CompiledName("EmitCmp")>]
  let cmp6 (op1 : RegisterOrMemory<S32>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0x83uy) ; (emitModrm' op2 op1 7uy builder)

  /// Emits an 'cmp' instruction.
  [<CompiledName("EmitCmp")>]
  let cmp7 (op1 : RegisterOrMemory<S64>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (() ; builder.Emit 0x48uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 7uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov1 (op1 : RegisterOrMemory<S8>) (op2 : Register<S8>) (builder : CodeBuilder) = (builder.Emit 0x88uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov2 (op1 : RegisterOrMemory<S16>) (op2 : Register<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0x89uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov3 (op1 : RegisterOrMemory<S32>) (op2 : Register<S32>) (builder : CodeBuilder) = (builder.Emit 0x89uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov4 (op1 : RegisterOrMemory<S64>) (op2 : Register<S64>) (builder : CodeBuilder) = (builder.Emit 0x48uy) ; (builder.Emit 0x89uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov5 (op1 : Register<S8>) (op2 : RegisterOrMemory<S8>) (builder : CodeBuilder) = (builder.Emit 0x8auy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov6 (op1 : Register<S16>) (op2 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0x8buy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov7 (op1 : Register<S32>) (op2 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (builder.Emit 0x8buy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov8 (op1 : Register<S64>) (op2 : RegisterOrMemory<S64>) (builder : CodeBuilder) = (builder.Emit 0x48uy) ; (builder.Emit 0x8buy) ; (emitModrm' op1 op2 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov9 (op1 : Register<S8>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (emitRegisterOpcode 0xb0uy op1 builder) ; (emitImmediate op2 builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov10 (op1 : Register<S16>) (op2 : Immediate<S16>) (builder : CodeBuilder) = (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0xb8uy + op1.Reg.Value % 8uy)) ; (emitImmediate op2 builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov11 (op1 : Register<S32>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (emitRegisterOpcode 0xb8uy op1 builder) ; (emitImmediate op2 builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov12 (op1 : Register<S64>) (op2 : Immediate<S64>) (builder : CodeBuilder) = (() ; builder.Emit (0x48uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0xb8uy + op1.Reg.Value % 8uy)) ; (emitImmediate op2 builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov13 (op1 : RegisterOrMemory<S8>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0xc6uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov14 (op1 : RegisterOrMemory<S16>) (op2 : Immediate<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0xc7uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov15 (op1 : RegisterOrMemory<S32>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (builder.Emit 0xc7uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov16 (op1 : RegisterOrMemory<S64>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (builder.Emit 0x48uy) ; (builder.Emit 0xc7uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'lea' instruction.
  [<CompiledName("EmitLea")>]
  let lea1 (op1 : Register<S16>) (op2 : Memory<_>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0x8duy) ; (emitRegister op1 builder ; emitMemory op2 0uy builder)

  /// Emits an 'lea' instruction.
  [<CompiledName("EmitLea")>]
  let lea2 (op1 : Register<S32>) (op2 : Memory<_>) (builder : CodeBuilder) = (builder.Emit 0x8duy) ; (emitRegister op1 builder ; emitMemory op2 0uy builder)

  /// Emits an 'lea' instruction.
  [<CompiledName("EmitLea")>]
  let lea3 (op1 : Register<S64>) (op2 : Memory<_>) (builder : CodeBuilder) = (builder.Emit 0x48uy) ; (builder.Emit 0x8duy) ; (emitRegister op1 builder ; emitMemory op2 0uy builder)

  /// Emits an 'pushf' instruction.
  [<CompiledName("EmitPushf")>]
  let pushf  (builder : CodeBuilder) = (builder.Emit 0x9cuy)

  /// Emits an 'popf' instruction.
  [<CompiledName("EmitPopf")>]
  let popf  (builder : CodeBuilder) = (builder.Emit 0x9duy)

  /// Emits an 'ret' instruction.
  [<CompiledName("EmitRet")>]
  let ret  (builder : CodeBuilder) = (builder.Emit 0xc3uy)

  /// Emits an 'call' instruction.
  [<CompiledName("EmitCall")>]
  let call1 (op1 : Immediate<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0xe8uy) ; emitImmediate op1 builder

  /// Emits an 'call' instruction.
  [<CompiledName("EmitCall")>]
  let call2 (op1 : Immediate<S32>) (builder : CodeBuilder) = (builder.Emit 0xe8uy) ; emitImmediate op1 builder

  /// Emits an 'call' instruction.
  [<CompiledName("EmitCall")>]
  let call3 (op1 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (() ; () ; emitPrefix op1 builder) ; (builder.Emit 0xffuy) ; emitMemory op1 2uy builder

  /// Emits an 'call' instruction.
  [<CompiledName("EmitCall")>]
  let call4 (op1 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (() ; () ; emitPrefix op1 builder) ; (builder.Emit 0xffuy) ; emitMemory op1 2uy builder

  /// Emits an 'call' instruction.
  [<CompiledName("EmitCall")>]
  let call5 (op1 : RegisterOrMemory<S64>) (builder : CodeBuilder) = (() ; () ; emitPrefix op1 builder) ; (builder.Emit 0xffuy) ; emitMemory op1 2uy builder

  /// Emits an 'jmp' instruction.
  [<CompiledName("EmitJmp")>]
  let jmp1 (op1 : Immediate<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0xe9uy) ; emitImmediate op1 builder

  /// Emits an 'jmp' instruction.
  [<CompiledName("EmitJmp")>]
  let jmp2 (op1 : Immediate<S32>) (builder : CodeBuilder) = (builder.Emit 0xe9uy) ; emitImmediate op1 builder

  /// Emits an 'jmp' instruction.
  [<CompiledName("EmitJmp")>]
  let jmp3 (op1 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0xebuy) ; emitImmediate op1 builder

  /// Emits an 'jmp' instruction.
  [<CompiledName("EmitJmp")>]
  let jmp4 (op1 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 4uy builder

  /// Emits an 'jmp' instruction.
  [<CompiledName("EmitJmp")>]
  let jmp5 (op1 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 4uy builder

  /// Emits an 'jmpf' instruction.
  [<CompiledName("EmitJmpf")>]
  let jmpf1 (op1 : Immediate<S16>) (builder : CodeBuilder) = (builder.Emit 0x66uy) ; (builder.Emit 0xeauy) ; emitImmediate op1 builder

  /// Emits an 'jmpf' instruction.
  [<CompiledName("EmitJmpf")>]
  let jmpf2 (op1 : Immediate<S32>) (builder : CodeBuilder) = (builder.Emit 0xeauy) ; emitImmediate op1 builder

  /// Emits an 'jmpf' instruction.
  [<CompiledName("EmitJmpf")>]
  let jmpf3 (op1 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 5uy builder

  /// Emits an 'jmpf' instruction.
  [<CompiledName("EmitJmpf")>]
  let jmpf4 (op1 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 5uy builder

  /// Emits an 'test' instruction.
  [<CompiledName("EmitTest")>]
  let test1 (op1 : RegisterOrMemory<S8>) (op2 : Immediate<S8>) (builder : CodeBuilder) = (builder.Emit 0xf6uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'test' instruction.
  [<CompiledName("EmitTest")>]
  let test2 (op1 : RegisterOrMemory<S16>) (op2 : Immediate<S16>) (builder : CodeBuilder) = (() ; builder.Emit 0x66uy) ; (builder.Emit 0xf7uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'test' instruction.
  [<CompiledName("EmitTest")>]
  let test3 (op1 : RegisterOrMemory<S32>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (builder.Emit 0xf7uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'test' instruction.
  [<CompiledName("EmitTest")>]
  let test4 (op1 : RegisterOrMemory<S64>) (op2 : Immediate<S32>) (builder : CodeBuilder) = (() ; builder.Emit 0x48uy) ; (builder.Emit 0xf7uy) ; (emitModrm' op2 op1 0uy builder)

  /// Emits an 'callf' instruction.
  [<CompiledName("EmitCallf")>]
  let callf1 (op1 : RegisterOrMemory<S16>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 3uy builder

  /// Emits an 'callf' instruction.
  [<CompiledName("EmitCallf")>]
  let callf2 (op1 : RegisterOrMemory<S32>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 3uy builder

  /// Emits an 'callf' instruction.
  [<CompiledName("EmitCallf")>]
  let callf3 (op1 : RegisterOrMemory<S64>) (builder : CodeBuilder) = (builder.Emit 0xffuy) ; emitMemory op1 3uy builder

[<AutoOpen>]
module Untyped =
  /// Emits an 'add' instruction.
  [<CompiledName("EmitAdd")>]
  let add (dst : Operand) (src : Operand) (builder : CodeBuilder) =
    match (dst :> obj), (src :> obj) with
    | (:? RegisterOrMemory<S8> as op1), (:? Register<S8> as op2) -> (builder.Emit 0x0uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Register<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x1uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Register<S32> as op2) -> (builder.Emit 0x1uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Register<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x1uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? Register<S8> as op1), (:? RegisterOrMemory<S8> as op2) -> (builder.Emit 0x2uy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S16> as op1), (:? RegisterOrMemory<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x3uy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S32> as op1), (:? RegisterOrMemory<S32> as op2) -> (builder.Emit 0x3uy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S64> as op1), (:? RegisterOrMemory<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x3uy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x80uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0x81uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x83uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | _ -> false

  /// Emits an 'sub' instruction.
  [<CompiledName("EmitSub")>]
  let sub (dst : Operand) (src : Operand) (builder : CodeBuilder) =
    match (dst :> obj), (src :> obj) with
    | (:? RegisterOrMemory<S8> as op1), (:? Register<S8> as op2) -> (builder.Emit 0x28uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Register<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x29uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Register<S32> as op2) -> (builder.Emit 0x29uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Register<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x29uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? Register<S8> as op1), (:? RegisterOrMemory<S8> as op2) -> (builder.Emit 0x2auy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S16> as op1), (:? RegisterOrMemory<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x2buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S32> as op1), (:? RegisterOrMemory<S32> as op2) -> (builder.Emit 0x2buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S64> as op1), (:? RegisterOrMemory<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x2buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x80uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0x81uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x83uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | _ -> false

  /// Emits an 'inc' instruction.
  [<CompiledName("EmitInc")>]
  let inc (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Register<S16> as op1) -> (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0x40uy + op1.Reg.Value % 8uy)) ; true
    | (:? Register<S32> as op1) -> (emitRegisterOpcode 0x40uy op1 builder) ; true
    | (:? RegisterOrMemory<S8> as op1) -> (builder.Emit 0xfeuy) ; emitMemory op1 0uy builder ; true
    | (:? RegisterOrMemory<S16> as op1) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 0uy builder ; true
    | (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 0uy builder ; true
    | (:? RegisterOrMemory<S64> as op1) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0xffuy) ; emitMemory op1 0uy builder ; true
    | _ -> false

  /// Emits an 'dec' instruction.
  [<CompiledName("EmitDec")>]
  let dec (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Register<S16> as op1) -> (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0x48uy + op1.Reg.Value % 8uy)) ; true
    | (:? Register<S32> as op1) -> (emitRegisterOpcode 0x48uy op1 builder) ; true
    | (:? RegisterOrMemory<S8> as op1) -> (builder.Emit 0xfeuy) ; emitMemory op1 1uy builder ; true
    | (:? RegisterOrMemory<S16> as op1) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 1uy builder ; true
    | (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 1uy builder ; true
    | (:? RegisterOrMemory<S64> as op1) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0xffuy) ; emitMemory op1 1uy builder ; true
    | _ -> false

  /// Emits an 'push' instruction.
  [<CompiledName("EmitPush")>]
  let push (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Register<S16> as op1) -> (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0x50uy + op1.Reg.Value % 8uy)) ; true
    | (:? Register<S32> as op1) -> (emitRegisterOpcode 0x50uy op1 builder) ; true
    | (:? RegisterOrMemory<S16> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 6uy builder ; true
    | (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 6uy builder ; true
    | (:? RegisterOrMemory<S64> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 6uy builder ; true
    | _ -> false

  /// Emits an 'pop' instruction.
  [<CompiledName("EmitPop")>]
  let pop (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Register<S16> as op1) -> (emitRegisterOpcode 0x58uy op1 builder) ; true
    | (:? Register<S32> as op1) -> (emitRegisterOpcode 0x58uy op1 builder) ; true
    | (:? Register<S64> as op1) -> (emitRegisterOpcode 0x58uy op1 builder) ; true
    | _ -> false

  /// Emits an 'jo' instruction.
  [<CompiledName("EmitJo")>]
  let jo (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x70uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jno' instruction.
  [<CompiledName("EmitJno")>]
  let jno (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x71uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jc' instruction.
  [<CompiledName("EmitJc")>]
  let jc (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x72uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jnc' instruction.
  [<CompiledName("EmitJnc")>]
  let jnc (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x73uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jz' instruction.
  [<CompiledName("EmitJz")>]
  let jz (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x74uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jnz' instruction.
  [<CompiledName("EmitJnz")>]
  let jnz (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x75uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'je' instruction.
  [<CompiledName("EmitJe")>]
  let je (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x74uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jne' instruction.
  [<CompiledName("EmitJne")>]
  let jne (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x75uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jbe' instruction.
  [<CompiledName("EmitJbe")>]
  let jbe (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x76uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jnbe' instruction.
  [<CompiledName("EmitJnbe")>]
  let jnbe (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x77uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'js' instruction.
  [<CompiledName("EmitJs")>]
  let js (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x78uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jns' instruction.
  [<CompiledName("EmitJns")>]
  let jns (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x79uy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jp' instruction.
  [<CompiledName("EmitJp")>]
  let jp (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x7auy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jnp' instruction.
  [<CompiledName("EmitJnp")>]
  let jnp (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x7buy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jl' instruction.
  [<CompiledName("EmitJl")>]
  let jl (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x7cuy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jnl' instruction.
  [<CompiledName("EmitJnl")>]
  let jnl (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x7duy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jle' instruction.
  [<CompiledName("EmitJle")>]
  let jle (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x7euy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'jnle' instruction.
  [<CompiledName("EmitJnle")>]
  let jnle (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S8> as op1) -> (builder.Emit 0x7fuy) ; emitImmediate op1 builder ; true
    | _ -> false

  /// Emits an 'cmp' instruction.
  [<CompiledName("EmitCmp")>]
  let cmp (dst : Operand) (src : Operand) (builder : CodeBuilder) =
    match (dst :> obj), (src :> obj) with
    | (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x80uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0x81uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x83uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | _ -> false

  /// Emits an 'mov' instruction.
  [<CompiledName("EmitMov")>]
  let mov (dst : Operand) (src : Operand) (builder : CodeBuilder) =
    match (dst :> obj), (src :> obj) with
    | (:? RegisterOrMemory<S8> as op1), (:? Register<S8> as op2) -> (builder.Emit 0x88uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Register<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x89uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Register<S32> as op2) -> (builder.Emit 0x89uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Register<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x89uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? Register<S8> as op1), (:? RegisterOrMemory<S8> as op2) -> (builder.Emit 0x8auy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S16> as op1), (:? RegisterOrMemory<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x8buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S32> as op1), (:? RegisterOrMemory<S32> as op2) -> (builder.Emit 0x8buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S64> as op1), (:? RegisterOrMemory<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x8buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | (:? Register<S8> as op1), (:? Immediate<S8> as op2) -> (emitRegisterOpcode 0xb0uy op1 builder) ; (emitImmediate op2 builder) ; true
    | (:? Register<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0xb8uy + op1.Reg.Value % 8uy)) ; (emitImmediate op2 builder) ; true
    | (:? Register<S32> as op1), (:? Immediate<S32> as op2) -> (emitRegisterOpcode 0xb8uy op1 builder) ; (emitImmediate op2 builder) ; true
    | (:? Register<S64> as op1), (:? Immediate<S64> as op2) -> (() ; builder.Emit (0x48uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0xb8uy + op1.Reg.Value % 8uy)) ; (emitImmediate op2 builder) ; true
    | (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0xc6uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0xc7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0xc7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0xc7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | _ -> false

  /// Emits an 'lea' instruction.
  [<CompiledName("EmitLea")>]
  let lea (dst : Operand) (src : Operand) (builder : CodeBuilder) =
    match (dst :> obj), (src :> obj) with
    | _ -> false

  /// Emits an 'pushf' instruction.
  [<CompiledName("EmitPushf")>]
  let pushf (builder : CodeBuilder) =
    (builder.Emit 0x9cuy) ; true

  /// Emits an 'popf' instruction.
  [<CompiledName("EmitPopf")>]
  let popf (builder : CodeBuilder) =
    (builder.Emit 0x9duy) ; true

  /// Emits an 'ret' instruction.
  [<CompiledName("EmitRet")>]
  let ret (builder : CodeBuilder) =
    (builder.Emit 0xc3uy) ; true

  /// Emits an 'call' instruction.
  [<CompiledName("EmitCall")>]
  let call (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S16> as op1) -> (builder.Emit 0x66uy) ; (builder.Emit 0xe8uy) ; emitImmediate op1 builder ; true
    | (:? Immediate<S32> as op1) -> (builder.Emit 0xe8uy) ; emitImmediate op1 builder ; true
    | (:? RegisterOrMemory<S16> as op1) -> (() ; () ; emitPrefix op1 builder) ; (builder.Emit 0xffuy) ; emitMemory op1 2uy builder ; true
    | (:? RegisterOrMemory<S32> as op1) -> (() ; () ; emitPrefix op1 builder) ; (builder.Emit 0xffuy) ; emitMemory op1 2uy builder ; true
    | (:? RegisterOrMemory<S64> as op1) -> (() ; () ; emitPrefix op1 builder) ; (builder.Emit 0xffuy) ; emitMemory op1 2uy builder ; true
    | _ -> false

  /// Emits an 'jmp' instruction.
  [<CompiledName("EmitJmp")>]
  let jmp (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S16> as op1) -> (builder.Emit 0x66uy) ; (builder.Emit 0xe9uy) ; emitImmediate op1 builder ; true
    | (:? Immediate<S32> as op1) -> (builder.Emit 0xe9uy) ; emitImmediate op1 builder ; true
    | (:? Immediate<S8> as op1) -> (builder.Emit 0xebuy) ; emitImmediate op1 builder ; true
    | (:? RegisterOrMemory<S16> as op1) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 4uy builder ; true
    | (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 4uy builder ; true
    | _ -> false

  /// Emits an 'jmpf' instruction.
  [<CompiledName("EmitJmpf")>]
  let jmpf (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? Immediate<S16> as op1) -> (builder.Emit 0x66uy) ; (builder.Emit 0xeauy) ; emitImmediate op1 builder ; true
    | (:? Immediate<S32> as op1) -> (builder.Emit 0xeauy) ; emitImmediate op1 builder ; true
    | (:? RegisterOrMemory<S16> as op1) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 5uy builder ; true
    | (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 5uy builder ; true
    | _ -> false

  /// Emits an 'test' instruction.
  [<CompiledName("EmitTest")>]
  let test (dst : Operand) (src : Operand) (builder : CodeBuilder) =
    match (dst :> obj), (src :> obj) with
    | (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0xf6uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xf7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0xf7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0xf7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | _ -> false

  /// Emits an 'callf' instruction.
  [<CompiledName("EmitCallf")>]
  let callf (operand : Operand) (builder : CodeBuilder) =
    match (operand :> obj) with
    | (:? RegisterOrMemory<S16> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 3uy builder ; true
    | (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 3uy builder ; true
    | (:? RegisterOrMemory<S64> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 3uy builder ; true
    | _ -> false


  /// Emits an 'add' instruction, or throws if it is unknown.
  [<CompiledName("EmitAddOrThrow")>]
  let add' (dst : Operand) (src : Operand) (builder : CodeBuilder) = if not <| add dst src builder then failwith "Unknown instruction."

  /// Emits an 'sub' instruction, or throws if it is unknown.
  [<CompiledName("EmitSubOrThrow")>]
  let sub' (dst : Operand) (src : Operand) (builder : CodeBuilder) = if not <| sub dst src builder then failwith "Unknown instruction."

  /// Emits an 'inc' instruction, or throws if it is unknown.
  [<CompiledName("EmitIncOrThrow")>]
  let inc' (operand : Operand) (builder : CodeBuilder) = if not <| inc operand builder then failwith "Unknown instruction."

  /// Emits an 'dec' instruction, or throws if it is unknown.
  [<CompiledName("EmitDecOrThrow")>]
  let dec' (operand : Operand) (builder : CodeBuilder) = if not <| dec operand builder then failwith "Unknown instruction."

  /// Emits an 'push' instruction, or throws if it is unknown.
  [<CompiledName("EmitPushOrThrow")>]
  let push' (operand : Operand) (builder : CodeBuilder) = if not <| push operand builder then failwith "Unknown instruction."

  /// Emits an 'pop' instruction, or throws if it is unknown.
  [<CompiledName("EmitPopOrThrow")>]
  let pop' (operand : Operand) (builder : CodeBuilder) = if not <| pop operand builder then failwith "Unknown instruction."

  /// Emits an 'jo' instruction, or throws if it is unknown.
  [<CompiledName("EmitJoOrThrow")>]
  let jo' (operand : Operand) (builder : CodeBuilder) = if not <| jo operand builder then failwith "Unknown instruction."

  /// Emits an 'jno' instruction, or throws if it is unknown.
  [<CompiledName("EmitJnoOrThrow")>]
  let jno' (operand : Operand) (builder : CodeBuilder) = if not <| jno operand builder then failwith "Unknown instruction."

  /// Emits an 'jc' instruction, or throws if it is unknown.
  [<CompiledName("EmitJcOrThrow")>]
  let jc' (operand : Operand) (builder : CodeBuilder) = if not <| jc operand builder then failwith "Unknown instruction."

  /// Emits an 'jnc' instruction, or throws if it is unknown.
  [<CompiledName("EmitJncOrThrow")>]
  let jnc' (operand : Operand) (builder : CodeBuilder) = if not <| jnc operand builder then failwith "Unknown instruction."

  /// Emits an 'jz' instruction, or throws if it is unknown.
  [<CompiledName("EmitJzOrThrow")>]
  let jz' (operand : Operand) (builder : CodeBuilder) = if not <| jz operand builder then failwith "Unknown instruction."

  /// Emits an 'jnz' instruction, or throws if it is unknown.
  [<CompiledName("EmitJnzOrThrow")>]
  let jnz' (operand : Operand) (builder : CodeBuilder) = if not <| jnz operand builder then failwith "Unknown instruction."

  /// Emits an 'je' instruction, or throws if it is unknown.
  [<CompiledName("EmitJeOrThrow")>]
  let je' (operand : Operand) (builder : CodeBuilder) = if not <| je operand builder then failwith "Unknown instruction."

  /// Emits an 'jne' instruction, or throws if it is unknown.
  [<CompiledName("EmitJneOrThrow")>]
  let jne' (operand : Operand) (builder : CodeBuilder) = if not <| jne operand builder then failwith "Unknown instruction."

  /// Emits an 'jbe' instruction, or throws if it is unknown.
  [<CompiledName("EmitJbeOrThrow")>]
  let jbe' (operand : Operand) (builder : CodeBuilder) = if not <| jbe operand builder then failwith "Unknown instruction."

  /// Emits an 'jnbe' instruction, or throws if it is unknown.
  [<CompiledName("EmitJnbeOrThrow")>]
  let jnbe' (operand : Operand) (builder : CodeBuilder) = if not <| jnbe operand builder then failwith "Unknown instruction."

  /// Emits an 'js' instruction, or throws if it is unknown.
  [<CompiledName("EmitJsOrThrow")>]
  let js' (operand : Operand) (builder : CodeBuilder) = if not <| js operand builder then failwith "Unknown instruction."

  /// Emits an 'jns' instruction, or throws if it is unknown.
  [<CompiledName("EmitJnsOrThrow")>]
  let jns' (operand : Operand) (builder : CodeBuilder) = if not <| jns operand builder then failwith "Unknown instruction."

  /// Emits an 'jp' instruction, or throws if it is unknown.
  [<CompiledName("EmitJpOrThrow")>]
  let jp' (operand : Operand) (builder : CodeBuilder) = if not <| jp operand builder then failwith "Unknown instruction."

  /// Emits an 'jnp' instruction, or throws if it is unknown.
  [<CompiledName("EmitJnpOrThrow")>]
  let jnp' (operand : Operand) (builder : CodeBuilder) = if not <| jnp operand builder then failwith "Unknown instruction."

  /// Emits an 'jl' instruction, or throws if it is unknown.
  [<CompiledName("EmitJlOrThrow")>]
  let jl' (operand : Operand) (builder : CodeBuilder) = if not <| jl operand builder then failwith "Unknown instruction."

  /// Emits an 'jnl' instruction, or throws if it is unknown.
  [<CompiledName("EmitJnlOrThrow")>]
  let jnl' (operand : Operand) (builder : CodeBuilder) = if not <| jnl operand builder then failwith "Unknown instruction."

  /// Emits an 'jle' instruction, or throws if it is unknown.
  [<CompiledName("EmitJleOrThrow")>]
  let jle' (operand : Operand) (builder : CodeBuilder) = if not <| jle operand builder then failwith "Unknown instruction."

  /// Emits an 'jnle' instruction, or throws if it is unknown.
  [<CompiledName("EmitJnleOrThrow")>]
  let jnle' (operand : Operand) (builder : CodeBuilder) = if not <| jnle operand builder then failwith "Unknown instruction."

  /// Emits an 'cmp' instruction, or throws if it is unknown.
  [<CompiledName("EmitCmpOrThrow")>]
  let cmp' (dst : Operand) (src : Operand) (builder : CodeBuilder) = if not <| cmp dst src builder then failwith "Unknown instruction."

  /// Emits an 'mov' instruction, or throws if it is unknown.
  [<CompiledName("EmitMovOrThrow")>]
  let mov' (dst : Operand) (src : Operand) (builder : CodeBuilder) = if not <| mov dst src builder then failwith "Unknown instruction."

  /// Emits an 'lea' instruction, or throws if it is unknown.
  [<CompiledName("EmitLeaOrThrow")>]
  let lea' (dst : Operand) (src : Operand) (builder : CodeBuilder) = if not <| lea dst src builder then failwith "Unknown instruction."

  /// Emits an 'pushf' instruction, or throws if it is unknown.
  [<CompiledName("EmitPushfOrThrow")>]
  let pushf' (builder : CodeBuilder) = if not <| pushf builder then failwith "Unknown instruction."

  /// Emits an 'popf' instruction, or throws if it is unknown.
  [<CompiledName("EmitPopfOrThrow")>]
  let popf' (builder : CodeBuilder) = if not <| popf builder then failwith "Unknown instruction."

  /// Emits an 'ret' instruction, or throws if it is unknown.
  [<CompiledName("EmitRetOrThrow")>]
  let ret' (builder : CodeBuilder) = if not <| ret builder then failwith "Unknown instruction."

  /// Emits an 'call' instruction, or throws if it is unknown.
  [<CompiledName("EmitCallOrThrow")>]
  let call' (operand : Operand) (builder : CodeBuilder) = if not <| call operand builder then failwith "Unknown instruction."

  /// Emits an 'jmp' instruction, or throws if it is unknown.
  [<CompiledName("EmitJmpOrThrow")>]
  let jmp' (operand : Operand) (builder : CodeBuilder) = if not <| jmp operand builder then failwith "Unknown instruction."

  /// Emits an 'jmpf' instruction, or throws if it is unknown.
  [<CompiledName("EmitJmpfOrThrow")>]
  let jmpf' (operand : Operand) (builder : CodeBuilder) = if not <| jmpf operand builder then failwith "Unknown instruction."

  /// Emits an 'test' instruction, or throws if it is unknown.
  [<CompiledName("EmitTestOrThrow")>]
  let test' (dst : Operand) (src : Operand) (builder : CodeBuilder) = if not <| test dst src builder then failwith "Unknown instruction."

  /// Emits an 'callf' instruction, or throws if it is unknown.
  [<CompiledName("EmitCallfOrThrow")>]
  let callf' (operand : Operand) (builder : CodeBuilder) = if not <| callf operand builder then failwith "Unknown instruction."

  /// Translates the given instruction (encoded by its mnemonic) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate0 mnemonic (builder : CodeBuilder) =
    match mnemonic with
    | "pushf" -> (builder.Emit 0x9cuy) ; true
    | "popf" -> (builder.Emit 0x9duy) ; true
    | "ret" -> (builder.Emit 0xc3uy) ; true
    | _ -> false

  /// Translates the given instruction (encoded by its mnemonic and operand) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate1<'s when 's :> OS and 's : (new : unit -> 's)> mnemonic (op1 : Operand<'s>) (builder : CodeBuilder) =
    match mnemonic, (op1 :> obj) with
    | "inc", (:? Register<S16> as op1) -> (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0x40uy + op1.Reg.Value % 8uy)) ; true
    | "inc", (:? Register<S32> as op1) -> (emitRegisterOpcode 0x40uy op1 builder) ; true
    | "inc", (:? RegisterOrMemory<S8> as op1) -> (builder.Emit 0xfeuy) ; emitMemory op1 0uy builder ; true
    | "inc", (:? RegisterOrMemory<S16> as op1) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 0uy builder ; true
    | "inc", (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 0uy builder ; true
    | "inc", (:? RegisterOrMemory<S64> as op1) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0xffuy) ; emitMemory op1 0uy builder ; true
    | "dec", (:? Register<S16> as op1) -> (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0x48uy + op1.Reg.Value % 8uy)) ; true
    | "dec", (:? Register<S32> as op1) -> (emitRegisterOpcode 0x48uy op1 builder) ; true
    | "dec", (:? RegisterOrMemory<S8> as op1) -> (builder.Emit 0xfeuy) ; emitMemory op1 1uy builder ; true
    | "dec", (:? RegisterOrMemory<S16> as op1) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 1uy builder ; true
    | "dec", (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 1uy builder ; true
    | "dec", (:? RegisterOrMemory<S64> as op1) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0xffuy) ; emitMemory op1 1uy builder ; true
    | "push", (:? Register<S16> as op1) -> (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0x50uy + op1.Reg.Value % 8uy)) ; true
    | "push", (:? Register<S32> as op1) -> (emitRegisterOpcode 0x50uy op1 builder) ; true
    | "push", (:? RegisterOrMemory<S16> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 6uy builder ; true
    | "push", (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 6uy builder ; true
    | "push", (:? RegisterOrMemory<S64> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 6uy builder ; true
    | "pop", (:? Register<S16> as op1) -> (emitRegisterOpcode 0x58uy op1 builder) ; true
    | "pop", (:? Register<S32> as op1) -> (emitRegisterOpcode 0x58uy op1 builder) ; true
    | "pop", (:? Register<S64> as op1) -> (emitRegisterOpcode 0x58uy op1 builder) ; true
    | "jo", (:? Immediate<S8> as op1) -> (builder.Emit 0x70uy) ; emitImmediate op1 builder ; true
    | "jno", (:? Immediate<S8> as op1) -> (builder.Emit 0x71uy) ; emitImmediate op1 builder ; true
    | "jc", (:? Immediate<S8> as op1) -> (builder.Emit 0x72uy) ; emitImmediate op1 builder ; true
    | "jnc", (:? Immediate<S8> as op1) -> (builder.Emit 0x73uy) ; emitImmediate op1 builder ; true
    | "jz", (:? Immediate<S8> as op1) -> (builder.Emit 0x74uy) ; emitImmediate op1 builder ; true
    | "jnz", (:? Immediate<S8> as op1) -> (builder.Emit 0x75uy) ; emitImmediate op1 builder ; true
    | "je", (:? Immediate<S8> as op1) -> (builder.Emit 0x74uy) ; emitImmediate op1 builder ; true
    | "jne", (:? Immediate<S8> as op1) -> (builder.Emit 0x75uy) ; emitImmediate op1 builder ; true
    | "jbe", (:? Immediate<S8> as op1) -> (builder.Emit 0x76uy) ; emitImmediate op1 builder ; true
    | "jnbe", (:? Immediate<S8> as op1) -> (builder.Emit 0x77uy) ; emitImmediate op1 builder ; true
    | "js", (:? Immediate<S8> as op1) -> (builder.Emit 0x78uy) ; emitImmediate op1 builder ; true
    | "jns", (:? Immediate<S8> as op1) -> (builder.Emit 0x79uy) ; emitImmediate op1 builder ; true
    | "jp", (:? Immediate<S8> as op1) -> (builder.Emit 0x7auy) ; emitImmediate op1 builder ; true
    | "jnp", (:? Immediate<S8> as op1) -> (builder.Emit 0x7buy) ; emitImmediate op1 builder ; true
    | "jl", (:? Immediate<S8> as op1) -> (builder.Emit 0x7cuy) ; emitImmediate op1 builder ; true
    | "jnl", (:? Immediate<S8> as op1) -> (builder.Emit 0x7duy) ; emitImmediate op1 builder ; true
    | "jle", (:? Immediate<S8> as op1) -> (builder.Emit 0x7euy) ; emitImmediate op1 builder ; true
    | "jnle", (:? Immediate<S8> as op1) -> (builder.Emit 0x7fuy) ; emitImmediate op1 builder ; true
    | "call", (:? Immediate<S16> as op1) -> (builder.Emit 0x66uy) ; (builder.Emit 0xe8uy) ; emitImmediate op1 builder ; true
    | "call", (:? Immediate<S32> as op1) -> (builder.Emit 0xe8uy) ; emitImmediate op1 builder ; true
    | "call", (:? RegisterOrMemory<S16> as op1) -> (() ; () ; emitPrefix op1 builder) ; (builder.Emit 0xffuy) ; emitMemory op1 2uy builder ; true
    | "call", (:? RegisterOrMemory<S32> as op1) -> (() ; () ; emitPrefix op1 builder) ; (builder.Emit 0xffuy) ; emitMemory op1 2uy builder ; true
    | "call", (:? RegisterOrMemory<S64> as op1) -> (() ; () ; emitPrefix op1 builder) ; (builder.Emit 0xffuy) ; emitMemory op1 2uy builder ; true
    | "jmp", (:? Immediate<S16> as op1) -> (builder.Emit 0x66uy) ; (builder.Emit 0xe9uy) ; emitImmediate op1 builder ; true
    | "jmp", (:? Immediate<S32> as op1) -> (builder.Emit 0xe9uy) ; emitImmediate op1 builder ; true
    | "jmp", (:? Immediate<S8> as op1) -> (builder.Emit 0xebuy) ; emitImmediate op1 builder ; true
    | "jmp", (:? RegisterOrMemory<S16> as op1) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 4uy builder ; true
    | "jmp", (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 4uy builder ; true
    | "jmpf", (:? Immediate<S16> as op1) -> (builder.Emit 0x66uy) ; (builder.Emit 0xeauy) ; emitImmediate op1 builder ; true
    | "jmpf", (:? Immediate<S32> as op1) -> (builder.Emit 0xeauy) ; emitImmediate op1 builder ; true
    | "jmpf", (:? RegisterOrMemory<S16> as op1) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xffuy) ; emitMemory op1 5uy builder ; true
    | "jmpf", (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 5uy builder ; true
    | "callf", (:? RegisterOrMemory<S16> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 3uy builder ; true
    | "callf", (:? RegisterOrMemory<S32> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 3uy builder ; true
    | "callf", (:? RegisterOrMemory<S64> as op1) -> (builder.Emit 0xffuy) ; emitMemory op1 3uy builder ; true
    | _ -> false

  /// Translates the given instruction (encoded by its mnemonic and two operands) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate2<'s when 's :> OS and 's : (new : unit -> 's)> mnemonic (op1 : Operand<'s>) (op2 : Operand<'s>) (builder : CodeBuilder) =
    match mnemonic, (op1 :> obj), (op2 :> obj) with
    | "add", (:? RegisterOrMemory<S8> as op1), (:? Register<S8> as op2) -> (builder.Emit 0x0uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S16> as op1), (:? Register<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x1uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S32> as op1), (:? Register<S32> as op2) -> (builder.Emit 0x1uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S64> as op1), (:? Register<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x1uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? Register<S8> as op1), (:? RegisterOrMemory<S8> as op2) -> (builder.Emit 0x2uy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "add", (:? Register<S16> as op1), (:? RegisterOrMemory<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x3uy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "add", (:? Register<S32> as op1), (:? RegisterOrMemory<S32> as op2) -> (builder.Emit 0x3uy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "add", (:? Register<S64> as op1), (:? RegisterOrMemory<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x3uy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x80uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0x81uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S16> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S32> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x83uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "add", (:? RegisterOrMemory<S64> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "sub", (:? RegisterOrMemory<S8> as op1), (:? Register<S8> as op2) -> (builder.Emit 0x28uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "sub", (:? RegisterOrMemory<S16> as op1), (:? Register<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x29uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "sub", (:? RegisterOrMemory<S32> as op1), (:? Register<S32> as op2) -> (builder.Emit 0x29uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "sub", (:? RegisterOrMemory<S64> as op1), (:? Register<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x29uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "sub", (:? Register<S8> as op1), (:? RegisterOrMemory<S8> as op2) -> (builder.Emit 0x2auy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "sub", (:? Register<S16> as op1), (:? RegisterOrMemory<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x2buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "sub", (:? Register<S32> as op1), (:? RegisterOrMemory<S32> as op2) -> (builder.Emit 0x2buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "sub", (:? Register<S64> as op1), (:? RegisterOrMemory<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x2buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "sub", (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x80uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | "sub", (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | "sub", (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0x81uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | "sub", (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | "sub", (:? RegisterOrMemory<S16> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | "sub", (:? RegisterOrMemory<S32> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x83uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | "sub", (:? RegisterOrMemory<S64> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 5uy builder) ; true
    | "cmp", (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x80uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | "cmp", (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | "cmp", (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0x81uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | "cmp", (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x81uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | "cmp", (:? RegisterOrMemory<S16> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | "cmp", (:? RegisterOrMemory<S32> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0x83uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | "cmp", (:? RegisterOrMemory<S64> as op1), (:? Immediate<S8> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0x83uy) ; (emitModrm' op2 op1 7uy builder) ; true
    | "mov", (:? RegisterOrMemory<S8> as op1), (:? Register<S8> as op2) -> (builder.Emit 0x88uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "mov", (:? RegisterOrMemory<S16> as op1), (:? Register<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x89uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "mov", (:? RegisterOrMemory<S32> as op1), (:? Register<S32> as op2) -> (builder.Emit 0x89uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "mov", (:? RegisterOrMemory<S64> as op1), (:? Register<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x89uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "mov", (:? Register<S8> as op1), (:? RegisterOrMemory<S8> as op2) -> (builder.Emit 0x8auy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "mov", (:? Register<S16> as op1), (:? RegisterOrMemory<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x8buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "mov", (:? Register<S32> as op1), (:? RegisterOrMemory<S32> as op2) -> (builder.Emit 0x8buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "mov", (:? Register<S64> as op1), (:? RegisterOrMemory<S64> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x8buy) ; (emitModrm' op1 op2 0uy builder) ; true
    | "mov", (:? Register<S8> as op1), (:? Immediate<S8> as op2) -> (emitRegisterOpcode 0xb0uy op1 builder) ; (emitImmediate op2 builder) ; true
    | "mov", (:? Register<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit (0x66uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0xb8uy + op1.Reg.Value % 8uy)) ; (emitImmediate op2 builder) ; true
    | "mov", (:? Register<S32> as op1), (:? Immediate<S32> as op2) -> (emitRegisterOpcode 0xb8uy op1 builder) ; (emitImmediate op2 builder) ; true
    | "mov", (:? Register<S64> as op1), (:? Immediate<S64> as op2) -> (() ; builder.Emit (0x48uy + op1.Reg.PrefixAdder)) ; (builder.Emit (0xb8uy + op1.Reg.Value % 8uy)) ; (emitImmediate op2 builder) ; true
    | "mov", (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0xc6uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "mov", (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0xc7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "mov", (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0xc7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "mov", (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0xc7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "lea", (:? Register<S16> as op1), (:? Memory<'s> as op2) -> (builder.Emit 0x66uy) ; (builder.Emit 0x8duy) ; (emitRegister op1 builder ; emitMemory op2 0uy builder) ; true
    | "lea", (:? Register<S32> as op1), (:? Memory<'s> as op2) -> (builder.Emit 0x8duy) ; (emitRegister op1 builder ; emitMemory op2 0uy builder) ; true
    | "lea", (:? Register<S64> as op1), (:? Memory<'s> as op2) -> (builder.Emit 0x48uy) ; (builder.Emit 0x8duy) ; (emitRegister op1 builder ; emitMemory op2 0uy builder) ; true
    | "test", (:? RegisterOrMemory<S8> as op1), (:? Immediate<S8> as op2) -> (builder.Emit 0xf6uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "test", (:? RegisterOrMemory<S16> as op1), (:? Immediate<S16> as op2) -> (() ; builder.Emit 0x66uy) ; (builder.Emit 0xf7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "test", (:? RegisterOrMemory<S32> as op1), (:? Immediate<S32> as op2) -> (builder.Emit 0xf7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | "test", (:? RegisterOrMemory<S64> as op1), (:? Immediate<S32> as op2) -> (() ; builder.Emit 0x48uy) ; (builder.Emit 0xf7uy) ; (emitModrm' op2 op1 0uy builder) ; true
    | _ -> false

  /// Translates the given instruction (encoded by its mnemonic and three operands) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate3<'s when 's :> OS and 's : (new : unit -> 's)> mnemonic (op1 : Operand<'s>) (op2 : Operand<'s>) (op3 : Operand<'s>) (builder : CodeBuilder) =
    match mnemonic, (op1 :> obj), (op2 :> obj), (op3 :> obj) with
    | _ -> false

  /// Translates the given instruction (encoded by its mnemonic and optional operands) into
  /// machine code.
  [<CompiledName("Translate")>]
  let translate mnemonic operands builder =
    match operands with
    | []          -> translate0 mnemonic builder
    | [ a ]       -> translate1 mnemonic a builder
    | [ a; b ]    -> translate2 mnemonic a b builder
    | [ a; b; c ] -> translate3 mnemonic a b c builder
    | _           -> false


