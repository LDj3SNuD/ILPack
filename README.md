﻿# Lokad.ILPack

Exports a .NET type to a serialized assembly. This feature existed in .NET Framework,
but has been lost with .NET Core, hence, it is re-implemented here.

Usage:

```cs
var assembly = Assembly.GetAssembly(t);
var generator = new AssemblyGenerator();

// for ad-hoc serialization
var bytes = generator.GenerateAssemblyBytes(assembly);

// direct serialization to disk
generator.GenerateAssembly(assembly, "/path/to/file");
```

Based on an open source initial implementation at: https://github.com/Dolfik1/AssemblyGenerator
