using MonkeyFinder2.Services;
using MonkeyFinder2.View;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace MonkeyFinder2.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;
    IConnectivity connectivity;
    IGeolocation geolocation;

    public ObservableCollection<Monkey> Monkeys { get; } = new();

    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        this.monkeyService = monkeyService;
        Title = "Monkey Finder";
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GetMonkeysAsync()
    {
        IsRefreshing = false;
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess == NetworkAccess.None)
            {
                await Shell.Current.DisplayAlert("Internet issue!", $"Check your Internet and try again", "OK");
                return;
            }

            IsBusy = true;
            var monkeys = await monkeyService.GetMonkeys();

            if (Monkeys.Count != 0)
                Monkeys.Clear();

            monkeys.ForEach(m => Monkeys.Add(m));
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get monkeys: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Monkey monkey)
    {
        if (monkey is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, 
            new Dictionary<string,object>
            {
                {"Monkey", monkey }
            });
    }

    [RelayCommand]
    async Task GetClosestMonkey()
    {
        if (IsBusy || Monkeys.Count == 0)
            return;

        try
        {
            var location = await geolocation.GetLastKnownLocationAsync();
            if(location is null)
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromMinutes(1)
                });
            }

            if (location is null)
                return;

            var closestMonkey = Monkeys
                .OrderBy(m => location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Kilometers))
                .FirstOrDefault();

            if (closestMonkey is null)
                return;

            await Shell.Current.DisplayAlert("Closest Monkey", $"{closestMonkey.Name} in {closestMonkey.Location}", "OK");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await Shell.Current.DisplayAlert("Error!", $"Unable to find closest monkey: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    void SwitchMode()
    {
        var currentMode = Application.Current.UserAppTheme;

        if (currentMode == AppTheme.Light || currentMode == AppTheme.Unspecified)
            Application.Current.UserAppTheme = AppTheme.Dark;
        else
            Application.Current.UserAppTheme = AppTheme.Light;
    }

    [RelayCommand]
    async Task Foo()
    {
        CancellationTokenSource cancellationTokenSource = new();

        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = Colors.Red,
            TextColor = Colors.Green,
            ActionButtonTextColor = Colors.Yellow,
            CornerRadius = new CornerRadius(10),
            Font = Font.SystemFontOfSize(14),
            ActionButtonFont = Font.SystemFontOfSize(14),
            CharacterSpacing = 0.5
        };

        string text = "This is a snackbar";
        string actionButtonText = "click here to dismiss";
        Action action = async () =>
            await Shell.Current.DisplayAlert("Snackbar ActionButton Tapped", "The user has tapped the Snackbar ActionButton", "OK");
        TimeSpan duration = TimeSpan.FromSeconds(3);

        var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);
        var toast = Toast.Make(text, ToastDuration.Long, 14);
        //await snackbar.Show(cancellationTokenSource.Token);
        await toast.Show();
    }

    [RelayCommand]
    async Task GoToTest()
    {
        await Shell.Current.GoToAsync(nameof(Test), true);
    }
}