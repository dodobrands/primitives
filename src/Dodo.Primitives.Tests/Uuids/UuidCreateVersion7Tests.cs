using System;
using System.Buffers.Binary;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids;

public class UuidCreateVersion7Tests
{
    [Test]
    public void CreateVersion7WithDateTimeOffset()
    {
        DateTimeOffset startDate = DateTimeOffset.FromUnixTimeMilliseconds(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
        Assert.Multiple(() =>
        {
            long startTicks = startDate.UtcTicks;
            for (var i = 0; i <= TimeSpan.TicksPerMillisecond; i++)
            {
                long ticks = startTicks + i;
                var expectedDate = new DateTimeOffset(ticks, TimeSpan.Zero);
                var uuid = Uuid.CreateVersion7(expectedDate);
                DateTimeOffset actualDate = ExtractDateTimeOffset(uuid);
                Assert.That(actualDate, Is.EqualTo(expectedDate));
            }
        });
    }

    [Test]
    public void CreateVersion7WithoutDateTimeOffset()
    {
        DateTimeOffset startDate = DateTimeOffset.UtcNow;
        var uuid = Uuid.CreateVersion7();
        DateTimeOffset endDate = DateTimeOffset.UtcNow;

        DateTimeOffset actualDate = ExtractDateTimeOffset(uuid);
        Assert.That(actualDate.UtcTicks >= startDate.UtcTicks && actualDate.UtcTicks <= endDate.UtcTicks, Is.True);
    }

    private static DateTimeOffset ExtractDateTimeOffset(Uuid uuid)
    {
        const long unixEpochTicks = 621_355_968_000_000_000L;

        Span<byte> bytes = stackalloc byte[16];
        uuid.TryWriteBytes(bytes);
        Span<byte> unixTsMsBuffer = stackalloc byte[8];
        bytes[..6].CopyTo(unixTsMsBuffer);
        long unixTsMs = (long) BinaryPrimitives.ReadUInt64BigEndian(unixTsMsBuffer) >> 16;
        Span<byte> randABuffer = stackalloc byte[2];
        bytes.Slice(6, 2).CopyTo(randABuffer);
        randABuffer[0] = (byte) (randABuffer[0] & 0b00001111);
        var randA = (ushort) (BinaryPrimitives.ReadUInt16BigEndian(randABuffer) << 2);
        var ticksNotFitInRandA = (ushort) ((byte) (bytes[8] & 0b00110000) >> 4);
        var remainTicks = (long) (ushort) (randA | ticksNotFitInRandA);
        long unixTimeTicks = (unixTsMs * TimeSpan.TicksPerMillisecond) + remainTicks;
        long utcTicks = unixEpochTicks + unixTimeTicks;
        return new DateTimeOffset(utcTicks, TimeSpan.Zero);
    }
}
