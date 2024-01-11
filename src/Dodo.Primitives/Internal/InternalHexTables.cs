using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Dodo.Primitives.Internal;

internal static unsafe class InternalHexTables
{
    internal const ushort MaximalChar = 103;
    internal static readonly uint* TableToHexUtf16;
    internal static readonly ushort* TableToHexUtf8;
    internal static readonly byte* TableFromHexToBytesUtf16;

    [SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
    static InternalHexTables()
    {
        const int bytesPerCharUtf16 = 2;
        const int bytesPerCharUtf8 = 1;
        const int charsPerTableToHexCell = 2;
        const int charsPerTableToHex = 256;

        IntPtr tableToHexUtf16Buffer = Marshal.AllocHGlobal(bytesPerCharUtf16 * charsPerTableToHexCell * charsPerTableToHex);
        IntPtr tableToHexUtf8Buffer = Marshal.AllocHGlobal(bytesPerCharUtf8 * charsPerTableToHexCell * charsPerTableToHex);

        void* tableToHexUtf16Pointer = tableToHexUtf16Buffer.ToPointer();
        void* tableToHexUtf8Pointer = tableToHexUtf8Buffer.ToPointer();

        TableToHexUtf16 = (uint*) tableToHexUtf16Pointer;
        TableToHexUtf8 = (ushort*) tableToHexUtf8Pointer;
        for (var i = 0; i < charsPerTableToHex; i++)
        {
            string chars = Convert.ToString(i, 16).PadLeft(2, '0');
            uint utf16 = ((uint) chars[1] << 16) | chars[0];
            ushort utf8 = (ushort) ((ushort) ((byte) chars[1] << 8) | (byte) chars[0]);
            TableToHexUtf8[i] = utf8;
            TableToHexUtf16[i] = utf16;
        }

        IntPtr tableFromHexToBytesUtf16Buffer = Marshal.AllocHGlobal(MaximalChar);
        void* tableFromHexToBytesUtf16BufferPointer = tableFromHexToBytesUtf16Buffer.ToPointer();
        TableFromHexToBytesUtf16 = (byte*) tableFromHexToBytesUtf16BufferPointer;
        for (var i = 0; i < MaximalChar; i++)
        {
            TableFromHexToBytesUtf16[i] = (char) i switch
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
