namespace MikkelApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        ImageCarousel.ItemsSource = new List<string>
        {
            "trending_carousel1.jpg",
            "trending_carousel2.jpg",
            "trending_carousel3.jpg"
        };
    }

    private async void OnAnimationButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GifPage());
    }
}
