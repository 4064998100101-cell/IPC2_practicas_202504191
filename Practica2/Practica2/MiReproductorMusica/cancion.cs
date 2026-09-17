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

    public int obtenerduracionoficial()
    {
        if (string.IsNullOrEmpty(Genero))
        {
            return Duracion;
        }

        string gen=Genero;
        if (gen.Contains("pop"))
        {
            return 3;
            
        }
        if (gen.Contains("rock"))
        {
            return 4;
            
        }
        if (gen.Contains("jazz"))
        {
            return 5;
            
        }
        if (gen.Contains("clasica") || gen.Contains("clasica"))
        {
            return 8;
            
        }       
        
        return Duracion>0 ? Duracion:Duracion; 


    }
}