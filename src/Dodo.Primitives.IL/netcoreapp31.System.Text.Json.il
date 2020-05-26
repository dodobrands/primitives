
.class public auto ansi abstract sealed beforefieldinit Dodo.Primitives.IL.SystemTextJson
	extends [System.Runtime]System.Object
{
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
		01 00 00 00
	)
	// Methods
	.method public hidebysig static
		bool IsStringHasEscaping (
			valuetype [System.Text.Json]System.Text.Json.Utf8JsonReader& reader
		) cil managed aggressiveinlining aggressiveoptimization
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x2050
		// Code size 7 (0x7)
		.maxstack 8
		IL_0000: ldarg.0
		IL_0001: ldfld bool [System.Text.Json]System.Text.Json.Utf8JsonReader::_stringHasEscaping
		IL_0006: ret
	} // end of method SystemTextJson::IsStringHasEscaping
	.method public hidebysig static
		void Unescape (
			valuetype [System.Runtime]System.ReadOnlySpan`1<uint8> source,
			valuetype [System.Runtime]System.Span`1<uint8> destination,
			int32 idx,
			[out] int32& written
		) cil managed aggressiveinlining aggressiveoptimization
	{
		// Method begins at RVA 0x2058
		// Code size 10 (0xa)
		.maxstack 8
		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: ldarg.2
		IL_0003: ldarg.3
		IL_0004: call void [System.Text.Json]System.Text.Json.JsonReaderHelper::Unescape(valuetype [System.Runtime]System.ReadOnlySpan`1<uint8>, valuetype [System.Runtime]System.Span`1<uint8>, int32, int32&)
		IL_0009: ret
	} // end of method SystemTextJson::Unescape
} // end of class Dodo.Primitives.IL.SystemTextJson
