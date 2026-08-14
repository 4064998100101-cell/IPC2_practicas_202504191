namespace Practica1
{
    public class Gato : Mascota
    {
        public string Raza { get; private set; }
        public bool Esterilizado { get; private set; }

        public Gato(string nombre, double peso, string sexo, int edad, string propietario, string raza, bool esterilizado)
            : base(nombre, peso, sexo, edad, propietario)
        {
            Raza = raza;
            Esterilizado = esterilizado;
        }

        // Dosis ajustada al 90% (0.90)
        public override double CalcularDosis(double dosisPorKg)
        {
            return (Peso * dosisPorKg) * 0.90;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Raza: {Raza}");
            Console.WriteLine($"Esterilizado: {(Esterilizado ? "Sí" : "No")}");
        }
    }
}