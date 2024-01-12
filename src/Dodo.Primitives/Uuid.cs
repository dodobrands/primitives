using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Dodo.Primitives.Internal;
#if NET8_0_OR_GREATER
using System.Numerics;
using System.Runtime.Intrinsics;
#endif

namespace Dodo.Primitives;

/// <summary>
///     Represents a universally unique identifier (UUID).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[TypeConverter(typeof(UuidTypeConverter))]
[JsonConverter(typeof(SystemTextJsonUuidJsonConverter))]
[SuppressMessage("ReSharper", "RedundantNameQualifier")]
public unsafe struct Uuid :
    ISpanFormattable,
    IComparable,
    IComparable<Uuid>,
    IEquatable<Uuid>, IFormattable
#if NET8_0_OR_GREATER
    , ISpanParsable<Uuid>, IParsable<Uuid>, IUtf8SpanFormattable, IComparisonOperators<Uuid, Uuid, bool>
#endif
{
    static Uuid()
    {
        TableToHexUtf16 = InternalHexTables.TableToHexUtf16;
        TableToHexUtf8 = InternalHexTables.TableToHexUtf8;
        TableFromHexToBytesUtf16 = InternalHexTables.TableFromHexToBytesUtf16;
    }

    private const ushort MaximalCharUtf16 = InternalHexTables.MaximalChar;

    private static readonly uint* TableToHexUtf16;
    private static readonly ushort* TableToHexUtf8;
    private static readonly byte* TableFromHexToBytesUtf16;

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
    public static readonly Uuid Empty = new Uuid();

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
    ///     Initializes a new instance of the <see cref="Uuid" /> structure by using the specified byte pointer.
    /// </summary>
    /// <param name="bytes">A byte pointer containing bytes which used to initialize the <see cref="Uuid" />.</param>
    public Uuid(byte* bytes)
    {
        this = Unsafe.ReadUnaligned<Uuid>(bytes);
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
    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (!(obj is Uuid))
        {
            throw new ArgumentException("Object must be of type Uuid.", nameof(obj));
        }

        var other = (Uuid) obj;
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
    ///     Compares this instance to a specified <see cref="Uuid" /> object and returns an indication of their relative values.
    /// </summary>
    /// <param name="other">An <see cref="Uuid" /> object to compare to this instance.</param>
    /// <returns>A signed number indicating the relative values of this instance and <paramref name="other" />.</returns>
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
#if NET8_0_OR_GREATER
            if (Vector128.IsHardwareAccelerated)
            {
                return Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref this)) == Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref other));
            }
#endif
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
#if NET8_0_OR_GREATER
        if (Vector128.IsHardwareAccelerated)
        {
            return Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref this)) == Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref other));
        }
#endif
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
#if NET8_0_OR_GREATER
        if (Vector128.IsHardwareAccelerated)
        {
            return Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref left)) == Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref right));
        }
#endif
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
#if NET8_0_OR_GREATER
        if (Vector128.IsHardwareAccelerated)
        {
            return Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref left)) != Vector128.LoadUnsafe(ref Unsafe.As<Uuid, byte>(ref right));
        }
#endif
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
#if NET8_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
#endif
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
                        FormatUtf16N(uuidChars);
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
                        FormatUtf16D(uuidChars);
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
                        FormatUtf16B(uuidChars);
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
                        FormatUtf16P(uuidChars);
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
                        FormatUtf16X(uuidChars);
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
#if NET8_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
#endif
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
                        FormatUtf16N(uuidChars);
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
                        FormatUtf16D(uuidChars);
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
                        FormatUtf16B(uuidChars);
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
                        FormatUtf16P(uuidChars);
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
                        FormatUtf16X(uuidChars);
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
#if NET8_0_OR_GREATER
    /// <inheritdoc cref="System.IUtf8SpanFormattable.TryFormat" />
#else
    /// <summary>Tries to format the value of the current instance as UTF-8 into the provided span of bytes.</summary>
    /// <param name="utf8Destination">When this method returns, this instance's value formatted as a span of bytes.</param>
    /// <param name="bytesWritten">When this method returns, the number of bytes that were written in <paramref name="utf8Destination" />.</param>
    /// <param name="format">A span containing the characters that represent a standard or custom format string that defines the acceptable format for <paramref name="utf8Destination" />.</param>
    /// <param name="provider">An optional object that supplies culture-specific formatting information for <paramref name="utf8Destination" />.</param>
    /// <returns><see langword="true" /> if the formatting was successful; otherwise, <see langword="false" />.</returns>
    /// <remarks>
    ///     An implementation of this interface should produce the same string of characters as an implementation of <see cref="IFormattable.ToString(string?, IFormatProvider?)" /> or <see cref="ISpanFormattable.TryFormat" />
    ///     on the same type. TryFormat should return false only if there is not enough space in the destination buffer; any other failures should throw an exception.
    /// </remarks>
