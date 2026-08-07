

namespace Practica1
{
    public class Perro : Mascota
    {
        public string Raza { get; set; }
        public string Tamano { get; set; }

        public Perro(string nombre, double peso, string sexo, int edad, string propietario, string raza, string tamano)
            : base(nombre, peso, sexo, edad, propietario)
        {
            Raza = raza;
            Tamano = tamano;
        }

        public override double CalcularDosis(double dosisPorKg)
        {
            return Peso * dosisPorKg;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Raza: {Raza}");
            Console.WriteLine($"Tamaño: {Tamano}");
        }
    }
}