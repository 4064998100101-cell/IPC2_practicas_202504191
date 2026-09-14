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
            
            if (comparacion<0)
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

    }
}