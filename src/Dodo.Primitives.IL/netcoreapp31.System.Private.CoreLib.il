.class public auto ansi abstract sealed beforefieldinit Dodo.Primitives.IL.CoreLib
	extends [System.Runtime]System.Object
{
	// Methods
	.method public hidebysig static 
		string FastAllocateString (
			int32 length
		) cil managed aggressiveinlining aggressiveoptimization
	{
		.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
			01 00 01 00 00
		)
		.param [0]
			.custom instance void [System.Runtime]System.Diagnostics.CodeAnalysis.NotNullAttribute::.ctor() = (
				01 00 00 00
			)
		// Method begins at RVA 0x20a5
		// Code size 7 (0x7)
		.maxstack 8
		ldarg.0
		call string [System.Runtime]System.String::FastAllocateString(int32)
		ret
	} // end of method CoreLib::FastAllocateString
} // end of class Dodo.Primitives.IL.CoreLib
