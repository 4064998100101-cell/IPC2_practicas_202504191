namespace Practica1
{
    public class Ave : Mascota
    {
        public double EnvergaduraAlas { get; set; }
        public bool PuedeVolar { get; set; }

        public Ave(string nombre, double peso, string sexo, int edad, string propietario, double envergaduraAlas, bool puedeVolar)
            : base(nombre, peso, sexo, edad, propietario)
        {
            EnvergaduraAlas = envergaduraAlas;
            PuedeVolar = puedeVolar;
        }

        public override double CalcularDosis(double dosisPorKg)
        {
            return (Peso * dosisPorKg) * 0.50;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Envergadura de Alas: {EnvergaduraAlas} cm");
            Console.WriteLine($"Puede Volar: {(PuedeVolar ? "Sí" : "No")}");
        }
    }
}