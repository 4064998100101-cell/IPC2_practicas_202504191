namespace Practica1;

using System.Reflection.Metadata;
using System.Xml.Serialization;
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
   static  private void registrarmascota()
    {
    
       Console.WriteLine("+=========================================+");
        Console.WriteLine("|         Registrar una nueva mascota     |");
        Console.WriteLine("+=========================================+");
        
        Console.WriteLine("Ingrese el nombre de la mascota:");
        string nombre = Console.ReadLine();
        
        Console.WriteLine("Ingrese el sexo de la mascota:");
        string sexo = Console.ReadLine();
        
        Console.WriteLine("Ingrese el nombre del propietario:");
        string propietario = Console.ReadLine();

        
        double peso = 0;
        int edad = 0;

        try
        {
            Console.WriteLine("Ingrese el peso de la mascota en kg y en numeros:");
            peso = double.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la edad de la mascota en numeros enteros:");
            edad = int.Parse(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Ha ingresado un dato no numerico valido. Registro cancelado.");
            return;
        }

        Console.WriteLine("Ingrese el tipo de animal (ave, gato, perro o tortuga):");
        string tipo = Console.ReadLine();

        if (tipo == "tortuga")
        {
            Console.WriteLine("Ingrese el tipo de caparazon:");
            string caparazon = Console.ReadLine();
            
            Console.WriteLine("Es acuatica? (Y/N):");
            string respAcuatica = Console.ReadLine().ToLower();
            bool acuatica = respAcuatica == "y" || respAcuatica == "s";

            Tortuga turtle = new Tortuga(nombre, peso, sexo, edad, propietario, caparazon, acuatica);

            listamascota.agregarmascota(turtle);
            Console.WriteLine("Tortuga registrada exitosamente."); 
            turtle.MostrarInformacion();
                    
        }

        else if (tipo == "perro")
        {
            Console.WriteLine("Ingrese la raza");
            string raza = Console.ReadLine();
            
            Console.WriteLine("Ingrese el tamaño");
            string tamanio=Console.ReadLine();

            Perro perrito1 = new Perro(nombre, peso, sexo, edad, propietario, raza, tamanio);

            listamascota.agregarmascota(perrito1);
            Console.WriteLine("Perro registrano correctamente");
            perrito1.MostrarInformacion();
            
          
        }
        else if (tipo=="ave")

        {
            double envergadura=0;
            try
            {
                            Console.WriteLine("Ingrese la envergadura de las alas en numeros");
             envergadura = double.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {
                
                Console.WriteLine("Debe ser un valor numerico el ingresado");
            }

            Console.WriteLine("puede volar? Y/N");
            bool siono=false;
            string vol=Console.ReadLine();
            if (vol=="Y")
            {
                siono=true;
            }


            Ave palomo = new Ave(nombre, peso, sexo, edad, propietario, envergadura, siono);

            listamascota.agregarmascota(palomo);
            Console.WriteLine("Ave registrada exitosamente.");
            palomo.MostrarInformacion();
            
        }
        else if (tipo=="gato")
        {
            Console.WriteLine("Ingrese la raza");
            string raza = Console.ReadLine();
            
            Console.WriteLine("esta esterilazado el gato? Y/N");
            bool siono=false;
            string vol=Console.ReadLine();
            if (vol=="Y")
            {
                siono=true;
            }


            Gato alan = new Gato(nombre, peso, sexo, edad, propietario, raza, siono);

            listamascota.agregarmascota(alan);
            Console.WriteLine("Gato registrado exitosamente.");
            alan.MostrarInformacion();
        }
        
        else
        {
            Console.WriteLine("El tipo de animal no esta registrado en la base del sistema.");
        }
    }
// Metodo gestion de datos 

    static private void gestion()
    {
        bool repetir=true;
        Console.WriteLine("+=========================================");
        Console.WriteLine("|         GESTION DE MASCOTAS            |");
        Console.WriteLine("+=========================================");
    

       
        listamascota.mostrarmascotas(); 
        Console.WriteLine("Ingrese el codigo unico de la mascota");  
        string codigo=Console.ReadLine();
        Mascota? animalencontrado = listamascota.busca(codigo);
        
        if(animalencontrado!=null)
        {
        Console.WriteLine("+=========================================+");
        Console.WriteLine("|           Animal encontrado             |");
        Console.WriteLine("+=========================================+");
        Console.WriteLine("Codigo: " + animalencontrado.CodigoUnico);
        Console.WriteLine("Nombre: " + animalencontrado.Nombre);
        Console.WriteLine("Propietario: "+ animalencontrado.Propietario);
        Console.WriteLine("+=========================================");
        while (repetir==true)
        
        {  
        Console.WriteLine("+=========================================");   
        Console.WriteLine("1. Cambiar estado de la mascota (enfermo, recuperado, etc) ");
        Console.WriteLine("2. Calcular las dosis necesaria");
        Console.WriteLine("3. Consultar los datos");
        Console.WriteLine("4. Mostrar todas las mascotas");
        Console.WriteLine("0. Regresar");
        Console.WriteLine("+=========================================");
        

        Console.WriteLine("Ingrese alguna opcion");
          string opc = Console.ReadLine();
        int opciones = int.Parse(opc);

            switch (opciones)

            {
                case 1:

                Console.WriteLine("Ingrese el nuevo estado de la mascota");
                string nuevoestado=Console.ReadLine();
                animalencontrado.CambiarEstado(nuevoestado);
                break;

                case 2:
                Console.WriteLine("Ingrese la dosis");
                double dosis=double.Parse(Console.ReadLine());
                Console.WriteLine("La dosis para este animal es: "+animalencontrado.CalcularDosis(dosis)+" mg");

                break;

                case 3:

                animalencontrado.MostrarInformacion();
                break;

                case 4:
                
                listamascota.mostrarmascotas(); 
                break;

                case 0: repetir=false; break;
                default: Console.WriteLine("Opcion no valida"); break;
            }
            
        }
         


        }
        else
        {
            Console.WriteLine("Animal no encontrado en los archivos");
        }





        
    }

    
    

}
