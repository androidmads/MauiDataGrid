using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private readonly HttpClient httpClient = new();
    public bool IsRefreshing { get; set; }
    public ObservableCollection<Monkey> Items { get; set; } = new();
    public Command RefreshCommand { get; set; }
    public Monkey SelectedItem { get; set; }

    public MainPage()
    {
        RefreshCommand = new Command(async () =>
        {
            // Simulate delay
            await Task.Delay(2000);

            await LoadData();

            IsRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        });

        BindingContext = this;
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(2000);
        await LoadData();
    }

    private async Task LoadData()
    {
        var data = await httpClient.GetFromJsonAsync<Monkey[]>("https://montemagno.com/monkeys.json");

        Items.Clear();

        foreach (Monkey monkey in data)
        {
            Items.Add(monkey);
        }
    }


    public class Monkey
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public int Population { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}