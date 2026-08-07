namespace Practica1;
using Practica1;
class Program
{

    static listamascotas listamascota=new();
    static void Main(string[] args)
    {
     bool repetir=true;
      while (repetir)
      {         
       Console.WriteLine("+=========================================+");
       Console.WriteLine("|      GESTION DE MASCOTAS PRACTICA 1     |");
       Console.WriteLine("+=========================================+");
       Console.WriteLine("|1. Registrar nueva mascota para revision |");
       Console.WriteLine("|2. Gestionar ya pacientes registrados    |");
       Console.WriteLine("|0. Salir                                 |");
       Console.WriteLine("+=========================================+");
       Console.WriteLine("Ingrese una opcion"); 

        string opc = Console.ReadLine();
        int opciones = int.Parse(opc);

      // opciones diferentes para el programa  
      switch (opciones)
      {
        case 1: registrarmascota(); break;
        case 2: gestion(); break;
        case 0: Console.WriteLine("Gracias por usar este programa de gestion"); repetir=false; break;
        default: Console.WriteLine("Dato no encontrado"); break;
      }
      }

    }
    
    static private void registrarmascota()
    {
       
        
    }

    static private void gestion()
    {
        
    }
    

}
