namespace MiReproductorMusica
{
    public class NodoArbol
    {
        public Cancion CancionDatos {get; private set;}
        public NodoArbol Hijoizq{get; set;}
        public NodoArbol Hijoder{get; set;}
        public NodoArbol (Cancion cancion)
        {
            CancionDatos=cancion;
            Hijoder=null;
            Hijoizq=null;
        }
    }

}