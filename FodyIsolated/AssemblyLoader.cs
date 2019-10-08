using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public partial class InnerWeaver
{
    static Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>(StringComparer.OrdinalIgnoreCase);

    public Assembly LoadWeaverAssembly(string assemblyPath)
    {
        if (assemblies.TryGetValue(assemblyPath, out var assembly))
        {
            Logger.LogDebug($"  Loading '{assemblyPath}' from cache.");
            return assembly;
        }
        Logger.LogDebug($"  Loading '{assemblyPath}' from disk.");
        return assemblies[assemblyPath] = LoadFromFile(assemblyPath);
    }

    static Assembly LoadFromFile(string assemblyPath)
    {
        var rawAssembly = File.ReadAllBytes(assemblyPath);
        return Assembly.Load(rawAssembly);
    }
}