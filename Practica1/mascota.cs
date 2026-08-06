using System.Xml.Serialization;

namespace Practica1;
public abstract class mascota{
    private string Nombre {get; set; }
    private string sexo {get; set;}
    private int edad{get; set;}
    private string propietario{get; set;}
    private string codigo {get; set;}
    private string estado{get; set;}
    public mascota(string Nombre, string sexo, int edad, string propietario, string codigo, string estado)
    {
        Nombre=Nombre;
        sexo=sexo;
        edad=edad;
        propietario=propietario;
        codigo=generarcodigo();
        estado="Enfermo";

    }

  private string generarcodigo(){
    string caracteres="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    char[] caracter=new char[8];
    Random random = new Random();

    for (int i = 0; i < caracter.Length; i++)
    {
        caracter[i]=caracteres[random.Next(caracteres.Length)]
    }

    return new string(caracter);
        
    }  

}