using System;
using System.Text;

namespace MiReproductorMusica
{
    public class ArbolBinarioBusqueda
    {
        private NodoArbol raiz;
        public ArbolBinarioBusqueda()
        {
            raiz=null;
        }
        public void Insertar(Cancion cancion)
        {
            raiz=insertar(raiz, cancion);
        }


        private NodoArbol insertar (NodoArbol actual, Cancion cancion)
        {
            if (actual==null)
            {
                return new NodoArbol(cancion);
            }

            int comparacion= string.Compare(cancion.Titulo, actual.CancionDatos.Titulo,StringComparison.OrdinalIgnoreCase);
            
            if (comparacion < 0)
            {
                actual.Hijoizq = insertar(actual.Hijoizq, cancion);
            }
            else if (comparacion > 0)
            {
                actual.Hijoder=insertar(actual.Hijoder, cancion);
            }
            return actual;
      
      
        }
        public Cancion Buscar (string titulo)
        {
            return BuscarRecursivo(raiz, titulo);
        }

        private Cancion BuscarRecursivo(NodoArbol actual, string titulo)
        {
            if (actual==null)
            {
                return null;

            }
            int comparacion=string.Compare(titulo, actual.CancionDatos.Titulo,StringComparison.OrdinalIgnoreCase);
            if (comparacion==0)
            {
                return actual.CancionDatos;
            }
            else if (comparacion<0)
            {
                return BuscarRecursivo(actual.Hijoizq, titulo);
            }
            else
            {
                return BuscarRecursivo(actual.Hijoder, titulo);
            }
        }

        public bool Estavacio()
        {
            return raiz==null;
        }

        // Para buscar la raiz cuando necesitemos graphiz
        public NodoArbol ObtenerRaiz()
        {
            return raiz;
        }
        
     public string GenerarDotGraphviz()
    {

        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph ArbolBiblioteca {");
        dot.AppendLine("    node [shape=circle, style=filled, fillcolor=lightyellow];");

        if (raiz == null)
        {
            dot.AppendLine("    vacio [label=\"Árbol Vacío\", fillcolor=lightgray];");
        }
        else
        {
            GenerarConexionesDot(raiz, dot);
        }

        dot.AppendLine("}");
        return dot.ToString();
        
    }

    private void GenerarConexionesDot(NodoArbol nodo, StringBuilder dot)
    {
        string idActual = $"\"{nodo.CancionDatos.Titulo}\"";
        dot.AppendLine($"    {idActual} [label=\"{nodo.CancionDatos.Titulo}\\n({nodo.CancionDatos.Genero})\"];");

        if (nodo.Hijoizq != null)
        {
            string idIzq = $"\"{nodo.Hijoizq.CancionDatos.Titulo}\"";
            dot.AppendLine($"    {idActual} -> {idIzq};");
            GenerarConexionesDot(nodo.Hijoizq, dot);
        }
        else
        {
            string idNullIzq = $"null_left_{nodo.GetHashCode()}";
            dot.AppendLine($"    {idNullIzq} [shape=point];");
            dot.AppendLine($"    {idActual} -> {idNullIzq};");
        }

        if (nodo.Hijoder != null)
        {
            string idDer = $"\"{nodo.Hijoder.CancionDatos.Titulo}\"";
            dot.AppendLine($"    {idActual} -> {idDer};");
            GenerarConexionesDot(nodo.Hijoder, dot);
        }
        else
        {
            string idNullDer = $"null_right_{nodo.GetHashCode()}";
            dot.AppendLine($"    {idNullDer} [shape=point];");
            dot.AppendLine($"    {idActual} -> {idNullDer};");
        }
    }

    }
}