using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids
{
    public class UuidEqualsTests
    {
        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void EqualsWithObjectNullReturnFalse(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);

            bool isEquals = uuid.Equals(null);

            Assert.False(isEquals);
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public void EqualsWithObjectOtherTypeReturnFalse(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);
            var objectWithAnotherType = (object) 42;

            bool isEquals = uuid.Equals(objectWithAnotherType);

            Assert.False(isEquals);
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectEqualsToBytesAndResult))]
        public void EqualsWithObjectUuid(
            byte[] correctBytes,
            byte[] correctEqualsBytes,
            bool expectedResult)
        {
            var uuid = new Uuid(correctBytes);
            var objectUuid = (object) new Uuid(correctEqualsBytes);

            bool isEquals = uuid.Equals(objectUuid);

            Assert.AreEqual(expectedResult, isEquals);
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectEqualsToBytesAndResult))]
        public void EqualsWithOtherUuid(
            byte[] correctBytes,
            byte[] correctEqualsBytes,
            bool expectedResult)
        {
            var uuid = new Uuid(correctBytes);
            var otherUuid = new Uuid(correctEqualsBytes);

            bool isEquals = uuid.Equals(otherUuid);

            Assert.AreEqual(expectedResult, isEquals);
        }
    }
}
