using System;
using System.Collections.ObjectModel;
using System.Windows.Markup;

namespace Assignment4;

public class Contacts : MarkupExtension
{

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        ContactUtility.Contacts;

}

static class ContactUtility
{

    static readonly ObservableCollection<Contact> contacts = new();
    public static ReadOnlyObservableCollection<Contact> Contacts { get; } = new(contacts);

    public static void CreateContact() => contacts.Add(new());
    public static void RemoveContact(Contact contact) => contacts.Remove(contact);

}
