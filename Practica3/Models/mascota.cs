using System.Xml.Serialization;

namespace Practica3.Models{
    // Clase abstracta base: accesible desde cualquier otro archivo del mismo namespace
    public abstract class Mascota
    {
        // Atributos encapsulados
        public string Nombre {  get; private set; }
        public double Peso { get; private set; } 
        private string Sexo { get; set; }
        private int Edad { get; set; }
        public string Propietario { get; private set; }
        public string CodigoUnico { get; private set; }
        public string Estado { get; private set; }

        // Constructor
        public Mascota(string nombre, double peso, string sexo, int edad, string propietario)
        {
            Nombre = nombre;
            Peso = peso;
            Sexo = sexo;
            Edad = edad;
            Propietario = propietario;
            CodigoUnico = GenerarCodigoUnico();
            Estado = "Enfermo";
        }

        private string GenerarCodigoUnico()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] stringChars = new char[8];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = caracteres[random.Next(caracteres.Length)];
            }

            return new string(stringChars);
        }

        public abstract double CalcularDosis(double dosisPorKg);

        public void CambiarEstado(string nuevoEstado)
        {
            Estado = nuevoEstado;
        }

        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"\n--- Código: {CodigoUnico} ---");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Tipo: {GetType().Name}");
            Console.WriteLine($"Edad: {Edad} años");
            Console.WriteLine($"Sexo: {Sexo}");
            Console.WriteLine($"Peso: {Peso} kg");
            Console.WriteLine($"Propietario: {Propietario}");
            Console.WriteLine($"Estado: {Estado}");
        }
    }
}