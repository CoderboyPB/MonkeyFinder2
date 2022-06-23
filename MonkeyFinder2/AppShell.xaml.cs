using MonkeyFinder2.View;

namespace MonkeyFinder2;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(Test), typeof(Test));
    }
}
