using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MikkelApp.Models;
using MikkelApp.Services;

namespace MikkelApp;

public partial class AddAnimePage : ContentPage
{
    private readonly AnimeService _animeService;
    private Anime _newAnime;

    public AddAnimePage()
    {
        InitializeComponent();
        _animeService = new AnimeService();
        _newAnime = new Anime();
        BindingContext = _newAnime;
    }

    private async void OnPickImageClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select an image"
            });

            if (result != null)
            {
                _newAnime.ImageUrl = result.FullPath;
                ImageUrlEntry.Text = result.FullPath;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to pick image: {ex.Message}", "OK");
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_newAnime.Title))
        {
            await DisplayAlert("Error", "Please enter a title", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(_newAnime.Synopsis))
        {
            await DisplayAlert("Error", "Please enter a description", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(_newAnime.ImageUrl))
        {
            await DisplayAlert("Error", "Please select an image", "OK");
            return;
        }

        try
        {
            // Set default values for required fields
            _newAnime.Type = "TV";
            _newAnime.Status = "Not yet aired";
            _newAnime.Rating = "PG-13";
            _newAnime.Episodes = 12;
            _newAnime.Score = 0;

            await _animeService.AddAnimeAsync(_newAnime);
            await DisplayAlert("Success", "Anime added successfully!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save anime: {ex.Message}", "OK");
        }
    }
}
