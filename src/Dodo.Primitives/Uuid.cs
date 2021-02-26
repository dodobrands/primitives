using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Dodo.Primitives.Internal;

namespace Dodo.Primitives
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    [TypeConverter(typeof(UuidTypeConverter))]
    [JsonConverter(typeof(SystemTextJsonUuidJsonConverter))]
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    public unsafe partial struct Uuid : IFormattable, IComparable, IComparable<Uuid>, IEquatable<Uuid>
    {
        static Uuid()
        {
            TableToHex = InternalHexTables.TableToHex;
            TableFromHexToBytes = InternalHexTables.TableFromHexToBytes;
        }

        private const ushort MaximalChar = InternalHexTables.MaximalChar;

        private static readonly uint* TableToHex;
        private static readonly byte* TableFromHexToBytes;

        [FieldOffset(0)]
        private readonly byte _byte0;

        [FieldOffset(1)]
        private readonly byte _byte1;

        [FieldOffset(2)]
        private readonly byte _byte2;

        [FieldOffset(3)]
        private readonly byte _byte3;

        [FieldOffset(4)]
        private readonly byte _byte4;

        [FieldOffset(5)]
        private readonly byte _byte5;

        [FieldOffset(6)]
        private readonly byte _byte6;

        [FieldOffset(7)]
        private readonly byte _byte7;

        [FieldOffset(8)]
        private readonly byte _byte8;

        [FieldOffset(9)]
        private readonly byte _byte9;

        [FieldOffset(10)]
        private readonly byte _byte10;

        [FieldOffset(11)]
        private readonly byte _byte11;

        [FieldOffset(12)]
        private readonly byte _byte12;

        [FieldOffset(13)]
        private readonly byte _byte13;

        [FieldOffset(14)]
        private readonly byte _byte14;

        [FieldOffset(15)]
        private readonly byte _byte15;

        [FieldOffset(0)]
        private readonly ulong _ulong0;

        [FieldOffset(8)]
        private readonly ulong _ulong8;

        [FieldOffset(0)]
        private readonly int _int0;

        [FieldOffset(4)]
        private readonly int _int4;

        [FieldOffset(8)]
        private readonly int _int8;

        [FieldOffset(12)]
        private readonly int _int12;

        // ReSharper disable once RedundantDefaultMemberInitializer
        // ReSharper disable once MemberCanBePrivate.Global
        public static readonly Uuid Empty = new();

        public Uuid(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (bytes.Length != 16)
            {
                throw new ArgumentException("Byte array for Uuid must be exactly 16 bytes long.", nameof(bytes));
            }

            this = Unsafe.ReadUnaligned<Uuid>(ref MemoryMarshal.GetReference(new ReadOnlySpan<byte>(bytes)));
        }

        public Uuid(byte* bytes)
        {
            this = Unsafe.ReadUnaligned<Uuid>(bytes);
        }

        public Uuid(ReadOnlySpan<byte> bytes)
        {
            if (bytes.Length != 16)
            {
                throw new ArgumentException("Byte array for Uuid must be exactly 16 bytes long.", nameof(bytes));
            }

            this = Unsafe.ReadUnaligned<Uuid>(ref MemoryMarshal.GetReference(bytes));
        }

        public byte[] ToByteArray()
        {
            var result = new byte[16];
            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(new Span<byte>(result)), this);
            return result;
        }

        public bool TryWriteBytes(Span<byte> destination)
        {
            if (Unsafe.SizeOf<Uuid>() > destination.Length)
            {
                return false;
            }

            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), this);
            return true;
        }

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

        // Do not change that code syntax (do not merge checks, do not remove else) - perf critical
        [SuppressMessage("ReSharper", "MergeSequentialChecks")]
        [SuppressMessage("ReSharper", "RedundantIfElseBlock")]
        public override bool Equals(object? obj)
        {
            Uuid other;
            if (obj == null || !(obj is Uuid))
            {
                return false;
            }
            else
            {
                other = (Uuid) obj;
            }

            return _ulong0 == other._ulong0 && _ulong8 == other._ulong8;
        }

        public bool Equals(Uuid other)
        {
            return _ulong0 == other._ulong0 && _ulong8 == other._ulong8;
        }

        public override int GetHashCode()
        {
            return _int0 ^ _int4 ^ _int8 ^ _int12;
        }

        public static bool operator ==(Uuid left, Uuid right)
        {
            return left._ulong0 == right._ulong0 && left._ulong8 == right._ulong8;
        }

        public static bool operator !=(Uuid left, Uuid right)
        {
            return left._ulong0 != right._ulong0 || left._ulong8 != right._ulong8;
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default)
        {
            if (format.Length == 0)
            {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                format = "N";
#endif
#if NETSTANDARD2_0
                format = "N".AsSpan();
#endif
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
                        FormatN(uuidChars);
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
                        FormatD(uuidChars);
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
                        FormatB(uuidChars);
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
                        FormatP(uuidChars);
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
                        FormatX(uuidChars);
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

        public override string ToString()
        {
            return ToString("N", null);
        }

        public string ToString(string? format)
        {
            // ReSharper disable once IntroduceOptionalParameters.Global
            return ToString(format, null);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (format == null)
            {
                format = "N";
            }

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
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidChars = uuidString)
#endif
                    {
                        FormatN(uuidChars);
                    }

                    return uuidString;
                }
                case 'd':
                {
                    var uuidString = new string('\0', 36);
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidChars = uuidString)
#endif
                    {
                        FormatD(uuidChars);
                    }

                    return uuidString;
                }
                case 'b':
                {
                    var uuidString = new string('\0', 38);
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidChars = uuidString)
#endif
                    {
                        FormatB(uuidChars);
                    }

                    return uuidString;
                }
                case 'p':
                {
                    var uuidString = new string('\0', 38);
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidChars = uuidString)
#endif
                    {
                        FormatP(uuidChars);
                    }

                    return uuidString;
                }
                case 'x':
                {
                    var uuidString = new string('\0', 68);
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidChars = &uuidString.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidChars = uuidString)
#endif
                    {
                        FormatX(uuidChars);
                    }

                    return uuidString;
                }
                default:
                    throw new FormatException(
                        "Format string can be only \"N\", \"n\", \"D\", \"d\", \"P\", \"p\", \"B\", \"b\", \"X\" or \"x\".");
            }
        }
