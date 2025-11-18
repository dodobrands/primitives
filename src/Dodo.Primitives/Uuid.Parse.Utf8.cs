using System;
using Dodo.Primitives.Internal;

namespace Dodo.Primitives;

public unsafe partial struct Uuid
{
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

    private static bool Utf8ParseWithoutExceptions(ReadOnlySpan<byte> uuidUtf8String, byte* resultPtr)
    {
        // Only 1 usage, so we don't need to check length here
        // if ((uint) uuidUtf8String.Length == 0u)
        // {
        //     return false;
        // }

        fixed (byte* uuidUtf8StringPtr = &uuidUtf8String.GetPinnableReference())
        {
            return uuidUtf8StringPtr[0] switch
            {
                Utf8LeftParenthesis => Utf8ParseWithoutExceptionsP((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr),
                Utf8LeftCurlyBracket => uuidUtf8String.Contains(Utf8HyphenMinus) ? Utf8ParseWithoutExceptionsB((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr) : Utf8ParseWithoutExceptionsX((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr),
                _ => uuidUtf8String.Contains(Utf8HyphenMinus) ? Utf8ParseWithoutExceptionsD((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr) : Utf8ParseWithoutExceptionsN((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr)
            };
        }
    }

    private static bool Utf8ParseWithoutExceptionsD(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 36u)
        {
            return false;
        }

        if (uuidUtf8StringPtr[8] != Utf8HyphenMinus || uuidUtf8StringPtr[13] != Utf8HyphenMinus || uuidUtf8StringPtr[18] != Utf8HyphenMinus || uuidUtf8StringPtr[23] != Utf8HyphenMinus)
        {
            return false;
        }

        return Utf8TryParsePtrD(uuidUtf8StringPtr, resultPtr);
    }

    private static bool Utf8ParseWithoutExceptionsN(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        return uuidStringLength == 32u && Utf8TryParsePtrN(uuidUtf8StringPtr, resultPtr);
    }

    private static bool Utf8ParseWithoutExceptionsB(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            return false;
        }

        if (uuidUtf8StringPtr[0] != Utf8LeftCurlyBracket
            || uuidUtf8StringPtr[9] != Utf8HyphenMinus
            || uuidUtf8StringPtr[14] != Utf8HyphenMinus
            || uuidUtf8StringPtr[19] != Utf8HyphenMinus
            || uuidUtf8StringPtr[24] != Utf8HyphenMinus
            || uuidUtf8StringPtr[37] != Utf8RightCurlyBracket)
        {
            return false;
        }

        return Utf8TryParsePtrD(uuidUtf8StringPtr + 1, resultPtr);
    }

    private static bool Utf8ParseWithoutExceptionsP(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            return false;
        }

        if (uuidUtf8StringPtr[0] != Utf8LeftParenthesis
            || uuidUtf8StringPtr[9] != Utf8HyphenMinus
            || uuidUtf8StringPtr[14] != Utf8HyphenMinus
            || uuidUtf8StringPtr[19] != Utf8HyphenMinus
            || uuidUtf8StringPtr[24] != Utf8HyphenMinus
            || uuidUtf8StringPtr[37] != Utf8RightParenthesis)
        {
            return false;
        }

        return Utf8TryParsePtrD(uuidUtf8StringPtr + 1, resultPtr);
    }

    private static bool Utf8ParseWithoutExceptionsX(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 68u)
        {
            return false;
        }

        if (uuidUtf8StringPtr[0] != Utf8LeftCurlyBracket
            || uuidUtf8StringPtr[1] != Utf8DigitZero
            || uuidUtf8StringPtr[2] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[11] != Utf8Comma
            || uuidUtf8StringPtr[12] != Utf8DigitZero
            || uuidUtf8StringPtr[13] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[18] != Utf8Comma
            || uuidUtf8StringPtr[19] != Utf8DigitZero
            || uuidUtf8StringPtr[20] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[25] != Utf8Comma
            || uuidUtf8StringPtr[26] != Utf8LeftCurlyBracket
            || uuidUtf8StringPtr[27] != Utf8DigitZero
            || uuidUtf8StringPtr[28] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[31] != Utf8Comma
            || uuidUtf8StringPtr[32] != Utf8DigitZero
            || uuidUtf8StringPtr[33] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[36] != Utf8Comma
            || uuidUtf8StringPtr[37] != Utf8DigitZero
            || uuidUtf8StringPtr[38] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[41] != Utf8Comma
            || uuidUtf8StringPtr[42] != Utf8DigitZero
            || uuidUtf8StringPtr[43] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[46] != Utf8Comma
            || uuidUtf8StringPtr[47] != Utf8DigitZero
            || uuidUtf8StringPtr[48] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[51] != Utf8Comma
            || uuidUtf8StringPtr[52] != Utf8DigitZero
            || uuidUtf8StringPtr[53] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[56] != Utf8Comma
            || uuidUtf8StringPtr[57] != Utf8DigitZero
            || uuidUtf8StringPtr[58] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[61] != Utf8Comma
            || uuidUtf8StringPtr[62] != Utf8DigitZero
            || uuidUtf8StringPtr[63] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[66] != Utf8RightCurlyBracket
            || uuidUtf8StringPtr[67] != Utf8RightCurlyBracket)
        {
            return false;
        }

        return Utf8TryParsePtrX(uuidUtf8StringPtr, resultPtr);
    }

    private static void Utf8ParseWithExceptions(ReadOnlySpan<byte> uuidUtf8String, byte* resultPtr)
    {
        // Only 1 usage, so we don't need to check length here
        // if ((uint) uuidUtf8String.Length == 0u)
        // {
        //     throw new FormatException("Unrecognized Uuid format.");
        // }

        fixed (byte* uuidUtf8StringPtr = &uuidUtf8String.GetPinnableReference())
        {
            switch (uuidUtf8StringPtr[0])
            {
                case Utf8LeftParenthesis:
                    {
                        Utf8ParseWithExceptionsP((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr);
                        break;
                    }
                case Utf8LeftCurlyBracket:
                    {
                        if (uuidUtf8String.Contains(Utf8HyphenMinus))
                        {
                            Utf8ParseWithExceptionsB((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr);
                            break;
                        }

                        Utf8ParseWithExceptionsX((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr);
                        break;
                    }
                default:
                    {
                        if (uuidUtf8String.Contains(Utf8HyphenMinus))
                        {
                            Utf8ParseWithExceptionsD((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr);
                            break;
                        }

                        Utf8ParseWithExceptionsN((uint) uuidUtf8String.Length, uuidUtf8StringPtr, resultPtr);
                        break;
                    }
            }
        }
    }

    private static void Utf8ParseWithExceptionsD(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 36u)
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx.");
        }

        if (uuidUtf8StringPtr[8] != Utf8HyphenMinus || uuidUtf8StringPtr[13] != Utf8HyphenMinus || uuidUtf8StringPtr[18] != Utf8HyphenMinus || uuidUtf8StringPtr[23] != Utf8HyphenMinus)
        {
            throw new FormatException("Dashes are in the wrong position for Uuid parsing.");
        }

        if (!Utf8TryParsePtrD(uuidUtf8StringPtr, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static void Utf8ParseWithExceptionsN(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 32u)
        {
            throw new FormatException("Uuid should contain only 32 digits xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.");
        }

        if (!Utf8TryParsePtrN(uuidUtf8StringPtr, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static void Utf8ParseWithExceptionsB(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}.");
        }

        if (uuidUtf8StringPtr[0] != Utf8LeftCurlyBracket || uuidUtf8StringPtr[37] != Utf8RightCurlyBracket)
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}.");
        }

        if (uuidUtf8StringPtr[9] != Utf8HyphenMinus || uuidUtf8StringPtr[14] != Utf8HyphenMinus || uuidUtf8StringPtr[19] != Utf8HyphenMinus || uuidUtf8StringPtr[24] != Utf8HyphenMinus)
        {
            throw new FormatException("Dashes are in the wrong position for Uuid parsing.");
        }

        if (!Utf8TryParsePtrD(uuidUtf8StringPtr + 1, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static void Utf8ParseWithExceptionsP(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 38u)
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
        }

        if (uuidUtf8StringPtr[0] != Utf8LeftParenthesis || uuidUtf8StringPtr[37] != Utf8RightParenthesis)
        {
            throw new FormatException("Uuid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");
        }

        if (uuidUtf8StringPtr[9] != Utf8HyphenMinus || uuidUtf8StringPtr[14] != Utf8HyphenMinus || uuidUtf8StringPtr[19] != Utf8HyphenMinus || uuidUtf8StringPtr[24] != Utf8HyphenMinus)
        {
            throw new FormatException("Dashes are in the wrong position for Uuid parsing.");
        }

        if (!Utf8TryParsePtrD(uuidUtf8StringPtr + 1, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static void Utf8ParseWithExceptionsX(uint uuidStringLength, byte* uuidUtf8StringPtr, byte* resultPtr)
    {
        if (uuidStringLength != 68u)
        {
            throw new FormatException(
                "Could not find a brace, or the length between the previous token and the brace was zero (i.e., '0x,'etc.).");
        }

        if (uuidUtf8StringPtr[0] != Utf8LeftCurlyBracket
            || uuidUtf8StringPtr[26] != Utf8LeftCurlyBracket
            || uuidUtf8StringPtr[66] != Utf8RightCurlyBracket)
        {
            throw new FormatException(
                "Could not find a brace, or the length between the previous token and the brace was zero (i.e., '0x,'etc.).");
        }

        if (uuidUtf8StringPtr[67] != Utf8RightCurlyBracket)
        {
            throw new FormatException("Could not find the ending brace.");
        }

        if (uuidUtf8StringPtr[11] != Utf8Comma
            || uuidUtf8StringPtr[18] != Utf8Comma
            || uuidUtf8StringPtr[25] != Utf8Comma
            || uuidUtf8StringPtr[31] != Utf8Comma
            || uuidUtf8StringPtr[36] != Utf8Comma
            || uuidUtf8StringPtr[41] != Utf8Comma
            || uuidUtf8StringPtr[46] != Utf8Comma
            || uuidUtf8StringPtr[51] != Utf8Comma
            || uuidUtf8StringPtr[56] != Utf8Comma
            || uuidUtf8StringPtr[61] != Utf8Comma)
        {
            throw new FormatException(
                "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).");
        }

        if (uuidUtf8StringPtr[1] != Utf8DigitZero
            || uuidUtf8StringPtr[2] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[12] != Utf8DigitZero
            || uuidUtf8StringPtr[13] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[19] != Utf8DigitZero
            || uuidUtf8StringPtr[20] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[27] != Utf8DigitZero
            || uuidUtf8StringPtr[28] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[32] != Utf8DigitZero
            || uuidUtf8StringPtr[33] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[37] != Utf8DigitZero
            || uuidUtf8StringPtr[38] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[42] != Utf8DigitZero
            || uuidUtf8StringPtr[43] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[47] != Utf8DigitZero
            || uuidUtf8StringPtr[48] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[52] != Utf8DigitZero
            || uuidUtf8StringPtr[53] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[57] != Utf8DigitZero
            || uuidUtf8StringPtr[58] != Utf8LatinSmallLetterX
            || uuidUtf8StringPtr[62] != Utf8DigitZero
            || uuidUtf8StringPtr[63] != Utf8LatinSmallLetterX)
        {
            throw new FormatException("Expected 0x prefix.");
        }


        if (!Utf8TryParsePtrX(uuidUtf8StringPtr, resultPtr))
        {
            throw new FormatException("Uuid string should only contain hexadecimal characters.");
        }
    }

    private static bool Utf8TryParsePtrN(byte* input, byte* resultPtr)
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

    private static bool Utf8TryParsePtrD(byte* input, byte* resultPtr)
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

                        // value[8] == Utf8HyphenMinus

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

                                // value[13] == Utf8HyphenMinus

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

                                        // value[18] == Utf8HyphenMinus

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

                                                // value[23] == Utf8HyphenMinus

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

    private static bool Utf8TryParsePtrX(byte* input, byte* resultPtr)
    {
        // e.g. "{0xd85b1407,0x351d,0x4694,{0x93,0x92,0x03,0xac,0xc5,0x87,0x0e,0xb1}}"

        byte hexByteHi;
        byte hexByteLow;
        // value[0] == Utf8LeftCurlyBracket
        // value[1] == Utf8DigitZero
        // value[2] == Utf8LatinSmallLetterX
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

                        // value[11] == Utf8Comma
                        // value[12] == Utf8DigitZero
                        // value[13] == Utf8LatinSmallLetterX

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

                                // value[18] == Utf8Comma
                                // value[19] == Utf8DigitZero
                                // value[20] == Utf8LatinSmallLetterX

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

                                        // value[25] == Utf8Comma
                                        // value[26] == Utf8LeftCurlyBracket
                                        // value[27] == Utf8DigitZero
                                        // value[28] == Utf8LatinSmallLetterX

                                        // 8 byte
                                        if (input[29] < InternalHexTables.MaximalChar
                                            && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[29]]) != 0xFF
                                            && input[30] < InternalHexTables.MaximalChar
                                            && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[30]]) != 0xFF)
                                        {
                                            resultPtr[8] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                            // value[31] == Utf8Comma
                                            // value[32] == Utf8DigitZero
                                            // value[33] == Utf8LatinSmallLetterX

                                            // 9 byte
                                            if (input[34] < InternalHexTables.MaximalChar
                                                && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[34]]) != 0xFF
                                                && input[35] < InternalHexTables.MaximalChar
                                                && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[35]]) != 0xFF)
                                            {
                                                resultPtr[9] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                // value[36] == Utf8Comma
                                                // value[37] == Utf8DigitZero
                                                // value[38] == Utf8LatinSmallLetterX

                                                // 10 byte
                                                if (input[39] < InternalHexTables.MaximalChar
                                                    && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[39]]) != 0xFF
                                                    && input[40] < InternalHexTables.MaximalChar
                                                    && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[40]]) != 0xFF)
                                                {
                                                    resultPtr[10] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                    // value[41] == Utf8Comma
                                                    // value[42] == Utf8DigitZero
                                                    // value[43] == Utf8LatinSmallLetterX

                                                    // 11 byte
                                                    if (input[44] < InternalHexTables.MaximalChar
                                                        && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[44]]) != 0xFF
                                                        && input[45] < InternalHexTables.MaximalChar
                                                        && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[45]]) != 0xFF)
                                                    {
                                                        resultPtr[11] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                        // value[46] == Utf8Comma
                                                        // value[47] == Utf8DigitZero
                                                        // value[48] == Utf8LatinSmallLetterX

                                                        // 12 byte
                                                        if (input[49] < InternalHexTables.MaximalChar
                                                            && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[49]]) != 0xFF
                                                            && input[50] < InternalHexTables.MaximalChar
                                                            && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[50]]) != 0xFF)
                                                        {
                                                            resultPtr[12] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                            // value[51] == Utf8Comma
                                                            // value[52] == Utf8DigitZero
                                                            // value[53] == Utf8LatinSmallLetterX

                                                            // 13 byte
                                                            if (input[54] < InternalHexTables.MaximalChar
                                                                && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[54]]) != 0xFF
                                                                && input[55] < InternalHexTables.MaximalChar
                                                                && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[55]]) != 0xFF)
                                                            {
                                                                resultPtr[13] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                // value[56] == Utf8Comma
                                                                // value[57] == Utf8DigitZero
                                                                // value[58] == Utf8LatinSmallLetterX

                                                                // 14 byte
                                                                if (input[59] < InternalHexTables.MaximalChar
                                                                    && (hexByteHi = InternalHexTables.TableFromHexToBytes[input[59]]) != 0xFF
                                                                    && input[60] < InternalHexTables.MaximalChar
                                                                    && (hexByteLow = InternalHexTables.TableFromHexToBytes[input[60]]) != 0xFF)
                                                                {
                                                                    resultPtr[14] = (byte) ((byte) (hexByteHi << 4) | hexByteLow);

                                                                    // value[61] == Utf8Comma
                                                                    // value[62] == Utf8DigitZero
                                                                    // value[63] == Utf8LatinSmallLetterX

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
