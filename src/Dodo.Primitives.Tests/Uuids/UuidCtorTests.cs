using System;
using Dodo.Primitives.Tests.Uuids.Data;
using NUnit.Framework;

namespace Dodo.Primitives.Tests.Uuids
{
    public class UuidCtorTests
    {
        #region Bytes

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public unsafe void CtorFromByteArrayCorrectBytes(byte[] correctBytes)
        {
            var uuid = new Uuid(correctBytes);

            var uuidBytes = new byte[16];
            fixed (byte* pinnedUuidArray = uuidBytes)
            {
                *(Uuid*) pinnedUuidArray = uuid;
            }

            Assert.AreEqual(correctBytes, uuidBytes);
        }

        [Test]
        public void CtorFromByteArrayNullShouldThrows()
        {
#nullable disable
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Uuid((byte[]) null);
            });
#nullable restore
        }

        [Test]
        public void CtorFromByteArrayNot16BytesShouldThrows()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var _ = new Uuid(new byte[] {1, 2, 3});
            });
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public unsafe void CtorFromPtrCorrectData(byte[] correctBytes)
        {
            byte* bytePtr = stackalloc byte[correctBytes.Length];
            for (var i = 0; i < correctBytes.Length; i++)
            {
                bytePtr[i] = correctBytes[i];
            }

            var uuid = new Uuid(bytePtr);

            var uuidBytes = new byte[16];
            fixed (byte* pinnedUuidArray = uuidBytes)
            {
                *(Uuid*) pinnedUuidArray = uuid;
            }

            Assert.AreEqual(correctBytes, uuidBytes);
        }

        [TestCaseSource(typeof(UuidTestData), nameof(UuidTestData.CorrectUuidBytesArrays))]
        public unsafe void CtorFromReadOnlySpanCorrectBytes(byte[] correctBytes)
        {
            var span = new ReadOnlySpan<byte>(correctBytes);
            var uuid = new Uuid(span);

            var uuidBytes = new byte[16];
            fixed (byte* pinnedUuidArray = uuidBytes)
            {
                *(Uuid*) pinnedUuidArray = uuid;
            }

            Assert.AreEqual(correctBytes, uuidBytes);
        }

        [Test]
        public void CtorFromReadOnlySpanNot16BytesShouldThrows()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var span = new ReadOnlySpan<byte>(new byte[] {1, 2, 3});
                var _ = new Uuid(span);
            });
        }

        #endregion

        #region Chars_Strings

        [Test]
        public void CtorFromStringNullShouldThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
#nullable disable
                // ReSharper disable once RedundantCast
                var _ = new Uuid((string) null);
#nullable restore
            });
        }

        [Test]
        public void CtorFromStringEmptyShouldThrows()
        {
            Assert.Throws<FormatException>(() =>
            {
                var _ = new Uuid(string.Empty);
            });
        }

        [Test]
        public void CtorFromCharSpanEmptyShouldThrows()
        {
            Assert.Throws<FormatException>(() =>
            {
                var _ = new Uuid(new ReadOnlySpan<char>(new char[] { }));
            });
        }

        #region N

        public unsafe void CtorFromStringCorrectNString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctNString in UuidTestData.CorrectNStrings)
                {
                    string nString = correctNString.String;
                    byte[] expectedBytes = correctNString.Bytes;

                    var parsedUuid = new Uuid(nString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void CtorFromCharSpanCorrectN()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctNString in UuidTestData.CorrectNStrings)
                {
                    var nSpan = new ReadOnlySpan<char>(correctNString.String.ToCharArray());
                    byte[] expectedBytes = correctNString.Bytes;

                    var parsedUuid = new Uuid(nSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        #endregion

        #region D

        [Test]
        public unsafe void CtorFromStringCorrectDString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctDString in UuidTestData.CorrectDStrings)
                {
                    string dString = correctDString.String;
                    byte[] expectedBytes = correctDString.Bytes;

                    var parsedUuid = new Uuid(dString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void CtorFromCharSpanCorrectD()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctDString in UuidTestData.CorrectDStrings)
                {
                    var dSpan = new ReadOnlySpan<char>(correctDString.String.ToCharArray());
                    byte[] expectedBytes = correctDString.Bytes;

                    var parsedUuid = new Uuid(dSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        #endregion

        #region B

        [Test]
        public unsafe void CtorFromStringCorrectBString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctBString in UuidTestData.CorrectBStrings)
                {
                    string bString = correctBString.String;
                    byte[] expectedBytes = correctBString.Bytes;

                    var parsedUuid = new Uuid(bString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void CtorFromCharSpanCorrectB()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctBString in UuidTestData.CorrectBStrings)
                {
                    var bSpan = new ReadOnlySpan<char>(correctBString.String.ToCharArray());
                    byte[] expectedBytes = correctBString.Bytes;

                    var parsedUuid = new Uuid(bSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        #endregion

        #region P

        [Test]
        public unsafe void CtorFromStringCorrectPString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctPString in UuidTestData.CorrectPStrings)
                {
                    string pString = correctPString.String;
                    byte[] expectedBytes = correctPString.Bytes;

                    var parsedUuid = new Uuid(pString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void CtorFromCharSpanCorrectP()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctPString in UuidTestData.CorrectPStrings)
                {
                    var pSpan = new ReadOnlySpan<char>(correctPString.String.ToCharArray());
                    byte[] expectedBytes = correctPString.Bytes;

                    var parsedUuid = new Uuid(pSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        #endregion

        #region X

        [Test]
        public unsafe void CtorFromStringCorrectXString()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctXString in UuidTestData.CorrectXStrings)
                {
                    string xString = correctXString.String;
                    byte[] expectedBytes = correctXString.Bytes;

                    var parsedUuid = new Uuid(xString);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        [Test]
        public unsafe void CtorFromCharSpanCorrectX()
        {
            Assert.Multiple(() =>
            {
                foreach (var correctXString in UuidTestData.CorrectXStrings)
                {
                    var xSpan = new ReadOnlySpan<char>(correctXString.String.ToCharArray());
                    byte[] expectedBytes = correctXString.Bytes;

                    var parsedUuid = new Uuid(xSpan);

                    var actualBytes = new byte[16];
                    fixed (byte* pinnedActualBytes = actualBytes)
                    {
                        *(Uuid*) pinnedActualBytes = parsedUuid;
                    }

                    Assert.AreEqual(expectedBytes, actualBytes);
                }
            });
        }

        #endregion

        #endregion
    }
}
