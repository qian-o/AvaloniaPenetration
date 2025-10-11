using Avalonia.Controls;

namespace AvaloniaPenetration;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += (_, _) => this.Penetration();
    }
}