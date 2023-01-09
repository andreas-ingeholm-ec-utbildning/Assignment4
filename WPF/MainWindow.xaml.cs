using System.Windows;

namespace Assignment4;

public partial class MainWindow : Window
{

    public static readonly DependencyProperty SelectedContactProperty =
        DependencyProperty.Register("SelectedContact", typeof(Contact), typeof(MainWindow), new PropertyMetadata(null));

    public Contact SelectedContact
    {
        get => (Contact)GetValue(SelectedContactProperty);
        set => SetValue(SelectedContactProperty, value);
    }

    public MainWindow() =>
        InitializeComponent();

}
