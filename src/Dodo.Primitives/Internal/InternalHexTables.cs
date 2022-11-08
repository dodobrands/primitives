using System;
using System.Runtime.InteropServices;

namespace Dodo.Primitives.Internal;

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
            string chars = Convert.ToString(i, 16).PadLeft(2, '0');
            TableToHex[i] = ((uint) chars[1] << 16) | chars[0];
        }

        TableFromHexToBytes = (byte*) Marshal.AllocHGlobal(103).ToPointer();
        for (var i = 0; i < 103; i++)
        {
            TableFromHexToBytes[i] = (char) i switch
            {
                '0' => 0x0,
                '1' => 0x1,
                '2' => 0x2,
                '3' => 0x3,
                '4' => 0x4,
                '5' => 0x5,
                '6' => 0x6,
                '7' => 0x7,
                '8' => 0x8,
                '9' => 0x9,
                'a' => 0xa,
                'A' => 0xa,
                'b' => 0xb,
                'B' => 0xb,
                'c' => 0xc,
                'C' => 0xc,
                'd' => 0xd,
                'D' => 0xd,
                'e' => 0xe,
                'E' => 0xe,
                'f' => 0xf,
                'F' => 0xf,
                _ => byte.MaxValue
            };
        }
    }
}