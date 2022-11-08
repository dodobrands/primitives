using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dodo.Primitives.Tests.Uuids.Data.Models;

namespace Dodo.Primitives.Tests.Uuids.Data;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public static class UuidTestData
{
    private static readonly UuidStringWithBytes[] NStrings = UuidTestsUtils.GenerateNStrings();
    private static readonly UuidStringWithBytes[] DStrings = UuidTestsUtils.GenerateDStrings();
    private static readonly UuidStringWithBytes[] BStrings = UuidTestsUtils.GenerateBStrings();
    private static readonly UuidStringWithBytes[] PStrings = UuidTestsUtils.GeneratePStrings();
    private static readonly UuidStringWithBytes[] XStrings = UuidTestsUtils.GenerateXStrings();

    public static object[] CorrectUuidBytesArrays { get; } =
    {
        new object[] { new byte[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160 } },
        new object[] { new byte[] { 163, 167, 252, 114, 206, 122, 17, 233, 128, 237, 0, 13, 58, 17, 37, 233 } },
        new object[] { new byte[] { 241, 186, 230, 119, 206, 55, 78, 240, 175, 188, 141, 114, 36, 63, 217, 193 } },
        new object[] { new byte[] { 230, 35, 75, 5, 129, 19, 99, 68, 152, 188, 145, 109, 120, 166, 14, 235 } },
        new object[] { new byte[] { 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
        new object[] { new byte[] { 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0 } },
        new object[] { new byte[] { 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0, 0, 0, 0, 0 } },
        new object[] { new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255, 0, 0, 0, 0 } },
        new object[] { new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255, 255, 255 } }
    };

    public static object[] LeftLessThanRight()
    {
        var src = CorrectCompareToArraysAndResult;
        var results = new List<object>();
        foreach (var arg in src)
        {
            var args = (object[])arg;
            var left = (byte[])args[0];
            var right = (byte[])args[1];
            var flag = (int)args[2];
            if (flag == -1)
            {
                var outputArgs = new object[]
                {
                    new Uuid(left),
                    new Uuid(right)
                };
                results.Add(outputArgs);
            }
        }

        return results.ToArray();
    }

    public static object[] RightLessThanLeft()
    {
        var src = CorrectCompareToArraysAndResult;
        var results = new List<object>();
        foreach (var arg in src)
        {
            var args = (object[])arg;
            var left = (byte[])args[0];
            var right = (byte[])args[1];
            var flag = (int)args[2];
            if (flag == 1)
            {
                var outputArgs = new object[]
                {
                    new Uuid(left),
                    new Uuid(right)
                };
                results.Add(outputArgs);
            }
        }

        return results.ToArray();
    }

    public static object[] CorrectCompareToArraysAndResult { get; } =
    {
        new object[]
        {
            new byte[] { 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0 },
            new byte[] { 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0, 0 },
            new byte[] { 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0 },
            new byte[] { 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0, 0 },
            new byte[] { 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0 },
            new byte[] { 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0, 0 },
            new byte[] { 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0 },
            new byte[] { 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0, 0 },
            new byte[] { 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0 },
            new byte[] { 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0 },
            1
        },
        new object[]
        {
            new byte[] { 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0, 0 },
            new byte[] { 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0 },
            new byte[] { 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0 },
            1
        },
        new object[]
        {
            new byte[] { 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13, 0 },
            new byte[] { 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42, 0 },
            -1
        },
        new object[]
        {
            new byte[] { 72, 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42 },
            new byte[] { 72, 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13 },
            1
        },
        new object[]
        {
            new byte[] { 72, 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 13 },
            new byte[] { 72, 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17, 42 },
            -1
        },
        new object[]
        {
            new byte[] { 1, 72, 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17 },
            new byte[] { 1, 72, 99, 252, 201, 77, 117, 69, 125, 81, 23, 97, 234, 173, 29, 17 },
            0
        }
    };

