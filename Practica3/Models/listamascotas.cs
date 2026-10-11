using System.Runtime.Serialization;

namespace Practica3.Models
{
    public class listamascotas
    {
        public class nodomascota
        {
        public Mascota mascota;
        public nodomascota? siguiente;

        public nodomascota(Mascota m)
        {
            mascota=m;
            siguiente =null;
        }


            
        }
    private nodomascota? cabeza;
        private nodomascota? cola;

 public listamascotas()
            {
                cabeza=null;
                cola=null;
                
            }

           public void agregarmascota(Mascota mascota)
        {
            nodomascota nuevonodo=new nodomascota(mascota);
            if (cabeza == null)
            {
                cabeza=nuevonodo;
                cola=nuevonodo;
            }
            else
            {
                cola.siguiente=nuevonodo;
                cola=nuevonodo;
            }
        } 
    public Mascota? busca(string cod)
        {
            nodomascota? actual=cabeza;
            while (actual != null)
            {
                if (actual.mascota.CodigoUnico==cod)
                {
                    return actual.mascota;

                }
                actual=actual.siguiente;
            }
            return null;

        } 

     public void mostrarmascotas()
        {

    if (cabeza == null)
    {
        Console.WriteLine("La lista esta vacia.");
        return; 
    }

    Console.WriteLine("+---------------------------------------------------------+");
    Console.WriteLine("| Codigo | Nombre | Propietario | Estado | Tipo de animal |");
    Console.WriteLine("+---------------------------------------------------------+");
    nodomascota? actual = cabeza;


    while (actual != null)
    {
        Console.WriteLine($"| {actual.mascota.CodigoUnico} | {actual.mascota.Nombre} | {actual.mascota.Propietario} | {actual.mascota.Estado} | |{actual.mascota.GetType().Name}|");
        
        actual = actual.siguiente;
    }
    Console.WriteLine("+----------------------------------------+");
        }   
    }



}