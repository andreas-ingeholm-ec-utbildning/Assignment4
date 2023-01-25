using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text.Json.Serialization;
using Assignment4.Models;
using Assignment4.Utility;

namespace Assignment4;

/// <summary>Represents a list of <see cref="Contact"/>.</summary>
public class ContactList : ObservableCollection<Contact>
{

    #region Constructors

    /// <summary>Creates a <see cref="ContactList"/> from a file.</summary>
    /// <param name="file">The file to deserialize.</param>
    /// <param name="createIfNotExists">Create if file did not exist. <see cref="FileNotFoundException"/> will be thrown if <see langword="false"/>.</param>
    public static ContactList FromFile(string file, bool createIfNotExists = false)
    {

        if (FileUtility.Load<ContactList>(file, out var result))
            return result;

        if (createIfNotExists)
            return new(file);
        else
            throw new FileNotFoundException();

    }

    /// <summary>Creates an in-memory <see cref="ContactList"/>.</summary>
    /// <remarks><see cref="Save(string)"/> can be used to convert to normal.</remarks>
    public static ContactList InMemory() =>
        new(null);

    ContactList(string? file) =>
        Path = file;

    /// <summary>JsonConstructor.</summary>
    /// <remarks>See also: <see cref="FromFile(string, bool)"/>, or <see cref="InMemory"/>.</remarks>
    [JsonConstructor]
    public ContactList()
    { }

    #endregion
    #region Save

    /// <summary>Saves the file at the specified path.</summary>
    public void Save(string file)
    {
        Path = file;
        Save();
    }

    /// <summary>Saves the file at the specified path.</summary>
    /// <remarks>Throws <see cref="InvalidOperationException"/> if <see cref="ContactList"/> is currently in-memory.</remarks>
    public void Save()
    {
        if (string.IsNullOrEmpty(Path))
            throw new InvalidOperationException("Cannot save an in-memory contact list.");
        FileUtility.Save(Path, this);
    }

    protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        base.OnCollectionChanged(e);
        if (!string.IsNullOrEmpty(Path))
            Save();
    }

    #endregion

    /// <summary>The path to file file that this <see cref="ContactList"/> is persisted at.</summary>
    /// <remarks><see cref="Save(string)"/> can be used to save an in-memory <see cref="ContactList"/>.</remarks>
    public string? Path { get; private set; }

    /// <summary>Creates a contact with the specified fields.</summary>
    /// <param name="firstName">The first name of the contact.</param>
    /// <param name="lastName">The last name of the contact.</param>
    /// <param name="email">The email of the contact.</param>
    /// <param name="phoneNumber">The phone number of the contact.</param>
    /// <param name="address">The address of the contact.</param>
    public void CreateContact(string? firstName, string? lastName, string? email, string? phoneNumber, string? address) =>
        Add(new() { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phoneNumber, Address = address });

}