    public static object[] CorrectEqualsToBytesAndResult { get; } =
    {
        new object[]
        {
            new byte[] { 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            false
        },
        new object[]
        {
            new byte[] { 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            true
        },
        new object[]
        {
            new byte[] { 13, 0, 0, 0, 0, 0, 0, 0, 42, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 13, 0, 0, 0, 0, 0, 0, 0, 37, 0, 0, 0, 0, 0, 0, 0 },
            false
        },
        new object[]
        {
            new byte[] { 13, 0, 0, 0, 0, 0, 0, 0, 42, 0, 0, 0, 0, 0, 0, 0 },
            new byte[] { 13, 0, 0, 0, 0, 0, 0, 0, 42, 0, 0, 0, 0, 0, 0, 0 },
            true
        }
    };

    // ---------
    // --- N ---
    // ---------
    public static UuidStringWithBytes[] CorrectNStrings => NStrings;

    public static string[] LargeNStrings { get; } = NStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallNStrings { get; } = NStrings
        .Select(x => x.String.Substring(x.String.Length / 2))
        .ToArray();

    public static string[] BrokenNStrings { get; } = UuidTestsUtils.GenerateBrokenNStringsArray();

    // ---------
    // --- D ---
    // ---------
    public static UuidStringWithBytes[] CorrectDStrings => DStrings;

    public static string[] LargeDStrings { get; } = DStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallDStrings { get; } = DStrings
        .Select(x => x.String.Substring(x.String.Length / 2))
        .ToArray();

    public static string[] BrokenDStrings { get; } = UuidTestsUtils.GenerateBrokenDStringsArray();

    // ---------
    // --- B ---
    // ---------
    public static UuidStringWithBytes[] CorrectBStrings => BStrings;

    public static string[] LargeBStrings { get; } = BStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallBStrings { get; } = BStrings
        .Select(x => x.String.Substring(x.String.Length / 2))
        .ToArray();

    public static string[] BrokenBStrings { get; } = UuidTestsUtils.GenerateBrokenBStringsArray();

    // ---------
    // --- P ---
    // ---------
    public static UuidStringWithBytes[] CorrectPStrings => PStrings;

    public static string[] LargePStrings { get; } = PStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallPStrings { get; } = PStrings
        .Select(x => x.String.Substring(x.String.Length / 2))
        .ToArray();

    public static string[] BrokenPStrings { get; } = UuidTestsUtils.GenerateBrokenPStringsArray();

    // ---------
    // --- X ---
    // ---------
    public static UuidStringWithBytes[] CorrectXStrings => XStrings;

    public static string[] LargeXStrings { get; } = XStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallXStrings { get; } = XStrings
        .Select(x => x.String.Substring(x.String.Length / 2))
        .ToArray();

    public static string[] BrokenXStrings { get; } = UuidTestsUtils.GenerateBrokenXStringsArray();

    // ReSharper disable once InconsistentNaming
    public static class Formats
    {
        public static string[] All { get; } =
        {
            "N",
            "n",
            "D",
            "d",
            "B",
            "b",
            "P",
            "p",
            "X",
            "x"
        };

        public static string[] N { get; } =
        {
            "N",
            "n"
        };

        public static string[] D { get; } =
        {
            "D",
            "d"
        };

        public static string[] B { get; } =
        {
            "B",
            "b"
        };

        public static string[] P { get; } =
        {
            "P",
            "p"
        };

        public static string[] X { get; } =
        {
            "X",
            "x"
        };

        public static string[] AllExceptN { get; } =
        {
            "D",
            "d",
            "B",
            "b",
            "P",
            "p",
            "X",
            "x"
        };

        public static string[] AllExceptD { get; } =
        {
            "N",
            "n",
            "B",
            "b",
            "P",
            "p",
            "X",
            "x"
        };

        public static string[] AllExceptB { get; } =
        {
            "N",
            "n",
            "D",
            "d",
            "P",
            "p",
            "X",
            "x"
        };

        public static string[] AllExceptP { get; } =
        {
            "N",
            "n",
            "D",
            "d",
            "B",
            "b",
            "X",
            "x"
        };

        public static string[] AllExceptX { get; } =
        {
            "N",
            "n",
            "D",
            "d",
            "B",
            "b",
            "P",
            "p"
        };
    }
}
