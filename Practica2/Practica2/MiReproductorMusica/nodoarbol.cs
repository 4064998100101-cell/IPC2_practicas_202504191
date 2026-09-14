namespace MiReproductorMusica
{
    public class NodoArbol
    {
        public Cancion CancionDatos {get; private set;}
        public NodoArbol Hijoizq{get; private set;}
        public NodoArbol Hijoder{get; private set;}
        public NodoArbol (Cancion cancion)
        {
            CancionDatos=cancion;
            Hijoder=null;
            Hijoizq=null;
        }
    }

}