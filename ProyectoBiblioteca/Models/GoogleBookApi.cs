namespace ProyectoBiblioteca.Models;

public class GoogleBook
{
    public string id { get; set; }
    public VolumeInfo volumeInfo { get; set; }
}

public class VolumeInfo
{
    public string title { get; set; }
    public List<string> authors { get; set; }
    public string description { get; set; }
    public ImageLinks imageLinks { get; set; }
}

public class ImageLinks
{
    public string thumbnail { get; set; }
}
