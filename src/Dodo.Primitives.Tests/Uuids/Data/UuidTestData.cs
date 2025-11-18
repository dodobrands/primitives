using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Dodo.Primitives.Tests.Uuids.Data.Models;

namespace Dodo.Primitives.Tests.Uuids.Data;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public static class UuidTestData
{
    public static object[] CorrectUuidBytesArrays { get; } =
    {
        new object[]
        {
            new byte[]
            {
                10,
                20,
                30,
                40,
                50,
                60,
                70,
                80,
                90,
                100,
                110,
                120,
                130,
                140,
                150,
                160
            }
        },
        new object[]
        {
            new byte[]
            {
                163,
                167,
                252,
                114,
                206,
                122,
                17,
                233,
                128,
                237,
                0,
                13,
                58,
                17,
                37,
                233
            }
        },
        new object[]
        {
            new byte[]
            {
                241,
                186,
                230,
                119,
                206,
                55,
                78,
                240,
                175,
                188,
                141,
                114,
                36,
                63,
                217,
                193
            }
        },
        new object[]
        {
            new byte[]
            {
                230,
                35,
                75,
                5,
                129,
                19,
                99,
                68,
                152,
                188,
                145,
                109,
                120,
                166,
                14,
                235
            }
        },
        new object[]
        {
            new byte[]
            {
                255,
                255,
                255,
                255,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }
        },
        new object[]
        {
            new byte[]
            {
                0,
                0,
                0,
                0,
                255,
                255,
                255,
                255,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }
        },
        new object[]
        {
            new byte[]
            {
                0,
                0,
                0,
                0,
                255,
                255,
                255,
                255,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }
        },
        new object[]
        {
            new byte[]
            {
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                255,
                255,
                255,
                255,
                0,
                0,
                0,
                0
            }
        },
        new object[]
        {
            new byte[]
            {
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                255,
                255,
                255,
                255
            }
        }
    };

    public static object[] CorrectCompareToArraysAndResult { get; } =
    {
        new ByteArraysToCompare(
            new byte[]
            {
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0
            },
            new byte[]
            {
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0,
                0
            },
            new byte[]
            {
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0
            },
            new byte[]
            {
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0,
                0
            },
            new byte[]
            {
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0
            },
            new byte[]
            {
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13,
                0
            },
            new byte[]
            {
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42,
                0
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                72,
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42
            },
            new byte[]
            {
                72,
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                72,
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                13
            },
            new byte[]
            {
                72,
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17,
                42
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                1,
                72,
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17
            },
            new byte[]
            {
                1,
                72,
                99,
                252,
                201,
                77,
                117,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17
            }).ToArgs(),
        new ByteArraysToCompare(
            new byte[]
            {
                1,
                72,
                99,
                252,
                201,
                77,
                117,
                0,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17
            },
            new byte[]
            {
                1,
                72,
                99,
                252,
                201,
                77,
                0,
                69,
                125,
                81,
                23,
                97,
                234,
                173,
                29,
                17
            }).ToArgs(),
        new ByteArraysToCompare(
                new byte[]
                {
                    1,
                    72,
                    99,
                    252,
                    201,
                    77,
                    0,
                    69,
                    125,
                    81,
                    23,
                    97,
                    234,
                    173,
                    29,
                    17
                },
                new byte[]
                {
                    1,
                    72,
                    99,
                    252,
                    201,
                    77,
                    117,
                    0,
                    125,
                    81,
                    23,
                    97,
                    234,
                    173,
                    29,
                    17
                })
            .ToArgs()
    };


