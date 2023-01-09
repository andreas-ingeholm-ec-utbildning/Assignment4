using System.Text.Json;

static class ContactUtility
{

    /// <summary>Initializes ContactUtility. Loads contacts from persistent storage.</summary>
    public static void Initialize()
    {
        if (File.Exists("contacts.json"))
            contacts = JsonSerializer.Deserialize<List<Contact>>(File.ReadAllText("contacts.json")) ?? new();
    }

    /// <summary>Save contacts to persistent storage.</summary>
    public static void Save() =>
        File.WriteAllText("contacts.json", JsonSerializer.Serialize(contacts));

    static List<Contact> contacts = new();

    /// <summary>Enumerate all contacts.</summary>
    public static IEnumerable<Contact> Enumerate() =>
        contacts;

    /// <summary>Adds a contact.</summary>
    public static void Add(Contact contact)
    {
        contacts.Add(contact);
        Save();
    }

    /// <summary>Removes a contact.</summary>
    public static void Remove(Contact contact)
    {
        if (contacts.Remove(contact))
            Save();
    }

}
