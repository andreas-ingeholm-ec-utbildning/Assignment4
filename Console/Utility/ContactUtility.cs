using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

static class ContactUtility
{

    public static void Initialize()
    {
        if (File.Exists("contacts.json"))
            contacts = JsonSerializer.Deserialize<List<Contact>>(File.ReadAllText("contacts.json")) ?? new();
    }

    public static void Save() =>
        File.WriteAllText("contacts.json", JsonSerializer.Serialize(contacts));

    static List<Contact> contacts = new();

    public static IEnumerable<Contact> Enumerate() =>
        contacts;

    public static bool Find(int index, [NotNullWhen(true)] out Contact? contact) =>
        (contact = Find(index)) is not null;

    public static Contact? Find(int index) =>
        contacts.ElementAtOrDefault(index);

    public static void Add(Contact contact)
    {
        contacts.Add(contact);
        Save();
    }

    public static void Remove(Contact contact)
    {
        contacts.Remove(contact);
        Save();
    }
}
