using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;

namespace Assignment4.Utility;

public static class FileUtility
{

    public static bool Load<T>(string file, [NotNullWhen(true)] out T? value)
    {

        value = default;
        try
        {
            if (File.Exists(file))
                value = JsonSerializer.Deserialize<T>(File.ReadAllText(file));
        }
        catch
        {
            throw;
        }

        return value is not null;

    }

    public static void Save<T>(string file, T value)
    {

        if (value is null)
            throw new ArgumentNullException(nameof(value));

        try
        {
            File.WriteAllText(file, JsonSerializer.Serialize(value));
        }
        catch
        {
            throw;
        }

    }

}
