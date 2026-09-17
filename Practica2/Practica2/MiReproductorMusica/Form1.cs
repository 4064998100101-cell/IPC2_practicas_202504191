using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using MiReproductorMusica;

public partial class Form1 : Form
{
    // Controles de la Interfaz
    private Button btnCargarJson;
    private Button btnSiguiente;
    private Button btnBuscar;
    private Button btnAbrirForm2;
    private TextBox txtBuscarTitulo;
    private Label lblTituloVentana;
    private Label lblInfoActual;
    private Label lblTiempoTotal;
    private Label lblResultadoBusqueda;

    // Estructuras de datos
    private ColaCanciones colaReproduccion;
    private ArbolBinarioBusqueda bibliotecaArbol;
    private LectorJson lectorJson;

    public Form1()
    {
        InitializeComponent();
        
        colaReproduccion = new ColaCanciones();
        bibliotecaArbol = new ArbolBinarioBusqueda();
        lectorJson = new LectorJson();

        ConfigurarInterfaz();
    }

    private void ConfigurarInterfaz()
    {
        this.Text = "Reproductor de Música - Práctica 2";
        this.Size = new Size(520, 520);
        this.StartPosition = FormStartPosition.CenterScreen;

        // Título principal
        lblTituloVentana = new Label();
        lblTituloVentana.Text = "Reproductor con Cola y Árbol Binario";
        lblTituloVentana.Font = new Font("Arial", 12, FontStyle.Bold);
        lblTituloVentana.Location = new Point(30, 20);
        lblTituloVentana.AutoSize = true;
        this.Controls.Add(lblTituloVentana);

        // Botón para cargar el JSON
        btnCargarJson = new Button();
        btnCargarJson.Text = "Cargar canciones.json";
        btnCargarJson.Location = new Point(30, 60);
        btnCargarJson.Size = new Size(210, 30);
        btnCargarJson.Click += BtnCargarJson_Click;
        this.Controls.Add(btnCargarJson);

        // Botón para reproducir / siguiente
        btnSiguiente = new Button();
        btnSiguiente.Text = "Siguiente Canción (Cola)";
        btnSiguiente.Location = new Point(260, 60);
        btnSiguiente.Size = new Size(210, 30);
        btnSiguiente.Click += BtnSiguiente_Click;
        this.Controls.Add(btnSiguiente);

        // Etiqueta de Información de la canción actual
        lblInfoActual = new Label();
        lblInfoActual.Text = "Reproduciendo: Ninguna";
        lblInfoActual.Location = new Point(30, 110);
        lblInfoActual.Size = new Size(440, 45);
        lblInfoActual.Font = new Font("Arial", 10, FontStyle.Regular);
        this.Controls.Add(lblInfoActual);

        // Etiqueta del estado de la cola
        lblTiempoTotal = new Label();
        lblTiempoTotal.Text = "Canciones en espera en la cola: 0";
        lblTiempoTotal.Location = new Point(30, 165);
        lblTiempoTotal.AutoSize = true;
        this.Controls.Add(lblTiempoTotal);

        // Controles de Búsqueda por Árbol Binario
        Label lblBusquedaTitulo = new Label();
        lblBusquedaTitulo.Text = "Buscar canción en Biblioteca (Árbol ABB):";
        lblBusquedaTitulo.Location = new Point(30, 210);
        lblBusquedaTitulo.AutoSize = true;
        this.Controls.Add(lblBusquedaTitulo);

        txtBuscarTitulo = new TextBox();
        txtBuscarTitulo.Location = new Point(30, 235);
        txtBuscarTitulo.Size = new Size(250, 25);
        this.Controls.Add(txtBuscarTitulo);

        btnBuscar = new Button();
        btnBuscar.Text = "Buscar en ABB";
        btnBuscar.Location = new Point(295, 233);
        btnBuscar.Size = new Size(175, 28);
        btnBuscar.Click += BtnBuscar_Click;
        this.Controls.Add(btnBuscar);

        lblResultadoBusqueda = new Label();
        lblResultadoBusqueda.Text = "Resultado: [Esperando búsqueda...]";
        lblResultadoBusqueda.Location = new Point(30, 280);
        lblResultadoBusqueda.Size = new Size(440, 75);
        lblResultadoBusqueda.Font = new Font("Arial", 9, FontStyle.Italic);
        this.Controls.Add(lblResultadoBusqueda);

        // Botón para abrir el Form2 (Vista JSON)
        btnAbrirForm2 = new Button();
        btnAbrirForm2.Text = "Ver JSON en Form 2";
        btnAbrirForm2.Location = new Point(30, 375);
        btnAbrirForm2.Size = new Size(210, 30);
        btnAbrirForm2.Click += BtnAbrirForm2_Click;
        this.Controls.Add(btnAbrirForm2);
    }

