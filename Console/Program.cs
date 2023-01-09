Main();
void Main()
{

    //Load contacts from storage
    ContactUtility.Initialize();

    while (true)
    {

        Console.Clear();

        Console.WriteLine("Welcome to the adress book!\n");
        ConsoleUtility.Choose(BadInput, prompt: () => Console.WriteLine("\nChoose an option:"),
            ("View all contacts", List),
            ("View a contact", () => ChooseContact(ViewContact)),
            ("Add contact", () => Add()),
            ("Remove a contact", () => ChooseContact(Remove))
            );

    }

}

void BadInput() =>
    Console.WriteLine("Bad input. Please type the number of the corresponding option.");

void List()
{

    var contacts = ContactUtility.Enumerate();

    Console.Clear();

    WriteColumn("Firstname", "Lastname", "Email address");

    if (!contacts.Any())
        Console.WriteLine("No contacts added.");
    else
        foreach (var contact in contacts)
            WriteColumn(contact.FirstName, contact.LastName, contact.Email);

    Console.WriteLine("\nPress any key to return");
    _ = Console.ReadKey();

    void WriteColumn(params string?[] columns) =>
        Console.WriteLine(string.Join(string.Empty, columns.Select((c, i) => (c ?? "").PadRight(ColumnWidths().ElementAtOrDefault(i) + 4))));

    IEnumerable<int> ColumnWidths()
    {

        var items = contacts;

        if (!items.Any())
            items = new[] { new Contact() };

        return new[]
        {
            items.Select(c => Math.Max(c.FirstName?.Length ?? 0, "Firstname".Length)).Max(),
            items.Select(c => Math.Max(c.LastName?.Length ?? 0, "Lastname".Length)).Max(),
            items.Select(c => Math.Max(c.Email?.Length ?? 0, "Email address".Length)).Max()
        };

    }

}

void ViewContact(Contact contact)
{

    Console.Clear();
    WriteDetails(contact);

    Console.WriteLine("Press any key to return");
    _ = Console.ReadKey();

}

void Add(Contact? contact = null, int? fieldIndex = null, bool ignorePrompt = false)
{

    Console.Clear();
    contact ??= new Contact();

    if (!ignorePrompt)
        if (fieldIndex is null)
        {
            Console.WriteLine("To add a contact, the following information is needed:");
            for (int i = 0; i <= 4; i++)
                Prompt(i);
        }
        else
            Prompt(fieldIndex.Value);

    Console.Clear();
    WriteDetails(contact);

    Console.WriteLine("\nIs this information correct?");
    var option = ConsoleUtility.Choose(
          "Yes",
          "Cancel",
          "Change firstname",
          "Change lastname",
          "Change email address",
          "Change phone number",
          "Change address"
          );

    if (option == 0)
        ContactUtility.Add(contact);
    else if (option == 1)
        return;
    else if (option <= 6)
    {
        Add(contact, option - 2);
        List();
    }
    else
        Add(contact, ignorePrompt: true);

    void Prompt(int fieldIndex)
    {

        if (contact is null)
            return;

        switch (fieldIndex)
        {
            case 0: Console.Write("Firstname: "); contact.FirstName = Console.ReadLine(); break;
            case 1: Console.Write("Lastname: "); contact.LastName = Console.ReadLine(); break;
            case 2: Console.Write("Email address: "); contact.Email = Console.ReadLine(); break;
            case 3: Console.Write("Phone number: "); contact.PhoneNumber = Console.ReadLine(); break;
            case 4: Console.Write("Adress: "); contact.Address = Console.ReadLine(); break;
            default:
                break;
        }

    }

}

void Remove(Contact contact)
{

    Console.Clear();
    Console.WriteLine("Are you sure you wish to remove the following contact?\n");
    WriteDetails(contact);
    Console.WriteLine();

    if (ConsoleUtility.Prompt(""))
    {
        ContactUtility.Remove(contact);
        List();
    }

}

void WriteDetails(Contact contact)
{
    Console.WriteLine("Firstname: " + contact.FirstName);
    Console.WriteLine("Lastname: " + contact.LastName);
    Console.WriteLine("Email address: " + contact.Email);
    Console.WriteLine("Phone number: " + contact.PhoneNumber);
    Console.WriteLine("Address: " + contact.Address);
}

void ChooseContact(Action<Contact> continueWith)
{

    Console.Clear();

    if (!ContactUtility.Enumerate().Any())
    {
        Console.WriteLine("No contacts added");
        Console.WriteLine("\nPress any key to return");
        _ = Console.ReadKey();
    }
    else
    {

        var contacts = ContactUtility.Enumerate().ToArray();
        var index = ConsoleUtility.Choose(() => Console.WriteLine("\nChoose a contact:"), ContactUtility.Enumerate().Select(c => c.ToString()).ToArray());
        if (contacts.ElementAtOrDefault(index ?? -1) is Contact contact)
            continueWith.Invoke(contact);

    }

}
