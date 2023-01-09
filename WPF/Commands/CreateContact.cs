using System;
using System.Windows.Input;
using System.Windows.Markup;
using Assignment4;

namespace WPF.Commands;

public class CreateContact : MarkupExtension, ICommand
{

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => ContactUtility.CreateContact();

    public override object ProvideValue(IServiceProvider serviceProvider) => this;

}