#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void FormatN(char* dest)
        {
            // dddddddddddddddddddddddddddddddd
            var destUints = (uint*) dest;
            destUints[0] = TableToHex[_byte0];
            destUints[1] = TableToHex[_byte1];
            destUints[2] = TableToHex[_byte2];
            destUints[3] = TableToHex[_byte3];
            destUints[4] = TableToHex[_byte4];
            destUints[5] = TableToHex[_byte5];
            destUints[6] = TableToHex[_byte6];
            destUints[7] = TableToHex[_byte7];
            destUints[8] = TableToHex[_byte8];
            destUints[9] = TableToHex[_byte9];
            destUints[10] = TableToHex[_byte10];
            destUints[11] = TableToHex[_byte11];
            destUints[12] = TableToHex[_byte12];
            destUints[13] = TableToHex[_byte13];
            destUints[14] = TableToHex[_byte14];
            destUints[15] = TableToHex[_byte15];
        }

#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void FormatD(char* dest)
        {
            // dddddddd-dddd-dddd-dddd-dddddddddddd
            var destUints = (uint*) dest;
            var destUintsAsChars = (char**) &destUints;
            dest[8] = dest[13] = dest[18] = dest[23] = '-';
            destUints[0] = TableToHex[_byte0];
            destUints[1] = TableToHex[_byte1];
            destUints[2] = TableToHex[_byte2];
            destUints[3] = TableToHex[_byte3];
            destUints[7] = TableToHex[_byte6];
            destUints[8] = TableToHex[_byte7];
            destUints[12] = TableToHex[_byte10];
            destUints[13] = TableToHex[_byte11];
            destUints[14] = TableToHex[_byte12];
            destUints[15] = TableToHex[_byte13];
            destUints[16] = TableToHex[_byte14];
            destUints[17] = TableToHex[_byte15];
            *destUintsAsChars += 1;
            destUints[4] = TableToHex[_byte4];
            destUints[5] = TableToHex[_byte5];
            destUints[9] = TableToHex[_byte8];
            destUints[10] = TableToHex[_byte9];
        }

#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void FormatB(char* dest)
        {
            // {dddddddd-dddd-dddd-dddd-dddddddddddd}
            var destUints = (uint*) dest;
            var destUintsAsChars = (char**) &destUints;
            dest[0] = '{';
            dest[9] = dest[14] = dest[19] = dest[24] = '-';
            dest[37] = '}';
            destUints[5] = TableToHex[_byte4];
            destUints[6] = TableToHex[_byte5];
            destUints[10] = TableToHex[_byte8];
            destUints[11] = TableToHex[_byte9];
            *destUintsAsChars += 1;
            destUints[0] = TableToHex[_byte0];
            destUints[1] = TableToHex[_byte1];
            destUints[2] = TableToHex[_byte2];
            destUints[3] = TableToHex[_byte3];
            destUints[7] = TableToHex[_byte6];
            destUints[8] = TableToHex[_byte7];
            destUints[12] = TableToHex[_byte10];
            destUints[13] = TableToHex[_byte11];
            destUints[14] = TableToHex[_byte12];
            destUints[15] = TableToHex[_byte13];
            destUints[16] = TableToHex[_byte14];
            destUints[17] = TableToHex[_byte15];
        }

#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void FormatP(char* dest)
        {
            // (dddddddd-dddd-dddd-dddd-dddddddddddd)
            var destUints = (uint*) dest;
            var destUintsAsChars = (char**) &destUints;
            dest[0] = '(';
            dest[9] = dest[14] = dest[19] = dest[24] = '-';
            dest[37] = ')';
            destUints[5] = TableToHex[_byte4];
            destUints[6] = TableToHex[_byte5];
            destUints[10] = TableToHex[_byte8];
            destUints[11] = TableToHex[_byte9];
            *destUintsAsChars += 1;
            destUints[0] = TableToHex[_byte0];
            destUints[1] = TableToHex[_byte1];
            destUints[2] = TableToHex[_byte2];
            destUints[3] = TableToHex[_byte3];
            destUints[7] = TableToHex[_byte6];
            destUints[8] = TableToHex[_byte7];
            destUints[12] = TableToHex[_byte10];
            destUints[13] = TableToHex[_byte11];
            destUints[14] = TableToHex[_byte12];
            destUints[15] = TableToHex[_byte13];
            destUints[16] = TableToHex[_byte14];
            destUints[17] = TableToHex[_byte15];
        }

        private const uint ZeroX = 7864368; // 0x
        private const uint CommaBrace = 8060972; // ,{
        private const uint CloseBraces = 8192125; // }}

#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void FormatX(char* dest)
        {
            // {0xdddddddd,0xdddd,0xdddd,{0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd,0xdd}}
            var destUints = (uint*) dest;
            var uintDestAsChars = (char**) &destUints;
            dest[0] = '{';
            dest[11] = dest[18] = dest[31] = dest[36] = dest[41] = dest[46] = dest[51] = dest[56] = dest[61] = ',';
            destUints[6] = destUints[16] = destUints[21] = destUints[26] = destUints[31] = ZeroX; // 0x
            destUints[7] = TableToHex[_byte4];
            destUints[8] = TableToHex[_byte5];
            destUints[17] = TableToHex[_byte9];
            destUints[22] = TableToHex[_byte11];
            destUints[27] = TableToHex[_byte13];
            destUints[32] = TableToHex[_byte15];
            destUints[33] = CloseBraces; // }}
            *uintDestAsChars += 1;
            destUints[0] = destUints[9] = destUints[13] = destUints[18] = destUints[23] = destUints[28] = ZeroX; // 0x
            destUints[1] = TableToHex[_byte0];
            destUints[2] = TableToHex[_byte1];
            destUints[3] = TableToHex[_byte2];
            destUints[4] = TableToHex[_byte3];
            destUints[10] = TableToHex[_byte6];
            destUints[11] = TableToHex[_byte7];
            destUints[12] = CommaBrace; // ,{
            destUints[14] = TableToHex[_byte8];
            destUints[19] = TableToHex[_byte10];
            destUints[24] = TableToHex[_byte12];
            destUints[29] = TableToHex[_byte14];
        }

        /// <summary>
        ///     Converts <see cref="Dodo.Primitives.Uuid" /> to <see cref="System.Guid" /> preserve same binary representation.
        /// </summary>
        /// <returns></returns>
