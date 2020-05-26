using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids
{
    public class UuidOperatorsTests
    {
        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectEqualsToBytesAndResult))]
        public void EqualsOperator(
            byte[] correctBytes,
            byte[] correctEqualsBytes,
            bool expectedResult)
        {
            var uuid = new Uuid(correctBytes);
            var otherUuid = new Uuid(correctEqualsBytes);

            bool isEquals = uuid == otherUuid;

            Assert.AreEqual(expectedResult, isEquals);
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectEqualsToBytesAndResult))]
        public void NotEqualsOperator(
            byte[] correctBytes,
            byte[] correctEqualsBytes,
            bool notExpectedResult)
        {
            bool expectedResult = !notExpectedResult;
            var uuid = new Uuid(correctBytes);
            var otherUuid = new Uuid(correctEqualsBytes);

            bool isEquals = uuid != otherUuid;

            Assert.AreEqual(expectedResult, isEquals);
        }
    }
}
