using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment4.ViewModels;

public class ViewModel : ObservableValidator
{

    public void ReturnToMainView() =>
        OnReturnRequest?.Invoke();

    public ViewModel AllowReturn(Action action)
    {
        OnReturnRequest = action;
        return this;
    }

    public Action? OnReturnRequest { get; private set; }

}

public partial class Main : ObservableObject
{

    [ObservableProperty]
    private ViewModel currentViewModel;

    public bool IsListContactsView => currentViewModel is ViewModels.ListContacts;
    public bool IsAddContactView => currentViewModel is ViewModels.AddContact;

    [RelayCommand]
    private void GoToAddContact()
    {
        CurrentViewModel = new ViewModels.AddContact().AllowReturn(GoToListContacts);
        OnPropertyChanged(nameof(IsListContactsView));
        OnPropertyChanged(nameof(IsAddContactView));
    }

    [RelayCommand]
    private void GoToListContacts()
    {
        CurrentViewModel = new ViewModels.ListContacts();
        OnPropertyChanged(nameof(IsListContactsView));
        OnPropertyChanged(nameof(IsAddContactView));
    }

    public Main() =>
        currentViewModel = new ListContacts();

}
