using System;
using Dodo.Primitives.Internal;

namespace Dodo.Primitives;

public unsafe partial struct Uuid
{
    private static bool CharParseWithoutExceptions(ReadOnlySpan<char> uuidString, byte* resultPtr)
    {
        if ((uint) uuidString.Length == 0u)
        {
            return false;
        }

        fixed (char* uuidStringPtr = &uuidString.GetPinnableReference())
        {
            return uuidStringPtr[0] switch
            {
                '(' => CharParseWithoutExceptionsP((uint) uuidString.Length, uuidStringPtr, resultPtr),
                '{' => uuidString.Contains('-') ? CharParseWithoutExceptionsB((uint) uuidString.Length, uuidStringPtr, resultPtr) : CharParseWithoutExceptionsX((uint) uuidString.Length, uuidStringPtr, resultPtr),
                _ => uuidString.Contains('-') ? CharParseWithoutExceptionsD((uint) uuidString.Length, uuidStringPtr, resultPtr) : CharParseWithoutExceptionsN((uint) uuidString.Length, uuidStringPtr, resultPtr)
            };
        }
    }

    private static bool CharParseWithoutExceptionsD(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 36u)
        {
            return false;
        }

        if (uuidStringPtr[8] != '-' || uuidStringPtr[13] != '-' || uuidStringPtr[18] != '-' || uuidStringPtr[23] != '-')
        {
            return false;
        }

