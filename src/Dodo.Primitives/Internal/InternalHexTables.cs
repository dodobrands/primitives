using System;
using System.Runtime.InteropServices;

namespace Dodo.Primitives.Internal
{
    internal static unsafe class InternalHexTables
    {
        internal const ushort MaximalChar = 103;
        internal static readonly uint* TableToHex;
        internal static readonly byte* TableFromHexToBytes;

        static InternalHexTables()
        {
            TableToHex = (uint*) Marshal.AllocHGlobal(sizeof(uint) * 256).ToPointer();
            for (var i = 0; i < 256; i++)
            {
                var chars = Convert.ToString(i, 16).PadLeft(2, '0');
                TableToHex[i] = ((uint) chars[1] << 16) | chars[0];
            }

            TableFromHexToBytes = (byte*) Marshal.AllocHGlobal(103).ToPointer();
            for (var i = 0; i < 103; i++)
            {
                TableFromHexToBytes[i] = (char) i switch
                {
                    '0' => (byte) 0x0,
                    '1' => (byte) 0x1,
                    '2' => (byte) 0x2,
                    '3' => (byte) 0x3,
                    '4' => (byte) 0x4,
                    '5' => (byte) 0x5,
                    '6' => (byte) 0x6,
                    '7' => (byte) 0x7,
                    '8' => (byte) 0x8,
                    '9' => (byte) 0x9,
                    'a' => (byte) 0xa,
                    'A' => (byte) 0xa,
                    'b' => (byte) 0xb,
                    'B' => (byte) 0xb,
                    'c' => (byte) 0xc,
                    'C' => (byte) 0xc,
                    'd' => (byte) 0xd,
                    'D' => (byte) 0xd,
                    'e' => (byte) 0xe,
                    'E' => (byte) 0xe,
                    'f' => (byte) 0xf,
                    'F' => (byte) 0xf,
                    _ => byte.MaxValue
                };
            }
        }
    }
}
