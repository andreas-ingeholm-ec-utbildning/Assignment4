using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment4.ViewModels;

public partial class AddContact : ViewModel
{

    [ObservableProperty, NotifyDataErrorInfo]
    [Required]
    private string? firstName;

    [ObservableProperty, NotifyDataErrorInfo]
    [Required]
    private string? lastName;

    [ObservableProperty, NotifyDataErrorInfo]
    [Required, EmailAddress]
    private string? email;

    [ObservableProperty, NotifyDataErrorInfo]
    [Required, Phone]
    private string? phoneNumber;

    [ObservableProperty, NotifyDataErrorInfo]
    [Required]
    private string? address;

    [ObservableProperty]
    private bool canAdd;

    //Gets if all fields have a value, if they have, then they will also have been validated.
    //Issue with base.ValidateAllProperties() is that it triggers error visual, which I don't
    //want before user has interacted with fields
    bool isFilled =>
        !GetType().GetProperties().
        Where(p => p.PropertyType == typeof(string)).
        Select(p => p.GetValue(this)).
        Any(s => string.IsNullOrWhiteSpace(s as string));

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        CanAdd = !HasErrors && isFilled;
    }

    [RelayCommand]
    void Add()
    {
        ContactUtility.CreateContact(firstName, lastName, email, phoneNumber, address);
        ReturnToMainView();
    }

    [RelayCommand]
    void Discard() =>
        ReturnToMainView();

    [RelayCommand]
    void AddTestData()
    {
        FirstName = "test";
        LastName = "test";
        Email = "test@test";
        PhoneNumber = "10";
        Address = "test";
    }

}
