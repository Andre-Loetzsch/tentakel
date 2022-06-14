using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Tentakel.Extensions.Configuration.Tests;

public static class JsonStringBuilder
{
    public static string Build(IDictionary<string, object> configuration, string sectionName)
    {
        var sb = new StringBuilder();
        var typeDescriptions = new ConfiguredTypes();

        foreach (var (key, value) in configuration)
        {
            var assemblyQualifiedName = value.GetType().AssemblyQualifiedName;
            if (assemblyQualifiedName == null) continue;
            var typeInfos = assemblyQualifiedName.Split(", ");
            typeDescriptions.Add(key, new ConfiguredType { Type = $"{typeInfos[0]}, {typeInfos[1]}" });
        }

        sb.AppendLine("{")
            .Append($"  \"{sectionName}\":");

        var jsonString = JsonSerializer.Serialize(typeDescriptions, new JsonSerializerOptions { WriteIndented = true });
        var lines = jsonString.Split(Environment.NewLine);

        for (var i = 0; i < lines.Length; i++)
        {
            if (i > 0)
            {
                sb.AppendLine().Append("    ");
            }

            sb.Append(lines[i]);
        }

        foreach (var (key, value) in configuration)
        {
            sb.AppendLine(",").Append($"  \"{key}\":");

            jsonString = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
            lines = jsonString.Split(Environment.NewLine);

            for (var i = 0; i < lines.Length; i++)
            {
                if (i > 0)
                {
                    sb.AppendLine().Append("    ");
                }

                sb.Append(lines[i]);
            }
        }

        return sb.AppendLine().Append('}').ToString();
    }
}