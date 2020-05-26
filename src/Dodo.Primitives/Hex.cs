using Dodo.Primitives.IL;
using Dodo.Primitives.Internal;

namespace Dodo.Primitives
{
    public static unsafe class Hex
    {
        private const ushort MaximalChar = InternalHexTables.MaximalChar;
        private static readonly uint* TableToHex;
        private static readonly byte* TableFromHexToBytes;

        static Hex()
        {
            TableToHex = InternalHexTables.TableToHex;
            TableFromHexToBytes = InternalHexTables.TableFromHexToBytes;
        }

        public static bool IsHexString(string? possibleHexString)
        {
            if (string.IsNullOrWhiteSpace(possibleHexString))
            {
                return false;
            }

            if (possibleHexString!.Length % 2 != 0)
            {
                return false;
            }

            int length = possibleHexString.Length;

            fixed (char* stringPtr = &possibleHexString.GetPinnableReference())
            {
                for (var i = 0; i < length;)
                {
                    if (stringPtr[i] < MaximalChar
                        && TableFromHexToBytes[stringPtr[i]] != 0xFF
                        && stringPtr[i + 1] < MaximalChar
                        && TableFromHexToBytes[stringPtr[i + 1]] != 0xFF)
                    {
                        i += 2;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static byte[]? GetBytes(string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return null;
            }

            if (hexString.Length % 2 != 0)
            {
                return null;
            }

            int length = hexString.Length;
            var result = new byte[length / 2];
            fixed (char* stringPtr = &hexString.GetPinnableReference())
            fixed (byte* resultPtr = &result[0])
            {
                var resultIndex = 0;
                for (var i = 0; i < length;)
                {
                    byte hexByteHi;
                    byte hexByteLow;
                    if (stringPtr[i] < MaximalChar
                        && (hexByteHi = TableFromHexToBytes[stringPtr[i]]) != 0xFF
                        && stringPtr[i + 1] < MaximalChar
                        && (hexByteLow = TableFromHexToBytes[stringPtr[i + 1]]) != 0xFF)
                    {
                        var resultByte = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                        resultPtr[resultIndex] = resultByte;
                        i += 2;
                        resultIndex += 1;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return result;
        }

        public static string? GetString(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            if (bytes.Length == 0)
            {
                return string.Empty;
            }

            var resultString = CoreLib.FastAllocateString(bytes.Length * 2);
            fixed (char* stringPtr = &resultString.GetPinnableReference())
            {
                var destUints = (uint*) stringPtr;
                for (var i = 0; i < bytes.Length; i++)
                {
                    destUints[i] = TableToHex[bytes[i]];
                }
            }

            return resultString;
        }
    }
}
