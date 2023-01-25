using System.Linq;
using Assignment4.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment4.ViewModels;

public partial class ListContacts : ViewModel
{

    [ObservableProperty]
    private Contact? selectedContact;

    [RelayCommand]
    void RemoveContact(Contact contact)
    {

        var i = Contacts.IndexOf(contact);
        if (Contacts.Remove(contact) && i >= 0)
            SelectedContact = Contacts.ElementAtOrDefault(i) ?? Contacts.LastOrDefault();

    }

}