    public static object[] CorrectEqualsToBytesAndResult { get; } =
    {
        new object[]
        {
            new byte[]
            {
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            false
        },
        new object[]
        {
            new byte[]
            {
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            true
        },
        new object[]
        {
            new byte[]
            {
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                37,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            false
        },
        new object[]
        {
            new byte[]
            {
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            new byte[]
            {
                13,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                42,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            },
            true
        }
    };

    // ---------
    // --- N ---
    // ---------
    public static UuidStringWithBytes[] CorrectNStrings { get; } = UuidTestsUtils.GenerateNStrings();

    public static string[] LargeNStrings { get; } = CorrectNStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallNStrings { get; } = CorrectNStrings
        .Select(x => x.String[(x.String.Length / 2)..])
        .ToArray();

    public static string[] BrokenNStrings { get; } = UuidTestsUtils.GenerateBrokenNStringsArray();

    // ---------
    // --- D ---
    // ---------
    public static UuidStringWithBytes[] CorrectDStrings { get; } = UuidTestsUtils.GenerateDStrings();

    public static string[] LargeDStrings { get; } = CorrectDStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallDStrings { get; } = CorrectDStrings
        .Select(x => x.String[(x.String.Length / 2)..])
        .ToArray();

    public static string[] BrokenDStrings { get; } = UuidTestsUtils.GenerateBrokenDStringsArray();

    // ---------
    // --- B ---
    // ---------
    public static UuidStringWithBytes[] CorrectBStrings { get; } = UuidTestsUtils.GenerateBStrings();

    public static string[] LargeBStrings { get; } = CorrectBStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallBStrings { get; } = CorrectBStrings
        .Select(x => x.String[(x.String.Length / 2)..])
        .ToArray();

    public static string[] BrokenBStrings { get; } = UuidTestsUtils.GenerateBrokenBStringsArray();

    // ---------
    // --- P ---
    // ---------
    public static UuidStringWithBytes[] CorrectPStrings { get; } = UuidTestsUtils.GeneratePStrings();

    public static string[] LargePStrings { get; } = CorrectPStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallPStrings { get; } = CorrectPStrings
        .Select(x => x.String[(x.String.Length / 2)..])
        .ToArray();

    public static string[] BrokenPStrings { get; } = UuidTestsUtils.GenerateBrokenPStringsArray();

    // ---------
    // --- X ---
    // ---------
    public static UuidStringWithBytes[] CorrectXStrings { get; } = UuidTestsUtils.GenerateXStrings();

    public static string[] LargeXStrings { get; } = CorrectXStrings
        .Select(x => x.String + "f")
        .ToArray();

    public static string[] SmallXStrings { get; } = CorrectXStrings
        .Select(x => x.String[(x.String.Length / 2)..])
        .ToArray();

    public static string[] BrokenXStrings { get; } = UuidTestsUtils.GenerateBrokenXStringsArray();

    private static int CompareByteArrays(byte[] left, byte[] right)
    {
        if (left == null)
        {
            throw new ArgumentNullException(nameof(left));
        }

        if (right == null)
        {
            throw new ArgumentNullException(nameof(right));
        }

        if (left.Length != 16)
        {
            throw new ArgumentOutOfRangeException(nameof(left));
        }

        if (right.Length != 16)
        {
            throw new ArgumentOutOfRangeException(nameof(right));
        }

        for (var i = 0; i < 16; i++)
        {
            if (left[i] != right[i])
            {
                return left[i] < right[i] ? -1 : 1;
            }
        }

        return 0;
    }

    public static object[] LeftLessThanRight()
    {
        object[] src = CorrectCompareToArraysAndResult;
        var results = new List<object>();
        foreach (object arg in src)
        {
            var args = (object[]) arg;
            var left = (byte[]) args[0];
            var right = (byte[]) args[1];
            var flag = (int) args[2];
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
        object[] src = CorrectCompareToArraysAndResult;
        var results = new List<object>();
        foreach (object arg in src)
        {
            var args = (object[]) arg;
            var left = (byte[]) args[0];
            var right = (byte[]) args[1];
            var flag = (int) args[2];
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

    public static object[] GetAllUuidVersions()
    {
        return new object[]
        {
            new object[]
            {
                "C232AB00-9414-11EC-B3C8-9F6BDECED846",
                1
            },
            new object[]
            {
                "000003e8-a2bd-21ef-8f00-325096b39f47",
                2
            },
            new object[]
            {
                "5df41881-3aed-3515-88a7-2f4a814cf09e",
                3
            },
            new object[]
            {
                "919108f7-52d1-4320-9bac-f847db4148a8",
                4
            },
            new object[]
            {
                "2ed6657d-e927-568b-95e1-2665a8aea6a2",
                5
            },
            new object[]
            {
                "1EC9414C-232A-6B00-B3C8-9F6BDECED846",
                6
            },
            new object[]
            {
                "017F22E2-79B0-7CC3-98C4-DC0C0C07398F",
                7
            },
            new object[]
            {
                "2489E9AD-2EE2-8E00-8EC9-32D5F69181C0",
                8
            }
        };
    }

    public static object[] GetAllUuidVariants()
    {
        return new object[]
        {
            new object[]
            {
                "01932c7c3db674000f1aa8ecd44dba8e",
                0 //0b[0000]1111 >> 4 = 0b[0000] aka [ 0 x x x ] - Reserved. Network Computing System (NCS) backward compatibility, and includes Nil UUID as per Section 5.9 (RFC9562).
            },
            new object[]
            {
                "01932c7db4f974c78f4112fec6261162",
                8 //0b[1000]1111 >> 4 = 0b[1000] aka [ 1 0 x x ] - The variant specified in this document (RFC9562).
            },
            new object[]
            {
                "01932c7e84f2701bcf01138af0833ce7",
                12 //0b[1100]1111 >> 4 = 0b[1100] aka [ 1 1 0 x ] - Reserved. Microsoft Corporation backward compatibility (RFC9562).
            },
            new object[]
            {
                "01932c7fafd776a2efd103f756df5a38",
                14 //0b[1110]1111 >> 4 = 0b[1110] aka [ 1 1 1 x ] - Reserved for future definition and includes Max UUID as per Section 5.10 (RFC9562).
            },
            new object[]
            {
                "01932c834499700bff144503c6259bdc",
                15 //0b[1111]1111 >> 4 = 0b[1111] aka [ 1 1 1 x ] - Reserved for future definition and includes Max UUID as per Section 5.10 (RFC9562).
            }
        };
    }

    private record ByteArraysToCompare(byte[] Left, byte[] Right)
    {
        public object[] ToArgs()
        {
            return new object[]
            {
                Left,
                Right,
                CompareByteArrays(Left, Right)
            };
        }
    }

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
