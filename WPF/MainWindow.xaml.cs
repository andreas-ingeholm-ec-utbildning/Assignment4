using System.Windows;
using Assignment4.Models;

namespace Assignment4;

public partial class MainWindow : Window
{

    public MainWindow() =>
        InitializeComponent();

    public static readonly DependencyProperty SelectedContactProperty =
        DependencyProperty.Register("SelectedContact", typeof(Contact), typeof(MainWindow), new PropertyMetadata(null));

    public Contact SelectedContact
    {
        get => (Contact)GetValue(SelectedContactProperty);
        set => SetValue(SelectedContactProperty, value);
    }

}
