using System.Collections.ObjectModel;
using System.Linq;
using Assignment4.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment4.ViewModels;

public partial class ListContacts : ViewModel
{

    public ReadOnlyObservableCollection<Contact> Contacts => ContactUtility.Contacts;

    [ObservableProperty]
    private Contact? selectedContact;

    [RelayCommand]
    void RemoveContact(Contact contact)
    {

        var i = Contacts.IndexOf(contact);
        ContactUtility.RemoveContact(contact);

        if (i == -1)
            return;

        SelectedContact = Contacts.ElementAtOrDefault(i) ?? Contacts.LastOrDefault();

    }

}
