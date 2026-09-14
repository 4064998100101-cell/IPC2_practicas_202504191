using System.Text.Json.Serialization;
namespace MiReproductorMusica
{
    public class Cancion
    {
       [JsonPropertyName("titulo")]
       public string Titulo {get; private set;}

       [JsonPropertyName("artista")]
       public string Artista {get; private set;}

       [JsonPropertyName("genero")]
       public string Genero {get; private set;}
       [JsonPropertyName("duracion")]
       public int Duracion {get; private set;} 
    }
}