        return CharTryParsePtrD(uuidStringPtr, resultPtr);
    }

    private static bool CharParseWithoutExceptionsN(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        return uuidStringLength == 32u && CharTryParsePtrN(uuidStringPtr, resultPtr);
    }

    private static bool CharParseWithoutExceptionsB(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
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

        return CharTryParsePtrD(uuidStringPtr + 1, resultPtr);
    }

    private static bool CharParseWithoutExceptionsP(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
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

        return CharTryParsePtrD(uuidStringPtr + 1, resultPtr);
    }

    private static bool CharParseWithoutExceptionsX(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
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

        return CharTryParsePtrX(uuidStringPtr, resultPtr);
    }

    private static void CharParseWithExceptions(ReadOnlySpan<char> uuidString, byte* resultPtr)
    {
        if ((uint) uuidString.Length == 0u)
        {
            throw new FormatException("Unrecognized Uuid format.");
        }

        fixed (char* uuidStringPtr = &uuidString.GetPinnableReference())
        {
            switch (uuidStringPtr[0])
            {
                case '(':
                    {
                        CharParseWithExceptionsP((uint) uuidString.Length, uuidStringPtr, resultPtr);
                        break;
                    }
                case '{':
                    {
                        if (uuidString.Contains('-'))
                        {
                            CharParseWithExceptionsB((uint) uuidString.Length, uuidStringPtr, resultPtr);
                            break;
                        }

                        CharParseWithExceptionsX((uint) uuidString.Length, uuidStringPtr, resultPtr);
                        break;
                    }
                default:
                    {
                        if (uuidString.Contains('-'))
                        {
                            CharParseWithExceptionsD((uint) uuidString.Length, uuidStringPtr, resultPtr);
                            break;
                        }

                        CharParseWithExceptionsN((uint) uuidString.Length, uuidStringPtr, resultPtr);
                        break;
                    }
            }
        }
    }

    private static void CharParseWithExceptionsD(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 36u)
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx.");
        }

        if (uuidStringPtr[8] != '-' || uuidStringPtr[13] != '-' || uuidStringPtr[18] != '-' || uuidStringPtr[23] != '-')
        {
            throw new FormatException("Dashes are in the wrong position for Uuid parsing.");
        }

        if (!CharTryParsePtrD(uuidStringPtr, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static void CharParseWithExceptionsN(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 32u)
        {
            throw new FormatException("Uuid should contain only 32 digits xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.");
        }

        if (!CharTryParsePtrN(uuidStringPtr, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static void CharParseWithExceptionsB(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
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

        if (!CharTryParsePtrD(uuidStringPtr + 1, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static void CharParseWithExceptionsP(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
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

        if (!CharTryParsePtrD(uuidStringPtr + 1, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static void CharParseWithExceptionsX(uint uuidStringLength, char* uuidStringPtr, byte* resultPtr)
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


        if (!CharTryParsePtrX(uuidStringPtr, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static bool CharTryParsePtrN(char* input, byte* resultPtr)
    {
        // e.g. "d85b1407351d4694939203acc5870eb1"
        byte hi;
        byte lo;
        // 0 byte
        if (input[0] < InternalHexTables.MaximalChar
            && (hi = InternalHexTables.TableFromHexToBytes[input[0]]) != 0xFF
            && input[1] < InternalHexTables.MaximalChar
            && (lo = InternalHexTables.TableFromHexToBytes[input[1]]) != 0xFF)
        {
            resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
            // 1 byte
            if (input[2] < InternalHexTables.MaximalChar
                && (hi = InternalHexTables.TableFromHexToBytes[input[2]]) != 0xFF
                && input[3] < InternalHexTables.MaximalChar
                && (lo = InternalHexTables.TableFromHexToBytes[input[3]]) != 0xFF)
            {
                resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                // 2 byte
                if (input[4] < InternalHexTables.MaximalChar
                    && (hi = InternalHexTables.TableFromHexToBytes[input[4]]) != 0xFF
                    && input[5] < InternalHexTables.MaximalChar
                    && (lo = InternalHexTables.TableFromHexToBytes[input[5]]) != 0xFF)
                {
                    resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                    // 3 byte
                    if (input[6] < InternalHexTables.MaximalChar
                        && (hi = InternalHexTables.TableFromHexToBytes[input[6]]) != 0xFF
                        && input[7] < InternalHexTables.MaximalChar
                        && (lo = InternalHexTables.TableFromHexToBytes[input[7]]) != 0xFF)
                    {
                        resultPtr[3] = (byte) ((byte) (hi << 4) | lo);
                        // 4 byte
                        if (input[8] < InternalHexTables.MaximalChar
                            && (hi = InternalHexTables.TableFromHexToBytes[input[8]]) != 0xFF
                            && input[9] < InternalHexTables.MaximalChar
                            && (lo = InternalHexTables.TableFromHexToBytes[input[9]]) != 0xFF)
                        {
                            resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                            // 5 byte
                            if (input[10] < InternalHexTables.MaximalChar
                                && (hi = InternalHexTables.TableFromHexToBytes[input[10]]) != 0xFF
                                && input[11] < InternalHexTables.MaximalChar
                                && (lo = InternalHexTables.TableFromHexToBytes[input[11]]) != 0xFF)
                            {
                                resultPtr[5] = (byte) ((byte) (hi << 4) | lo);
                                // 6 byte
                                if (input[12] < InternalHexTables.MaximalChar
                                    && (hi = InternalHexTables.TableFromHexToBytes[input[12]]) != 0xFF
                                    && input[13] < InternalHexTables.MaximalChar
                                    && (lo = InternalHexTables.TableFromHexToBytes[input[13]]) != 0xFF)
                                {
                                    resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                    // 7 byte
                                    if (input[14] < InternalHexTables.MaximalChar
                                        && (hi = InternalHexTables.TableFromHexToBytes[input[14]]) != 0xFF
                                        && input[15] < InternalHexTables.MaximalChar
                                        && (lo = InternalHexTables.TableFromHexToBytes[input[15]]) != 0xFF)
                                    {
                                        resultPtr[7] = (byte) ((byte) (hi << 4) | lo);
                                        // 8 byte
                                        if (input[16] < InternalHexTables.MaximalChar
                                            && (hi = InternalHexTables.TableFromHexToBytes[input[16]]) != 0xFF
                                            && input[17] < InternalHexTables.MaximalChar
                                            && (lo = InternalHexTables.TableFromHexToBytes[input[17]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                            // 9 byte
                                            if (input[18] < InternalHexTables.MaximalChar
                                                && (hi = InternalHexTables.TableFromHexToBytes[input[18]]) != 0xFF
                                                && input[19] < InternalHexTables.MaximalChar
                                                && (lo = InternalHexTables.TableFromHexToBytes[input[19]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hi << 4) | lo);
                                                // 10 byte
                                                if (input[20] < InternalHexTables.MaximalChar
                                                    && (hi = InternalHexTables.TableFromHexToBytes[input[20]]) != 0xFF
                                                    && input[21] < InternalHexTables.MaximalChar
                                                    && (lo = InternalHexTables.TableFromHexToBytes[input[21]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                    // 11 byte
                                                    if (input[22] < InternalHexTables.MaximalChar
                                                        && (hi = InternalHexTables.TableFromHexToBytes[input[22]]) != 0xFF
                                                        && input[23] < InternalHexTables.MaximalChar
                                                        && (lo = InternalHexTables.TableFromHexToBytes[input[23]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                        // 12 byte
                                                        if (input[24] < InternalHexTables.MaximalChar
                                                            && (hi = InternalHexTables.TableFromHexToBytes[input[24]]) != 0xFF
                                                            && input[25] < InternalHexTables.MaximalChar
                                                            && (lo = InternalHexTables.TableFromHexToBytes[input[25]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                            // 13 byte
                                                            if (input[26] < InternalHexTables.MaximalChar
                                                                && (hi = InternalHexTables.TableFromHexToBytes[input[26]]) != 0xFF
                                                                && input[27] < InternalHexTables.MaximalChar
                                                                && (lo = InternalHexTables.TableFromHexToBytes[input[27]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                // 14 byte
                                                                if (input[28] < InternalHexTables.MaximalChar
                                                                    && (hi = InternalHexTables.TableFromHexToBytes[input[28]]) != 0xFF
                                                                    && input[29] < InternalHexTables.MaximalChar
                                                                    && (lo = InternalHexTables.TableFromHexToBytes[input[29]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 15 byte
                                                                    if (input[30] < InternalHexTables.MaximalChar
                                                                        && (hi = InternalHexTables.TableFromHexToBytes[input[30]]) != 0xFF
                                                                        && input[31] < InternalHexTables.MaximalChar
                                                                        && (lo = InternalHexTables.TableFromHexToBytes[input[31]]) != 0xFF)
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

    private static bool CharTryParsePtrD(char* input, byte* resultPtr)
    {
        // e.g. "d85b1407-351d-4694-9392-03acc5870eb1"
        byte hi;
        byte lo;
        // 0 byte
        if (input[0] < InternalHexTables.MaximalChar
            && (hi = InternalHexTables.TableFromHexToBytes[input[0]]) != 0xFF
            && input[1] < InternalHexTables.MaximalChar
            && (lo = InternalHexTables.TableFromHexToBytes[input[1]]) != 0xFF)
        {
            resultPtr[0] = (byte) ((byte) (hi << 4) | lo);
            // 1 byte
            if (input[2] < InternalHexTables.MaximalChar
                && (hi = InternalHexTables.TableFromHexToBytes[input[2]]) != 0xFF
                && input[3] < InternalHexTables.MaximalChar
                && (lo = InternalHexTables.TableFromHexToBytes[input[3]]) != 0xFF)
            {
                resultPtr[1] = (byte) ((byte) (hi << 4) | lo);
                // 2 byte
                if (input[4] < InternalHexTables.MaximalChar
                    && (hi = InternalHexTables.TableFromHexToBytes[input[4]]) != 0xFF
                    && input[5] < InternalHexTables.MaximalChar
                    && (lo = InternalHexTables.TableFromHexToBytes[input[5]]) != 0xFF)
                {
                    resultPtr[2] = (byte) ((byte) (hi << 4) | lo);
                    // 3 byte
                    if (input[6] < InternalHexTables.MaximalChar
                        && (hi = InternalHexTables.TableFromHexToBytes[input[6]]) != 0xFF
                        && input[7] < InternalHexTables.MaximalChar
                        && (lo = InternalHexTables.TableFromHexToBytes[input[7]]) != 0xFF)
                    {
                        resultPtr[3] = (byte) ((byte) (hi << 4) | lo);

                        // value[8] == '-'

                        // 4 byte
                        if (input[9] < InternalHexTables.MaximalChar
                            && (hi = InternalHexTables.TableFromHexToBytes[input[9]]) != 0xFF
                            && input[10] < InternalHexTables.MaximalChar
                            && (lo = InternalHexTables.TableFromHexToBytes[input[10]]) != 0xFF)
                        {
                            resultPtr[4] = (byte) ((byte) (hi << 4) | lo);
                            // 5 byte
                            if (input[11] < InternalHexTables.MaximalChar
                                && (hi = InternalHexTables.TableFromHexToBytes[input[11]]) != 0xFF
                                && input[12] < InternalHexTables.MaximalChar
                                && (lo = InternalHexTables.TableFromHexToBytes[input[12]]) != 0xFF)
                            {
                                resultPtr[5] = (byte) ((byte) (hi << 4) | lo);

                                // value[13] == '-'

                                // 6 byte
                                if (input[14] < InternalHexTables.MaximalChar
                                    && (hi = InternalHexTables.TableFromHexToBytes[input[14]]) != 0xFF
                                    && input[15] < InternalHexTables.MaximalChar
                                    && (lo = InternalHexTables.TableFromHexToBytes[input[15]]) != 0xFF)
                                {
                                    resultPtr[6] = (byte) ((byte) (hi << 4) | lo);
                                    // 7 byte
                                    if (input[16] < InternalHexTables.MaximalChar
                                        && (hi = InternalHexTables.TableFromHexToBytes[input[16]]) != 0xFF
                                        && input[17] < InternalHexTables.MaximalChar
                                        && (lo = InternalHexTables.TableFromHexToBytes[input[17]]) != 0xFF)
                                    {
                                        resultPtr[7] = (byte) ((byte) (hi << 4) | lo);

                                        // value[18] == '-'

                                        // 8 byte
                                        if (input[19] < InternalHexTables.MaximalChar
                                            && (hi = InternalHexTables.TableFromHexToBytes[input[19]]) != 0xFF
                                            && input[20] < InternalHexTables.MaximalChar
                                            && (lo = InternalHexTables.TableFromHexToBytes[input[20]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hi << 4) | lo);
                                            // 9 byte
                                            if (input[21] < InternalHexTables.MaximalChar
                                                && (hi = InternalHexTables.TableFromHexToBytes[input[21]]) != 0xFF
                                                && input[22] < InternalHexTables.MaximalChar
                                                && (lo = InternalHexTables.TableFromHexToBytes[input[22]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hi << 4) | lo);

                                                // value[23] == '-'

                                                // 10 byte
                                                if (input[24] < InternalHexTables.MaximalChar
                                                    && (hi = InternalHexTables.TableFromHexToBytes[input[24]]) != 0xFF
                                                    && input[25] < InternalHexTables.MaximalChar
                                                    && (lo = InternalHexTables.TableFromHexToBytes[input[25]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hi << 4) | lo);
                                                    // 11 byte
                                                    if (input[26] < InternalHexTables.MaximalChar
                                                        && (hi = InternalHexTables.TableFromHexToBytes[input[26]]) != 0xFF
                                                        && input[27] < InternalHexTables.MaximalChar
                                                        && (lo = InternalHexTables.TableFromHexToBytes[input[27]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hi << 4) | lo);
                                                        // 12 byte
                                                        if (input[28] < InternalHexTables.MaximalChar
                                                            && (hi = InternalHexTables.TableFromHexToBytes[input[28]]) != 0xFF
                                                            && input[29] < InternalHexTables.MaximalChar
                                                            && (lo = InternalHexTables.TableFromHexToBytes[input[29]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hi << 4) | lo);
                                                            // 13 byte
                                                            if (input[30] < InternalHexTables.MaximalChar
                                                                && (hi = InternalHexTables.TableFromHexToBytes[input[30]]) != 0xFF
                                                                && input[31] < InternalHexTables.MaximalChar
                                                                && (lo = InternalHexTables.TableFromHexToBytes[input[31]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hi << 4) | lo);
                                                                // 14 byte
                                                                if (input[32] < InternalHexTables.MaximalChar
                                                                    && (hi = InternalHexTables.TableFromHexToBytes[input[32]]) != 0xFF
                                                                    && input[33] < InternalHexTables.MaximalChar
                                                                    && (lo = InternalHexTables.TableFromHexToBytes[input[33]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hi << 4) | lo);
                                                                    // 15 byte
                                                                    if (input[34] < InternalHexTables.MaximalChar
                                                                        && (hi = InternalHexTables.TableFromHexToBytes[input[34]]) != 0xFF
                                                                        && input[35] < InternalHexTables.MaximalChar
                                                                        && (lo = InternalHexTables.TableFromHexToBytes[input[35]]) != 0xFF)
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

    private static bool CharTryParsePtrX(char* input, byte* resultPtr)
    {
        // e.g. "{0xd85b1407,0x351d,0x4694,{0x93,0x92,0x03,0xac,0xc5,0x87,0x0e,0xb1}}"

        byte hexByteHi;
        byte hexByteLow;
        // value[0] == '{'
        // value[1] == '0'
        // value[2] == 'x'
        // 0 byte
        if (input[3] < InternalHexTables.MaximalChar
            && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[3]]) != 0xFF
            && input[4] < InternalHexTables.MaximalChar
            && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[4]]) != 0xFF)
        {
            resultPtr[0] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
            // 1 byte
            if (input[5] < InternalHexTables.MaximalChar
                && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[5]]) != 0xFF
                && input[6] < InternalHexTables.MaximalChar
                && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[6]]) != 0xFF)
            {
                resultPtr[1] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                // 2 byte
                if (input[7] < InternalHexTables.MaximalChar
                    && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[7]]) != 0xFF
                    && input[8] < InternalHexTables.MaximalChar
                    && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[8]]) != 0xFF)
                {
                    resultPtr[2] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                    // 3 byte
                    if (input[9] < InternalHexTables.MaximalChar
                        && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[9]]) != 0xFF
                        && input[10] < InternalHexTables.MaximalChar
                        && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[10]]) != 0xFF)
                    {
                        resultPtr[3] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                        // value[11] == ','
                        // value[12] == '0'
                        // value[13] == 'x'

                        // 4 byte
                        if (input[14] < InternalHexTables.MaximalChar
                            && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[14]]) != 0xFF
                            && input[15] < InternalHexTables.MaximalChar
                            && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[15]]) != 0xFF)
                        {
                            resultPtr[4] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                            // 5 byte
                            if (input[16] < InternalHexTables.MaximalChar
                                && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[16]]) != 0xFF
                                && input[17] < InternalHexTables.MaximalChar
                                && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[17]]) != 0xFF)
                            {
                                resultPtr[5] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                // value[18] == ','
                                // value[19] == '0'
                                // value[20] == 'x'

                                // 6 byte
                                if (input[21] < InternalHexTables.MaximalChar
                                    && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[21]]) != 0xFF
                                    && input[22] < InternalHexTables.MaximalChar
                                    && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[22]]) != 0xFF)
                                {
                                    resultPtr[6] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);
                                    // 7 byte
                                    if (input[23] < InternalHexTables.MaximalChar
                                        && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[23]]) != 0xFF
                                        && input[24] < InternalHexTables.MaximalChar
                                        && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[24]]) != 0xFF)
                                    {
                                        resultPtr[7] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                        // value[25] == ','
                                        // value[26] == '{'
                                        // value[27] == '0'
                                        // value[28] == 'x'

                                        // 8 byte
                                        if (input[29] < InternalHexTables.MaximalChar
                                            && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[29]]) != 0xFF
                                            && input[30] < InternalHexTables.MaximalChar
                                            && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[30]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                            // value[31] == ','
                                            // value[32] == '0'
                                            // value[33] == 'x'

                                            // 9 byte
                                            if (input[34] < InternalHexTables.MaximalChar
                                                && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[34]]) != 0xFF
                                                && input[35] < InternalHexTables.MaximalChar
                                                && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[35]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                // value[36] == ','
                                                // value[37] == '0'
                                                // value[38] == 'x'

                                                // 10 byte
                                                if (input[39] < InternalHexTables.MaximalChar
                                                    && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[39]]) != 0xFF
                                                    && input[40] < InternalHexTables.MaximalChar
                                                    && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[40]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                    // value[41] == ','
                                                    // value[42] == '0'
                                                    // value[43] == 'x'

                                                    // 11 byte
                                                    if (input[44] < InternalHexTables.MaximalChar
                                                        && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[44]]) != 0xFF
                                                        && input[45] < InternalHexTables.MaximalChar
                                                        && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[45]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                        // value[46] == ','
                                                        // value[47] == '0'
                                                        // value[48] == 'x'

                                                        // 12 byte
                                                        if (input[49] < InternalHexTables.MaximalChar
                                                            && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[49]]) != 0xFF
                                                            && input[50] < InternalHexTables.MaximalChar
                                                            && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[50]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                            // value[51] == ','
                                                            // value[52] == '0'
                                                            // value[53] == 'x'

                                                            // 13 byte
                                                            if (input[54] < InternalHexTables.MaximalChar
                                                                && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[54]]) != 0xFF
                                                                && input[55] < InternalHexTables.MaximalChar
                                                                && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[55]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                // value[56] == ','
                                                                // value[57] == '0'
                                                                // value[58] == 'x'

                                                                // 14 byte
                                                                if (input[59] < InternalHexTables.MaximalChar
                                                                    && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[59]]) != 0xFF
                                                                    && input[60] < InternalHexTables.MaximalChar
                                                                    && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[60]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                    // value[61] == ','
                                                                    // value[62] == '0'
                                                                    // value[63] == 'x'

                                                                    // 15 byte
                                                                    if (input[64] < InternalHexTables.MaximalChar
                                                                        && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[64]]) != 0xFF
                                                                        && input[65] < InternalHexTables.MaximalChar
                                                                        && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[65]]) != 0xFF)
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
