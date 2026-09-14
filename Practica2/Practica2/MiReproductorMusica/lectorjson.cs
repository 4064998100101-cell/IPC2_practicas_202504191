
namespace MiReproductorMusica
{
using System;
using System.IO;
using System.Text.Json;

    public class LectorJson
{

    public ColaCanciones CargarColaCanciones(string rutaArchivo)
    {
        ColaCanciones colaReproduccion = new ColaCanciones();

        try
        {
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine($"El archivo no se encontró en la ruta: {rutaArchivo}");
                return colaReproduccion;
            }

            string jsonString = File.ReadAllText(rutaArchivo);

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            Cancion[] cancionesLeidas = JsonSerializer.Deserialize<Cancion[]>(jsonString, opciones);

            if (cancionesLeidas != null)
            {
                foreach (var cancion in cancionesLeidas)
                {

                    colaReproduccion.Encolar(cancion);
                }
            }

            Console.WriteLine("¡Canciones cargadas exitosamente en la cola de reproducción!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar el archivo JSON: {ex.Message}");
        }

        return colaReproduccion;
    }
}
}