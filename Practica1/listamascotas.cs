namespace Practica1
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


        
    }


}