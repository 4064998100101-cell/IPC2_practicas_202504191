using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using MiReproductorMusica;
using System.Diagnostics;

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
    private Button btgharbol;
    private Button btghcola;

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
    this.Size = new Size(540, 580);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
    this.BackColor = Color.FromArgb(245, 247, 250); // Fondo gris muy suave y elegante


    lblTituloVentana = new Label();
    lblTituloVentana.Text = "🎵 Reproductor Digital de Música";
    lblTituloVentana.Font = new Font("Segoe UI", 13, FontStyle.Bold);
    lblTituloVentana.ForeColor = Color.FromArgb(33, 37, 41);
    lblTituloVentana.Location = new Point(25, 20);
    lblTituloVentana.AutoSize = true;
    this.Controls.Add(lblTituloVentana);

    
        btnCargarJson = CrearBoton("Cargar canciones.json", new Point(25, 65), new Size(230, 35), Color.FromArgb(0, 120, 212), Color.White);
        btnCargarJson.Click += BtnCargarJson_Click;
        this.Controls.Add(btnCargarJson);

        btnSiguiente = CrearBoton("Siguiente Canción (Cola)", new Point(275, 65), new Size(230, 35), Color.FromArgb(40, 167, 69), Color.White);
        btnSiguiente.Click += BtnSiguiente_Click;
        this.Controls.Add(btnSiguiente);

    
       lblInfoActual = new Label();
       lblInfoActual.Text = "Reproduciendo: Ninguna canción en curso.";
       lblInfoActual.Location = new Point(25, 115);
       lblInfoActual.Size = new Size(480, 50);
       lblInfoActual.Font = new Font("Segoe UI", 10, FontStyle.Italic);
       lblInfoActual.ForeColor = Color.FromArgb(70, 80, 95);
       this.Controls.Add(lblInfoActual);

    lblTiempoTotal = new Label();
    lblTiempoTotal.Text = "En espera: 0 canciones | Tiempo total: 0 min";
    lblTiempoTotal.Location = new Point(25, 175);
    lblTiempoTotal.AutoSize = true;
    lblTiempoTotal.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
    lblTiempoTotal.ForeColor = Color.FromArgb(45, 55, 72);
    this.Controls.Add(lblTiempoTotal);


    Label lblBusquedaTitulo = new Label();
    lblBusquedaTitulo.Text = "Buscar canción en Biblioteca (Árbol ABB):";
    lblBusquedaTitulo.Location = new Point(25, 220);
    lblBusquedaTitulo.AutoSize = true;
    lblBusquedaTitulo.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
    this.Controls.Add(lblBusquedaTitulo);

    txtBuscarTitulo = new TextBox();
    txtBuscarTitulo.Location = new Point(25, 248);
    txtBuscarTitulo.Size = new Size(310, 26);
    txtBuscarTitulo.Font = new Font("Segoe UI", 10, FontStyle.Regular);
    this.Controls.Add(txtBuscarTitulo);

    btnBuscar = CrearBoton("Buscar en ABB", new Point(345, 246), new Size(160, 30), Color.FromArgb(90, 105, 120), Color.White);
    btnBuscar.Click += BtnBuscar_Click;
    this.Controls.Add(btnBuscar);

    lblResultadoBusqueda = new Label();
    lblResultadoBusqueda.Text = "Resultado de búsqueda: [Esperando consulta...]";
    lblResultadoBusqueda.Location = new Point(25, 290);
    lblResultadoBusqueda.Size = new Size(480, 70);
    lblResultadoBusqueda.ForeColor = Color.FromArgb(80, 90, 105);
    this.Controls.Add(lblResultadoBusqueda);


    Label lblReportes = new Label();
    lblReportes.Text = "📈 Visualización y Reportes:";
    lblReportes.Location = new Point(25, 375);
    lblReportes.AutoSize = true;
    lblReportes.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
    this.Controls.Add(lblReportes);

    btnAbrirForm2 = CrearBoton(" Ver JSON en Form 2", new Point(25, 405), new Size(150, 32), Color.FromArgb(108, 117, 125), Color.White);
    btnAbrirForm2.Click += BtnAbrirForm2_Click;
    this.Controls.Add(btnAbrirForm2);

    Button btnGraphvizCola = CrearBoton("Cola (PNG)", new Point(185, 405), new Size(150, 32), Color.FromArgb(23, 162, 184), Color.White);
    btnGraphvizCola.Click += (s, e) => GenerarGraphviz(colaReproduccion.GenerarDotGraphiviz(), "cola_reproduccion");
    this.Controls.Add(btnGraphvizCola);

    Button btnGraphvizArbol = CrearBoton("Árbol (PNG)", new Point(345, 405), new Size(160, 32), Color.FromArgb(23, 162, 184), Color.White);
    btnGraphvizArbol.Click += (s, e) => GenerarGraphviz(bibliotecaArbol.GenerarDotGraphviz(), "arbol_biblioteca");
    this.Controls.Add(btnGraphvizArbol);
  }

private Button CrearBoton(string texto, Point ubicacion, Size tamanio, Color colorFondo, Color colorTexto)
{
    Button btn = new Button();
    btn.Text = texto;
    btn.Location = ubicacion;
    btn.Size = tamanio;
    btn.BackColor = colorFondo;
    btn.ForeColor = colorTexto;
    btn.FlatStyle = FlatStyle.Flat;
    btn.FlatAppearance.BorderSize = 0;
    btn.Cursor = Cursors.Hand;
    return btn;
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
        lblTiempoTotal.Text = $"Canciones en espera en la cola: {cancionesRestantes} | Tiempo total estimado:{tiempototal} min";
        
    }

    private void GenerarGraphviz (string contenidoDot, string nombreArchivo)
    {
        try
        {
            string archivodot=nombreArchivo +".dot";
            string archivopng= nombreArchivo+".png";
            File.WriteAllText(archivodot, contenidoDot);
            var procesoInfo=new ProcessStartInfo
            {
FileName = "dot",
            Arguments = $"-Tpng \"{archivodot}\" -o \"{archivopng}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
            };

            using (var proceso=Process.Start(procesoInfo))
            {
                proceso.WaitForExit();
                if (proceso.ExitCode==0)
                {
                    MessageBox.Show($"Imagen generada con exito {archivopng}",
                    "Graphviz", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    string error = proceso.StandardError.ReadToEnd();
                     MessageBox.Show($"Graphviz se ejecuto pero dio un error {archivopng}",
                    "Error de compilacion", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("No se pudo encontrar o ejecutar el comando con Graphiz",
            "Graphviz No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void BtnVerColaPng_Click(object sender, EventArgs e)
    {
        string dotCola=colaReproduccion.GenerarDotGraphiviz();
        GenerarGraphviz(dotCola, "cola_reproduccion");
    }

    private void BtnVerArbolPng_Click(object sender, EventArgs e)
    {
        string dotArbol= bibliotecaArbol.GenerarDotGraphviz();
        GenerarGraphviz(dotArbol, "arbol_biblioteca");
    }

}
