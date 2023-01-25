using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace ContactLib.Utility;

/// <summary>Provides utility functions for working with files.</summary>
public static class FileUtility
{

    /// <summary>Reads and deserializes the file, returns <see langword="true"/> if successful.</summary>
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

    /// <summary>Serializes and writes <paramref name="value"/> to disk.</summary>
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
