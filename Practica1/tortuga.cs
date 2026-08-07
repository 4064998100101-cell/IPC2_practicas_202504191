namespace Practica1
{
    public class Tortuga : Mascota
    {
        public string TipoCaparazon { get; set; }
        public bool EsAcuatica { get; set; }

        public Tortuga(string nombre, double peso, string sexo, int edad, string propietario, string tipoCaparazon, bool esAcuatica)
            : base(nombre, peso, sexo, edad, propietario)
        {
            TipoCaparazon = tipoCaparazon;
            EsAcuatica = esAcuatica;
        }

        // Dosis ajustada al 80% (0.80)
        public override double CalcularDosis(double dosisPorKg)
        {
            return (Peso * dosisPorKg) * 0.80;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Tipo de Caparazón: {TipoCaparazon}");
            Console.WriteLine($"Es Acuática: {(EsAcuatica ? "Sí" : "No")}");
        }
    }
}