using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment4.ViewModels;

public partial class ViewModel : ObservableValidator
{

    #region ReturnToMainView

    public void ReturnToMainView() =>
        OnReturnRequest?.Invoke();

    public ViewModel AllowReturn(Action action)
    {
        OnReturnRequest = action;
        return this;
    }

    public Action? OnReturnRequest { get; private set; }

    #endregion
    #region ContactList

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [ObservableProperty] private ContactList contacts;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ViewModel With(ContactList list)
    {
        Contacts = list;
        return this;
    }

    #endregion

}

public partial class Main : ObservableObject
{

    public static ContactList Contacts { get; } = ContactList.FromFile("contacts.json", createIfNotExists: true);

    [ObservableProperty]
    private ViewModel currentViewModel;

    public bool IsListContactsView => currentViewModel is ViewModels.ListContacts;
    public bool IsAddContactView => currentViewModel is ViewModels.AddContact;

    [RelayCommand]
    private void GoToAddContact()
    {
        CurrentViewModel = new ViewModels.AddContact().With(Contacts).AllowReturn(GoToListContacts);
        OnPropertyChanged(nameof(IsListContactsView));
        OnPropertyChanged(nameof(IsAddContactView));
    }

    [RelayCommand]
    private void GoToListContacts()
    {
        CurrentViewModel = new ViewModels.ListContacts().With(Contacts);
        OnPropertyChanged(nameof(IsListContactsView));
        OnPropertyChanged(nameof(IsAddContactView));
    }

    public Main() =>
        GoToListContacts();

}
