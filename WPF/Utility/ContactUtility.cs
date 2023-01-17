using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using Assignment4.Models;

namespace Assignment4;

static class ContactUtility
{

    static ContactUtility()
    {

        if (File.Exists("contacts.json"))
        {
            try
            {
                var json = File.ReadAllText("contacts.json");
                contacts = JsonSerializer.Deserialize<ObservableCollection<Contact>>(json)!;
            }
            catch (Exception)
            { }
        }

        contacts ??= new();
        Contacts = new(contacts);

    }

    public static void Save()
    {

        try
        {
            File.WriteAllText("contacts.json", JsonSerializer.Serialize(contacts));
        }
        catch (Exception)
        { }

    }

    static readonly ObservableCollection<Contact> contacts;
    public static ReadOnlyObservableCollection<Contact> Contacts { get; private set; }

    public static void CreateContact(string? firstName, string? lastName, string? email, string? phoneNumber, string? address)
    {
        contacts.Add(new() { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber, Address = address });
        Save();
    }

    public static void RemoveContact(Contact contact)
    {
        if (contacts.Remove(contact))
            Save();
    }

}
