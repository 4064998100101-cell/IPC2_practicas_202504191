using System.Runtime.ConstrainedExecution;
using System.Text;
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
        public int ObtenerTamanio()
    {
        return contador;
    }
    public int CalculartiempoTotal()
        {
            int tiempototal=0;
            for (int i = 0; i < contador; i++)
            {
                int index = (frente+i)%capacidad;
                if(arreglo[index]!= null)
                {
                    tiempototal=tiempototal+arreglo[index].obtenerduracionoficial();
                }

            }
            return tiempototal;
        }

    private void Redimensionar()
        {
            int nuevaCapacidad=capacidad*2;
            Cancion[] nuevoArreglo =new Cancion[nuevaCapacidad];
            for (int i = 0; i < contador; i++)
            {
                nuevoArreglo[i]=arreglo[(frente+i)%capacidad];
            }
            arreglo=nuevoArreglo;
            frente=0;
            final=contador-1;
            capacidad=nuevaCapacidad;
        }  
    public string GenerarDotGraphiviz()
     {
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph ColaReproduccion {");
        dot.AppendLine("    rankdir=LR;");
        dot.AppendLine("    node [shape=record, style=filled, fillcolor=lightblue];");
        
        if (EstaVacia())
        {
            dot.AppendLine("    vacia [label=\"Cola Vacía\", fillcolor=lightgray];");
        }
        else
        {
            StringBuilder nodos = new StringBuilder();
            StringBuilder conexiones = new StringBuilder();

            for (int i = 0; i < contador; i++)
            {
                int index = (frente + i) % contador; // Nota: ajustado para representación lineal o circular
                // Usamos una representación clara de los nodos en la cola
                Cancion c = arreglo[(frente + i) % capacidad];
                if (c != null)
                {
                    nodos.AppendLine($"    nodo{i} [label=\"{{Título: {c.Titulo}\\nArtista: {c.Artista}\\nGénero: {c.Genero} ({c.obtenerduracionoficial()} min)}}\"];");
                    if (i > 0)
                    {
                        conexiones.AppendLine($"    nodo{i - 1} -> nodo{i};");
                    }
                }
            }
            dot.Append(nodos.ToString());
            dot.Append(conexiones.ToString());
        }

        dot.AppendLine("}");
        return dot.ToString();
     }   
    }
}