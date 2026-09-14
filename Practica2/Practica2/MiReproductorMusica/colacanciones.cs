using System.Runtime.ConstrainedExecution;

namespace MiReproductorMusica
{
    public class ColaCanciones
    {
        private Cancion[] arreglo;
        private int frente;
        private int final;
        private int capacidad;
        private int contador;
        public ColaCanciones (int capacidadInicial =80)
        {
            capacidad=capacidadInicial;
            arreglo=new Cancion[capacidad];
            frente=0;
            final=-1;
            contador=0;

        }
        public bool EstaVacia()
        {
            return contador==0;
        }
        public bool EstaLlena()
        {
            return contador== capacidad;

        }
        public void Encolar(Cancion cancion)
        {
            if (EstaLlena())
            {
                ampliar();
            }
            final=(final+1)%capacidad;
            arreglo[final]=cancion;
            contador++;
        }

        public Cancion Desencolar()
        {
            if (EstaVacia())
            {
                return null;
            }
            Cancion cancion=arreglo[frente];
            arreglo[frente]=null;
            frente=(frente+1)%capacidad;
            contador--;
            return cancion;
        }
        private void ampliar()
        {
            int nuevaCapacidad =capacidad*2;
            Cancion[] nuevoArreglo=new Cancion[nuevaCapacidad];
            for (int i = 0; i < contador; i++)
            {
                nuevoArreglo[i]=arreglo[(frente+i)%capacidad];
            }
            arreglo=nuevoArreglo;
            frente=0;
            final=contador-1;
            capacidad=nuevaCapacidad;
        }
    }
}