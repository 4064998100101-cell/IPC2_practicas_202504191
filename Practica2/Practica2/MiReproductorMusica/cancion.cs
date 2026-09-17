using System.Text.Json.Serialization;

public class Cancion
{
    [JsonPropertyName("titulo")]
    [JsonInclude]
    public string Titulo { get; set; }

    [JsonPropertyName("artista")]
    [JsonInclude]
    public string Artista { get; set; }

    [JsonPropertyName("genero")]
    [JsonInclude]
    public string Genero { get; set; }

    [JsonPropertyName("duracion")]
    [JsonInclude]
    public int Duracion { get; set; }
}