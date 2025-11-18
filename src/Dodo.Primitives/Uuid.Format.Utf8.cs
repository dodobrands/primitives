using Dodo.Primitives.Internal;

namespace Dodo.Primitives;

public unsafe partial struct Uuid
{
    private void Utf8FormatN(byte* dest)
    {
        // dddddddddddddddddddddddddddddddd
        var destInt16 = (ushort*) dest;
        destInt16[0] = InternalHexTables.TableToHexUtf8[_byte0];
        destInt16[1] = InternalHexTables.TableToHexUtf8[_byte1];
        destInt16[2] = InternalHexTables.TableToHexUtf8[_byte2];
        destInt16[3] = InternalHexTables.TableToHexUtf8[_byte3];
        destInt16[4] = InternalHexTables.TableToHexUtf8[_byte4];
        destInt16[5] = InternalHexTables.TableToHexUtf8[_byte5];
        destInt16[6] = InternalHexTables.TableToHexUtf8[_byte6];
        destInt16[7] = InternalHexTables.TableToHexUtf8[_byte7];
        destInt16[8] = InternalHexTables.TableToHexUtf8[_byte8];
        destInt16[9] = InternalHexTables.TableToHexUtf8[_byte9];
        destInt16[10] = InternalHexTables.TableToHexUtf8[_byte10];
        destInt16[11] = InternalHexTables.TableToHexUtf8[_byte11];
        destInt16[12] = InternalHexTables.TableToHexUtf8[_byte12];
        destInt16[13] = InternalHexTables.TableToHexUtf8[_byte13];
        destInt16[14] = InternalHexTables.TableToHexUtf8[_byte14];
        destInt16[15] = InternalHexTables.TableToHexUtf8[_byte15];
    }

    private void Utf8FormatD(byte* dest)
    {
        // dddddddd-dddd-dddd-dddd-dddddddddddd
        var destInt16 = (ushort*) dest;
        var destInt16AsInt8 = (byte**) &destInt16;
        dest[8] = dest[13] = dest[18] = dest[23] = Utf8HyphenMinus;
        destInt16[0] = InternalHexTables.TableToHexUtf8[_byte0];
        destInt16[1] = InternalHexTables.TableToHexUtf8[_byte1];
        destInt16[2] = InternalHexTables.TableToHexUtf8[_byte2];
        destInt16[3] = InternalHexTables.TableToHexUtf8[_byte3];
        destInt16[7] = InternalHexTables.TableToHexUtf8[_byte6];
        destInt16[8] = InternalHexTables.TableToHexUtf8[_byte7];
        destInt16[12] = InternalHexTables.TableToHexUtf8[_byte10];
        destInt16[13] = InternalHexTables.TableToHexUtf8[_byte11];
        destInt16[14] = InternalHexTables.TableToHexUtf8[_byte12];
        destInt16[15] = InternalHexTables.TableToHexUtf8[_byte13];
        destInt16[16] = InternalHexTables.TableToHexUtf8[_byte14];
        destInt16[17] = InternalHexTables.TableToHexUtf8[_byte15];
        *destInt16AsInt8 += 1;
        destInt16[4] = InternalHexTables.TableToHexUtf8[_byte4];
        destInt16[5] = InternalHexTables.TableToHexUtf8[_byte5];
        destInt16[9] = InternalHexTables.TableToHexUtf8[_byte8];
        destInt16[10] = InternalHexTables.TableToHexUtf8[_byte9];
    }

    private void Utf8FormatB(byte* dest)
    {
        // {dddddddd-dddd-dddd-dddd-dddddddddddd}
        var destInt16 = (ushort*) dest;
        var destInt16AsInt8 = (byte**) &destInt16;
        dest[0] = Utf8LeftCurlyBracket;
        dest[9] = dest[14] = dest[19] = dest[24] = Utf8HyphenMinus;
        dest[37] = Utf8RightCurlyBracket;
        destInt16[5] = InternalHexTables.TableToHexUtf8[_byte4];
        destInt16[6] = InternalHexTables.TableToHexUtf8[_byte5];
        destInt16[10] = InternalHexTables.TableToHexUtf8[_byte8];
        destInt16[11] = InternalHexTables.TableToHexUtf8[_byte9];
        *destInt16AsInt8 += 1;
        destInt16[0] = InternalHexTables.TableToHexUtf8[_byte0];
        destInt16[1] = InternalHexTables.TableToHexUtf8[_byte1];
        destInt16[2] = InternalHexTables.TableToHexUtf8[_byte2];
        destInt16[3] = InternalHexTables.TableToHexUtf8[_byte3];
        destInt16[7] = InternalHexTables.TableToHexUtf8[_byte6];
        destInt16[8] = InternalHexTables.TableToHexUtf8[_byte7];
        destInt16[12] = InternalHexTables.TableToHexUtf8[_byte10];
        destInt16[13] = InternalHexTables.TableToHexUtf8[_byte11];
        destInt16[14] = InternalHexTables.TableToHexUtf8[_byte12];
        destInt16[15] = InternalHexTables.TableToHexUtf8[_byte13];
        destInt16[16] = InternalHexTables.TableToHexUtf8[_byte14];
        destInt16[17] = InternalHexTables.TableToHexUtf8[_byte15];
    }

