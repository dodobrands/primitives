using System;
using System.Buffers.Binary;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Text.Json.Serialization;

namespace Dodo.Primitives;

/// <summary>
///     Represents a universally unique identifier (UUID).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[TypeConverter(typeof(UuidTypeConverter))]
[JsonConverter(typeof(SystemTextJsonUuidJsonConverter))]
[SuppressMessage("ReSharper", "RedundantNameQualifier")]
public unsafe partial struct Uuid :
    ISpanFormattable,
    IFormattable,
    IComparable,
    IComparable<Uuid>,
    IEquatable<Uuid>,
    ISpanParsable<Uuid>,
    IParsable<Uuid>,
    IUtf8SpanFormattable,
    IUtf8SpanParsable<Uuid>,
    IComparisonOperators<Uuid, Uuid, bool>
{
    private readonly byte _byte0;
    private readonly byte _byte1;
    private readonly byte _byte2;
    private readonly byte _byte3;
    private readonly byte _byte4;
    private readonly byte _byte5;
    private readonly byte _byte6;
    private readonly byte _byte7;
    private readonly byte _byte8;
    private readonly byte _byte9;
    private readonly byte _byte10;
    private readonly byte _byte11;
    private readonly byte _byte12;
    private readonly byte _byte13;
    private readonly byte _byte14;
    private readonly byte _byte15;

    /// <summary>
    ///     A read-only instance of the <see cref="Uuid" /> structure whose value is all zeros.
    /// </summary>
    // ReSharper disable once RedundantDefaultMemberInitializer
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnassignedReadonlyField
    public static readonly Uuid Empty;

    /// <summary>Gets a <see cref="Uuid" /> where all bits are set.</summary>
    /// <remarks>This returns the value: FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF</remarks>
    public static Uuid AllBitsSet => new Uuid(
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue);


    /// <summary>
    ///     Initializes a new instance of the <see cref="Uuid" /> structure by using the specified array of bytes.
    /// </summary>
    /// <param name="bytes">A 16-element byte array containing values with which to initialize the <see cref="Uuid" />.</param>
    /// <exception cref="ArgumentNullException"><paramref name="bytes" /> is <see langword="null" />.</exception>
    /// <exception cref="ArgumentException"><paramref name="bytes" /> is not 16 bytes long.</exception>
    public Uuid(byte[] bytes)
    {
        ArgumentNullException.ThrowIfNull(bytes);
        if (bytes.Length != 16)
        {
            throw new ArgumentException("Byte array for Uuid must be exactly 16 bytes long.", nameof(bytes));
        }

        this = Unsafe.ReadUnaligned<Uuid>(ref MemoryMarshal.GetReference(bytes.AsSpan()));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Uuid" /> structure by using the value represented by the specified read-only span of
    ///     bytes.
    /// </summary>
    /// <param name="bytes">A read-only span containing the bytes representing the <see cref="Uuid" />. The span must be exactly 16 bytes long.</param>
    /// <exception cref="ArgumentException"><paramref name="bytes" /> is not 16 bytes long.</exception>
    public Uuid(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 16)
        {
            throw new ArgumentException("Byte array for Uuid must be exactly 16 bytes long.", nameof(bytes));
        }

        this = Unsafe.ReadUnaligned<Uuid>(ref MemoryMarshal.GetReference(bytes));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Uuid" /> structure by using the specified bytes, numbering from zero.
    /// </summary>
    /// <param name="byte0">Byte 0.</param>
    /// <param name="byte1">Byte 1.</param>
    /// <param name="byte2">Byte 2.</param>
    /// <param name="byte3">Byte 3.</param>
    /// <param name="byte4">Byte 4.</param>
    /// <param name="byte5">Byte 5.</param>
    /// <param name="byte6">Byte 6.</param>
    /// <param name="byte7">Byte 7.</param>
    /// <param name="byte8">Byte 8.</param>
    /// <param name="byte9">Byte 9.</param>
    /// <param name="byte10">Byte 10.</param>
    /// <param name="byte11">Byte 11.</param>
    /// <param name="byte12">Byte 12.</param>
    /// <param name="byte13">Byte 13.</param>
    /// <param name="byte14">Byte 14.</param>
    /// <param name="byte15">Byte 15.</param>
    public Uuid(
        byte byte0,
        byte byte1,
        byte byte2,
        byte byte3,
        byte byte4,
        byte byte5,
        byte byte6,
        byte byte7,
        byte byte8,
        byte byte9,
        byte byte10,
        byte byte11,
        byte byte12,
        byte byte13,
        byte byte14,
        byte byte15)
    {
        _byte0 = byte0;
        _byte1 = byte1;
        _byte2 = byte2;
        _byte3 = byte3;
        _byte4 = byte4;
        _byte5 = byte5;
        _byte6 = byte6;
        _byte7 = byte7;
        _byte8 = byte8;
        _byte9 = byte9;
        _byte10 = byte10;
        _byte11 = byte11;
        _byte12 = byte12;
        _byte13 = byte13;
        _byte14 = byte14;
        _byte15 = byte15;
    }

    /// <summary>Gets the value of the variant field for the <see cref="Uuid" />.</summary>
    /// <remarks>
    ///     <para>This corresponds to the most significant 4 bits of the 8th byte: 00000000-0000-0000-F000-000000000000. The "don't-care" bits are not masked out.</para>
    ///     <para>See RFC 9562 for more information on how to interpret this value.</para>
    /// </remarks>
    public int Variant => _byte8 >> 4;

    /// <summary>Gets the value of the version field for the <see cref="Uuid" />.</summary>
    /// <remarks>
    ///     <para>This corresponds to the most significant 4 bits of the 6th byte: 00000000-0000-F000-0000-000000000000.</para>
    ///     <para>See RFC 9562 for more information on how to interpret this value.</para>
    /// </remarks>
    public int Version => _byte6 >> 4;

    /// <summary>Creates a new <see cref="Uuid" /> according to RFC 9562, following the Version 7 format.</summary>
    /// <returns>A new <see cref="Uuid" /> according to RFC 9562, following the Version 7 format.</returns>
    /// <remarks>
    ///     <para>This uses <see cref="DateTimeOffset.UtcNow" /> to determine the Unix Epoch timestamp source.</para>
    ///     <para>This seeds the unix_ts_ms, rand_a, and 2 bits of rand_b fields with the number of ticks of the Unix time epoch. The remaining part of the field rand_b is filled with random data.</para>
    /// </remarks>
    public static Uuid CreateVersion7()
    {
        return CreateVersion7(DateTimeOffset.UtcNow);
    }

    /// <summary>Creates a new <see cref="Uuid" /> according to RFC 9562, following the Version 7 format.</summary>
    /// <param name="timestamp">The date time offset used to determine the Unix Epoch timestamp.</param>
    /// <returns>A new <see cref="Uuid" /> according to RFC 9562, following the Version 7 format.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="timestamp" /> represents an offset prior to <see cref="DateTimeOffset.UnixEpoch" />.</exception>
    /// <remarks>
    ///     <para>This seeds the unix_ts_ms, rand_a, and 2 bits of rand_b fields with the number of ticks of the Unix time epoch. The remaining part of the field rand_b is filled with random data.</para>
    /// </remarks>
    public static Uuid CreateVersion7(DateTimeOffset timestamp)
    {
        const long unixEpochTicks = 621_355_968_000_000_000L;
        const byte variant10XxMask = 0b11000000;
        const byte variant10XxValue = 0b10000000;
        const ushort version7Mask = 0b11110000_00000000;
        const ushort version7Value = 0b01110000_00000000;
        const ushort ticksNotFitInRandAExtractMask = 0b00000000_00000011;
        const ushort ticksNotFitInRandASetMask = 0b00110000;
        if (timestamp.UtcTicks < unixEpochTicks)
        {
            throw new ArgumentOutOfRangeException(
                $"{nameof(timestamp)} must be greater than {DateTimeOffset.UnixEpoch}.");
        }

        Span<byte> result = stackalloc byte[16];
        var tempGuid = Guid.NewGuid();
        tempGuid.TryWriteBytes(result);
        var unixTsTicks = (ulong) (timestamp.UtcTicks - unixEpochTicks);
        ulong unixTsMs = unixTsTicks / TimeSpan.TicksPerMillisecond;
        BinaryPrimitives.WriteUInt64BigEndian(result, unixTsMs << 16);
        var remainTicks = (ushort) (unixTsTicks - (unixTsMs * TimeSpan.TicksPerMillisecond)); // up to 14 bits
        var randA = (ushort) (remainTicks >> 2);
        var randAVer = (ushort) ((randA & ~version7Mask) | version7Value);
        BinaryPrimitives.WriteUInt16BigEndian(result[6..], randAVer);
        result[8] = (byte) ((result[8] & ~variant10XxMask) | variant10XxValue);
        var ticksNotFitInRandA = (byte) ((byte) (remainTicks & ticksNotFitInRandAExtractMask) << 4);
        result[8] = (byte) ((result[8] & ~ticksNotFitInRandASetMask) | ticksNotFitInRandA);
        return new Uuid(result);
    }

    /// <summary>
    ///     Returns a 16-element byte array that contains the value of this instance.
    /// </summary>
    /// <returns>A 16-element byte array.</returns>
    public byte[] ToByteArray()
    {
        var result = new byte[16];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(new Span<byte>(result)), this);
        return result;
    }

    /// <summary>
    ///     Tries to write the current <see cref="Uuid" /> instance into a span of bytes.
    /// </summary>
    /// <param name="destination">When this method returns <see langword="true" />, the <see cref="Uuid" /> as a span of bytes.</param>
    /// <returns>
    ///     <see langword="true" /> if the <see cref="Uuid" /> is successfully written to the specified span; <see langword="false" />
    ///     otherwise.
    /// </returns>
    public bool TryWriteBytes(Span<byte> destination)
    {
        if (Unsafe.SizeOf<Uuid>() > (uint) destination.Length)
        {
            return false;
        }

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), this);
        return true;
    }

    /// <summary>
    ///     Compares this instance to a specified object or <see cref="Uuid" /> and returns an indication of their relative values.
    /// </summary>
    /// <param name="obj">An object to compare, or <see langword="null" />.</param>
    /// <returns>A signed number indicating the relative values of this instance and <paramref name="obj" />.</returns>
    /// <exception cref="ArgumentException"><paramref name="obj" /> must be of type <see cref="Uuid" />.</exception>
    [SuppressMessage("ReSharper", "MergeCastWithTypeCheck")]
    [SuppressMessage("ReSharper", "UseNegatedPatternInIsExpression")]
    [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Uuid other)
        {
            throw new ArgumentException("Object must be of type Uuid.", nameof(obj));
        }

        return CompareTo(other);
    }

    /// <summary>
    ///     Compares this instance to a specified <see cref="Uuid" /> object and returns an indication of their relative values.
    /// </summary>
    /// <param name="other">An <see cref="Uuid" /> object to compare to this instance.</param>
    /// <returns>A signed number indicating the relative values of this instance and <paramref name="other" />.</returns>
    [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
    public int CompareTo(Uuid other)
    {
        if (other._byte0 != _byte0)
        {
            return _byte0 < other._byte0 ? -1 : 1;
        }

        if (other._byte1 != _byte1)
        {
            return _byte1 < other._byte1 ? -1 : 1;
        }

        if (other._byte2 != _byte2)
        {
            return _byte2 < other._byte2 ? -1 : 1;
        }

        if (other._byte3 != _byte3)
        {
            return _byte3 < other._byte3 ? -1 : 1;
        }

        if (other._byte4 != _byte4)
        {
            return _byte4 < other._byte4 ? -1 : 1;
        }

        if (other._byte5 != _byte5)
        {
            return _byte5 < other._byte5 ? -1 : 1;
        }

        if (other._byte6 != _byte6)
        {
            return _byte6 < other._byte6 ? -1 : 1;
        }

        if (other._byte7 != _byte7)
        {
            return _byte7 < other._byte7 ? -1 : 1;
        }

        if (other._byte8 != _byte8)
        {
            return _byte8 < other._byte8 ? -1 : 1;
        }

        if (other._byte9 != _byte9)
        {
            return _byte9 < other._byte9 ? -1 : 1;
        }

        if (other._byte10 != _byte10)
        {
            return _byte10 < other._byte10 ? -1 : 1;
        }

        if (other._byte11 != _byte11)
        {
            return _byte11 < other._byte11 ? -1 : 1;
        }

        if (other._byte12 != _byte12)
        {
            return _byte12 < other._byte12 ? -1 : 1;
        }

        if (other._byte13 != _byte13)
        {
            return _byte13 < other._byte13 ? -1 : 1;
        }

        if (other._byte14 != _byte14)
        {
            return _byte14 < other._byte14 ? -1 : 1;
        }

        if (other._byte15 != _byte15)
        {
            return _byte15 < other._byte15 ? -1 : 1;
        }

        return 0;
    }

    /// <summary>
    ///     Returns a value that indicates whether two instances of <see cref="Uuid" /> represent the same value.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="obj" /> is <see cref="Uuid" /> that has the same value as this instance; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    [SuppressMessage("ReSharper", "MergeSequentialChecks")]
    [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
    // Do not change that code syntax (do not merge checks, do not remove else) - perf critical
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Uuid other)
        {
            if (Vector128.IsHardwareAccelerated)
            {
                return Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref this)) == Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref other));
            }

            ref long rA = ref Unsafe.As<Uuid, long>(ref this);
            ref long rB = ref Unsafe.As<Uuid, long>(ref other);
            // Compare each element
            return rA == rB && Unsafe.Add(ref rA, 1) == Unsafe.Add(ref rB, 1);
        }

        return false;
    }

    /// <summary>
    ///     Returns a value indicating whether this instance and a specified <see cref="Uuid" /> object represent the same value.
    /// </summary>
    /// <param name="other">An object to compare to this instance.</param>
    /// <returns><see langword="true" /> if <paramref name="other" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
    public bool Equals(Uuid other)
    {
        if (Vector128.IsHardwareAccelerated)
        {
            return Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref this)) == Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref other));
        }

        ref long rA = ref Unsafe.As<Uuid, long>(ref this);
        ref long rB = ref Unsafe.As<Uuid, long>(ref other);
        // Compare each element
        return rA == rB && Unsafe.Add(ref rA, 1) == Unsafe.Add(ref rB, 1);
    }

    /// <summary>
    ///     Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code for this instance.</returns>
    public override int GetHashCode()
    {
        ref int r = ref Unsafe.As<Uuid, int>(ref this);
        return r ^ Unsafe.Add(ref r, 1) ^ Unsafe.Add(ref r, 2) ^ Unsafe.Add(ref r, 3);
    }

    /// <summary>
    ///     Indicates whether the values of two specified <see cref="Uuid" /> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
    public static bool operator ==(Uuid left, Uuid right)
    {
        if (Vector128.IsHardwareAccelerated)
        {
            return Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref left)) == Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref right));
        }

        ref long rA = ref Unsafe.As<Uuid, long>(ref left);
        ref long rB = ref Unsafe.As<Uuid, long>(ref right);
        // Compare each element
        return rA == rB && Unsafe.Add(ref rA, 1) == Unsafe.Add(ref rB, 1);
    }

    /// <summary>
    ///     Indicates whether the values of two specified <see cref="Uuid" /> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public static bool operator !=(Uuid left, Uuid right)
    {
        if (Vector128.IsHardwareAccelerated)
        {
            return Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref left)) != Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref right));
        }

        ref long rA = ref Unsafe.As<Uuid, long>(ref left);
        ref long rB = ref Unsafe.As<Uuid, long>(ref right);
        // Compare each element
        return rA != rB || Unsafe.Add(ref rA, 1) != Unsafe.Add(ref rB, 1);
    }

    /// <summary>
    ///     Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    /// <param name="destination">When this method returns <see langword="true" />, the <see cref="Uuid" /> as a span of characters.</param>
    /// <param name="charsWritten">
    ///     When this method returns <see langword="true" />, the number of characters written in
    ///     <paramref name="destination" />.
    /// </param>
    /// <param name="format">
    ///     A read-only span containing the character representing one of the following specifiers that indicates how to format
    ///     the value of this <see cref="Uuid" />. The format parameter can be "N", "D", "B", "P", or "X". If format is <see langword="null" /> or
    ///     an empty string (""), "N" is used.
    /// </param>
    /// <param name="provider">An optional object that supplies culture-specific formatting information for <paramref name="destination" />.</param>
    /// <returns><see langword="true" /> if the formatting operation was successful; <see langword="false" /> otherwise.</returns>
    public bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
        ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        if (format.Length == 0)
        {
            format = "N";
        }

        if (format.Length != 1)
        {
            charsWritten = 0;
            return false;
        }

        switch ((char) (format[0] | 0x20))
        {
            case 'n':
                {
                    if (destination.Length < 32)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatN(uuidChars);
                    }

                    charsWritten = 32;
                    return true;
                }
            case 'd':
                {
                    if (destination.Length < 36)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatD(uuidChars);
                    }

                    charsWritten = 36;
                    return true;
                }
            case 'b':
                {
                    if (destination.Length < 38)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatB(uuidChars);
                    }

                    charsWritten = 38;
                    return true;
                }
            case 'p':
                {
                    if (destination.Length < 38)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatP(uuidChars);
                    }

                    charsWritten = 38;
                    return true;
                }
            case 'x':
                {
                    if (destination.Length < 68)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatX(uuidChars);
                    }

                    charsWritten = 68;
                    return true;
                }
            default:
                {
                    charsWritten = 0;
                    return false;
                }
        }
    }

    /// <summary>
    ///     Tries to format the value of the current instance into the provided span of characters.
    /// </summary>
    /// <param name="destination">When this method returns <see langword="true" />, the <see cref="Uuid" /> as a span of characters.</param>
    /// <param name="charsWritten">
    ///     When this method returns <see langword="true" />, the number of characters written in
    ///     <paramref name="destination" />.
    /// </param>
    /// <param name="format">
    ///     A read-only span containing the character representing one of the following specifiers that indicates how to format
    ///     the value of this <see cref="Uuid" />. The format parameter can be "N", "D", "B", "P", or "X". If format is <see langword="null" /> or
    ///     an empty string (""), "N" is used.
    /// </param>
    /// <returns><see langword="true" /> if the formatting operation was successful; <see langword="false" /> otherwise.</returns>
    public bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
        ReadOnlySpan<char> format = default)
    {
        if (format.Length == 0)
        {
            format = "N";
        }

        if (format.Length != 1)
        {
            charsWritten = 0;
            return false;
        }

        switch ((char) (format[0] | 0x20))
        {
            case 'n':
                {
                    if (destination.Length < 32)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatN(uuidChars);
                    }

                    charsWritten = 32;
                    return true;
                }
            case 'd':
                {
                    if (destination.Length < 36)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatD(uuidChars);
                    }

                    charsWritten = 36;
                    return true;
                }
            case 'b':
                {
                    if (destination.Length < 38)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatB(uuidChars);
                    }

                    charsWritten = 38;
                    return true;
                }
            case 'p':
                {
                    if (destination.Length < 38)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatP(uuidChars);
                    }

                    charsWritten = 38;
                    return true;
                }
            case 'x':
                {
                    if (destination.Length < 68)
                    {
                        charsWritten = 0;
                        return false;
                    }

                    fixed (char* uuidChars = &destination.GetPinnableReference())
                    {
                        CharFormatX(uuidChars);
                    }

                    charsWritten = 68;
                    return true;
                }
            default:
                {
                    charsWritten = 0;
                    return false;
                }
        }
    }

    //
    // IUtf8SpanFormattable
    //
    /// <inheritdoc cref="System.IUtf8SpanFormattable.TryFormat" />
    public bool TryFormat(
        Span<byte> utf8Destination,
        out int bytesWritten,
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
        ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        if (format.Length == 0)
        {
            format = "N";
        }

        if (format.Length != 1)
        {
            bytesWritten = 0;
            return false;
        }

        switch ((char) (format[0] | 0x20))
        {
            case 'n':
                {
                    if (utf8Destination.Length < 32)
                    {
                        bytesWritten = 0;
                        return false;
                    }

                    fixed (byte* uuidChars = &utf8Destination.GetPinnableReference())
                    {
                        Utf8FormatN(uuidChars);
                    }

                    bytesWritten = 32;
                    return true;
                }
            case 'd':
                {
                    if (utf8Destination.Length < 36)
                    {
                        bytesWritten = 0;
                        return false;
                    }

                    fixed (byte* uuidChars = &utf8Destination.GetPinnableReference())
                    {
                        Utf8FormatD(uuidChars);
                    }

                    bytesWritten = 36;
                    return true;
                }
            case 'b':
                {
                    if (utf8Destination.Length < 38)
                    {
                        bytesWritten = 0;
                        return false;
                    }

                    fixed (byte* uuidChars = &utf8Destination.GetPinnableReference())
                    {
                        Utf8FormatB(uuidChars);
                    }

                    bytesWritten = 38;
                    return true;
                }
            case 'p':
                {
                    if (utf8Destination.Length < 38)
                    {
                        bytesWritten = 0;
                        return false;
                    }

                    fixed (byte* uuidChars = &utf8Destination.GetPinnableReference())
                    {
                        Utf8FormatP(uuidChars);
                    }

                    bytesWritten = 38;
                    return true;
                }
            case 'x':
                {
                    if (utf8Destination.Length < 68)
                    {
                        bytesWritten = 0;
                        return false;
                    }

                    fixed (byte* uuidChars = &utf8Destination.GetPinnableReference())
                    {
                        Utf8FormatX(uuidChars);
                    }

                    bytesWritten = 68;
                    return true;
                }
            default:
                {
                    bytesWritten = 0;
                    return false;
                }
        }
    }

    /// <summary>
    ///     Returns a string representation of the value of this instance.
    /// </summary>
    /// <returns>The value of this <see cref="Uuid" />, formatted by using the "N" format specifier as follows: xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx</returns>
    public override string ToString()
    {
        return ToString("N", null);
    }

    /// <summary>
    ///     Returns a string representation of the value of this <see cref="Uuid" /> instance, according to the provided format specifier.
    /// </summary>
    /// <param name="format">
    ///     A single format specifier that indicates how to format the value of this <see cref="Uuid" />. The format parameter can
    ///     be "N", "D", "B", "P", or "X". If format is <see langword="null" /> or an empty string (""), "N" is used.
    /// </param>
    /// <returns>The value of this <see cref="Uuid" />, represented as a series of lowercase hexadecimal digits in the specified format.</returns>
    public string ToString(
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
        string? format)
    {
        // ReSharper disable once IntroduceOptionalParameters.Global
        return ToString(format, null);
    }

    /// <summary>
    ///     Returns a string representation of the value of this <see cref="Uuid" /> instance, according to the provided format specifier and
    ///     culture-specific format information.
    /// </summary>
    /// <param name="format">
    ///     A single format specifier that indicates how to format the value of this <see cref="Uuid" />. The format parameter can
    ///     be "N", "D", "B", "P", or "X". If format is null or an empty string (""), "N" is used.
    /// </param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
    /// <returns>The value of this <see cref="Uuid" />, represented as a series of lowercase hexadecimal digits in the specified format.</returns>
    /// <exception cref="FormatException">
    ///     The value of <paramref name="format" /> is not <see langword="null" />, an empty string (""), "N", "D",
    ///     "B", "P", or "X".
    /// </exception>
    public string ToString(
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
        string? format,
        IFormatProvider? formatProvider)
    {
        format ??= "N";

        if (string.IsNullOrEmpty(format))
        {
            format = "N";
        }

        if (format.Length != 1)
        {
            throw new FormatException(
                "Format string can be only \"N\", \"n\", \"D\", \"d\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\".");
        }

        switch ((char) (format[0] | 0x20))
        {
            case 'n':
                {
                    var uuidString = new string('\0', 32);
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
                    {
                        CharFormatN(uuidChars);
                    }

                    return uuidString;
                }
            case 'd':
                {
                    var uuidString = new string('\0', 36);
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
                    {
                        CharFormatD(uuidChars);
                    }

                    return uuidString;
                }
            case 'b':
                {
                    var uuidString = new string('\0', 38);
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
                    {
                        CharFormatB(uuidChars);
                    }

                    return uuidString;
                }
            case 'p':
                {
                    var uuidString = new string('\0', 38);
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
                    {
                        CharFormatP(uuidChars);
                    }

                    return uuidString;
                }
            case 'x':
                {
                    var uuidString = new string('\0', 68);
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
                    {
                        CharFormatX(uuidChars);
                    }

                    return uuidString;
                }
            default:
                throw new FormatException(
                    "Format string can be only \"N\", \"n\", \"D\", \"d\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\".");
        }
    }

    /// <summary>
    ///     Converts <see cref="Dodo.Primitives.Uuid" /> to <see cref="System.Guid" /> in little endian format.
    /// </summary>
    /// <returns><see cref="System.Guid" /> in little endian format.</returns>
    public Guid ToGuidLittleEndian()
    {
        var result = Guid.Empty;
        Guid* resultPtr = &result;
        var resultPtrBytes = (byte*) resultPtr;
        resultPtrBytes[0] = _byte0;
        resultPtrBytes[1] = _byte1;
        resultPtrBytes[2] = _byte2;
        resultPtrBytes[3] = _byte3;
        resultPtrBytes[4] = _byte4;
        resultPtrBytes[5] = _byte5;
        resultPtrBytes[6] = _byte6;
        resultPtrBytes[7] = _byte7;
        resultPtrBytes[8] = _byte8;
        resultPtrBytes[9] = _byte9;
        resultPtrBytes[10] = _byte10;
        resultPtrBytes[11] = _byte11;
        resultPtrBytes[12] = _byte12;
        resultPtrBytes[13] = _byte13;
        resultPtrBytes[14] = _byte14;
        resultPtrBytes[15] = _byte15;
        return result;
    }

    /// <summary>
    ///     Converts <see cref="Dodo.Primitives.Uuid" /> to <see cref="System.Guid" /> in big endian format.
    /// </summary>
    /// <returns><see cref="System.Guid" /> in big endian format.</returns>
    public Guid ToGuidBigEndian()
    {
        var result = Guid.Empty;
        Guid* resultPtr = &result;
        var resultPtrBytes = (byte*) resultPtr;
        resultPtrBytes[0] = _byte3;
        resultPtrBytes[1] = _byte2;
        resultPtrBytes[2] = _byte1;
        resultPtrBytes[3] = _byte0;
        resultPtrBytes[4] = _byte5;
        resultPtrBytes[5] = _byte4;
        resultPtrBytes[6] = _byte7;
        resultPtrBytes[7] = _byte6;
        resultPtrBytes[8] = _byte8;
        resultPtrBytes[9] = _byte9;
        resultPtrBytes[10] = _byte10;
        resultPtrBytes[11] = _byte11;
        resultPtrBytes[12] = _byte12;
        resultPtrBytes[13] = _byte13;
        resultPtrBytes[14] = _byte14;
        resultPtrBytes[15] = _byte15;
        return result;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Uuid" /> structure by using the value represented by the specified string.
    /// </summary>
    /// <param name="input">A string that contains a UUID.</param>
    /// <exception cref="ArgumentNullException"><paramref name="input" /> is <see langword="null" />.</exception>
    /// <exception cref="FormatException"><paramref name="input" /> is not in the correct format.</exception>
    public Uuid(string input)
    {
        ArgumentNullException.ThrowIfNull(input);
        var result = new Uuid();
        var resultPtr = (byte*) &result;
        CharParseWithExceptions(input, resultPtr);
        this = result;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Uuid" /> structure by using the value represented by the specified read-only span of
    ///     characters.
    /// </summary>
    /// <param name="input">A read-only span of characters that contains a UUID.</param>
    /// <exception cref="FormatException"><paramref name="input" /> is empty or contains unrecognized <see cref="Uuid" /> format.</exception>
    public Uuid(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            throw new FormatException("Unrecognized Uuid format.");
        }

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        CharParseWithExceptions(input, resultPtr);
        this = result;
    }

    /// <summary>
    ///     Converts the string representation of a UUID to the equivalent <see cref="Uuid" /> structure.
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>A structure that contains the value that was parsed.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="input" /> is <see langword="null" />.</exception>
    /// <exception cref="FormatException"><paramref name="input" /> is not in the correct format.</exception>
    public static Uuid Parse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);
        var result = new Uuid();
        var resultPtr = (byte*) &result;
        CharParseWithExceptions(input, resultPtr);
        return result;
    }

    /// <summary>
    ///     Converts a read-only character span that represents a UUID to the equivalent <see cref="Uuid" /> structure.
    /// </summary>
    /// <param name="input">A read-only span containing the bytes representing a <see cref="Uuid" />.</param>
    /// <returns>A structure that contains the value that was parsed.</returns>
    /// <exception cref="FormatException"><paramref name="input" /> is not in a recognized format.</exception>
    public static Uuid Parse(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            throw new FormatException("Unrecognized Uuid format.");
        }

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        CharParseWithExceptions(input, resultPtr);
        return result;
    }

    /// <summary>
    ///     Converts a read-only span of UTF-8 bytes that represents a UUID to the equivalent <see cref="Uuid" /> structure.
    /// </summary>
    /// <param name="utf8Text">The span of UTF-8 characters to parse.</param>
    /// <returns>A structure that contains the value that was parsed.</returns>
    /// <exception cref="FormatException"><paramref name="utf8Text" /> is not in a recognized format.</exception>
    public static Uuid Parse(ReadOnlySpan<byte> utf8Text)
    {
        if (utf8Text.IsEmpty)
        {
            throw new FormatException("Unrecognized Uuid format.");
        }

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        Utf8ParseWithExceptions(utf8Text, resultPtr);
        return result;
    }

    /// <summary>
    ///     Converts the string representation of a <see cref="Uuid" /> to the equivalent <see cref="Uuid" /> structure, provided that the string
    ///     is in the specified format.
    /// </summary>
    /// <param name="input">The <see cref="Uuid" /> to convert.</param>
    /// <param name="format">
    ///     One of the following specifiers that indicates the exact format to use when interpreting <paramref name="input" />:
    ///     "N", "D", "B", "P", or "X".
    /// </param>
    /// <returns>A structure that contains the value that was parsed.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="input" /> or <paramref name="format" /> is <see langword="null" />.</exception>
    /// <exception cref="FormatException"><paramref name="input" /> is not in the format specified by <paramref name="format" />.</exception>
    public static Uuid ParseExact(
        string input,
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
        string format)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(format);

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        switch ((char) (format[0] | 0x20))
        {
            case 'n':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'd':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'b':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'p':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'x':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsX((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            default:
                {
                    throw new FormatException(
                        "Format string can be only \"N\", \"n\", \"D\", \"d\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\".");
                }
        }
    }

    /// <summary>
    ///     Converts the character span representation of a <see cref="Uuid" /> to the equivalent <see cref="Uuid" /> structure, provided that the
    ///     string is in the specified format.
    /// </summary>
    /// <param name="input">A read-only span containing the characters representing the <see cref="Uuid" /> to convert.</param>
    /// <param name="format">
    ///     A read-only span of characters representing one of the following specifiers that indicates the exact format to use
    ///     when interpreting <paramref name="input" />: "N", "D", "B", "P", or "X".
    /// </param>
    /// <returns>A structure that contains the value that was parsed.</returns>
    /// <exception cref="FormatException"><paramref name="input" /> is not in the format specified by <paramref name="format" />.</exception>
    public static Uuid ParseExact(
        ReadOnlySpan<char> input,
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
        ReadOnlySpan<char> format)
    {
        if (input.IsEmpty)
        {
            throw new FormatException("Unrecognized Uuid format.");
        }

        if (format.Length != 1)
        {
            throw new FormatException(
                "Format string can be only \"N\", \"n\", \"D\", \"d\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\".");
        }

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        switch ((char) (format[0] | 0x20))
        {
            case 'n':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'd':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'b':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'p':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'x':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        CharParseWithExceptionsX((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            default:
                {
                    throw new FormatException(
                        "Format string can be only \"N\", \"n\", \"D\", \"d\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\".");
                }
        }
    }

    /// <summary>
    ///     Converts the string representation of a UUID to the equivalent <see cref="Uuid" /> structure.
    /// </summary>
    /// <param name="input">A string containing the UUID to convert.</param>
    /// <param name="output">
    ///     A <see cref="Uuid" /> instance to contain the parsed value. If the method returns <see langword="true" />,
    ///     <paramref name="output" /> contains a valid <see cref="Uuid" />. If the method returns <see langword="false" />,
    ///     <paramref name="output" /> equals <see cref="Empty" />.
    /// </param>
    /// <returns><see langword="true" /> if the parse operation was successful; otherwise, <see langword="false" />.</returns>
    public static bool TryParse([NotNullWhen(true)] string? input, out Uuid output)
    {
        if (input == null)
        {
            output = default;
            return false;
        }

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        if (CharParseWithoutExceptions(input, resultPtr))
        {
            output = result;
            return true;
        }

        output = default;
        return false;
    }

    /// <summary>
    ///     Converts the specified read-only span of characters containing the representation of a UUID to the equivalent <see cref="Uuid" />
    ///     structure.
    /// </summary>
    /// <param name="input">A span containing the characters representing the UUID to convert.</param>
    /// <param name="output">
    ///     A <see cref="Uuid" /> instance to contain the parsed value. If the method returns <see langword="true" />,
    ///     <paramref name="output" /> contains a valid <see cref="Uuid" />. If the method returns <see langword="false" />,
    ///     <paramref name="output" /> equals <see cref="Empty" />.
    /// </param>
    /// <returns><see langword="true" /> if the parse operation was successful; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(ReadOnlySpan<char> input, out Uuid output)
    {
        if (input.IsEmpty)
        {
            output = default;
            return false;
        }

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        if (CharParseWithoutExceptions(input, resultPtr))
        {
            output = result;
            return true;
        }

        output = default;
        return false;
    }

    /// <summary>
    ///     Converts the specified read-only span bytes of UTF-8 characters containing the representation of a UUID to the equivalent
    ///     <see cref="Uuid" /> structure.
    /// </summary>
    /// <param name="uuidUtf8String">A span containing the bytes of UTF-8 characters representing the UUID to convert.</param>
    /// <param name="output">
    ///     A <see cref="Uuid" /> instance to contain the parsed value. If the method returns <see langword="true" />,
    ///     <paramref name="output" /> contains a valid <see cref="Uuid" />. If the method returns <see langword="false" />,
    ///     <paramref name="output" /> equals <see cref="Empty" />.
    /// </param>
    /// <returns><see langword="true" /> if the parse operation was successful; otherwise, <see langword="false" />.</returns>
    public static bool TryParse(ReadOnlySpan<byte> uuidUtf8String, out Uuid output)
    {
        if (uuidUtf8String.IsEmpty)
        {
            output = default;
            return false;
        }

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        if (Utf8ParseWithoutExceptions(uuidUtf8String, resultPtr))
        {
            output = result;
            return true;
        }

        output = default;
        return false;
    }

    /// <summary>
    ///     Converts the string representation of a UUID to the equivalent <see cref="Uuid" /> structure, provided that the string is in the
    ///     specified format.
    /// </summary>
    /// <param name="input">The UUID to convert.</param>
    /// <param name="format">
    ///     One of the following specifiers that indicates the exact format to use when interpreting <paramref name="input" />:
    ///     "N", "D", "B", "P", or "X".
    /// </param>
    /// <param name="output">
    ///     A <see cref="Uuid" /> instance to contain the parsed value. If the method returns <see langword="true" />,
    ///     <paramref name="output" /> contains a valid <see cref="Uuid" />. If the method returns <see langword="false" />,
    ///     <paramref name="output" /> equals <see cref="Empty" />.
    /// </param>
    /// <returns><see langword="true" /> if the parse operation was successful; otherwise, <see langword="false" />.</returns>
    public static bool TryParseExact(
        [NotNullWhen(true)] string? input,
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
        string format,
        out Uuid output)
    {
        if (input == null || string.IsNullOrEmpty(format) || format.Length != 1)
        {
            output = default;
            return false;
        }

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        var parsed = false;
        switch ((char) (format[0] | 0x20))
        {
            case 'd':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'n':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'b':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'p':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'x':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsX((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
        }

        if (parsed)
        {
            output = result;
            return true;
        }

        output = default;
        return false;
    }

    /// <summary>
    ///     Converts span of characters representing the UUID to the equivalent <see cref="Uuid" /> structure, provided that the string is in the
    ///     specified format.
    /// </summary>
    /// <param name="input">A read-only span containing the characters representing the UUID to convert.</param>
    /// <param name="format">
    ///     A read-only span containing a character representing one of the following specifiers that indicates the exact format
    ///     to use when interpreting <paramref name="input" />: "N", "D", "B", "P", or "X".
    /// </param>
    /// <param name="output">
    ///     A <see cref="Uuid" /> instance to contain the parsed value. If the method returns <see langword="true" />,
    ///     <paramref name="output" /> contains a valid <see cref="Uuid" />. If the method returns <see langword="false" />,
    ///     <paramref name="output" /> equals <see cref="Empty" />.
    /// </param>
    /// <returns><see langword="true" /> if the parse operation was successful; otherwise, <see langword="false" />.</returns>
    public static bool TryParseExact(
        ReadOnlySpan<char> input,
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
        ReadOnlySpan<char> format,
        out Uuid output)
    {
        if (format.Length != 1)
        {
            output = default;
            return false;
        }

        var result = new Uuid();
        var resultPtr = (byte*) &result;
        var parsed = false;
        switch ((char) (format[0] | 0x20))
        {
            case 'd':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'n':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'b':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'p':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'x':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = CharParseWithoutExceptionsX((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
        }

        if (parsed)
        {
            output = result;
            return true;
        }

        output = default;
        return false;
    }

    //
    // IComparisonOperators
    //
    /// <inheritdoc cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_LessThan(TSelf, TOther)" />
    public static bool operator <(Uuid left, Uuid right)
    {
        int compareResult = left.CompareTo(right);
        return compareResult is -1;
    }

    /// <inheritdoc cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_LessThanOrEqual(TSelf, TOther)" />
    public static bool operator <=(Uuid left, Uuid right)
    {
        int compareResult = left.CompareTo(right);
        return compareResult is 0 or -1;
    }

    /// <inheritdoc cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThan(TSelf, TOther)" />
    public static bool operator >(Uuid left, Uuid right)
    {
        int compareResult = left.CompareTo(right);
        return compareResult is 1;
    }

    /// <inheritdoc cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThanOrEqual(TSelf, TOther)" />
    public static bool operator >=(Uuid left, Uuid right)
    {
        int compareResult = left.CompareTo(right);
        return compareResult is 0 or 1;
    }

    //
    // IParsable
    //
    /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)" />
    public static Uuid Parse(string s, IFormatProvider? provider)
    {
        return Parse(s);
    }

    /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)" />
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out Uuid result)
    {
        return TryParse(s, out result);
    }

    //
    // ISpanParsable
    //
    /// <inheritdoc cref="ISpanParsable{TSelf}.Parse(ReadOnlySpan{char}, IFormatProvider?)" />
    public static Uuid Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return Parse(s);
    }

    /// <inheritdoc cref="ISpanParsable{TSelf}.TryParse(ReadOnlySpan{char}, IFormatProvider?, out TSelf)" />
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Uuid result)
    {
        return TryParse(s, out result);
    }

    //
    // IUtf8SpanParsable
    //
    /// <inheritdoc cref="IUtf8SpanParsable{TSelf}.Parse(ReadOnlySpan{byte}, IFormatProvider?)" />
    public static Uuid Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider)
    {
        return Parse(utf8Text);
    }

    /// <inheritdoc cref="IUtf8SpanParsable{TSelf}.TryParse(ReadOnlySpan{byte}, IFormatProvider?, out TSelf)" />
    public static bool TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out Uuid result)
    {
        return TryParse(utf8Text, out result);
    }

    #region Generator

    private const long ChristianCalendarGregorianReformTicksDate = 499_163_040_000_000_000L;

    private const byte ResetVersionMask = 0b0000_1111;
    private const byte Version1Flag = 0b0001_0000;

    private const byte ResetReservedMask = 0b0011_1111;
    private const byte ReservedFlag = 0b1000_0000;

    /// <summary>
    ///     <para>
    ///         <b>Obsolete. Use <see cref="CreateVersion7()" /> instead.</b>
    ///     </para>
    ///     <para>Initializes a new instance of the <see cref="Uuid" /> structure that represents Uuid v1 (RFC4122).</para>
    /// </summary>
    /// <returns></returns>
    [Obsolete("Use CreateVersion7() instead.")]
    public static Uuid NewTimeBased()
    {
        byte* resultPtr = stackalloc byte[16];
        var resultAsGuidPtr = (Guid*) resultPtr;
        var guid = Guid.NewGuid();
        resultAsGuidPtr[0] = guid;
        long currentTicks = DateTime.UtcNow.Ticks - ChristianCalendarGregorianReformTicksDate;
        var ticksPtr = (byte*) &currentTicks;
        resultPtr[0] = ticksPtr[3];
        resultPtr[1] = ticksPtr[2];
        resultPtr[2] = ticksPtr[1];
        resultPtr[3] = ticksPtr[0];
        resultPtr[4] = ticksPtr[5];
        resultPtr[5] = ticksPtr[4];
        resultPtr[6] = (byte) ((ticksPtr[7] & ResetVersionMask) | Version1Flag);
        resultPtr[7] = ticksPtr[6];
        resultPtr[8] = (byte) ((resultPtr[8] & ResetReservedMask) | ReservedFlag);
        return new Uuid(new Span<byte>(resultPtr, 16));
    }

    /// <summary>
    ///     <para>
    ///         <b>Obsolete. Use <see cref="CreateVersion7()" /> instead.</b>
    ///     </para>
    ///     <para>Initializes a new instance of the <see cref="Uuid" /> structure that works the same way as UUID_TO_BIN(UUID(), 1) from MySQL 8.0.</para>
    /// </summary>
    /// <returns></returns>
    [Obsolete("Use CreateVersion7() instead.")]
    public static Uuid NewMySqlOptimized()
    {
        byte* resultPtr = stackalloc byte[16];
        var resultAsGuidPtr = (Guid*) resultPtr;
        var guid = Guid.NewGuid();
        resultAsGuidPtr[0] = guid;
        long currentTicks = DateTime.UtcNow.Ticks - ChristianCalendarGregorianReformTicksDate;
        var ticksPtr = (byte*) &currentTicks;
        resultPtr[0] = (byte) ((ticksPtr[7] & ResetVersionMask) | Version1Flag);
        resultPtr[1] = ticksPtr[6];
        resultPtr[2] = ticksPtr[5];
        resultPtr[3] = ticksPtr[4];
        resultPtr[4] = ticksPtr[3];
        resultPtr[5] = ticksPtr[2];
        resultPtr[6] = ticksPtr[1];
        resultPtr[7] = ticksPtr[0];
        resultPtr[8] = (byte) ((resultPtr[8] & ResetReservedMask) | ReservedFlag);
        return new Uuid(new Span<byte>(resultPtr, 16));
    }

    #endregion
}
