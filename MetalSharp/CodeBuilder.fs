namespace MetalSharp

open System
open System.Collections.Generic
open System.Runtime.InteropServices
open ExpandableAllocator
open System.IO

#nowarn "9"

/// Defines a label in a `CodeBuilder`.
type Label internal(addr: nativeint) =
    let mutable addr = addr

    static let mutable ids = 0
    let id = ids

    do ids <- id + 1 

    /// Gets the address of the label.
    member __.Address = addr

    member __.IsUnknown = addr = 0n

    /// Gets a function that points to this label.
    member __.AsFunction<'F when 'F :> Delegate>() = Marshal.GetDelegateForFunctionPointer<'F>(addr)

    member internal __.SetAddress(a) = addr <- a

    override __.GetHashCode() = id
    override __.Equals(other) = match other with :? Label as l -> l.GetHashCode() = id | _ -> false


/// Defines a structure that builds up executable X86 code.
[<Sealed>]
type CodeBuilder() =

    let mutable index = 0L
    let alloc = Allocator.Create(Protection.ReadWriteExecute, 1_000_000n)
    let addr = alloc.Address
    let pendingLabels = Dictionary<Label, int64 * Action<_>>()

    let ensureAdditional(size: int) =
        let currentSize = int64 alloc.ActualSize
        let neededSize = index + int64 size

        if currentSize <= neededSize then
            alloc.ActualSize <- nativeint <| max (currentSize * 2L) neededSize

    member __.Address = addr + nativeint index

    /// Emits the given byte to the executable machine code stream.
    member __.Emit(data: byte) =
        ensureAdditional 1
        NativeInterop.NativePtr.write (NativeInterop.NativePtr.ofNativeInt <| (nativeint index) + addr) data
        index <- index + 1L

    /// Emits the given buffer to the executable machine code stream.
    member __.Emit(data: byte[]) =
        let len = data.Length
        ensureAdditional len
        Marshal.Copy(data, 0, nativeint index + addr, len)
        index <- index + int64 len

    /// Copies the content of the builder in an byte array, and returns it.
    member __.GetByteArray() =
        let buf = Array.zeroCreate<byte> (int index)
        Marshal.Copy(addr, buf, 0, buf.Length)
        buf

    /// Returns a label that points to the current location.
    member __.CreateLabel() = Label <| addr + nativeint index
    /// Returns a label that points to the current position, plus the given offset.
    member __.CreateLabel(offset: int) = Label <| nativeint (int64 addr + int64 offset + index)

    /// Returns a label that points nowhere.
    member __.CreateNullLabel() = Label IntPtr.Zero

    member this.MarkLabel(label: Label) =
        label.SetAddress(this.Address)

        match pendingLabels.TryGetValue(label) with
        | true, (idx, cb) ->
            use __ = this.At(idx)

            cb.Invoke this
            pendingLabels.Remove(label) |> ignore
        | false, _ -> ()

    member __.AddPending(label: Label, f: CodeBuilder -> unit) =
        match pendingLabels.TryGetValue(label) with
        | true, (idx, cb) -> pendingLabels.[label] <- (idx, Delegate.Combine(cb, Action<_> f) :?> Action<_>)
        | false, _ -> pendingLabels.Add(label, (index, Action<_> f))

    member __.At(idx) =
        let prev = index
        
        index <- idx

        { new IDisposable with member __.Dispose() = index <- prev }

    member __.WriteShellcode(out: TextWriter) =
        for i in 0L..index do
            let ptr = NativeInterop.NativePtr.ofNativeInt<byte>(addr + nativeint i)

            fprintf out "\\x%02X" <| NativeInterop.NativePtr.read(ptr)

    member this.Bind(x: CodeBuilder -> 'T, b: 'T -> 'b) = b (x this)
    member __.Return(_: unit) = ()
    member __.ReturnFrom(label: Label) = label

    interface IDisposable with override __.Dispose() = alloc.Dispose()

/// Defines an instruction that, once emitted by a `CodeBuider`, returns a value.
type Instruction<'x> = delegate of CodeBuilder -> 'x

/// Defines an instruction that can be emitted by a `CodeBuider`.
type Instruction = Instruction<unit>

[<AutoOpen>]
module CodeBuilder =

    let inline label (builder: CodeBuilder) = builder.CreateLabel(0)
    let inline emitByte (byte: byte) (builder: CodeBuilder) = builder.Emit(byte)
    let inline emitBytes (bytes: byte[]) (builder: CodeBuilder) = builder.Emit(bytes)

    let inline (>.) f g (builder: CodeBuilder) = f builder ; g builder
