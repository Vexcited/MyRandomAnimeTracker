namespace MikkelApp;

public partial class GifPage : ContentPage
{
    public GifPage()
    {
        InitializeComponent();
        GifImage.IsAnimationPlaying = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GifImage.IsAnimationPlaying = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        GifImage.IsAnimationPlaying = false;
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