    private void BtnCargarJson_Click(object sender, EventArgs e)
    {
        string rutaArchivo = "canciones.json";

        if (!File.Exists(rutaArchivo))
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos JSON (*.json)|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = openFileDialog.FileName;
            }
            else
            {
                return;
            }
        }

        Carga resultado = lectorJson.CargarDatos(rutaArchivo);
        colaReproduccion = resultado.Cola;
        bibliotecaArbol = resultado.Arbol;

        ActualizarMetricasCola();
        MessageBox.Show("¡Archivo JSON cargado e indexado en Cola y Árbol Binario con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void BtnSiguiente_Click(object sender, EventArgs e)
    {
        if (colaReproduccion.EstaVacia())
        {
            lblInfoActual.Text = "Reproduciendo: La cola de reproducción está vacía.";
            MessageBox.Show("No hay más canciones en espera en la cola.", "Cola Vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Cancion cancionActual = colaReproduccion.Desencolar();

        if (cancionActual != null)
        {

 lblInfoActual.Text = $"Reproduciendo: Título: {cancionActual.Titulo} | Artista: {cancionActual.Artista} | Duracion ({cancionActual.Duracion} min)";
        
        }

        ActualizarMetricasCola();
    }

    private void BtnBuscar_Click(object sender, EventArgs e)
    {
        string tituloBuscado = txtBuscarTitulo.Text.Trim();

        if (string.IsNullOrEmpty(tituloBuscado))
        {
            MessageBox.Show("Ingrese un título de canción para buscar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (bibliotecaArbol.Estavacio())
        {
            lblResultadoBusqueda.Text = "El árbol binario está vacío. Cargue un archivo JSON primero.";
            return;
        }

        Cancion encontrada = bibliotecaArbol.Buscar(tituloBuscado);

        if (encontrada != null)
        {
            lblResultadoBusqueda.Text = $"¡Canción Encontrada en el ABB!\n" +
                                        $"• Título: {encontrada.Titulo}\n" +
                                        $"• Artista: {encontrada.Artista}\n" +
                                        $"• Género: {encontrada.Genero}\n" +
                                        $"• Duración: {encontrada.Duracion} min";
        }
        else
        {
            lblResultadoBusqueda.Text = $"No se encontró ninguna canción con el título '{tituloBuscado}' en la biblioteca.";
        }
    }

    private void BtnAbrirForm2_Click(object sender, EventArgs e)
    {
        string rutaArchivo = "canciones.json";

        if (!File.Exists(rutaArchivo))
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos JSON (*.json)|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = openFileDialog.FileName;
            }
            else
            {
                return;
            }
        }

        try
        {
            string jsonString = File.ReadAllText(rutaArchivo);
            var opciones = new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Cancion[] listaCanciones = System.Text.Json.JsonSerializer.Deserialize<Cancion[]>(jsonString, opciones);

            Form2 form2 = new Form2(listaCanciones);
            form2.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al abrir el formulario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ActualizarMetricasCola()
    {
        int cancionesRestantes = colaReproduccion.ObtenerTamanio();
        int tiempototal=colaReproduccion.CalculartiempoTotal();
        File.WriteAllText("cola.dot", colaReproduccion.GenerarDotGraphiviz());
       File.WriteAllText("arbol.dot", bibliotecaArbol.GenerarDotGraphviz());
       MessageBox.Show("Archivos DOT de Graphviz generados con éxito en la raíz del proyecto.", "Graphviz", MessageBoxButtons.OK, MessageBoxIcon.Information);
        lblTiempoTotal.Text = $"Canciones en espera en la cola: {cancionesRestantes} | Tiempo total estimado:{tiempototal} min";
        MessageBox.Show("Archivos DOT de Graphviz generados con éxito en la raíz del proyecto.", "Graphviz", MessageBoxButtons.OK, MessageBoxIcon.Information);
    
    
    }
}
