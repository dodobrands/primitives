.class public auto ansi beforefieldinit System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute
	extends [System.Runtime]System.Attribute
{
	.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 01 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	.custom instance void [System.Runtime]System.AttributeUsageAttribute::.ctor(valuetype [System.Runtime]System.AttributeTargets) = (
		01 00 01 00 00 00 01 00 54 02 0d 41 6c 6c 6f 77
		4d 75 6c 74 69 70 6c 65 01
	)
	// Fields
	.field private initonly string '<AssemblyName>k__BackingField'
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)

	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor (
			string assemblyName
		) cil managed 
	{
		// Method begins at RVA 0x208e
		// Code size 14 (0xe)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [System.Runtime]System.Attribute::.ctor()
		IL_0006: ldarg.0
		IL_0007: ldarg.1
		IL_0008: stfld string System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute::'<AssemblyName>k__BackingField'
		IL_000d: ret
	} // end of method IgnoresAccessChecksToAttribute::.ctor

	.method public hidebysig specialname 
		instance string get_AssemblyName () cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x209d
		// Code size 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld string System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute::'<AssemblyName>k__BackingField'
		IL_0006: ret
	} // end of method IgnoresAccessChecksToAttribute::get_AssemblyName

	// Properties
	.property instance string AssemblyName()
	{
		.get instance string System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute::get_AssemblyName()
	}

} // end of class System.Runtime.CompilerServices.IgnoresAccessChecksToAttribute


.class private auto ansi sealed beforefieldinit System.Runtime.CompilerServices.NullableAttribute
	extends [System.Runtime]System.Attribute
{
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void Microsoft.CodeAnalysis.EmbeddedAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void [System.Runtime]System.AttributeUsageAttribute::.ctor(valuetype [System.Runtime]System.AttributeTargets) = (
		01 00 84 6b 00 00 02 00 54 02 0d 41 6c 6c 6f 77
		4d 75 6c 74 69 70 6c 65 00 54 02 09 49 6e 68 65
		72 69 74 65 64 00
	)
	// Fields
	.field public initonly uint8[] NullableFlags

	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor (
			uint8 ''
		) cil managed 
	{
		// Method begins at RVA 0x2058
		// Code size 23 (0x17)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [System.Runtime]System.Attribute::.ctor()
		IL_0006: ldarg.0
		IL_0007: ldc.i4.1
		IL_0008: newarr [System.Runtime]System.Byte
		IL_000d: dup
		IL_000e: ldc.i4.0
		IL_000f: ldarg.1
		IL_0010: stelem.i1
		IL_0011: stfld uint8[] System.Runtime.CompilerServices.NullableAttribute::NullableFlags
		IL_0016: ret
	} // end of method NullableAttribute::.ctor

	.method public hidebysig specialname rtspecialname 
		instance void .ctor (
			uint8[] ''
		) cil managed 
	{
		// Method begins at RVA 0x2070
		// Code size 14 (0xe)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [System.Runtime]System.Attribute::.ctor()
		IL_0006: ldarg.0
		IL_0007: ldarg.1
		IL_0008: stfld uint8[] System.Runtime.CompilerServices.NullableAttribute::NullableFlags
		IL_000d: ret
	} // end of method NullableAttribute::.ctor

} // end of class System.Runtime.CompilerServices.NullableAttribute

.class private auto ansi sealed beforefieldinit System.Runtime.CompilerServices.NullableContextAttribute
	extends [System.Runtime]System.Attribute
{
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void Microsoft.CodeAnalysis.EmbeddedAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void [System.Runtime]System.AttributeUsageAttribute::.ctor(valuetype [System.Runtime]System.AttributeTargets) = (
		01 00 4c 14 00 00 02 00 54 02 0d 41 6c 6c 6f 77
		4d 75 6c 74 69 70 6c 65 00 54 02 09 49 6e 68 65
		72 69 74 65 64 00
	)
	// Fields
	.field public initonly uint8 Flag

	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor (
			uint8 ''
		) cil managed 
	{
		// Method begins at RVA 0x207f
		// Code size 14 (0xe)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [System.Runtime]System.Attribute::.ctor()
		IL_0006: ldarg.0
		IL_0007: ldarg.1
		IL_0008: stfld uint8 System.Runtime.CompilerServices.NullableContextAttribute::Flag
		IL_000d: ret
	} // end of method NullableContextAttribute::.ctor

} // end of class System.Runtime.CompilerServices.NullableContextAttribute