using Dodo.Primitives.Internal;

namespace Dodo.Primitives;

public unsafe partial struct Uuid
{
    private void CharFormatN(char* dest)
    {
        // dddddddddddddddddddddddddddddddd
        var destInt32 = (uint*) dest;
        destInt32[0] = InternalHexTables.TableToHexUtf16[_byte0];
        destInt32[1] = InternalHexTables.TableToHexUtf16[_byte1];
        destInt32[2] = InternalHexTables.TableToHexUtf16[_byte2];
        destInt32[3] = InternalHexTables.TableToHexUtf16[_byte3];
        destInt32[4] = InternalHexTables.TableToHexUtf16[_byte4];
        destInt32[5] = InternalHexTables.TableToHexUtf16[_byte5];
        destInt32[6] = InternalHexTables.TableToHexUtf16[_byte6];
        destInt32[7] = InternalHexTables.TableToHexUtf16[_byte7];
        destInt32[8] = InternalHexTables.TableToHexUtf16[_byte8];
        destInt32[9] = InternalHexTables.TableToHexUtf16[_byte9];
        destInt32[10] = InternalHexTables.TableToHexUtf16[_byte10];
        destInt32[11] = InternalHexTables.TableToHexUtf16[_byte11];
        destInt32[12] = InternalHexTables.TableToHexUtf16[_byte12];
        destInt32[13] = InternalHexTables.TableToHexUtf16[_byte13];
        destInt32[14] = InternalHexTables.TableToHexUtf16[_byte14];
        destInt32[15] = InternalHexTables.TableToHexUtf16[_byte15];
    }

    private void CharFormatD(char* dest)
    {
        // dddddddd-dddd-dddd-dddd-dddddddddddd
        var destInt32 = (uint*) dest;
        var destInt32AsInt16 = (char**) &destInt32;
        dest[8] = dest[13] = dest[18] = dest[23] = '-';
        destInt32[0] = InternalHexTables.TableToHexUtf16[_byte0];
        destInt32[1] = InternalHexTables.TableToHexUtf16[_byte1];
        destInt32[2] = InternalHexTables.TableToHexUtf16[_byte2];
        destInt32[3] = InternalHexTables.TableToHexUtf16[_byte3];
        destInt32[7] = InternalHexTables.TableToHexUtf16[_byte6];
        destInt32[8] = InternalHexTables.TableToHexUtf16[_byte7];
        destInt32[12] = InternalHexTables.TableToHexUtf16[_byte10];
        destInt32[13] = InternalHexTables.TableToHexUtf16[_byte11];
        destInt32[14] = InternalHexTables.TableToHexUtf16[_byte12];
        destInt32[15] = InternalHexTables.TableToHexUtf16[_byte13];
        destInt32[16] = InternalHexTables.TableToHexUtf16[_byte14];
        destInt32[17] = InternalHexTables.TableToHexUtf16[_byte15];
        *destInt32AsInt16 += 1;
        destInt32[4] = InternalHexTables.TableToHexUtf16[_byte4];
        destInt32[5] = InternalHexTables.TableToHexUtf16[_byte5];
        destInt32[9] = InternalHexTables.TableToHexUtf16[_byte8];
        destInt32[10] = InternalHexTables.TableToHexUtf16[_byte9];
    }

    private void CharFormatB(char* dest)
    {
        // {dddddddd-dddd-dddd-dddd-dddddddddddd}
        var destInt32 = (uint*) dest;
        var destInt32AsInt16 = (char**) &destInt32;
        dest[0] = '{';
        dest[9] = dest[14] = dest[19] = dest[24] = '-';
        dest[37] = '}';
        destInt32[5] = InternalHexTables.TableToHexUtf16[_byte4];
        destInt32[6] = InternalHexTables.TableToHexUtf16[_byte5];
        destInt32[10] = InternalHexTables.TableToHexUtf16[_byte8];
        destInt32[11] = InternalHexTables.TableToHexUtf16[_byte9];
        *destInt32AsInt16 += 1;
        destInt32[0] = InternalHexTables.TableToHexUtf16[_byte0];
        destInt32[1] = InternalHexTables.TableToHexUtf16[_byte1];
        destInt32[2] = InternalHexTables.TableToHexUtf16[_byte2];
        destInt32[3] = InternalHexTables.TableToHexUtf16[_byte3];
        destInt32[7] = InternalHexTables.TableToHexUtf16[_byte6];
        destInt32[8] = InternalHexTables.TableToHexUtf16[_byte7];
        destInt32[12] = InternalHexTables.TableToHexUtf16[_byte10];
        destInt32[13] = InternalHexTables.TableToHexUtf16[_byte11];
        destInt32[14] = InternalHexTables.TableToHexUtf16[_byte12];
        destInt32[15] = InternalHexTables.TableToHexUtf16[_byte13];
        destInt32[16] = InternalHexTables.TableToHexUtf16[_byte14];
        destInt32[17] = InternalHexTables.TableToHexUtf16[_byte15];
    }

