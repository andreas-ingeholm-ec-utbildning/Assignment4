using System;
using System.Windows.Input;
using System.Windows.Markup;
using Assignment4;
using Assignment4.Models;

namespace Assignment4.Commands;

public class RemoveContact : MarkupExtension, ICommand
{

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        if (parameter is Contact contact)
            ContactUtility.RemoveContact(contact);
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => this;

}