#endif
    public bool TryFormat(
        Span<byte> utf8Destination,
        out int bytesWritten,
#if NET8_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
#endif
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
                        FormatUtf8N(uuidChars);
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
                        FormatUtf8D(uuidChars);
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
                        FormatUtf8B(uuidChars);
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
                        FormatUtf8P(uuidChars);
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
                        FormatUtf8X(uuidChars);
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
#if NET8_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
#endif
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
#if NET8_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
#endif
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
                        FormatUtf16N(uuidChars);
                    }

                    return uuidString;
                }
            case 'd':
                {
                    var uuidString = new string('\0', 36);
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
                    {
                        FormatUtf16D(uuidChars);
                    }

                    return uuidString;
                }
            case 'b':
                {
                    var uuidString = new string('\0', 38);
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
                    {
                        FormatUtf16B(uuidChars);
                    }

                    return uuidString;
                }
            case 'p':
                {
                    var uuidString = new string('\0', 38);
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
                    {
                        FormatUtf16P(uuidChars);
                    }

                    return uuidString;
                }
            case 'x':
                {
                    var uuidString = new string('\0', 68);
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
                    {
                        FormatUtf16X(uuidChars);
                    }

                    return uuidString;
                }
            default:
                throw new FormatException(
                    "Format string can be only \"N\", \"n\", \"D\", \"d\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\".");
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf8N(byte* dest)
    {
        // dddddddddddddddddddddddddddddddd
        var destInt16 = (ushort*) dest;
        destInt16[0] = TableToHexUtf8[_byte0];
        destInt16[1] = TableToHexUtf8[_byte1];
        destInt16[2] = TableToHexUtf8[_byte2];
        destInt16[3] = TableToHexUtf8[_byte3];
        destInt16[4] = TableToHexUtf8[_byte4];
        destInt16[5] = TableToHexUtf8[_byte5];
        destInt16[6] = TableToHexUtf8[_byte6];
        destInt16[7] = TableToHexUtf8[_byte7];
        destInt16[8] = TableToHexUtf8[_byte8];
        destInt16[9] = TableToHexUtf8[_byte9];
        destInt16[10] = TableToHexUtf8[_byte10];
        destInt16[11] = TableToHexUtf8[_byte11];
        destInt16[12] = TableToHexUtf8[_byte12];
        destInt16[13] = TableToHexUtf8[_byte13];
        destInt16[14] = TableToHexUtf8[_byte14];
        destInt16[15] = TableToHexUtf8[_byte15];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf8D(byte* dest)
    {
        // dddddddd-dddd-dddd-dddd-dddddddddddd
        var destInt16 = (ushort*) dest;
        var destInt16AsInt8 = (byte**) &destInt16;
        dest[8] = dest[13] = dest[18] = dest[23] = Utf8Dash;
        destInt16[0] = TableToHexUtf8[_byte0];
        destInt16[1] = TableToHexUtf8[_byte1];
        destInt16[2] = TableToHexUtf8[_byte2];
        destInt16[3] = TableToHexUtf8[_byte3];
        destInt16[7] = TableToHexUtf8[_byte6];
        destInt16[8] = TableToHexUtf8[_byte7];
        destInt16[12] = TableToHexUtf8[_byte10];
        destInt16[13] = TableToHexUtf8[_byte11];
        destInt16[14] = TableToHexUtf8[_byte12];
        destInt16[15] = TableToHexUtf8[_byte13];
        destInt16[16] = TableToHexUtf8[_byte14];
        destInt16[17] = TableToHexUtf8[_byte15];
        *destInt16AsInt8 += 1;
        destInt16[4] = TableToHexUtf8[_byte4];
        destInt16[5] = TableToHexUtf8[_byte5];
        destInt16[9] = TableToHexUtf8[_byte8];
        destInt16[10] = TableToHexUtf8[_byte9];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf8B(byte* dest)
    {
        // {dddddddd-dddd-dddd-dddd-dddddddddddd}
        var destInt16 = (ushort*) dest;
        var destInt16AsInt8 = (byte**) &destInt16;
        dest[0] = Utf8LeftCurlyBracket;
        dest[9] = dest[14] = dest[19] = dest[24] = Utf8Dash;
        dest[37] = Utf8RightCurlyBracket;
        destInt16[5] = TableToHexUtf8[_byte4];
        destInt16[6] = TableToHexUtf8[_byte5];
        destInt16[10] = TableToHexUtf8[_byte8];
        destInt16[11] = TableToHexUtf8[_byte9];
        *destInt16AsInt8 += 1;
        destInt16[0] = TableToHexUtf8[_byte0];
        destInt16[1] = TableToHexUtf8[_byte1];
        destInt16[2] = TableToHexUtf8[_byte2];
        destInt16[3] = TableToHexUtf8[_byte3];
        destInt16[7] = TableToHexUtf8[_byte6];
        destInt16[8] = TableToHexUtf8[_byte7];
        destInt16[12] = TableToHexUtf8[_byte10];
        destInt16[13] = TableToHexUtf8[_byte11];
        destInt16[14] = TableToHexUtf8[_byte12];
        destInt16[15] = TableToHexUtf8[_byte13];
        destInt16[16] = TableToHexUtf8[_byte14];
        destInt16[17] = TableToHexUtf8[_byte15];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf8P(byte* dest)
    {
        // (dddddddd-dddd-dddd-dddd-dddddddddddd)
        var destInt16 = (ushort*) dest;
        var destInt16AsInt8 = (byte**) &destInt16;
        dest[0] = Utf8LeftParenthesis;
        dest[9] = dest[14] = dest[19] = dest[24] = Utf8Dash;
        dest[37] = Utf8RightParenthesis;
        destInt16[5] = TableToHexUtf8[_byte4];
        destInt16[6] = TableToHexUtf8[_byte5];
        destInt16[10] = TableToHexUtf8[_byte8];
        destInt16[11] = TableToHexUtf8[_byte9];
        *destInt16AsInt8 += 1;
        destInt16[0] = TableToHexUtf8[_byte0];
        destInt16[1] = TableToHexUtf8[_byte1];
        destInt16[2] = TableToHexUtf8[_byte2];
        destInt16[3] = TableToHexUtf8[_byte3];
        destInt16[7] = TableToHexUtf8[_byte6];
        destInt16[8] = TableToHexUtf8[_byte7];
        destInt16[12] = TableToHexUtf8[_byte10];
        destInt16[13] = TableToHexUtf8[_byte11];
        destInt16[14] = TableToHexUtf8[_byte12];
        destInt16[15] = TableToHexUtf8[_byte13];
        destInt16[16] = TableToHexUtf8[_byte14];
        destInt16[17] = TableToHexUtf8[_byte15];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf8X(byte* dest)
    {
        const ushort zeroXUtf8 = ('x' << 8) | '0'; // 0x
        const ushort commaBraceUtf8 = ('{' << 8) | ','; // ,{
        const ushort closeBracesUtf8 = ('}' << 8) | '}'; // }}

        // {0xdddddddd,0xdddd,0xdddd,{0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd}}
        var destInt16 = (ushort*) dest;
        var destInt16AsInt8 = (byte**) &destInt16;
        dest[0] = Utf8LeftCurlyBracket;
        dest[11] = dest[18] = dest[31] = dest[36] = dest[41] = dest[46] = dest[51] = dest[56] = dest[61] = Utf8Comma;
        destInt16[6] = destInt16[16] = destInt16[21] = destInt16[26] = destInt16[31] = zeroXUtf8; // 0x
        destInt16[7] = TableToHexUtf8[_byte4];
        destInt16[8] = TableToHexUtf8[_byte5];
        destInt16[17] = TableToHexUtf8[_byte9];
        destInt16[22] = TableToHexUtf8[_byte11];
        destInt16[27] = TableToHexUtf8[_byte13];
        destInt16[32] = TableToHexUtf8[_byte15];
        destInt16[33] = closeBracesUtf8; // }}
        *destInt16AsInt8 += 1;
        destInt16[0] = destInt16[9] = destInt16[13] = destInt16[18] = destInt16[23] = destInt16[28] = zeroXUtf8; // 0x
        destInt16[1] = TableToHexUtf8[_byte0];
        destInt16[2] = TableToHexUtf8[_byte1];
        destInt16[3] = TableToHexUtf8[_byte2];
        destInt16[4] = TableToHexUtf8[_byte3];
        destInt16[10] = TableToHexUtf8[_byte6];
        destInt16[11] = TableToHexUtf8[_byte7];
        destInt16[12] = commaBraceUtf8; // ,{
        destInt16[14] = TableToHexUtf8[_byte8];
        destInt16[19] = TableToHexUtf8[_byte10];
        destInt16[24] = TableToHexUtf8[_byte12];
        destInt16[29] = TableToHexUtf8[_byte14];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf16N(char* dest)
    {
        // dddddddddddddddddddddddddddddddd
        var destInt32 = (uint*) dest;
        destInt32[0] = TableToHexUtf16[_byte0];
        destInt32[1] = TableToHexUtf16[_byte1];
        destInt32[2] = TableToHexUtf16[_byte2];
        destInt32[3] = TableToHexUtf16[_byte3];
        destInt32[4] = TableToHexUtf16[_byte4];
        destInt32[5] = TableToHexUtf16[_byte5];
        destInt32[6] = TableToHexUtf16[_byte6];
        destInt32[7] = TableToHexUtf16[_byte7];
        destInt32[8] = TableToHexUtf16[_byte8];
        destInt32[9] = TableToHexUtf16[_byte9];
        destInt32[10] = TableToHexUtf16[_byte10];
        destInt32[11] = TableToHexUtf16[_byte11];
        destInt32[12] = TableToHexUtf16[_byte12];
        destInt32[13] = TableToHexUtf16[_byte13];
        destInt32[14] = TableToHexUtf16[_byte14];
        destInt32[15] = TableToHexUtf16[_byte15];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf16D(char* dest)
    {
        // dddddddd-dddd-dddd-dddd-dddddddddddd
        var destInt32 = (uint*) dest;
        var destInt32AsInt16 = (char**) &destInt32;
        dest[8] = dest[13] = dest[18] = dest[23] = '-';
        destInt32[0] = TableToHexUtf16[_byte0];
        destInt32[1] = TableToHexUtf16[_byte1];
        destInt32[2] = TableToHexUtf16[_byte2];
        destInt32[3] = TableToHexUtf16[_byte3];
        destInt32[7] = TableToHexUtf16[_byte6];
        destInt32[8] = TableToHexUtf16[_byte7];
        destInt32[12] = TableToHexUtf16[_byte10];
        destInt32[13] = TableToHexUtf16[_byte11];
        destInt32[14] = TableToHexUtf16[_byte12];
        destInt32[15] = TableToHexUtf16[_byte13];
        destInt32[16] = TableToHexUtf16[_byte14];
        destInt32[17] = TableToHexUtf16[_byte15];
        *destInt32AsInt16 += 1;
        destInt32[4] = TableToHexUtf16[_byte4];
        destInt32[5] = TableToHexUtf16[_byte5];
        destInt32[9] = TableToHexUtf16[_byte8];
        destInt32[10] = TableToHexUtf16[_byte9];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf16B(char* dest)
    {
        // {dddddddd-dddd-dddd-dddd-dddddddddddd}
        var destInt32 = (uint*) dest;
        var destInt32AsInt16 = (char**) &destInt32;
        dest[0] = '{';
        dest[9] = dest[14] = dest[19] = dest[24] = '-';
        dest[37] = '}';
        destInt32[5] = TableToHexUtf16[_byte4];
        destInt32[6] = TableToHexUtf16[_byte5];
        destInt32[10] = TableToHexUtf16[_byte8];
        destInt32[11] = TableToHexUtf16[_byte9];
        *destInt32AsInt16 += 1;
        destInt32[0] = TableToHexUtf16[_byte0];
        destInt32[1] = TableToHexUtf16[_byte1];
        destInt32[2] = TableToHexUtf16[_byte2];
        destInt32[3] = TableToHexUtf16[_byte3];
        destInt32[7] = TableToHexUtf16[_byte6];
        destInt32[8] = TableToHexUtf16[_byte7];
        destInt32[12] = TableToHexUtf16[_byte10];
        destInt32[13] = TableToHexUtf16[_byte11];
        destInt32[14] = TableToHexUtf16[_byte12];
        destInt32[15] = TableToHexUtf16[_byte13];
        destInt32[16] = TableToHexUtf16[_byte14];
        destInt32[17] = TableToHexUtf16[_byte15];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf16P(char* dest)
    {
        // (dddddddd-dddd-dddd-dddd-dddddddddddd)
        var destInt32 = (uint*) dest;
        var destInt32AsInt16 = (char**) &destInt32;
        dest[0] = '(';
        dest[9] = dest[14] = dest[19] = dest[24] = '-';
        dest[37] = ')';
        destInt32[5] = TableToHexUtf16[_byte4];
        destInt32[6] = TableToHexUtf16[_byte5];
        destInt32[10] = TableToHexUtf16[_byte8];
        destInt32[11] = TableToHexUtf16[_byte9];
        *destInt32AsInt16 += 1;
        destInt32[0] = TableToHexUtf16[_byte0];
        destInt32[1] = TableToHexUtf16[_byte1];
        destInt32[2] = TableToHexUtf16[_byte2];
        destInt32[3] = TableToHexUtf16[_byte3];
        destInt32[7] = TableToHexUtf16[_byte6];
        destInt32[8] = TableToHexUtf16[_byte7];
        destInt32[12] = TableToHexUtf16[_byte10];
        destInt32[13] = TableToHexUtf16[_byte11];
        destInt32[14] = TableToHexUtf16[_byte12];
        destInt32[15] = TableToHexUtf16[_byte13];
        destInt32[16] = TableToHexUtf16[_byte14];
        destInt32[17] = TableToHexUtf16[_byte15];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private void FormatUtf16X(char* dest)
    {
        const uint zeroXUtf16 = ((uint) 'x' << 16) | '0'; // 0x
        const uint commaBraceUtf16 = ((uint) '{' << 16) | ','; // ,{
        const uint closeBracesUtf16 = ((uint) '}' << 16) | '}'; // }}

        // {0xdddddddd,0xdddd,0xdddd,{0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd}}
        var destInt32 = (uint*) dest;
        var destInt32AsInt16 = (char**) &destInt32;
        dest[0] = '{';
        dest[11] = dest[18] = dest[31] = dest[36] = dest[41] = dest[46] = dest[51] = dest[56] = dest[61] = ',';
        destInt32[6] = destInt32[16] = destInt32[21] = destInt32[26] = destInt32[31] = zeroXUtf16; // 0x
        destInt32[7] = TableToHexUtf16[_byte4];
        destInt32[8] = TableToHexUtf16[_byte5];
        destInt32[17] = TableToHexUtf16[_byte9];
        destInt32[22] = TableToHexUtf16[_byte11];
        destInt32[27] = TableToHexUtf16[_byte13];
        destInt32[32] = TableToHexUtf16[_byte15];
        destInt32[33] = closeBracesUtf16; // }}
        *destInt32AsInt16 += 1;
        destInt32[0] = destInt32[9] = destInt32[13] = destInt32[18] = destInt32[23] = destInt32[28] = zeroXUtf16; // 0x
        destInt32[1] = TableToHexUtf16[_byte0];
        destInt32[2] = TableToHexUtf16[_byte1];
        destInt32[3] = TableToHexUtf16[_byte2];
        destInt32[4] = TableToHexUtf16[_byte3];
        destInt32[10] = TableToHexUtf16[_byte6];
        destInt32[11] = TableToHexUtf16[_byte7];
        destInt32[12] = commaBraceUtf16; // ,{
        destInt32[14] = TableToHexUtf16[_byte8];
        destInt32[19] = TableToHexUtf16[_byte10];
        destInt32[24] = TableToHexUtf16[_byte12];
        destInt32[29] = TableToHexUtf16[_byte14];
    }

    /// <summary>
    ///     Converts <see cref="Dodo.Primitives.Uuid" /> to <see cref="System.Guid" /> preserve same binary representation.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.NoInlining)]
    public Guid ToGuidByteLayout()
    {
        var result = new Guid();
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
    ///     Converts <see cref="Dodo.Primitives.Uuid" /> to <see cref="System.Guid" /> preserve same string representation.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.NoInlining)]
    public Guid ToGuidStringLayout()
    {
        var result = new Guid();
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
        fixed (char* uuidStringPtr = &input.GetPinnableReference())
        {
            ParseWithExceptions(new ReadOnlySpan<char>(uuidStringPtr, input.Length), uuidStringPtr, resultPtr);
        }

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
        fixed (char* uuidStringPtr = &input.GetPinnableReference())
        {
            ParseWithExceptions(input, uuidStringPtr, resultPtr);
        }

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
        fixed (char* uuidStringPtr = &input.GetPinnableReference())
        {
            ParseWithExceptions(new ReadOnlySpan<char>(uuidStringPtr, input.Length), uuidStringPtr, resultPtr);
        }

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
        fixed (char* uuidStringPtr = &input.GetPinnableReference())
        {
            ParseWithExceptions(input, uuidStringPtr, resultPtr);
        }

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
#if NET8_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
#endif
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
                        ParseWithExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'd':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        ParseWithExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'b':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        ParseWithExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'p':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        ParseWithExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'x':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        ParseWithExceptionsX((uint) input.Length, uuidStringPtr, resultPtr);
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
#if NET8_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
#endif
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
                        ParseWithExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'd':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        ParseWithExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'b':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        ParseWithExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'p':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        ParseWithExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
            case 'x':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        ParseWithExceptionsX((uint) input.Length, uuidStringPtr, resultPtr);
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
        fixed (char* uuidStringPtr = &input.GetPinnableReference())
        {
            if (ParseWithoutExceptions(input.AsSpan(), uuidStringPtr, resultPtr))
            {
                output = result;
                return true;
            }
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
        fixed (char* uuidStringPtr = &input.GetPinnableReference())
        {
            if (ParseWithoutExceptions(input, uuidStringPtr, resultPtr))
            {
                output = result;
                return true;
            }
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
        fixed (byte* uuidUtf8StringPtr = &uuidUtf8String.GetPinnableReference())
        {
            if (ParseWithoutExceptionsUtf8(uuidUtf8String, uuidUtf8StringPtr, resultPtr))
            {
                output = result;
                return true;
            }
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
#if NET8_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
#endif
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
                        parsed = ParseWithoutExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'n':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = ParseWithoutExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'b':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = ParseWithoutExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'p':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = ParseWithoutExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'x':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = ParseWithoutExceptionsX((uint) input.Length, uuidStringPtr, resultPtr);
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
#if NET8_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.GuidFormat)]
#endif
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
                        parsed = ParseWithoutExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'n':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = ParseWithoutExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'b':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = ParseWithoutExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'p':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = ParseWithoutExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
            case 'x':
                {
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
                    {
                        parsed = ParseWithoutExceptionsX((uint) input.Length, uuidStringPtr, resultPtr);
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

    private static bool ParseWithoutExceptions(ReadOnlySpan<char> uuidString, char* uuidStringPtr, byte* resultPtr)
    {
        var length = (uint) uuidString.Length;
        if (length == 0u)
        {
            return false;
        }

        char* dashBuffer = stackalloc char[1];
        dashBuffer[0] = '-';
        var dashSpan = new ReadOnlySpan<char>(dashBuffer, 1);
        switch (uuidString[0])
        {
            case '(':
                {
                    return ParseWithoutExceptionsP(length, uuidStringPtr, resultPtr);
                }
            case '{':
                {
                    return uuidString.Contains(dashSpan, StringComparison.Ordinal)
                        ? ParseWithoutExceptionsB(length, uuidStringPtr, resultPtr)
                        : ParseWithoutExceptionsX(length, uuidStringPtr, resultPtr);
                }
            default:
                {
                    return uuidString.Contains(dashSpan, StringComparison.Ordinal)
                        ? ParseWithoutExceptionsD(length, uuidStringPtr, resultPtr)
                        : ParseWithoutExceptionsN(length, uuidStringPtr, resultPtr);
                }
        }
    }

    private static bool ParseWithoutExceptionsD(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 36u)
        {
            return false;
        }

        if (uuidStringPtr[8] != '-' || uuidStringPtr[13] != '-' || uuidStringPtr[18] != '-' || uuidStringPtr[23] != '-')
        {
            return false;
        }

        return TryParsePtrD(uuidStringPtr, resultPtr);
    }

    private static bool ParseWithoutExceptionsN(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        return uuidStringLength == 32u && TryParsePtrN(uuidStringPtr, resultPtr);
    }

    private static bool ParseWithoutExceptionsB(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            return false;
        }

        if (uuidStringPtr[0] != '{'
            || uuidStringPtr[9] != '-'
            || uuidStringPtr[14] != '-'
            || uuidStringPtr[19] != '-'
            || uuidStringPtr[24] != '-'
            || uuidStringPtr[37] != '}')
        {
            return false;
        }

        return TryParsePtrD(uuidStringPtr + 1, resultPtr);
    }

    private static bool ParseWithoutExceptionsP(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            return false;
        }

        if (uuidStringPtr[0] != '('
            || uuidStringPtr[9] != '-'
            || uuidStringPtr[14] != '-'
            || uuidStringPtr[19] != '-'
            || uuidStringPtr[24] != '-'
            || uuidStringPtr[37] != ')')
        {
            return false;
        }

        return TryParsePtrD(uuidStringPtr + 1, resultPtr);
    }

    private static bool ParseWithoutExceptionsX(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 68u)
        {
            return false;
        }

        if (uuidStringPtr[0] != '{'
            || uuidStringPtr[1] != '0'
            || uuidStringPtr[2] != 'x'
            || uuidStringPtr[11] != ','
            || uuidStringPtr[12] != '0'
            || uuidStringPtr[13] != 'x'
            || uuidStringPtr[18] != ','
            || uuidStringPtr[19] != '0'
            || uuidStringPtr[20] != 'x'
            || uuidStringPtr[25] != ','
            || uuidStringPtr[26] != '{'
            || uuidStringPtr[27] != '0'
            || uuidStringPtr[28] != 'x'
            || uuidStringPtr[31] != ','
            || uuidStringPtr[32] != '0'
            || uuidStringPtr[33] != 'x'
            || uuidStringPtr[36] != ','
            || uuidStringPtr[37] != '0'
            || uuidStringPtr[38] != 'x'
            || uuidStringPtr[41] != ','
            || uuidStringPtr[42] != '0'
            || uuidStringPtr[43] != 'x'
            || uuidStringPtr[46] != ','
            || uuidStringPtr[47] != '0'
            || uuidStringPtr[48] != 'x'
            || uuidStringPtr[51] != ','
            || uuidStringPtr[52] != '0'
            || uuidStringPtr[53] != 'x'
            || uuidStringPtr[56] != ','
            || uuidStringPtr[57] != '0'
            || uuidStringPtr[58] != 'x'
            || uuidStringPtr[61] != ','
            || uuidStringPtr[62] != '0'
            || uuidStringPtr[63] != 'x'
            || uuidStringPtr[66] != '}'
            || uuidStringPtr[67] != '}')
        {
            return false;
        }

        return TryParsePtrX(uuidStringPtr, resultPtr);
    }

    /// <summary>
    ///     (
    /// </summary>
    private const byte Utf8LeftParenthesis = 0x28;

    /// <summary>
    ///     )
    /// </summary>
    private const byte Utf8RightParenthesis = 0x29;

    /// <summary>
    ///     {
    /// </summary>
    private const byte Utf8LeftCurlyBracket = 0x7B;

    /// <summary>
    ///     }
    /// </summary>
    private const byte Utf8RightCurlyBracket = 0x7D;

    /// <summary>
    ///     -
    /// </summary>
    private const byte Utf8HyphenMinus = 0x2D;

    /// <summary>
    ///     0
    /// </summary>
    private const byte Utf8DigitZero = 0x30;

    /// <summary>
    ///     x
    /// </summary>
    private const byte Utf8LatinSmallLetterX = 0x78;

    /// <summary>
    ///     ,
    /// </summary>
    private const byte Utf8Comma = 0x2C;

    /// <summary>
    ///     -
    /// </summary>
    private const byte Utf8Dash = 0x2D;

    private static bool ParseWithoutExceptionsUtf8(ReadOnlySpan<byte> uuidUtf8String, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        var length = (uint) uuidUtf8String.Length;
        switch (uuidUtf8String[0])
        {
            case Utf8LeftParenthesis: // (
                {
                    return ParseWithoutExceptionsPUtf8(length, uuidUtf8StringPtr, resultPtr);
                }
            case Utf8LeftCurlyBracket:
                {
                    return uuidUtf8String.Contains(Utf8HyphenMinus)
                        ? ParseWithoutExceptionsBUtf8(length, uuidUtf8StringPtr, resultPtr)
                        : ParseWithoutExceptionsXUtf8(length, uuidUtf8StringPtr, resultPtr);
                }
            default:
                {
                    return uuidUtf8String.IndexOf(Utf8HyphenMinus) >= 0
                        ? ParseWithoutExceptionsDUtf8(length, uuidUtf8StringPtr, resultPtr)
                        : ParseWithoutExceptionsNUtf8(length, uuidUtf8StringPtr, resultPtr);
                }
        }
    }


    private static bool ParseWithoutExceptionsDUtf8(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 36u)
        {
            return false;
        }

        if (uuidUtf8StringPtr[8] != Utf8HyphenMinus
            || uuidUtf8StringPtr[13] != Utf8HyphenMinus
            || uuidUtf8StringPtr[18] != Utf8HyphenMinus
            || uuidUtf8StringPtr[23] != Utf8HyphenMinus)
        {
            return false;
        }

        return TryParsePtrDUtf8(uuidUtf8StringPtr, resultPtr);
    }

    private static bool ParseWithoutExceptionsNUtf8(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        return uuidStringLength == 32u && TryParsePtrNUtf8(uuidUtf8StringPtr, resultPtr);
    }

    private static bool ParseWithoutExceptionsBUtf8(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            return false;
        }

        if (uuidUtf8StringPtr[0] != Utf8LeftCurlyBracket // {
            || uuidUtf8StringPtr[9] != Utf8HyphenMinus //-
            || uuidUtf8StringPtr[14] != Utf8HyphenMinus // -
            || uuidUtf8StringPtr[19] != Utf8HyphenMinus // -
            || uuidUtf8StringPtr[24] != Utf8HyphenMinus // -
            || uuidUtf8StringPtr[37] != Utf8RightCurlyBracket) // }
        {
            return false;
        }

        return TryParsePtrDUtf8(uuidUtf8StringPtr + 1, resultPtr);
    }

    private static bool ParseWithoutExceptionsPUtf8(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            return false;
        }

        if (uuidUtf8StringPtr[0] != Utf8LeftParenthesis // (
            || uuidUtf8StringPtr[9] != Utf8HyphenMinus // -
            || uuidUtf8StringPtr[14] != Utf8HyphenMinus // -
            || uuidUtf8StringPtr[19] != Utf8HyphenMinus // -
            || uuidUtf8StringPtr[24] != Utf8HyphenMinus // -
            || uuidUtf8StringPtr[37] != Utf8RightParenthesis) // )
        {
            return false;
        }

        return TryParsePtrDUtf8(uuidUtf8StringPtr + 1, resultPtr);
    }

    private static bool ParseWithoutExceptionsXUtf8(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 68u)
        {
            return false;
        }

        if (uuidUtf8StringPtr[0] != Utf8LeftCurlyBracket // {
            || uuidUtf8StringPtr[1] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[2] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[11] != Utf8Comma // ,
            || uuidUtf8StringPtr[12] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[13] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[18] != Utf8Comma // ,
            || uuidUtf8StringPtr[19] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[20] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[25] != Utf8Comma // ,
            || uuidUtf8StringPtr[26] != Utf8LeftCurlyBracket // {
            || uuidUtf8StringPtr[27] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[28] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[31] != Utf8Comma // ,
            || uuidUtf8StringPtr[32] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[33] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[36] != Utf8Comma // ,
            || uuidUtf8StringPtr[37] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[38] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[41] != Utf8Comma // ,
            || uuidUtf8StringPtr[42] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[43] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[46] != Utf8Comma // ,
            || uuidUtf8StringPtr[47] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[48] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[51] != Utf8Comma // ,
            || uuidUtf8StringPtr[52] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[53] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[56] != Utf8Comma // ,
            || uuidUtf8StringPtr[57] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[58] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[61] != Utf8Comma // ,
            || uuidUtf8StringPtr[62] != Utf8DigitZero // 0
            || uuidUtf8StringPtr[63] != Utf8LatinSmallLetterX // x
            || uuidUtf8StringPtr[66] != Utf8RightCurlyBracket // }
            || uuidUtf8StringPtr[67] != Utf8RightCurlyBracket // }
           )
        {
            return false;
        }

        return TryParsePtrXUtf8(uuidUtf8StringPtr, resultPtr);
    }

    private static void ParseWithExceptions(ReadOnlySpan<char> uuidString, char* uuidStringPtr, byte* resultPtr)
    {
        var length = (uint) uuidString.Length;
        if (length == 0u)
        {
            throw new FormatException("Unrecognized Uuid format.");
        }

        char* dashBuffer = stackalloc char[1];
        dashBuffer[0] = '-';
        var dashSpan = new ReadOnlySpan<char>(dashBuffer, 1);
        switch (uuidStringPtr[0])
        {
            case '(':
                {
                    ParseWithExceptionsP(length, uuidStringPtr, resultPtr);
                    break;
                }
            case '{':
                {
                    if (uuidString.Contains(dashSpan, StringComparison.Ordinal))
                    {
                        ParseWithExceptionsB(length, uuidStringPtr, resultPtr);
                        break;
                    }

                    ParseWithExceptionsX(length, uuidStringPtr, resultPtr);
                    break;
                }
            default:
                {
                    if (uuidString.Contains(dashSpan, StringComparison.Ordinal))
                    {
                        ParseWithExceptionsD(length, uuidStringPtr, resultPtr);
                        break;
                    }

                    ParseWithExceptionsN(length, uuidStringPtr, resultPtr);
                    break;
                }
        }
    }

    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    private static void ParseWithExceptionsD(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 36u)
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx.");
        }

        if (uuidStringPtr[8] != '-' || uuidStringPtr[13] != '-' || uuidStringPtr[18] != '-' || uuidStringPtr[23] != '-')
        {
            throw new FormatException("Dashes are in the wrong position for Uuid parsing.");
        }

        if (!TryParsePtrD(uuidStringPtr, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    private static void ParseWithExceptionsN(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 32u)
        {
            throw new FormatException("Uuid should contain only 32 digits xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.");
        }

        if (!TryParsePtrN(uuidStringPtr, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    private static void ParseWithExceptionsB(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}.");
        }

        if (uuidStringPtr[0] != '{' || uuidStringPtr[37] != '}')
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}.");
        }

        if (uuidStringPtr[9] != '-' || uuidStringPtr[14] != '-' || uuidStringPtr[19] != '-' || uuidStringPtr[24] != '-')
        {
            throw new FormatException("Dashes are in the wrong position for Uuid parsing.");
        }

        if (!TryParsePtrD(uuidStringPtr + 1, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    private static void ParseWithExceptionsP(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
        }

        if (uuidStringPtr[0] != '(' || uuidStringPtr[37] != ')')
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
        }

        if (uuidStringPtr[9] != '-' || uuidStringPtr[14] != '-' || uuidStringPtr[19] != '-' || uuidStringPtr[24] != '-')
        {
            throw new FormatException("Dashes are in the wrong position for Uuid parsing.");
        }

        if (!TryParsePtrD(uuidStringPtr + 1, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
    private static void ParseWithExceptionsX(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 68u)
        {
            throw new FormatException(
                "Could not find a brace, or the length between the previous token and the brace was zero (i.e., '0x,'etc.).");
        }

        if (uuidStringPtr[0] != '{'
            || uuidStringPtr[26] != '{'
            || uuidStringPtr[66] != '}')
        {
            throw new FormatException(
                "Could not find a brace, or the length between the previous token and the brace was zero (i.e., '0x,'etc.).");
        }

        if (uuidStringPtr[67] != '}')
        {
            throw new FormatException("Could not find the ending brace.");
        }

        if (uuidStringPtr[11] != ','
            || uuidStringPtr[18] != ','
            || uuidStringPtr[25] != ','
            || uuidStringPtr[31] != ','
            || uuidStringPtr[36] != ','
            || uuidStringPtr[41] != ','
            || uuidStringPtr[46] != ','
            || uuidStringPtr[51] != ','
            || uuidStringPtr[56] != ','
            || uuidStringPtr[61] != ',')
        {
            throw new FormatException(
                "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
        }

        if (uuidStringPtr[1] != '0'
            || uuidStringPtr[2] != 'x'
            || uuidStringPtr[12] != '0'
            || uuidStringPtr[13] != 'x'
            || uuidStringPtr[19] != '0'
            || uuidStringPtr[20] != 'x'
            || uuidStringPtr[27] != '0'
            || uuidStringPtr[28] != 'x'
            || uuidStringPtr[32] != '0'
            || uuidStringPtr[33] != 'x'
            || uuidStringPtr[37] != '0'
            || uuidStringPtr[38] != 'x'
            || uuidStringPtr[42] != '0'
            || uuidStringPtr[43] != 'x'
            || uuidStringPtr[47] != '0'
            || uuidStringPtr[48] != 'x'
            || uuidStringPtr[52] != '0'
            || uuidStringPtr[53] != 'x'
            || uuidStringPtr[57] != '0'
            || uuidStringPtr[58] != 'x'
            || uuidStringPtr[62] != '0'
            || uuidStringPtr[63] != 'x')
        {
            throw new FormatException("Expected 0x prefix.");
        }


        if (!TryParsePtrX(uuidStringPtr, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static bool TryParsePtrN(char* value, byte* resultPtr)
    {
        // e.g. "d85b1407351d4694939203acc5870eb1"
        byte hi;
        byte lo;
        // 0 byte
        if (value[0] < MaximalCharUtf16
            && (hi = TableFromHexToBytesUtf16[value[0]]) != 0xFF
            && value[1] < MaximalCharUtf16
            && (lo = TableFromHexToBytesUtf16[value[1]]) != 0xFF)
        {
            resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
            // 1 byte
            if (value[2] < MaximalCharUtf16
                && (hi = TableFromHexToBytesUtf16[value[2]]) != 0xFF
                && value[3] < MaximalCharUtf16
                && (lo = TableFromHexToBytesUtf16[value[3]]) != 0xFF)
            {
                resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                // 2 byte
                if (value[4] < MaximalCharUtf16
                    && (hi = TableFromHexToBytesUtf16[value[4]]) != 0xFF
                    && value[5] < MaximalCharUtf16
                    && (lo = TableFromHexToBytesUtf16[value[5]]) != 0xFF)
                {
                    resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                    // 3 byte
                    if (value[6] < MaximalCharUtf16
                        && (hi = TableFromHexToBytesUtf16[value[6]]) != 0xFF
                        && value[7] < MaximalCharUtf16
                        && (lo = TableFromHexToBytesUtf16[value[7]]) != 0xFF)
                    {
                        resultPtr[3] = (byte) ((byte) (hi << 4) | lo);
                        // 4 byte
                        if (value[8] < MaximalCharUtf16
                            && (hi = TableFromHexToBytesUtf16[value[8]]) != 0xFF
                            && value[9] < MaximalCharUtf16
                            && (lo = TableFromHexToBytesUtf16[value[9]]) != 0xFF)
                        {
                            resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                            // 5 byte
                            if (value[10] < MaximalCharUtf16
                                && (hi = TableFromHexToBytesUtf16[value[10]]) != 0xFF
                                && value[11] < MaximalCharUtf16
                                && (lo = TableFromHexToBytesUtf16[value[11]]) != 0xFF)
                            {
                                resultPtr[5] = (byte) ((byte) (hi << 4) | lo);
                                // 6 byte
                                if (value[12] < MaximalCharUtf16
                                    && (hi = TableFromHexToBytesUtf16[value[12]]) != 0xFF
                                    && value[13] < MaximalCharUtf16
                                    && (lo = TableFromHexToBytesUtf16[value[13]]) != 0xFF)
                                {
                                    resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                    // 7 byte
                                    if (value[14] < MaximalCharUtf16
                                        && (hi = TableFromHexToBytesUtf16[value[14]]) != 0xFF
                                        && value[15] < MaximalCharUtf16
                                        && (lo = TableFromHexToBytesUtf16[value[15]]) != 0xFF)
                                    {
                                        resultPtr[7] = (byte) ((byte) (hi << 4) | lo);
                                        // 8 byte
                                        if (value[16] < MaximalCharUtf16
                                            && (hi = TableFromHexToBytesUtf16[value[16]]) != 0xFF
                                            && value[17] < MaximalCharUtf16
                                            && (lo = TableFromHexToBytesUtf16[value[17]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                            // 9 byte
                                            if (value[18] < MaximalCharUtf16
                                                && (hi = TableFromHexToBytesUtf16[value[18]]) != 0xFF
                                                && value[19] < MaximalCharUtf16
                                                && (lo = TableFromHexToBytesUtf16[value[19]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hi << 4) | lo);
                                                // 10 byte
                                                if (value[20] < MaximalCharUtf16
                                                    && (hi = TableFromHexToBytesUtf16[value[20]]) != 0xFF
                                                    && value[21] < MaximalCharUtf16
                                                    && (lo = TableFromHexToBytesUtf16[value[21]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                    // 11 byte
                                                    if (value[22] < MaximalCharUtf16
                                                        && (hi = TableFromHexToBytesUtf16[value[22]]) != 0xFF
                                                        && value[23] < MaximalCharUtf16
                                                        && (lo = TableFromHexToBytesUtf16[value[23]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                        // 12 byte
                                                        if (value[24] < MaximalCharUtf16
                                                            && (hi = TableFromHexToBytesUtf16[value[24]]) != 0xFF
                                                            && value[25] < MaximalCharUtf16
                                                            && (lo = TableFromHexToBytesUtf16[value[25]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                            // 13 byte
                                                            if (value[26] < MaximalCharUtf16
                                                                && (hi = TableFromHexToBytesUtf16[value[26]]) != 0xFF
                                                                && value[27] < MaximalCharUtf16
                                                                && (lo = TableFromHexToBytesUtf16[value[27]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                // 14 byte
                                                                if (value[28] < MaximalCharUtf16
                                                                    && (hi = TableFromHexToBytesUtf16[value[28]]) != 0xFF
                                                                    && value[29] < MaximalCharUtf16
                                                                    && (lo = TableFromHexToBytesUtf16[value[29]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 15 byte
                                                                    if (value[30] < MaximalCharUtf16
                                                                        && (hi = TableFromHexToBytesUtf16[value[30]]) != 0xFF
                                                                        && value[31] < MaximalCharUtf16
                                                                        && (lo = TableFromHexToBytesUtf16[value[31]]) != 0xFF)
                                                                    {
                                                                        resultPtr[15] = (byte) ((byte) (hi << 4) | lo);
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static bool TryParsePtrD(char* value, byte* resultPtr)
    {
        // e.g. "d85b1407-351d-4694-9392-03acc5870eb1"
        byte hi;
        byte lo;
        // 0 byte
        if (value[0] < MaximalCharUtf16
            && (hi = TableFromHexToBytesUtf16[value[0]]) != 0xFF
            && value[1] < MaximalCharUtf16
            && (lo = TableFromHexToBytesUtf16[value[1]]) != 0xFF)
        {
            resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
            // 1 byte
            if (value[2] < MaximalCharUtf16
                && (hi = TableFromHexToBytesUtf16[value[2]]) != 0xFF
                && value[3] < MaximalCharUtf16
                && (lo = TableFromHexToBytesUtf16[value[3]]) != 0xFF)
            {
                resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                // 2 byte
                if (value[4] < MaximalCharUtf16
                    && (hi = TableFromHexToBytesUtf16[value[4]]) != 0xFF
                    && value[5] < MaximalCharUtf16
                    && (lo = TableFromHexToBytesUtf16[value[5]]) != 0xFF)
                {
                    resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                    // 3 byte
                    if (value[6] < MaximalCharUtf16
                        && (hi = TableFromHexToBytesUtf16[value[6]]) != 0xFF
                        && value[7] < MaximalCharUtf16
                        && (lo = TableFromHexToBytesUtf16[value[7]]) != 0xFF)
                    {
                        resultPtr[3] = (byte) ((byte) (hi << 4) | lo);

                        // value[8] == '-'

                        // 4 byte
                        if (value[9] < MaximalCharUtf16
                            && (hi = TableFromHexToBytesUtf16[value[9]]) != 0xFF
                            && value[10] < MaximalCharUtf16
                            && (lo = TableFromHexToBytesUtf16[value[10]]) != 0xFF)
                        {
                            resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                            // 5 byte
                            if (value[11] < MaximalCharUtf16
                                && (hi = TableFromHexToBytesUtf16[value[11]]) != 0xFF
                                && value[12] < MaximalCharUtf16
                                && (lo = TableFromHexToBytesUtf16[value[12]]) != 0xFF)
                            {
                                resultPtr[5] = (byte) ((byte) (hi << 4) | lo);

                                // value[13] == '-'

                                // 6 byte
                                if (value[14] < MaximalCharUtf16
                                    && (hi = TableFromHexToBytesUtf16[value[14]]) != 0xFF
                                    && value[15] < MaximalCharUtf16
                                    && (lo = TableFromHexToBytesUtf16[value[15]]) != 0xFF)
                                {
                                    resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                    // 7 byte
                                    if (value[16] < MaximalCharUtf16
                                        && (hi = TableFromHexToBytesUtf16[value[16]]) != 0xFF
                                        && value[17] < MaximalCharUtf16
                                        && (lo = TableFromHexToBytesUtf16[value[17]]) != 0xFF)
                                    {
                                        resultPtr[7] = (byte) ((byte) (hi << 4) | lo);

                                        // value[18] == '-'

                                        // 8 byte
                                        if (value[19] < MaximalCharUtf16
                                            && (hi = TableFromHexToBytesUtf16[value[19]]) != 0xFF
                                            && value[20] < MaximalCharUtf16
                                            && (lo = TableFromHexToBytesUtf16[value[20]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                            // 9 byte
                                            if (value[21] < MaximalCharUtf16
                                                && (hi = TableFromHexToBytesUtf16[value[21]]) != 0xFF
                                                && value[22] < MaximalCharUtf16
                                                && (lo = TableFromHexToBytesUtf16[value[22]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hi << 4) | lo);

                                                // value[23] == '-'

                                                // 10 byte
                                                if (value[24] < MaximalCharUtf16
                                                    && (hi = TableFromHexToBytesUtf16[value[24]]) != 0xFF
                                                    && value[25] < MaximalCharUtf16
                                                    && (lo = TableFromHexToBytesUtf16[value[25]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                    // 11 byte
                                                    if (value[26] < MaximalCharUtf16
                                                        && (hi = TableFromHexToBytesUtf16[value[26]]) != 0xFF
                                                        && value[27] < MaximalCharUtf16
                                                        && (lo = TableFromHexToBytesUtf16[value[27]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                        // 12 byte
                                                        if (value[28] < MaximalCharUtf16
                                                            && (hi = TableFromHexToBytesUtf16[value[28]]) != 0xFF
                                                            && value[29] < MaximalCharUtf16
                                                            && (lo = TableFromHexToBytesUtf16[value[29]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                            // 13 byte
                                                            if (value[30] < MaximalCharUtf16
                                                                && (hi = TableFromHexToBytesUtf16[value[30]]) != 0xFF
                                                                && value[31] < MaximalCharUtf16
                                                                && (lo = TableFromHexToBytesUtf16[value[31]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                // 14 byte
                                                                if (value[32] < MaximalCharUtf16
                                                                    && (hi = TableFromHexToBytesUtf16[value[32]]) != 0xFF
                                                                    && value[33] < MaximalCharUtf16
                                                                    && (lo = TableFromHexToBytesUtf16[value[33]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 15 byte
                                                                    if (value[34] < MaximalCharUtf16
                                                                        && (hi = TableFromHexToBytesUtf16[value[34]]) != 0xFF
                                                                        && value[35] < MaximalCharUtf16
                                                                        && (lo = TableFromHexToBytesUtf16[value[35]]) != 0xFF)
                                                                    {
                                                                        resultPtr[15] = (byte) ((byte) (hi << 4) | lo);
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static bool TryParsePtrX(char* value, byte* resultPtr)
    {
        // e.g. "{0xd85b1407,0x351d,0x4694,{0x93,0x92,0x03,0xac,0xc5,0x87,0x0e,0xb1}}"

        byte hexByteHi;
        byte hexByteLow;
        // value[0] == '{'
        // value[1] == '0'
        // value[2] == 'x'
        // 0 byte
        if (value[3] < MaximalCharUtf16
            && (hexByteHi = TableFromHexToBytesUtf16[value[3]]) != 0xFF
            && value[4] < MaximalCharUtf16
            && (hexByteLow = TableFromHexToBytesUtf16[value[4]]) != 0xFF)
        {
            resultPtr[0] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
            // 1 byte
            if (value[5] < MaximalCharUtf16
                && (hexByteHi = TableFromHexToBytesUtf16[value[5]]) != 0xFF
                && value[6] < MaximalCharUtf16
                && (hexByteLow = TableFromHexToBytesUtf16[value[6]]) != 0xFF)
            {
                resultPtr[1] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                // 2 byte
                if (value[7] < MaximalCharUtf16
                    && (hexByteHi = TableFromHexToBytesUtf16[value[7]]) != 0xFF
                    && value[8] < MaximalCharUtf16
                    && (hexByteLow = TableFromHexToBytesUtf16[value[8]]) != 0xFF)
                {
                    resultPtr[2] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                    // 3 byte
                    if (value[9] < MaximalCharUtf16
                        && (hexByteHi = TableFromHexToBytesUtf16[value[9]]) != 0xFF
                        && value[10] < MaximalCharUtf16
                        && (hexByteLow = TableFromHexToBytesUtf16[value[10]]) != 0xFF)
                    {
                        resultPtr[3] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                        // value[11] == ','
                        // value[12] == '0'
                        // value[13] == 'x'

                        // 4 byte
                        if (value[14] < MaximalCharUtf16
                            && (hexByteHi = TableFromHexToBytesUtf16[value[14]]) != 0xFF
                            && value[15] < MaximalCharUtf16
                            && (hexByteLow = TableFromHexToBytesUtf16[value[15]]) != 0xFF)
                        {
                            resultPtr[4] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                            // 5 byte
                            if (value[16] < MaximalCharUtf16
                                && (hexByteHi = TableFromHexToBytesUtf16[value[16]]) != 0xFF
                                && value[17] < MaximalCharUtf16
                                && (hexByteLow = TableFromHexToBytesUtf16[value[17]]) != 0xFF)
                            {
                                resultPtr[5] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                // value[18] == ','
                                // value[19] == '0'
                                // value[20] == 'x'

                                // 6 byte
                                if (value[21] < MaximalCharUtf16
                                    && (hexByteHi = TableFromHexToBytesUtf16[value[21]]) != 0xFF
                                    && value[22] < MaximalCharUtf16
                                    && (hexByteLow = TableFromHexToBytesUtf16[value[22]]) != 0xFF)
                                {
                                    resultPtr[6] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                                    // 7 byte
                                    if (value[23] < MaximalCharUtf16
                                        && (hexByteHi = TableFromHexToBytesUtf16[value[23]]) != 0xFF
                                        && value[24] < MaximalCharUtf16
                                        && (hexByteLow = TableFromHexToBytesUtf16[value[24]]) != 0xFF)
                                    {
                                        resultPtr[7] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                        // value[25] == ','
                                        // value[26] == '{'
                                        // value[27] == '0'
                                        // value[28] == 'x'

                                        // 8 byte
                                        if (value[29] < MaximalCharUtf16
                                            && (hexByteHi = TableFromHexToBytesUtf16[value[29]]) != 0xFF
                                            && value[30] < MaximalCharUtf16
                                            && (hexByteLow = TableFromHexToBytesUtf16[value[30]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                            // value[31] == ','
                                            // value[32] == '0'
                                            // value[33] == 'x'

                                            // 9 byte
                                            if (value[34] < MaximalCharUtf16
                                                && (hexByteHi = TableFromHexToBytesUtf16[value[34]]) != 0xFF
                                                && value[35] < MaximalCharUtf16
                                                && (hexByteLow = TableFromHexToBytesUtf16[value[35]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                // value[36] == ','
                                                // value[37] == '0'
                                                // value[38] == 'x'

                                                // 10 byte
                                                if (value[39] < MaximalCharUtf16
                                                    && (hexByteHi = TableFromHexToBytesUtf16[value[39]]) != 0xFF
                                                    && value[40] < MaximalCharUtf16
                                                    && (hexByteLow = TableFromHexToBytesUtf16[value[40]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                    // value[41] == ','
                                                    // value[42] == '0'
                                                    // value[43] == 'x'

                                                    // 11 byte
                                                    if (value[44] < MaximalCharUtf16
                                                        && (hexByteHi = TableFromHexToBytesUtf16[value[44]]) != 0xFF
                                                        && value[45] < MaximalCharUtf16
                                                        && (hexByteLow = TableFromHexToBytesUtf16[value[45]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                        // value[46] == ','
                                                        // value[47] == '0'
                                                        // value[48] == 'x'

                                                        // 12 byte
                                                        if (value[49] < MaximalCharUtf16
                                                            && (hexByteHi = TableFromHexToBytesUtf16[value[49]]) != 0xFF
                                                            && value[50] < MaximalCharUtf16
                                                            && (hexByteLow = TableFromHexToBytesUtf16[value[50]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                            // value[51] == ','
                                                            // value[52] == '0'
                                                            // value[53] == 'x'

                                                            // 13 byte
                                                            if (value[54] < MaximalCharUtf16
                                                                && (hexByteHi = TableFromHexToBytesUtf16[value[54]]) != 0xFF
                                                                && value[55] < MaximalCharUtf16
                                                                && (hexByteLow = TableFromHexToBytesUtf16[value[55]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                // value[56] == ','
                                                                // value[57] == '0'
                                                                // value[58] == 'x'

                                                                // 14 byte
                                                                if (value[59] < MaximalCharUtf16
                                                                    && (hexByteHi = TableFromHexToBytesUtf16[value[59]]) != 0xFF
                                                                    && value[60] < MaximalCharUtf16
                                                                    && (hexByteLow = TableFromHexToBytesUtf16[value[60]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                    // value[61] == ','
                                                                    // value[62] == '0'
                                                                    // value[63] == 'x'

                                                                    // 15 byte
                                                                    if (value[64] < MaximalCharUtf16
                                                                        && (hexByteHi = TableFromHexToBytesUtf16[value[64]]) != 0xFF
                                                                        && value[65] < MaximalCharUtf16
                                                                        && (hexByteLow = TableFromHexToBytesUtf16[value[65]]) != 0xFF)
                                                                    {
                                                                        resultPtr[15] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static bool TryParsePtrNUtf8(byte* value, byte* resultPtr)
    {
        // e.g. "d85b1407351d4694939203acc5870eb1"
        byte hi;
        byte lo;
        // 0 byte
        if (value[0] < MaximalCharUtf16
            && (hi = TableFromHexToBytesUtf16[value[0]]) != 0xFF
            && value[1] < MaximalCharUtf16
            && (lo = TableFromHexToBytesUtf16[value[1]]) != 0xFF)
        {
            resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
            // 1 byte
            if (value[2] < MaximalCharUtf16
                && (hi = TableFromHexToBytesUtf16[value[2]]) != 0xFF
                && value[3] < MaximalCharUtf16
                && (lo = TableFromHexToBytesUtf16[value[3]]) != 0xFF)
            {
                resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                // 2 byte
                if (value[4] < MaximalCharUtf16
                    && (hi = TableFromHexToBytesUtf16[value[4]]) != 0xFF
                    && value[5] < MaximalCharUtf16
                    && (lo = TableFromHexToBytesUtf16[value[5]]) != 0xFF)
                {
                    resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                    // 3 byte
                    if (value[6] < MaximalCharUtf16
                        && (hi = TableFromHexToBytesUtf16[value[6]]) != 0xFF
                        && value[7] < MaximalCharUtf16
                        && (lo = TableFromHexToBytesUtf16[value[7]]) != 0xFF)
                    {
                        resultPtr[3] = (byte) ((byte) (hi << 4) | lo);
                        // 4 byte
                        if (value[8] < MaximalCharUtf16
                            && (hi = TableFromHexToBytesUtf16[value[8]]) != 0xFF
                            && value[9] < MaximalCharUtf16
                            && (lo = TableFromHexToBytesUtf16[value[9]]) != 0xFF)
                        {
                            resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                            // 5 byte
                            if (value[10] < MaximalCharUtf16
                                && (hi = TableFromHexToBytesUtf16[value[10]]) != 0xFF
                                && value[11] < MaximalCharUtf16
                                && (lo = TableFromHexToBytesUtf16[value[11]]) != 0xFF)
                            {
                                resultPtr[5] = (byte) ((byte) (hi << 4) | lo);
                                // 6 byte
                                if (value[12] < MaximalCharUtf16
                                    && (hi = TableFromHexToBytesUtf16[value[12]]) != 0xFF
                                    && value[13] < MaximalCharUtf16
                                    && (lo = TableFromHexToBytesUtf16[value[13]]) != 0xFF)
                                {
                                    resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                    // 7 byte
                                    if (value[14] < MaximalCharUtf16
                                        && (hi = TableFromHexToBytesUtf16[value[14]]) != 0xFF
                                        && value[15] < MaximalCharUtf16
                                        && (lo = TableFromHexToBytesUtf16[value[15]]) != 0xFF)
                                    {
                                        resultPtr[7] = (byte) ((byte) (hi << 4) | lo);
                                        // 8 byte
                                        if (value[16] < MaximalCharUtf16
                                            && (hi = TableFromHexToBytesUtf16[value[16]]) != 0xFF
                                            && value[17] < MaximalCharUtf16
                                            && (lo = TableFromHexToBytesUtf16[value[17]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                            // 9 byte
                                            if (value[18] < MaximalCharUtf16
                                                && (hi = TableFromHexToBytesUtf16[value[18]]) != 0xFF
                                                && value[19] < MaximalCharUtf16
                                                && (lo = TableFromHexToBytesUtf16[value[19]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hi << 4) | lo);
                                                // 10 byte
                                                if (value[20] < MaximalCharUtf16
                                                    && (hi = TableFromHexToBytesUtf16[value[20]]) != 0xFF
                                                    && value[21] < MaximalCharUtf16
                                                    && (lo = TableFromHexToBytesUtf16[value[21]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                    // 11 byte
                                                    if (value[22] < MaximalCharUtf16
                                                        && (hi = TableFromHexToBytesUtf16[value[22]]) != 0xFF
                                                        && value[23] < MaximalCharUtf16
                                                        && (lo = TableFromHexToBytesUtf16[value[23]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                        // 12 byte
                                                        if (value[24] < MaximalCharUtf16
                                                            && (hi = TableFromHexToBytesUtf16[value[24]]) != 0xFF
                                                            && value[25] < MaximalCharUtf16
                                                            && (lo = TableFromHexToBytesUtf16[value[25]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                            // 13 byte
                                                            if (value[26] < MaximalCharUtf16
                                                                && (hi = TableFromHexToBytesUtf16[value[26]]) != 0xFF
                                                                && value[27] < MaximalCharUtf16
                                                                && (lo = TableFromHexToBytesUtf16[value[27]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                // 14 byte
                                                                if (value[28] < MaximalCharUtf16
                                                                    && (hi = TableFromHexToBytesUtf16[value[28]]) != 0xFF
                                                                    && value[29] < MaximalCharUtf16
                                                                    && (lo = TableFromHexToBytesUtf16[value[29]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 15 byte
                                                                    if (value[30] < MaximalCharUtf16
                                                                        && (hi = TableFromHexToBytesUtf16[value[30]]) != 0xFF
                                                                        && value[31] < MaximalCharUtf16
                                                                        && (lo = TableFromHexToBytesUtf16[value[31]]) != 0xFF)
                                                                    {
                                                                        resultPtr[15] = (byte) ((byte) (hi << 4) | lo);
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static bool TryParsePtrDUtf8(byte* value, byte* resultPtr)
    {
        // e.g. "d85b1407-351d-4694-9392-03acc5870eb1"
        byte hi;
        byte lo;
        // 0 byte
        if (value[0] < MaximalCharUtf16
            && (hi = TableFromHexToBytesUtf16[value[0]]) != 0xFF
            && value[1] < MaximalCharUtf16
            && (lo = TableFromHexToBytesUtf16[value[1]]) != 0xFF)
        {
            resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
            // 1 byte
            if (value[2] < MaximalCharUtf16
                && (hi = TableFromHexToBytesUtf16[value[2]]) != 0xFF
                && value[3] < MaximalCharUtf16
                && (lo = TableFromHexToBytesUtf16[value[3]]) != 0xFF)
            {
                resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                // 2 byte
                if (value[4] < MaximalCharUtf16
                    && (hi = TableFromHexToBytesUtf16[value[4]]) != 0xFF
                    && value[5] < MaximalCharUtf16
                    && (lo = TableFromHexToBytesUtf16[value[5]]) != 0xFF)
                {
                    resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                    // 3 byte
                    if (value[6] < MaximalCharUtf16
                        && (hi = TableFromHexToBytesUtf16[value[6]]) != 0xFF
                        && value[7] < MaximalCharUtf16
                        && (lo = TableFromHexToBytesUtf16[value[7]]) != 0xFF)
                    {
                        resultPtr[3] = (byte) ((byte) (hi << 4) | lo);

                        // value[8] == '-'

                        // 4 byte
                        if (value[9] < MaximalCharUtf16
                            && (hi = TableFromHexToBytesUtf16[value[9]]) != 0xFF
                            && value[10] < MaximalCharUtf16
                            && (lo = TableFromHexToBytesUtf16[value[10]]) != 0xFF)
                        {
                            resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                            // 5 byte
                            if (value[11] < MaximalCharUtf16
                                && (hi = TableFromHexToBytesUtf16[value[11]]) != 0xFF
                                && value[12] < MaximalCharUtf16
                                && (lo = TableFromHexToBytesUtf16[value[12]]) != 0xFF)
                            {
                                resultPtr[5] = (byte) ((byte) (hi << 4) | lo);

                                // value[13] == '-'

                                // 6 byte
                                if (value[14] < MaximalCharUtf16
                                    && (hi = TableFromHexToBytesUtf16[value[14]]) != 0xFF
                                    && value[15] < MaximalCharUtf16
                                    && (lo = TableFromHexToBytesUtf16[value[15]]) != 0xFF)
                                {
                                    resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                    // 7 byte
                                    if (value[16] < MaximalCharUtf16
                                        && (hi = TableFromHexToBytesUtf16[value[16]]) != 0xFF
                                        && value[17] < MaximalCharUtf16
                                        && (lo = TableFromHexToBytesUtf16[value[17]]) != 0xFF)
                                    {
                                        resultPtr[7] = (byte) ((byte) (hi << 4) | lo);

                                        // value[18] == '-'

                                        // 8 byte
                                        if (value[19] < MaximalCharUtf16
                                            && (hi = TableFromHexToBytesUtf16[value[19]]) != 0xFF
                                            && value[20] < MaximalCharUtf16
                                            && (lo = TableFromHexToBytesUtf16[value[20]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                            // 9 byte
                                            if (value[21] < MaximalCharUtf16
                                                && (hi = TableFromHexToBytesUtf16[value[21]]) != 0xFF
                                                && value[22] < MaximalCharUtf16
                                                && (lo = TableFromHexToBytesUtf16[value[22]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hi << 4) | lo);

                                                // value[23] == '-'

                                                // 10 byte
                                                if (value[24] < MaximalCharUtf16
                                                    && (hi = TableFromHexToBytesUtf16[value[24]]) != 0xFF
                                                    && value[25] < MaximalCharUtf16
                                                    && (lo = TableFromHexToBytesUtf16[value[25]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                    // 11 byte
                                                    if (value[26] < MaximalCharUtf16
                                                        && (hi = TableFromHexToBytesUtf16[value[26]]) != 0xFF
                                                        && value[27] < MaximalCharUtf16
                                                        && (lo = TableFromHexToBytesUtf16[value[27]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                        // 12 byte
                                                        if (value[28] < MaximalCharUtf16
                                                            && (hi = TableFromHexToBytesUtf16[value[28]]) != 0xFF
                                                            && value[29] < MaximalCharUtf16
                                                            && (lo = TableFromHexToBytesUtf16[value[29]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                            // 13 byte
                                                            if (value[30] < MaximalCharUtf16
                                                                && (hi = TableFromHexToBytesUtf16[value[30]]) != 0xFF
                                                                && value[31] < MaximalCharUtf16
                                                                && (lo = TableFromHexToBytesUtf16[value[31]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                // 14 byte
                                                                if (value[32] < MaximalCharUtf16
                                                                    && (hi = TableFromHexToBytesUtf16[value[32]]) != 0xFF
                                                                    && value[33] < MaximalCharUtf16
                                                                    && (lo = TableFromHexToBytesUtf16[value[33]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 15 byte
                                                                    if (value[34] < MaximalCharUtf16
                                                                        && (hi = TableFromHexToBytesUtf16[value[34]]) != 0xFF
                                                                        && value[35] < MaximalCharUtf16
                                                                        && (lo = TableFromHexToBytesUtf16[value[35]]) != 0xFF)
                                                                    {
                                                                        resultPtr[15] = (byte) ((byte) (hi << 4) | lo);
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static bool TryParsePtrXUtf8(byte* value, byte* resultPtr)
    {
        // e.g. "{0xd85b1407,0x351d,0x4694,{0x93,0x92,0x03,0xac,0xc5,0x87,0x0e,0xb1}}"

        byte hexByteHi;
        byte hexByteLow;
        // value[0] == '{'
        // value[1] == '0'
        // value[2] == 'x'
        // 0 byte
        if (value[3] < MaximalCharUtf16
            && (hexByteHi = TableFromHexToBytesUtf16[value[3]]) != 0xFF
            && value[4] < MaximalCharUtf16
            && (hexByteLow = TableFromHexToBytesUtf16[value[4]]) != 0xFF)
        {
            resultPtr[0] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
            // 1 byte
            if (value[5] < MaximalCharUtf16
                && (hexByteHi = TableFromHexToBytesUtf16[value[5]]) != 0xFF
                && value[6] < MaximalCharUtf16
                && (hexByteLow = TableFromHexToBytesUtf16[value[6]]) != 0xFF)
            {
                resultPtr[1] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                // 2 byte
                if (value[7] < MaximalCharUtf16
                    && (hexByteHi = TableFromHexToBytesUtf16[value[7]]) != 0xFF
                    && value[8] < MaximalCharUtf16
                    && (hexByteLow = TableFromHexToBytesUtf16[value[8]]) != 0xFF)
                {
                    resultPtr[2] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                    // 3 byte
                    if (value[9] < MaximalCharUtf16
                        && (hexByteHi = TableFromHexToBytesUtf16[value[9]]) != 0xFF
                        && value[10] < MaximalCharUtf16
                        && (hexByteLow = TableFromHexToBytesUtf16[value[10]]) != 0xFF)
                    {
                        resultPtr[3] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                        // value[11] == ','
                        // value[12] == '0'
                        // value[13] == 'x'

                        // 4 byte
                        if (value[14] < MaximalCharUtf16
                            && (hexByteHi = TableFromHexToBytesUtf16[value[14]]) != 0xFF
                            && value[15] < MaximalCharUtf16
                            && (hexByteLow = TableFromHexToBytesUtf16[value[15]]) != 0xFF)
                        {
                            resultPtr[4] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                            // 5 byte
                            if (value[16] < MaximalCharUtf16
                                && (hexByteHi = TableFromHexToBytesUtf16[value[16]]) != 0xFF
                                && value[17] < MaximalCharUtf16
                                && (hexByteLow = TableFromHexToBytesUtf16[value[17]]) != 0xFF)
                            {
                                resultPtr[5] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                // value[18] == ','
                                // value[19] == '0'
                                // value[20] == 'x'

                                // 6 byte
                                if (value[21] < MaximalCharUtf16
                                    && (hexByteHi = TableFromHexToBytesUtf16[value[21]]) != 0xFF
                                    && value[22] < MaximalCharUtf16
                                    && (hexByteLow = TableFromHexToBytesUtf16[value[22]]) != 0xFF)
                                {
                                    resultPtr[6] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                                    // 7 byte
                                    if (value[23] < MaximalCharUtf16
                                        && (hexByteHi = TableFromHexToBytesUtf16[value[23]]) != 0xFF
                                        && value[24] < MaximalCharUtf16
                                        && (hexByteLow = TableFromHexToBytesUtf16[value[24]]) != 0xFF)
                                    {
                                        resultPtr[7] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                        // value[25] == ','
                                        // value[26] == '{'
                                        // value[27] == '0'
                                        // value[28] == 'x'

                                        // 8 byte
                                        if (value[29] < MaximalCharUtf16
                                            && (hexByteHi = TableFromHexToBytesUtf16[value[29]]) != 0xFF
                                            && value[30] < MaximalCharUtf16
                                            && (hexByteLow = TableFromHexToBytesUtf16[value[30]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                            // value[31] == ','
                                            // value[32] == '0'
                                            // value[33] == 'x'

                                            // 9 byte
                                            if (value[34] < MaximalCharUtf16
                                                && (hexByteHi = TableFromHexToBytesUtf16[value[34]]) != 0xFF
                                                && value[35] < MaximalCharUtf16
                                                && (hexByteLow = TableFromHexToBytesUtf16[value[35]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                // value[36] == ','
                                                // value[37] == '0'
                                                // value[38] == 'x'

                                                // 10 byte
                                                if (value[39] < MaximalCharUtf16
                                                    && (hexByteHi = TableFromHexToBytesUtf16[value[39]]) != 0xFF
                                                    && value[40] < MaximalCharUtf16
                                                    && (hexByteLow = TableFromHexToBytesUtf16[value[40]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                    // value[41] == ','
                                                    // value[42] == '0'
                                                    // value[43] == 'x'

                                                    // 11 byte
                                                    if (value[44] < MaximalCharUtf16
                                                        && (hexByteHi = TableFromHexToBytesUtf16[value[44]]) != 0xFF
                                                        && value[45] < MaximalCharUtf16
                                                        && (hexByteLow = TableFromHexToBytesUtf16[value[45]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                        // value[46] == ','
                                                        // value[47] == '0'
                                                        // value[48] == 'x'

                                                        // 12 byte
                                                        if (value[49] < MaximalCharUtf16
                                                            && (hexByteHi = TableFromHexToBytesUtf16[value[49]]) != 0xFF
                                                            && value[50] < MaximalCharUtf16
                                                            && (hexByteLow = TableFromHexToBytesUtf16[value[50]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                            // value[51] == ','
                                                            // value[52] == '0'
                                                            // value[53] == 'x'

                                                            // 13 byte
                                                            if (value[54] < MaximalCharUtf16
                                                                && (hexByteHi = TableFromHexToBytesUtf16[value[54]]) != 0xFF
                                                                && value[55] < MaximalCharUtf16
                                                                && (hexByteLow = TableFromHexToBytesUtf16[value[55]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                // value[56] == ','
                                                                // value[57] == '0'
                                                                // value[58] == 'x'

                                                                // 14 byte
                                                                if (value[59] < MaximalCharUtf16
                                                                    && (hexByteHi = TableFromHexToBytesUtf16[value[59]]) != 0xFF
                                                                    && value[60] < MaximalCharUtf16
                                                                    && (hexByteLow = TableFromHexToBytesUtf16[value[60]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                    // value[61] == ','
                                                                    // value[62] == '0'
                                                                    // value[63] == 'x'

                                                                    // 15 byte
                                                                    if (value[64] < MaximalCharUtf16
                                                                        && (hexByteHi = TableFromHexToBytesUtf16[value[64]]) != 0xFF
                                                                        && value[65] < MaximalCharUtf16
                                                                        && (hexByteLow = TableFromHexToBytesUtf16[value[65]]) != 0xFF)
                                                                    {
                                                                        resultPtr[15] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    //
    // IComparisonOperators
    //
#if NET8_0_OR_GREATER
    /// <inheritdoc cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_LessThan(TSelf, TOther)" />
#else
    /// <summary>
    ///     Compares two values to determine which is less.
    /// </summary>
    /// <param name="left">The value to compare with <paramref name="right" />.</param>
    /// <param name="right">The value to compare with <paramref name="left" />.</param>
    /// <returns><see langword="true" /> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <see langword="false" />.</returns>
#endif
    public static bool operator <(Uuid left, Uuid right)
    {
        if (left._byte0 != right._byte0)
        {
            return left._byte0 < right._byte0;
        }

        if (left._byte1 != right._byte1)
        {
            return left._byte1 < right._byte1;
        }

        if (left._byte2 != right._byte2)
        {
            return left._byte2 < right._byte2;
        }

        if (left._byte3 != right._byte3)
        {
            return left._byte3 < right._byte3;
        }

        if (left._byte4 != right._byte4)
        {
            return left._byte4 < right._byte4;
        }

        if (left._byte5 != right._byte5)
        {
            return left._byte5 < right._byte5;
        }

        if (left._byte6 != right._byte6)
        {
            return left._byte6 < right._byte6;
        }

        if (left._byte7 != right._byte7)
        {
            return left._byte7 < right._byte7;
        }

        if (left._byte8 != right._byte8)
        {
            return left._byte8 < right._byte8;
        }

        if (left._byte9 != right._byte9)
        {
            return left._byte9 < right._byte9;
        }

        if (left._byte10 != right._byte10)
        {
            return left._byte10 < right._byte10;
        }

        if (left._byte11 != right._byte11)
        {
            return left._byte11 < right._byte11;
        }

        if (left._byte12 != right._byte12)
        {
            return left._byte12 < right._byte12;
        }

        if (left._byte13 != right._byte13)
        {
            return left._byte13 < right._byte13;
        }

        if (left._byte14 != right._byte14)
        {
            return left._byte14 < right._byte14;
        }

        if (left._byte15 != right._byte15)
        {
            return left._byte15 < right._byte15;
        }

        return false;
    }

#if NET8_0_OR_GREATER
    /// <inheritdoc cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_LessThanOrEqual(TSelf, TOther)" />
#else
    /// <summary>
    ///     Compares two values to determine which is less or equal.
    /// </summary>
    /// <param name="left">The value to compare with <paramref name="right" />.</param>
    /// <param name="right">The value to compare with <paramref name="left" />.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise,
    ///     <see langword="false" />.
    /// </returns>
#endif
    public static bool operator <=(Uuid left, Uuid right)
    {
        if (left._byte0 != right._byte0)
        {
            return left._byte0 < right._byte0;
        }

        if (left._byte1 != right._byte1)
        {
            return left._byte1 < right._byte1;
        }

        if (left._byte2 != right._byte2)
        {
            return left._byte2 < right._byte2;
        }

        if (left._byte3 != right._byte3)
        {
            return left._byte3 < right._byte3;
        }

        if (left._byte4 != right._byte4)
        {
            return left._byte4 < right._byte4;
        }

        if (left._byte5 != right._byte5)
        {
            return left._byte5 < right._byte5;
        }

        if (left._byte6 != right._byte6)
        {
            return left._byte6 < right._byte6;
        }

        if (left._byte7 != right._byte7)
        {
            return left._byte7 < right._byte7;
        }

        if (left._byte8 != right._byte8)
        {
            return left._byte8 < right._byte8;
        }

        if (left._byte9 != right._byte9)
        {
            return left._byte9 < right._byte9;
        }

        if (left._byte10 != right._byte10)
        {
            return left._byte10 < right._byte10;
        }

        if (left._byte11 != right._byte11)
        {
            return left._byte11 < right._byte11;
        }

        if (left._byte12 != right._byte12)
        {
            return left._byte12 < right._byte12;
        }

        if (left._byte13 != right._byte13)
        {
            return left._byte13 < right._byte13;
        }

        if (left._byte14 != right._byte14)
        {
            return left._byte14 < right._byte14;
        }

        if (left._byte15 != right._byte15)
        {
            return left._byte15 < right._byte15;
        }

        return true;
    }

#if NET8_0_OR_GREATER
    /// <inheritdoc cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThan(TSelf, TOther)" />
#else
    /// <summary>
    ///     Compares two values to determine which is greater.
    /// </summary>
    /// <param name="left">The value to compare with <paramref name="right" />.</param>
    /// <param name="right">The value to compare with <paramref name="left" />.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <see langword="false" />
    ///     .
    /// </returns>
#endif
    public static bool operator >(Uuid left, Uuid right)
    {
        if (left._byte0 != right._byte0)
        {
            return left._byte0 > right._byte0;
        }

        if (left._byte1 != right._byte1)
        {
            return left._byte1 > right._byte1;
        }

        if (left._byte2 != right._byte2)
        {
            return left._byte2 > right._byte2;
        }

        if (left._byte3 != right._byte3)
        {
            return left._byte3 > right._byte3;
        }

        if (left._byte4 != right._byte4)
        {
            return left._byte4 > right._byte4;
        }

        if (left._byte5 != right._byte5)
        {
            return left._byte5 > right._byte5;
        }

        if (left._byte6 != right._byte6)
        {
            return left._byte6 > right._byte6;
        }

        if (left._byte7 != right._byte7)
        {
            return left._byte7 > right._byte7;
        }

        if (left._byte8 != right._byte8)
        {
            return left._byte8 > right._byte8;
        }

        if (left._byte9 != right._byte9)
        {
            return left._byte9 > right._byte9;
        }

        if (left._byte10 != right._byte10)
        {
            return left._byte10 > right._byte10;
        }

        if (left._byte11 != right._byte11)
        {
            return left._byte11 > right._byte11;
        }

        if (left._byte12 != right._byte12)
        {
            return left._byte12 > right._byte12;
        }

        if (left._byte13 != right._byte13)
        {
            return left._byte13 > right._byte13;
        }

        if (left._byte14 != right._byte14)
        {
            return left._byte14 > right._byte14;
        }

        if (left._byte15 != right._byte15)
        {
            return left._byte15 > right._byte15;
        }

        return false;
    }

#if NET8_0_OR_GREATER
    /// <inheritdoc cref="System.Numerics.IComparisonOperators{TSelf, TOther, TResult}.op_GreaterThanOrEqual(TSelf, TOther)" />
#else
    /// <summary>
    ///     Compares two values to determine which is greater or equal.
    /// </summary>
    /// <param name="left">The value to compare with <paramref name="right" />.</param>
    /// <param name="right">The value to compare with <paramref name="left" />.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise,
    ///     <see langword="false" />.
    /// </returns>
#endif
    public static bool operator >=(Uuid left, Uuid right)
    {
        if (left._byte0 != right._byte0)
        {
            return left._byte0 > right._byte0;
        }

        if (left._byte1 != right._byte1)
        {
            return left._byte1 > right._byte1;
        }

        if (left._byte2 != right._byte2)
        {
            return left._byte2 > right._byte2;
        }

        if (left._byte3 != right._byte3)
        {
            return left._byte3 > right._byte3;
        }

        if (left._byte4 != right._byte4)
        {
            return left._byte4 > right._byte4;
        }

        if (left._byte5 != right._byte5)
        {
            return left._byte5 > right._byte5;
        }

        if (left._byte6 != right._byte6)
        {
            return left._byte6 > right._byte6;
        }

        if (left._byte7 != right._byte7)
        {
            return left._byte7 > right._byte7;
        }

        if (left._byte8 != right._byte8)
        {
            return left._byte8 > right._byte8;
        }

        if (left._byte9 != right._byte9)
        {
            return left._byte9 > right._byte9;
        }

        if (left._byte10 != right._byte10)
        {
            return left._byte10 > right._byte10;
        }

        if (left._byte11 != right._byte11)
        {
            return left._byte11 > right._byte11;
        }

        if (left._byte12 != right._byte12)
        {
            return left._byte12 > right._byte12;
        }

        if (left._byte13 != right._byte13)
        {
            return left._byte13 > right._byte13;
        }

        if (left._byte14 != right._byte14)
        {
            return left._byte14 > right._byte14;
        }

        if (left._byte15 != right._byte15)
        {
            return left._byte15 > right._byte15;
        }

        return true;
    }

    //
    // IParsable
    //
#if NET8_0_OR_GREATER
    /// <inheritdoc cref="IParsable{TSelf}.Parse(string, IFormatProvider?)" />
#else
    /// <summary>
    ///     Parses a string into a value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
    /// <exception cref="ArgumentNullException"><paramref name="s" /> is <see langword="null" />.</exception>
    /// <exception cref="FormatException"><paramref name="s" /> is not in the correct format.</exception>
#endif
    public static Uuid Parse(string s, IFormatProvider? provider)
    {
        return Parse(s);
    }

#if NET8_0_OR_GREATER
    /// <inheritdoc cref="IParsable{TSelf}.TryParse(string?, IFormatProvider?, out TSelf)" />
#else
    /// <summary>
    ///     Tries to parses a string into a value.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
    /// <param name="result">On return, contains the result of successfully parsing <paramref name="s" /> or an undefined value on failure.</param>
    /// <returns><see langword="true" /> if <paramref name="s" /> was successfully parsed; otherwise, <see langword="false" />.</returns>
#endif
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out Uuid result)
    {
        return TryParse(s, out result);
    }

    //
    // ISpanParsable
    //
#if NET8_0_OR_GREATER
    /// <inheritdoc cref="ISpanParsable{TSelf}.Parse(ReadOnlySpan{char}, IFormatProvider?)" />
#else
    /// <summary>
    ///     Parses a span of characters into a value.
    /// </summary>
    /// <param name="s">The span of characters to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
    /// <exception cref="FormatException"><paramref name="s" /> is not in a recognized format.</exception>
#endif
    public static Uuid Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        return Parse(s);
    }

#if NET8_0_OR_GREATER
    /// <inheritdoc cref="ISpanParsable{TSelf}.TryParse(ReadOnlySpan{char}, IFormatProvider?, out TSelf)" />
#else
    /// <summary>
    ///     Tries to parses a span of characters into a value.
    /// </summary>
    /// <param name="s">The span of characters to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
    /// <param name="result">On return, contains the result of successfully parsing <paramref name="s" /> or an undefined value on failure.</param>
    /// <returns><see langword="true" /> if <paramref name="s" /> was successfully parsed; otherwise, <see langword="false" />.</returns>
#endif
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Uuid result)
    {
        return TryParse(s, out result);
    }

    #region Generator

    private const long ChristianCalendarGregorianReformTicksDate = 499_163_040_000_000_000L;

    private const byte ResetVersionMask = 0b0000_1111;
    private const byte Version1Flag = 0b0001_0000;

    private const byte ResetReservedMask = 0b0011_1111;
    private const byte ReservedFlag = 0b1000_0000;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Uuid" /> structure that represents Uuid v1 (RFC4122).
    /// </summary>
    /// <returns></returns>
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
        return new Uuid(resultPtr);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Uuid" /> structure that works the same way as UUID_TO_BIN(UUID(), 1) from MySQL 8.0.
    /// </summary>
    /// <returns></returns>
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
        return new Uuid(resultPtr);
    }

    #endregion
}
