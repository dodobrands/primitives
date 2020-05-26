using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids
{
    public class UuidToByteArrayTests
    {
        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void ToByteArray(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);

            var uuidBytes = uuid.ToByteArray();

            Assert.AreEqual(correctBytes, uuidBytes);
        }
    }
}