    private void CharFormatP(char* dest)
    {
        // (dddddddd-dddd-dddd-dddd-dddddddddddd)
        var destInt32 = (uint*) dest;
        var destInt32AsInt16 = (char**) &destInt32;
        dest[0] = '(';
        dest[9] = dest[14] = dest[19] = dest[24] = '-';
        dest[37] = ')';
        destInt32[5] = InternalHexTables.TableToHexUtf16[_byte4];
        destInt32[6] = InternalHexTables.TableToHexUtf16[_byte5];
        destInt32[10] = InternalHexTables.TableToHexUtf16[_byte8];
        destInt32[11] = InternalHexTables.TableToHexUtf16[_byte9];
        *destInt32AsInt16 += 1;
        destInt32[0] = InternalHexTables.TableToHexUtf16[_byte0];
        destInt32[1] = InternalHexTables.TableToHexUtf16[_byte1];
        destInt32[2] = InternalHexTables.TableToHexUtf16[_byte2];
        destInt32[3] = InternalHexTables.TableToHexUtf16[_byte3];
        destInt32[7] = InternalHexTables.TableToHexUtf16[_byte6];
        destInt32[8] = InternalHexTables.TableToHexUtf16[_byte7];
        destInt32[12] = InternalHexTables.TableToHexUtf16[_byte10];
        destInt32[13] = InternalHexTables.TableToHexUtf16[_byte11];
        destInt32[14] = InternalHexTables.TableToHexUtf16[_byte12];
        destInt32[15] = InternalHexTables.TableToHexUtf16[_byte13];
        destInt32[16] = InternalHexTables.TableToHexUtf16[_byte14];
        destInt32[17] = InternalHexTables.TableToHexUtf16[_byte15];
    }

    private void CharFormatX(char* dest)
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
        destInt32[7] = InternalHexTables.TableToHexUtf16[_byte4];
        destInt32[8] = InternalHexTables.TableToHexUtf16[_byte5];
        destInt32[17] = InternalHexTables.TableToHexUtf16[_byte9];
        destInt32[22] = InternalHexTables.TableToHexUtf16[_byte11];
        destInt32[27] = InternalHexTables.TableToHexUtf16[_byte13];
        destInt32[32] = InternalHexTables.TableToHexUtf16[_byte15];
        destInt32[33] = closeBracesUtf16; // }}
        *destInt32AsInt16 += 1;
        destInt32[0] = destInt32[9] = destInt32[13] = destInt32[18] = destInt32[23] = destInt32[28] = zeroXUtf16; // 0x
        destInt32[1] = InternalHexTables.TableToHexUtf16[_byte0];
        destInt32[2] = InternalHexTables.TableToHexUtf16[_byte1];
        destInt32[3] = InternalHexTables.TableToHexUtf16[_byte2];
        destInt32[4] = InternalHexTables.TableToHexUtf16[_byte3];
        destInt32[10] = InternalHexTables.TableToHexUtf16[_byte6];
        destInt32[11] = InternalHexTables.TableToHexUtf16[_byte7];
        destInt32[12] = commaBraceUtf16; // ,{
        destInt32[14] = InternalHexTables.TableToHexUtf16[_byte8];
        destInt32[19] = InternalHexTables.TableToHexUtf16[_byte10];
        destInt32[24] = InternalHexTables.TableToHexUtf16[_byte12];
        destInt32[29] = InternalHexTables.TableToHexUtf16[_byte14];
    }
}
