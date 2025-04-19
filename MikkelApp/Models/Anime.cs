namespace MikkelApp.Models;

public class Anime
{
    public int MalId { get; set; }
    public string Title { get; set; }
    public string TitleEnglish { get; set; }
    public string TitleJapanese { get; set; }
    public string Synopsis { get; set; }
    public double Score { get; set; }
    public string ImageUrl { get; set; }
    public string Type { get; set; }
    public int Episodes { get; set; }
    public string Status { get; set; }
    public string Rating { get; set; }
}
