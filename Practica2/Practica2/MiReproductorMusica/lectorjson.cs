
namespace MiReproductorMusica
{
using System;
using System.IO;
using System.Text.Json;
public class Carga
    {
        public ColaCanciones Cola{get; set;}
        public ArbolBinarioBusqueda Arbol {get; set;}
        public Carga()
        {
            Cola=new ColaCanciones();
            Arbol=new ArbolBinarioBusqueda();
        }
    }
    public class LectorJson
{
public Carga CargarDatos(string rutaArchivo)
        {
            Carga resultado= new Carga();
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    Console.WriteLine("No se encontro la ruta del archivo");
                    return resultado;
                }
                string jsonString =File.ReadAllText(rutaArchivo);
                var opciones=new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive=true
                };
                Cancion[] cancionesLEidas=JsonSerializer.Deserialize<Cancion[]>(jsonString, opciones);
                if (cancionesLEidas != null)
                {
                    foreach(var cancion in cancionesLEidas)
                    {
                        resultado.Cola.Encolar(cancion);
                        resultado.Arbol.Insertar(cancion);
                    }
                    
                }
                Console.WriteLine("Datos cargados con exito en ambos TDAS");
            }
            catch (System.Exception)
            {
                Console.WriteLine("Error al procesar el JSON");
            }
            return resultado;
        }

}
}