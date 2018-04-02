namespace MetalSharp.X86

/// Defines the size of an operand.
[<AbstractClass>]
type OperandSize internal(sz: uint16) =
    /// Returns the size of the operand in bits.
    member __.Size = sz

/// Defines a null operand.
[<Sealed>] type S0()   = inherit OperandSize(0us)
/// Defines a 1-bit operand.
[<Sealed>] type S1()   = inherit OperandSize(1us)
/// Defines an 8-bits operand.
[<Sealed>] type S8()   = inherit OperandSize(8us)
/// Defines a 16-bits operand.
[<Sealed>] type S16()  = inherit OperandSize(16us)
/// Defines a 32-bits operand.
[<Sealed>] type S32()  = inherit OperandSize(32us)
/// Defines a 64-bits operand.
[<Sealed>] type S64()  = inherit OperandSize(64us)
/// Defines a 128-bits operand.
[<Sealed>] type S128() = inherit OperandSize(128us)

type private OS = OperandSize

/// Defines an operand with a typed size.
type IOperand<'s when 's :> OS and 's : (new : unit -> 's)> =
    /// Gets the size of the operand.
    abstract Size : int

/// Defines an untyped operand.
[<AbstractClass>]
type Operand internal() =
    abstract IsImmediate : bool
    abstract IsRegister : bool
    abstract IsMemory : bool

/// Defines an operand.
[<AbstractClass>]
type Operand<'s when 's :> OS and 's : (new : unit -> 's)> internal() =
    inherit Operand()

    interface IOperand<'s> with
        member __.Size = int (new 's()).Size