    private void Utf8FormatP(byte* dest)
    {
        // (dddddddd-dddd-dddd-dddd-dddddddddddd)
        var destInt16 = (ushort*) dest;
        var destInt16AsInt8 = (byte**) &destInt16;
        dest[0] = Utf8LeftParenthesis;
        dest[9] = dest[14] = dest[19] = dest[24] = Utf8HyphenMinus;
        dest[37] = Utf8RightParenthesis;
        destInt16[5] = InternalHexTables.TableToHexUtf8[_byte4];
        destInt16[6] = InternalHexTables.TableToHexUtf8[_byte5];
        destInt16[10] = InternalHexTables.TableToHexUtf8[_byte8];
        destInt16[11] = InternalHexTables.TableToHexUtf8[_byte9];
        *destInt16AsInt8 += 1;
        destInt16[0] = InternalHexTables.TableToHexUtf8[_byte0];
        destInt16[1] = InternalHexTables.TableToHexUtf8[_byte1];
        destInt16[2] = InternalHexTables.TableToHexUtf8[_byte2];
        destInt16[3] = InternalHexTables.TableToHexUtf8[_byte3];
        destInt16[7] = InternalHexTables.TableToHexUtf8[_byte6];
        destInt16[8] = InternalHexTables.TableToHexUtf8[_byte7];
        destInt16[12] = InternalHexTables.TableToHexUtf8[_byte10];
        destInt16[13] = InternalHexTables.TableToHexUtf8[_byte11];
        destInt16[14] = InternalHexTables.TableToHexUtf8[_byte12];
        destInt16[15] = InternalHexTables.TableToHexUtf8[_byte13];
        destInt16[16] = InternalHexTables.TableToHexUtf8[_byte14];
        destInt16[17] = InternalHexTables.TableToHexUtf8[_byte15];
    }

    private void Utf8FormatX(byte* dest)
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
        destInt16[7] = InternalHexTables.TableToHexUtf8[_byte4];
        destInt16[8] = InternalHexTables.TableToHexUtf8[_byte5];
        destInt16[17] = InternalHexTables.TableToHexUtf8[_byte9];
        destInt16[22] = InternalHexTables.TableToHexUtf8[_byte11];
        destInt16[27] = InternalHexTables.TableToHexUtf8[_byte13];
        destInt16[32] = InternalHexTables.TableToHexUtf8[_byte15];
        destInt16[33] = closeBracesUtf8; // }}
        *destInt16AsInt8 += 1;
        destInt16[0] = destInt16[9] = destInt16[13] = destInt16[18] = destInt16[23] = destInt16[28] = zeroXUtf8; // 0x
        destInt16[1] = InternalHexTables.TableToHexUtf8[_byte0];
        destInt16[2] = InternalHexTables.TableToHexUtf8[_byte1];
        destInt16[3] = InternalHexTables.TableToHexUtf8[_byte2];
        destInt16[4] = InternalHexTables.TableToHexUtf8[_byte3];
        destInt16[10] = InternalHexTables.TableToHexUtf8[_byte6];
        destInt16[11] = InternalHexTables.TableToHexUtf8[_byte7];
        destInt16[12] = commaBraceUtf8; // ,{
        destInt16[14] = InternalHexTables.TableToHexUtf8[_byte8];
        destInt16[19] = InternalHexTables.TableToHexUtf8[_byte10];
        destInt16[24] = InternalHexTables.TableToHexUtf8[_byte12];
        destInt16[29] = InternalHexTables.TableToHexUtf8[_byte14];
    }
}
