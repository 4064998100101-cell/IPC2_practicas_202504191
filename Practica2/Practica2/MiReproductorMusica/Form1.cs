namespace MiReproductorMusica;

public partial class Form1 : Form
{
    private Button btnCargarJson;
    private Button btnSiguiente;
    private Label lblTitulo;
    private Label lblinfo;
    private Label lblTiempototal;
    private ListBox lstColaEspera;
    private ColaCanciones colaReproduccion;
    private LectorJson lectorJson;
    public Form1()
    {
        InitializeComponent();

        colaReproduccion=new ColaCanciones();
        lectorJson =new LectorJson();
        
        ConfigurarInterfaz();
    }

    private void ConfigurarInterfaz()
    {
        this.Text="Reproductor de Música - Practica 2";
        this.Size =new Size(500, 450);
        this.StartPosition=FormStartPosition.CenterScreen;
        
        



    }
}
