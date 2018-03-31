namespace MetalSharp.ARM

open System.Runtime.CompilerServices

/// Defines the condition for an ARM instruction to be executed.
/// See ARM Architecture Reference Manual, Table 3-1 for more informations.
type Condition =
    /// Equal.
    | EQ = 0b0000uy
    /// Not equal.
    | NE = 0b0001uy
    /// Carry set.
    | CS = 0b0010uy
    /// Unsigned higher or same.
    | HS = 0b0010uy
    /// Carry clear.
    | CC = 0b0011uy
    /// Unsigned lower.
    | LO = 0b0011uy
    /// Minus / negative.
    | MI = 0b0100uy
    /// Plus / positive or zero.
    | PL = 0b0101uy
    /// Overflow.
    | VS = 0b0110uy
    /// No overflow.
    | VC = 0b0111uy
    /// Unsigned higher.
    | HI = 0b1000uy
    /// Unsigned lower or same.
    | LS = 0b1001uy
    /// Signed greater than or equal.
    | GE = 0b1010uy
    /// Signed less than.
    | LT = 0b1011uy
    /// Signed greater than.
    | GT = 0b1100uy
    /// Signed less than or equal.
    | LE = 0b1101uy
    /// Always (unconditional).
    | AL = 0b1110uy
    /// Unpredictable (ARMv4 and lower) or unconditional (ARMv5 and higher).
    | UN = 0b1111uy


/// Defines an ARM register.
[<Struct>]
type Register internal(value: byte) =
    /// Gets the value of the register.
    member __.Value = value


#nowarn "25"

/// Defines all supported ARM registers.
module Registers =
    /// Returns a register having the given value.
    [<CompiledName("Register")>]
    let reg v =
        if v > 15uy then
            invalidArg "v" "Registers cannot have a size higher than 15."
        else
            Register v

    let [ r0 ; r1 ; r2 ; r3 ; r4 ; r5 ; r6 ; r7 ; r8 ; r9 ; r10 ; r11 ; r12 ; r13 ; r14 ; r15 ] = List.init 16 (byte >> reg)
    let [ a1 ; a2 ; a3 ; a4 ; v1 ; v2 ; v3 ; v4 ; v5 ; v6 ; v7 ; v8 ; ip ; sp ; lr ; pc ] = List.init 16 (byte >> reg)
    let wr, sb, sl, fp = reg 7uy, reg 9uy, reg 10uy, reg 11uy


/// Defines all supported ARM instructions.
[<AutoOpen>]
module Instructions =

    /// Reverses a 32-bits integer from an endianness to the other.
    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let private rev i = ((i &&& 0xf000) >>> 3) + ((i &&& 0x0f00) >>> 1) + ((i &&& 0x00f0) <<< 1) + ((i &&& 0x000f) <<< 3)
    
    /// Reverses a 32-bits unsigned integer from an endianness to the other.
    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let private rev' i = ((i &&& 0xf000u) >>> 3) + ((i &&& 0x0f00u) >>> 1) + ((i &&& 0x00f0u) <<< 1) + ((i &&& 0x000fu) <<< 3)

    /// Reverses a 4-bytes array.
    [<MethodImpl(MethodImplOptions.AggressiveInlining)>]
    let private rev'' (arr: byte[]) = assert(arr.Length = 4) ; [| arr.[3] ; arr.[2] ; arr.[1] ; arr.[0] |]

    /// Creates an instruction that executes if the given condition is true.
    let inline instr (cond: Condition) =
        (int cond <<< 28)

    /// Creates a data-processing instruction.
    let inline dpInsr (opcode: byte) cond = instr cond + (int opcode <<< 21)

    /// Creates a software-interrupt instruction.
    let inline interrupt cond swi =
        if swi < 0 || swi > 0x00ffffff then
            invalidArg "swi" "Software-interrupt number out of bounds."
        else
            instr cond + 0xf + swi
