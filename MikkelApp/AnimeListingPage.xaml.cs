using MikkelApp.Models;
using MikkelApp.Services;

namespace MikkelApp;

public partial class AnimeListingPage : ContentPage
{
    private readonly AnimeService _animeService;
    private List<Anime> _animes;

    public AnimeListingPage()
    {
        InitializeComponent();
        _animeService = new AnimeService();
        _animes = new List<Anime>();
        BindingContext = this;
        LoadAnimes();
    }

    private async void LoadAnimes()
    {
        try
        {
            _animes = await _animeService.GetTopAnimeAsync();
            AnimeCollection.ItemsSource = _animes;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load anime: {ex.Message}", "OK");
        }
    }

    public Command<Anime> AnimeTappedCommand => new Command<Anime>(async (anime) =>
    {
        var message = $"Title: {anime.Title}\n" +
                     $"Type: {anime.Type}\n" +
                     $"Episodes: {anime.Episodes}\n" +
                     $"Score: {anime.Score}\n" +
                     $"Status: {anime.Status}\n" +
                     $"Rating: {anime.Rating}\n\n" +
                     $"Synopsis:\n{anime.Synopsis}";

        await DisplayAlert("Anime Details", message, "OK");
    });
}