#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.NoInlining)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.NoInlining)]
#endif
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
#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.NoInlining)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.NoInlining)]
#endif
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

        public Uuid(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var result = new Uuid();
            var resultPtr = (byte*) &result;
#if NETCOREAPP3_1 || NET5_0 || NET6_0
            fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
            fixed (char* uuidStringPtr = input)
#endif
            {
                ParseWithExceptions(new ReadOnlySpan<char>(uuidStringPtr, input.Length), uuidStringPtr, resultPtr);
            }

            this = result;
        }

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

        public static Uuid Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var result = new Uuid();
            var resultPtr = (byte*) &result;
#if NETCOREAPP3_1 || NET5_0 || NET6_0
            fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
            fixed (char* uuidStringPtr = input)
#endif
            {
                ParseWithExceptions(new ReadOnlySpan<char>(uuidStringPtr, input.Length), uuidStringPtr, resultPtr);
            }

            return result;
        }

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

        public static Uuid ParseExact(string input, string format)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (format == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var result = new Uuid();
            var resultPtr = (byte*) &result;
            switch ((char) (format[0] | 0x20))
            {
                case 'n':
                {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
                    {
                        ParseWithExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
                case 'd':
                {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
                    {
                        ParseWithExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
                case 'b':
                {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
                    {
                        ParseWithExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
                case 'p':
                {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
                    {
                        ParseWithExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    return result;
                }
                case 'x':
                {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
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

        public static Uuid ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format)
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

        public static bool TryParse(string? input, out Uuid output)
        {
            if (input == null)
            {
                output = default;
                return false;
            }

            var result = new Uuid();
            var resultPtr = (byte*) &result;
#if NETCOREAPP3_1 || NET5_0 || NET6_0
            fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
            fixed (char* uuidStringPtr = input)
#endif
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


        public static bool TryParseExact(string? input, string format, out Uuid output)
        {
            if (input == null || format?.Length != 1)
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
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
                    {
                        parsed = ParseWithoutExceptionsD((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
                case 'n':
                {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
                    {
                        parsed = ParseWithoutExceptionsN((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
                case 'b':
                {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
                    {
                        parsed = ParseWithoutExceptionsB((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
                case 'p':
                {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
                    {
                        parsed = ParseWithoutExceptionsP((uint) input.Length, uuidStringPtr, resultPtr);
                    }

                    break;
                }
                case 'x':
                {
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    fixed (char* uuidStringPtr = &input.GetPinnableReference())
#endif
#if NETSTANDARD2_0
                    fixed (char* uuidStringPtr = input)
#endif
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

        public static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, out Uuid output)
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
#if NETCOREAPP3_1 || NET5_0 || NET6_0
                    return uuidUtf8String.Contains(Utf8HyphenMinus)
                        ? ParseWithoutExceptionsBUtf8(length, uuidUtf8StringPtr, resultPtr)
                        : ParseWithoutExceptionsXUtf8(length, uuidUtf8StringPtr, resultPtr);
#endif
#if NETSTANDARD2_0
                    return uuidUtf8String.IndexOf(Utf8HyphenMinus) >= 0
                        ? ParseWithoutExceptionsBUtf8(length, uuidUtf8StringPtr, resultPtr)
                        : ParseWithoutExceptionsXUtf8(length, uuidUtf8StringPtr, resultPtr);
#endif
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
#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool TryParsePtrN(char* value, byte* resultPtr)
        {
            // e.g. "d85b1407351d4694939203acc5870eb1"
            byte hi;
            byte lo;
            // 0 byte
            if (value[0] < MaximalChar
                && (hi = TableFromHexToBytes[value[0]]) != 0xFF
                && value[1] < MaximalChar
                && (lo = TableFromHexToBytes[value[1]]) != 0xFF)
            {
                resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
                // 1 byte
                if (value[2] < MaximalChar
                    && (hi = TableFromHexToBytes[value[2]]) != 0xFF
                    && value[3] < MaximalChar
                    && (lo = TableFromHexToBytes[value[3]]) != 0xFF)
                {
                    resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                    // 2 byte
                    if (value[4] < MaximalChar
                        && (hi = TableFromHexToBytes[value[4]]) != 0xFF
                        && value[5] < MaximalChar
                        && (lo = TableFromHexToBytes[value[5]]) != 0xFF)
                    {
                        resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                        // 3 byte
                        if (value[6] < MaximalChar
                            && (hi = TableFromHexToBytes[value[6]]) != 0xFF
                            && value[7] < MaximalChar
                            && (lo = TableFromHexToBytes[value[7]]) != 0xFF)
                        {
                            resultPtr[3] = (byte) ((byte) (hi << 4) | lo);
                            // 4 byte
                            if (value[8] < MaximalChar
                                && (hi = TableFromHexToBytes[value[8]]) != 0xFF
                                && value[9] < MaximalChar
                                && (lo = TableFromHexToBytes[value[9]]) != 0xFF)
                            {
                                resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                                // 5 byte
                                if (value[10] < MaximalChar
                                    && (hi = TableFromHexToBytes[value[10]]) != 0xFF
                                    && value[11] < MaximalChar
                                    && (lo = TableFromHexToBytes[value[11]]) != 0xFF)
                                {
                                    resultPtr[5] = (byte) ((byte) (hi << 4) | lo);
                                    // 6 byte
                                    if (value[12] < MaximalChar
                                        && (hi = TableFromHexToBytes[value[12]]) != 0xFF
                                        && value[13] < MaximalChar
                                        && (lo = TableFromHexToBytes[value[13]]) != 0xFF)
                                    {
                                        resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                        // 7 byte
                                        if (value[14] < MaximalChar
                                            && (hi = TableFromHexToBytes[value[14]]) != 0xFF
                                            && value[15] < MaximalChar
                                            && (lo = TableFromHexToBytes[value[15]]) != 0xFF)
                                        {
                                            resultPtr[7] = (byte) ((byte) (hi << 4) | lo);
                                            // 8 byte
                                            if (value[16] < MaximalChar
                                                && (hi = TableFromHexToBytes[value[16]]) != 0xFF
                                                && value[17] < MaximalChar
                                                && (lo = TableFromHexToBytes[value[17]]) != 0xFF)
                                            {
                                                resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                                // 9 byte
                                                if (value[18] < MaximalChar
                                                    && (hi = TableFromHexToBytes[value[18]]) != 0xFF
                                                    && value[19] < MaximalChar
                                                    && (lo = TableFromHexToBytes[value[19]]) != 0xFF)
                                                {
                                                    resultPtr[9] = (byte) ((byte) (hi << 4) | lo);
                                                    // 10 byte
                                                    if (value[20] < MaximalChar
                                                        && (hi = TableFromHexToBytes[value[20]]) != 0xFF
                                                        && value[21] < MaximalChar
                                                        && (lo = TableFromHexToBytes[value[21]]) != 0xFF)
                                                    {
                                                        resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                        // 11 byte
                                                        if (value[22] < MaximalChar
                                                            && (hi = TableFromHexToBytes[value[22]]) != 0xFF
                                                            && value[23] < MaximalChar
                                                            && (lo = TableFromHexToBytes[value[23]]) != 0xFF)
                                                        {
                                                            resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                            // 12 byte
                                                            if (value[24] < MaximalChar
                                                                && (hi = TableFromHexToBytes[value[24]]) != 0xFF
                                                                && value[25] < MaximalChar
                                                                && (lo = TableFromHexToBytes[value[25]]) != 0xFF)
                                                            {
                                                                resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                                // 13 byte
                                                                if (value[26] < MaximalChar
                                                                    && (hi = TableFromHexToBytes[value[26]]) != 0xFF
                                                                    && value[27] < MaximalChar
                                                                    && (lo = TableFromHexToBytes[value[27]]) != 0xFF)
                                                                {
                                                                    resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 14 byte
                                                                    if (value[28] < MaximalChar
                                                                        && (hi = TableFromHexToBytes[value[28]]) != 0xFF
                                                                        && value[29] < MaximalChar
                                                                        && (lo = TableFromHexToBytes[value[29]]) != 0xFF)
                                                                    {
                                                                        resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                        // 15 byte
                                                                        if (value[30] < MaximalChar
                                                                            && (hi = TableFromHexToBytes[value[30]]) != 0xFF
                                                                            && value[31] < MaximalChar
                                                                            && (lo = TableFromHexToBytes[value[31]]) != 0xFF)
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

#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool TryParsePtrD(char* value, byte* resultPtr)
        {
            // e.g. "d85b1407-351d-4694-9392-03acc5870eb1"
            byte hi;
            byte lo;
            // 0 byte
            if (value[0] < MaximalChar
                && (hi = TableFromHexToBytes[value[0]]) != 0xFF
                && value[1] < MaximalChar
                && (lo = TableFromHexToBytes[value[1]]) != 0xFF)
            {
                resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
                // 1 byte
                if (value[2] < MaximalChar
                    && (hi = TableFromHexToBytes[value[2]]) != 0xFF
                    && value[3] < MaximalChar
                    && (lo = TableFromHexToBytes[value[3]]) != 0xFF)
                {
                    resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                    // 2 byte
                    if (value[4] < MaximalChar
                        && (hi = TableFromHexToBytes[value[4]]) != 0xFF
                        && value[5] < MaximalChar
                        && (lo = TableFromHexToBytes[value[5]]) != 0xFF)
                    {
                        resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                        // 3 byte
                        if (value[6] < MaximalChar
                            && (hi = TableFromHexToBytes[value[6]]) != 0xFF
                            && value[7] < MaximalChar
                            && (lo = TableFromHexToBytes[value[7]]) != 0xFF)
                        {
                            resultPtr[3] = (byte) ((byte) (hi << 4) | lo);

                            // value[8] == '-'

                            // 4 byte
                            if (value[9] < MaximalChar
                                && (hi = TableFromHexToBytes[value[9]]) != 0xFF
                                && value[10] < MaximalChar
                                && (lo = TableFromHexToBytes[value[10]]) != 0xFF)
                            {
                                resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                                // 5 byte
                                if (value[11] < MaximalChar
                                    && (hi = TableFromHexToBytes[value[11]]) != 0xFF
                                    && value[12] < MaximalChar
                                    && (lo = TableFromHexToBytes[value[12]]) != 0xFF)
                                {
                                    resultPtr[5] = (byte) ((byte) (hi << 4) | lo);

                                    // value[13] == '-'

                                    // 6 byte
                                    if (value[14] < MaximalChar
                                        && (hi = TableFromHexToBytes[value[14]]) != 0xFF
                                        && value[15] < MaximalChar
                                        && (lo = TableFromHexToBytes[value[15]]) != 0xFF)
                                    {
                                        resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                        // 7 byte
                                        if (value[16] < MaximalChar
                                            && (hi = TableFromHexToBytes[value[16]]) != 0xFF
                                            && value[17] < MaximalChar
                                            && (lo = TableFromHexToBytes[value[17]]) != 0xFF)
                                        {
                                            resultPtr[7] = (byte) ((byte) (hi << 4) | lo);

                                            // value[18] == '-'

                                            // 8 byte
                                            if (value[19] < MaximalChar
                                                && (hi = TableFromHexToBytes[value[19]]) != 0xFF
                                                && value[20] < MaximalChar
                                                && (lo = TableFromHexToBytes[value[20]]) != 0xFF)
                                            {
                                                resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                                // 9 byte
                                                if (value[21] < MaximalChar
                                                    && (hi = TableFromHexToBytes[value[21]]) != 0xFF
                                                    && value[22] < MaximalChar
                                                    && (lo = TableFromHexToBytes[value[22]]) != 0xFF)
                                                {
                                                    resultPtr[9] = (byte) ((byte) (hi << 4) | lo);

                                                    // value[23] == '-'

                                                    // 10 byte
                                                    if (value[24] < MaximalChar
                                                        && (hi = TableFromHexToBytes[value[24]]) != 0xFF
                                                        && value[25] < MaximalChar
                                                        && (lo = TableFromHexToBytes[value[25]]) != 0xFF)
                                                    {
                                                        resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                        // 11 byte
                                                        if (value[26] < MaximalChar
                                                            && (hi = TableFromHexToBytes[value[26]]) != 0xFF
                                                            && value[27] < MaximalChar
                                                            && (lo = TableFromHexToBytes[value[27]]) != 0xFF)
                                                        {
                                                            resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                            // 12 byte
                                                            if (value[28] < MaximalChar
                                                                && (hi = TableFromHexToBytes[value[28]]) != 0xFF
                                                                && value[29] < MaximalChar
                                                                && (lo = TableFromHexToBytes[value[29]]) != 0xFF)
                                                            {
                                                                resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                                // 13 byte
                                                                if (value[30] < MaximalChar
                                                                    && (hi = TableFromHexToBytes[value[30]]) != 0xFF
                                                                    && value[31] < MaximalChar
                                                                    && (lo = TableFromHexToBytes[value[31]]) != 0xFF)
                                                                {
                                                                    resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 14 byte
                                                                    if (value[32] < MaximalChar
                                                                        && (hi = TableFromHexToBytes[value[32]]) != 0xFF
                                                                        && value[33] < MaximalChar
                                                                        && (lo = TableFromHexToBytes[value[33]]) != 0xFF)
                                                                    {
                                                                        resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                        // 15 byte
                                                                        if (value[34] < MaximalChar
                                                                            && (hi = TableFromHexToBytes[value[34]]) != 0xFF
                                                                            && value[35] < MaximalChar
                                                                            && (lo = TableFromHexToBytes[value[35]]) != 0xFF)
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

#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool TryParsePtrX(char* value, byte* resultPtr)
        {
            // e.g. "{0xd85b1407,0x351d,0x4694,{0x93,0x92,0x03,0xac,0xc5,0x87,0x0e,0xb1}}"

            byte hexByteHi;
            byte hexByteLow;
            // value[0] == '{'
            // value[1] == '0'
            // value[2] == 'x'
            // 0 byte
            if (value[3] < MaximalChar
                && (hexByteHi = TableFromHexToBytes[value[3]]) != 0xFF
                && value[4] < MaximalChar
                && (hexByteLow = TableFromHexToBytes[value[4]]) != 0xFF)
            {
                resultPtr[0] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                // 1 byte
                if (value[5] < MaximalChar
                    && (hexByteHi = TableFromHexToBytes[value[5]]) != 0xFF
                    && value[6] < MaximalChar
                    && (hexByteLow = TableFromHexToBytes[value[6]]) != 0xFF)
                {
                    resultPtr[1] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                    // 2 byte
                    if (value[7] < MaximalChar
                        && (hexByteHi = TableFromHexToBytes[value[7]]) != 0xFF
                        && value[8] < MaximalChar
                        && (hexByteLow = TableFromHexToBytes[value[8]]) != 0xFF)
                    {
                        resultPtr[2] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                        // 3 byte
                        if (value[9] < MaximalChar
                            && (hexByteHi = TableFromHexToBytes[value[9]]) != 0xFF
                            && value[10] < MaximalChar
                            && (hexByteLow = TableFromHexToBytes[value[10]]) != 0xFF)
                        {
                            resultPtr[3] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                            // value[11] == ','
                            // value[12] == '0'
                            // value[13] == 'x'

                            // 4 byte
                            if (value[14] < MaximalChar
                                && (hexByteHi = TableFromHexToBytes[value[14]]) != 0xFF
                                && value[15] < MaximalChar
                                && (hexByteLow = TableFromHexToBytes[value[15]]) != 0xFF)
                            {
                                resultPtr[4] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                                // 5 byte
                                if (value[16] < MaximalChar
                                    && (hexByteHi = TableFromHexToBytes[value[16]]) != 0xFF
                                    && value[17] < MaximalChar
                                    && (hexByteLow = TableFromHexToBytes[value[17]]) != 0xFF)
                                {
                                    resultPtr[5] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                    // value[18] == ','
                                    // value[19] == '0'
                                    // value[20] == 'x'

                                    // 6 byte
                                    if (value[21] < MaximalChar
                                        && (hexByteHi = TableFromHexToBytes[value[21]]) != 0xFF
                                        && value[22] < MaximalChar
                                        && (hexByteLow = TableFromHexToBytes[value[22]]) != 0xFF)
                                    {
                                        resultPtr[6] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                                        // 7 byte
                                        if (value[23] < MaximalChar
                                            && (hexByteHi = TableFromHexToBytes[value[23]]) != 0xFF
                                            && value[24] < MaximalChar
                                            && (hexByteLow = TableFromHexToBytes[value[24]]) != 0xFF)
                                        {
                                            resultPtr[7] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                            // value[25] == ','
                                            // value[26] == '{'
                                            // value[27] == '0'
                                            // value[28] == 'x'

                                            // 8 byte
                                            if (value[29] < MaximalChar
                                                && (hexByteHi = TableFromHexToBytes[value[29]]) != 0xFF
                                                && value[30] < MaximalChar
                                                && (hexByteLow = TableFromHexToBytes[value[30]]) != 0xFF)
                                            {
                                                resultPtr[8] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                // value[31] == ','
                                                // value[32] == '0'
                                                // value[33] == 'x'

                                                // 9 byte
                                                if (value[34] < MaximalChar
                                                    && (hexByteHi = TableFromHexToBytes[value[34]]) != 0xFF
                                                    && value[35] < MaximalChar
                                                    && (hexByteLow = TableFromHexToBytes[value[35]]) != 0xFF)
                                                {
                                                    resultPtr[9] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                    // value[36] == ','
                                                    // value[37] == '0'
                                                    // value[38] == 'x'

                                                    // 10 byte
                                                    if (value[39] < MaximalChar
                                                        && (hexByteHi = TableFromHexToBytes[value[39]]) != 0xFF
                                                        && value[40] < MaximalChar
                                                        && (hexByteLow = TableFromHexToBytes[value[40]]) != 0xFF)
                                                    {
                                                        resultPtr[10] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                        // value[41] == ','
                                                        // value[42] == '0'
                                                        // value[43] == 'x'

                                                        // 11 byte
                                                        if (value[44] < MaximalChar
                                                            && (hexByteHi = TableFromHexToBytes[value[44]]) != 0xFF
                                                            && value[45] < MaximalChar
                                                            && (hexByteLow = TableFromHexToBytes[value[45]]) != 0xFF)
                                                        {
                                                            resultPtr[11] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                            // value[46] == ','
                                                            // value[47] == '0'
                                                            // value[48] == 'x'

                                                            // 12 byte
                                                            if (value[49] < MaximalChar
                                                                && (hexByteHi = TableFromHexToBytes[value[49]]) != 0xFF
                                                                && value[50] < MaximalChar
                                                                && (hexByteLow = TableFromHexToBytes[value[50]]) != 0xFF)
                                                            {
                                                                resultPtr[12] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                // value[51] == ','
                                                                // value[52] == '0'
                                                                // value[53] == 'x'

                                                                // 13 byte
                                                                if (value[54] < MaximalChar
                                                                    && (hexByteHi = TableFromHexToBytes[value[54]]) != 0xFF
                                                                    && value[55] < MaximalChar
                                                                    && (hexByteLow = TableFromHexToBytes[value[55]]) != 0xFF)
                                                                {
                                                                    resultPtr[13] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                    // value[56] == ','
                                                                    // value[57] == '0'
                                                                    // value[58] == 'x'

                                                                    // 14 byte
                                                                    if (value[59] < MaximalChar
                                                                        && (hexByteHi = TableFromHexToBytes[value[59]]) != 0xFF
                                                                        && value[60] < MaximalChar
                                                                        && (hexByteLow = TableFromHexToBytes[value[60]]) != 0xFF)
                                                                    {
                                                                        resultPtr[14] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                        // value[61] == ','
                                                                        // value[62] == '0'
                                                                        // value[63] == 'x'

                                                                        // 15 byte
                                                                        if (value[64] < MaximalChar
                                                                            && (hexByteHi = TableFromHexToBytes[value[64]]) != 0xFF
                                                                            && value[65] < MaximalChar
                                                                            && (hexByteLow = TableFromHexToBytes[value[65]]) != 0xFF)
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

#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool TryParsePtrNUtf8(byte* value, byte* resultPtr)
        {
            // e.g. "d85b1407351d4694939203acc5870eb1"
            byte hi;
            byte lo;
            // 0 byte
            if (value[0] < MaximalChar
                && (hi = TableFromHexToBytes[value[0]]) != 0xFF
                && value[1] < MaximalChar
                && (lo = TableFromHexToBytes[value[1]]) != 0xFF)
            {
                resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
                // 1 byte
                if (value[2] < MaximalChar
                    && (hi = TableFromHexToBytes[value[2]]) != 0xFF
                    && value[3] < MaximalChar
                    && (lo = TableFromHexToBytes[value[3]]) != 0xFF)
                {
                    resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                    // 2 byte
                    if (value[4] < MaximalChar
                        && (hi = TableFromHexToBytes[value[4]]) != 0xFF
                        && value[5] < MaximalChar
                        && (lo = TableFromHexToBytes[value[5]]) != 0xFF)
                    {
                        resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                        // 3 byte
                        if (value[6] < MaximalChar
                            && (hi = TableFromHexToBytes[value[6]]) != 0xFF
                            && value[7] < MaximalChar
                            && (lo = TableFromHexToBytes[value[7]]) != 0xFF)
                        {
                            resultPtr[3] = (byte) ((byte) (hi << 4) | lo);
                            // 4 byte
                            if (value[8] < MaximalChar
                                && (hi = TableFromHexToBytes[value[8]]) != 0xFF
                                && value[9] < MaximalChar
                                && (lo = TableFromHexToBytes[value[9]]) != 0xFF)
                            {
                                resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                                // 5 byte
                                if (value[10] < MaximalChar
                                    && (hi = TableFromHexToBytes[value[10]]) != 0xFF
                                    && value[11] < MaximalChar
                                    && (lo = TableFromHexToBytes[value[11]]) != 0xFF)
                                {
                                    resultPtr[5] = (byte) ((byte) (hi << 4) | lo);
                                    // 6 byte
                                    if (value[12] < MaximalChar
                                        && (hi = TableFromHexToBytes[value[12]]) != 0xFF
                                        && value[13] < MaximalChar
                                        && (lo = TableFromHexToBytes[value[13]]) != 0xFF)
                                    {
                                        resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                        // 7 byte
                                        if (value[14] < MaximalChar
                                            && (hi = TableFromHexToBytes[value[14]]) != 0xFF
                                            && value[15] < MaximalChar
                                            && (lo = TableFromHexToBytes[value[15]]) != 0xFF)
                                        {
                                            resultPtr[7] = (byte) ((byte) (hi << 4) | lo);
                                            // 8 byte
                                            if (value[16] < MaximalChar
                                                && (hi = TableFromHexToBytes[value[16]]) != 0xFF
                                                && value[17] < MaximalChar
                                                && (lo = TableFromHexToBytes[value[17]]) != 0xFF)
                                            {
                                                resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                                // 9 byte
                                                if (value[18] < MaximalChar
                                                    && (hi = TableFromHexToBytes[value[18]]) != 0xFF
                                                    && value[19] < MaximalChar
                                                    && (lo = TableFromHexToBytes[value[19]]) != 0xFF)
                                                {
                                                    resultPtr[9] = (byte) ((byte) (hi << 4) | lo);
                                                    // 10 byte
                                                    if (value[20] < MaximalChar
                                                        && (hi = TableFromHexToBytes[value[20]]) != 0xFF
                                                        && value[21] < MaximalChar
                                                        && (lo = TableFromHexToBytes[value[21]]) != 0xFF)
                                                    {
                                                        resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                        // 11 byte
                                                        if (value[22] < MaximalChar
                                                            && (hi = TableFromHexToBytes[value[22]]) != 0xFF
                                                            && value[23] < MaximalChar
                                                            && (lo = TableFromHexToBytes[value[23]]) != 0xFF)
                                                        {
                                                            resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                            // 12 byte
                                                            if (value[24] < MaximalChar
                                                                && (hi = TableFromHexToBytes[value[24]]) != 0xFF
                                                                && value[25] < MaximalChar
                                                                && (lo = TableFromHexToBytes[value[25]]) != 0xFF)
                                                            {
                                                                resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                                // 13 byte
                                                                if (value[26] < MaximalChar
                                                                    && (hi = TableFromHexToBytes[value[26]]) != 0xFF
                                                                    && value[27] < MaximalChar
                                                                    && (lo = TableFromHexToBytes[value[27]]) != 0xFF)
                                                                {
                                                                    resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 14 byte
                                                                    if (value[28] < MaximalChar
                                                                        && (hi = TableFromHexToBytes[value[28]]) != 0xFF
                                                                        && value[29] < MaximalChar
                                                                        && (lo = TableFromHexToBytes[value[29]]) != 0xFF)
                                                                    {
                                                                        resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                        // 15 byte
                                                                        if (value[30] < MaximalChar
                                                                            && (hi = TableFromHexToBytes[value[30]]) != 0xFF
                                                                            && value[31] < MaximalChar
                                                                            && (lo = TableFromHexToBytes[value[31]]) != 0xFF)
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

#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool TryParsePtrDUtf8(byte* value, byte* resultPtr)
        {
            // e.g. "d85b1407-351d-4694-9392-03acc5870eb1"
            byte hi;
            byte lo;
            // 0 byte
            if (value[0] < MaximalChar
                && (hi = TableFromHexToBytes[value[0]]) != 0xFF
                && value[1] < MaximalChar
                && (lo = TableFromHexToBytes[value[1]]) != 0xFF)
            {
                resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
                // 1 byte
                if (value[2] < MaximalChar
                    && (hi = TableFromHexToBytes[value[2]]) != 0xFF
                    && value[3] < MaximalChar
                    && (lo = TableFromHexToBytes[value[3]]) != 0xFF)
                {
                    resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                    // 2 byte
                    if (value[4] < MaximalChar
                        && (hi = TableFromHexToBytes[value[4]]) != 0xFF
                        && value[5] < MaximalChar
                        && (lo = TableFromHexToBytes[value[5]]) != 0xFF)
                    {
                        resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                        // 3 byte
                        if (value[6] < MaximalChar
                            && (hi = TableFromHexToBytes[value[6]]) != 0xFF
                            && value[7] < MaximalChar
                            && (lo = TableFromHexToBytes[value[7]]) != 0xFF)
                        {
                            resultPtr[3] = (byte) ((byte) (hi << 4) | lo);

                            // value[8] == '-'

                            // 4 byte
                            if (value[9] < MaximalChar
                                && (hi = TableFromHexToBytes[value[9]]) != 0xFF
                                && value[10] < MaximalChar
                                && (lo = TableFromHexToBytes[value[10]]) != 0xFF)
                            {
                                resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                                // 5 byte
                                if (value[11] < MaximalChar
                                    && (hi = TableFromHexToBytes[value[11]]) != 0xFF
                                    && value[12] < MaximalChar
                                    && (lo = TableFromHexToBytes[value[12]]) != 0xFF)
                                {
                                    resultPtr[5] = (byte) ((byte) (hi << 4) | lo);

                                    // value[13] == '-'

                                    // 6 byte
                                    if (value[14] < MaximalChar
                                        && (hi = TableFromHexToBytes[value[14]]) != 0xFF
                                        && value[15] < MaximalChar
                                        && (lo = TableFromHexToBytes[value[15]]) != 0xFF)
                                    {
                                        resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                        // 7 byte
                                        if (value[16] < MaximalChar
                                            && (hi = TableFromHexToBytes[value[16]]) != 0xFF
                                            && value[17] < MaximalChar
                                            && (lo = TableFromHexToBytes[value[17]]) != 0xFF)
                                        {
                                            resultPtr[7] = (byte) ((byte) (hi << 4) | lo);

                                            // value[18] == '-'

                                            // 8 byte
                                            if (value[19] < MaximalChar
                                                && (hi = TableFromHexToBytes[value[19]]) != 0xFF
                                                && value[20] < MaximalChar
                                                && (lo = TableFromHexToBytes[value[20]]) != 0xFF)
                                            {
                                                resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                                // 9 byte
                                                if (value[21] < MaximalChar
                                                    && (hi = TableFromHexToBytes[value[21]]) != 0xFF
                                                    && value[22] < MaximalChar
                                                    && (lo = TableFromHexToBytes[value[22]]) != 0xFF)
                                                {
                                                    resultPtr[9] = (byte) ((byte) (hi << 4) | lo);

                                                    // value[23] == '-'

                                                    // 10 byte
                                                    if (value[24] < MaximalChar
                                                        && (hi = TableFromHexToBytes[value[24]]) != 0xFF
                                                        && value[25] < MaximalChar
                                                        && (lo = TableFromHexToBytes[value[25]]) != 0xFF)
                                                    {
                                                        resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                        // 11 byte
                                                        if (value[26] < MaximalChar
                                                            && (hi = TableFromHexToBytes[value[26]]) != 0xFF
                                                            && value[27] < MaximalChar
                                                            && (lo = TableFromHexToBytes[value[27]]) != 0xFF)
                                                        {
                                                            resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                            // 12 byte
                                                            if (value[28] < MaximalChar
                                                                && (hi = TableFromHexToBytes[value[28]]) != 0xFF
                                                                && value[29] < MaximalChar
                                                                && (lo = TableFromHexToBytes[value[29]]) != 0xFF)
                                                            {
                                                                resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                                // 13 byte
                                                                if (value[30] < MaximalChar
                                                                    && (hi = TableFromHexToBytes[value[30]]) != 0xFF
                                                                    && value[31] < MaximalChar
                                                                    && (lo = TableFromHexToBytes[value[31]]) != 0xFF)
                                                                {
                                                                    resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 14 byte
                                                                    if (value[32] < MaximalChar
                                                                        && (hi = TableFromHexToBytes[value[32]]) != 0xFF
                                                                        && value[33] < MaximalChar
                                                                        && (lo = TableFromHexToBytes[value[33]]) != 0xFF)
                                                                    {
                                                                        resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                        // 15 byte
                                                                        if (value[34] < MaximalChar
                                                                            && (hi = TableFromHexToBytes[value[34]]) != 0xFF
                                                                            && value[35] < MaximalChar
                                                                            && (lo = TableFromHexToBytes[value[35]]) != 0xFF)
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

#if NETCOREAPP3_1 || NET5_0 || NET6_0
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
#endif
#if NETSTANDARD2_0
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static bool TryParsePtrXUtf8(byte* value, byte* resultPtr)
        {
            // e.g. "{0xd85b1407,0x351d,0x4694,{0x93,0x92,0x03,0xac,0xc5,0x87,0x0e,0xb1}}"

            byte hexByteHi;
            byte hexByteLow;
            // value[0] == '{'
            // value[1] == '0'
            // value[2] == 'x'
            // 0 byte
            if (value[3] < MaximalChar
                && (hexByteHi = TableFromHexToBytes[value[3]]) != 0xFF
                && value[4] < MaximalChar
                && (hexByteLow = TableFromHexToBytes[value[4]]) != 0xFF)
            {
                resultPtr[0] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                // 1 byte
                if (value[5] < MaximalChar
                    && (hexByteHi = TableFromHexToBytes[value[5]]) != 0xFF
                    && value[6] < MaximalChar
                    && (hexByteLow = TableFromHexToBytes[value[6]]) != 0xFF)
                {
                    resultPtr[1] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                    // 2 byte
                    if (value[7] < MaximalChar
                        && (hexByteHi = TableFromHexToBytes[value[7]]) != 0xFF
                        && value[8] < MaximalChar
                        && (hexByteLow = TableFromHexToBytes[value[8]]) != 0xFF)
                    {
                        resultPtr[2] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                        // 3 byte
                        if (value[9] < MaximalChar
                            && (hexByteHi = TableFromHexToBytes[value[9]]) != 0xFF
                            && value[10] < MaximalChar
                            && (hexByteLow = TableFromHexToBytes[value[10]]) != 0xFF)
                        {
                            resultPtr[3] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                            // value[11] == ','
                            // value[12] == '0'
                            // value[13] == 'x'

                            // 4 byte
                            if (value[14] < MaximalChar
                                && (hexByteHi = TableFromHexToBytes[value[14]]) != 0xFF
                                && value[15] < MaximalChar
                                && (hexByteLow = TableFromHexToBytes[value[15]]) != 0xFF)
                            {
                                resultPtr[4] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                                // 5 byte
                                if (value[16] < MaximalChar
                                    && (hexByteHi = TableFromHexToBytes[value[16]]) != 0xFF
                                    && value[17] < MaximalChar
                                    && (hexByteLow = TableFromHexToBytes[value[17]]) != 0xFF)
                                {
                                    resultPtr[5] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                    // value[18] == ','
                                    // value[19] == '0'
                                    // value[20] == 'x'

                                    // 6 byte
                                    if (value[21] < MaximalChar
                                        && (hexByteHi = TableFromHexToBytes[value[21]]) != 0xFF
                                        && value[22] < MaximalChar
                                        && (hexByteLow = TableFromHexToBytes[value[22]]) != 0xFF)
                                    {
                                        resultPtr[6] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                                        // 7 byte
                                        if (value[23] < MaximalChar
                                            && (hexByteHi = TableFromHexToBytes[value[23]]) != 0xFF
                                            && value[24] < MaximalChar
                                            && (hexByteLow = TableFromHexToBytes[value[24]]) != 0xFF)
                                        {
                                            resultPtr[7] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                            // value[25] == ','
                                            // value[26] == '{'
                                            // value[27] == '0'
                                            // value[28] == 'x'

                                            // 8 byte
                                            if (value[29] < MaximalChar
                                                && (hexByteHi = TableFromHexToBytes[value[29]]) != 0xFF
                                                && value[30] < MaximalChar
                                                && (hexByteLow = TableFromHexToBytes[value[30]]) != 0xFF)
                                            {
                                                resultPtr[8] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                // value[31] == ','
                                                // value[32] == '0'
                                                // value[33] == 'x'

                                                // 9 byte
                                                if (value[34] < MaximalChar
                                                    && (hexByteHi = TableFromHexToBytes[value[34]]) != 0xFF
                                                    && value[35] < MaximalChar
                                                    && (hexByteLow = TableFromHexToBytes[value[35]]) != 0xFF)
                                                {
                                                    resultPtr[9] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                    // value[36] == ','
                                                    // value[37] == '0'
                                                    // value[38] == 'x'

                                                    // 10 byte
                                                    if (value[39] < MaximalChar
                                                        && (hexByteHi = TableFromHexToBytes[value[39]]) != 0xFF
                                                        && value[40] < MaximalChar
                                                        && (hexByteLow = TableFromHexToBytes[value[40]]) != 0xFF)
                                                    {
                                                        resultPtr[10] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                        // value[41] == ','
                                                        // value[42] == '0'
                                                        // value[43] == 'x'

                                                        // 11 byte
                                                        if (value[44] < MaximalChar
                                                            && (hexByteHi = TableFromHexToBytes[value[44]]) != 0xFF
                                                            && value[45] < MaximalChar
                                                            && (hexByteLow = TableFromHexToBytes[value[45]]) != 0xFF)
                                                        {
                                                            resultPtr[11] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                            // value[46] == ','
                                                            // value[47] == '0'
                                                            // value[48] == 'x'

                                                            // 12 byte
                                                            if (value[49] < MaximalChar
                                                                && (hexByteHi = TableFromHexToBytes[value[49]]) != 0xFF
                                                                && value[50] < MaximalChar
                                                                && (hexByteLow = TableFromHexToBytes[value[50]]) != 0xFF)
                                                            {
                                                                resultPtr[12] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                // value[51] == ','
                                                                // value[52] == '0'
                                                                // value[53] == 'x'

                                                                // 13 byte
                                                                if (value[54] < MaximalChar
                                                                    && (hexByteHi = TableFromHexToBytes[value[54]]) != 0xFF
                                                                    && value[55] < MaximalChar
                                                                    && (hexByteLow = TableFromHexToBytes[value[55]]) != 0xFF)
                                                                {
                                                                    resultPtr[13] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                    // value[56] == ','
                                                                    // value[57] == '0'
                                                                    // value[58] == 'x'

                                                                    // 14 byte
                                                                    if (value[59] < MaximalChar
                                                                        && (hexByteHi = TableFromHexToBytes[value[59]]) != 0xFF
                                                                        && value[60] < MaximalChar
                                                                        && (hexByteLow = TableFromHexToBytes[value[60]]) != 0xFF)
                                                                    {
                                                                        resultPtr[14] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                        // value[61] == ','
                                                                        // value[62] == '0'
                                                                        // value[63] == 'x'

                                                                        // 15 byte
                                                                        if (value[64] < MaximalChar
                                                                            && (hexByteHi = TableFromHexToBytes[value[64]]) != 0xFF
                                                                            && value[65] < MaximalChar
                                                                            && (hexByteLow = TableFromHexToBytes[value[65]]) != 0xFF)
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
    }
}
