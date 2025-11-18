using System;

namespace Dodo.Primitives.Generation;

/// <summary>
///     Provides generation methods for <see cref="Dodo.Primitives.Uuid" />.
/// </summary>
public static unsafe class Uuid
{
    private const long ChristianCalendarGregorianReformTicksDate = 499_163_040_000_000_000L;

    private const byte ResetVersionMask = 0b0000_1111;
    private const byte Version1Flag = 0b0001_0000;

    private const byte ResetReservedMask = 0b0011_1111;
    private const byte ReservedFlag = 0b1000_0000;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Uuid" /> structure that represents Uuid v1 (RFC4122).
    /// </summary>
    /// <returns></returns>
    public static Primitives.Uuid NewTimeBased()
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
        return new Primitives.Uuid(new Span<byte>(resultPtr, 16));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Uuid" /> structure that works the same way as UUID_TO_BIN(UUID(), 1) from MySQL 8.0.
    /// </summary>
    /// <returns></returns>
    public static Primitives.Uuid NewMySqlOptimized()
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
        return new Primitives.Uuid(new Span<byte>(resultPtr, 16));
    }
}
