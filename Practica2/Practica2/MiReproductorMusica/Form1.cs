using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using MiReproductorMusica;
using System.Diagnostics;

public partial class Form1 : Form
{
    // Controles de la Interfaz - Paneles principales
    private Panel panelMenuLateral;
    private Panel panelContenido;

    // Botones del Menú Lateral
    private Button btnCargarJson;
    private Button btnSiguiente;
    private Button btnAbrirForm2;
    private Button btnGraphvizCola;
    private Button btnGraphvizArbol;

    // Controles de la Zona Central / Contenido
    private Button btnBuscar;
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
        // 1. Configuración general del Formulario
        this.Text = "Reproductor de Música - Práctica 2";
        this.Size = new Size(950, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.MinimumSize = new Size(800, 500);
        this.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
        this.BackColor = Color.FromArgb(245, 247, 250);

        // 2. Panel Menú Lateral Izquierdo (Estilo barra de navegación oscura)
        panelMenuLateral = new Panel();
        panelMenuLateral.Dock = DockStyle.Left;
        panelMenuLateral.Width = 240;
        panelMenuLateral.BackColor = Color.FromArgb(18, 40, 68); // Azul oscuro corporativo

        // Título o Cabecera del Menú Lateral
        Label lblMenuTitulo = new Label();
        lblMenuTitulo.Text = "≡ Menú Principal";
        lblMenuTitulo.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblMenuTitulo.ForeColor = Color.White;
        lblMenuTitulo.Location = new Point(20, 20);
        lblMenuTitulo.AutoSize = true;
        panelMenuLateral.Controls.Add(lblMenuTitulo);

        // Botones organizados verticalmente en el panel lateral
        btnCargarJson = CrearBotonSidebar("📂 Cargar canciones.json", 65, Color.FromArgb(0, 120, 212));
        btnCargarJson.Click += BtnCargarJson_Click;
        panelMenuLateral.Controls.Add(btnCargarJson);

        btnSiguiente = CrearBotonSidebar("▶ Siguiente Canción", 120, Color.FromArgb(40, 167, 69));
        btnSiguiente.Click += BtnSiguiente_Click;
        panelMenuLateral.Controls.Add(btnSiguiente);

        btnAbrirForm2 = CrearBotonSidebar("📊 Ver JSON en Form 2", 175, Color.FromArgb(108, 117, 125));
        btnAbrirForm2.Click += BtnAbrirForm2_Click;
        panelMenuLateral.Controls.Add(btnAbrirForm2);

        Label lblSeccionReportes = new Label();
        lblSeccionReportes.Text = "Reportes Graphviz:";
        lblSeccionReportes.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        lblSeccionReportes.ForeColor = Color.FromArgb(170, 190, 215);
        lblSeccionReportes.Location = new Point(20, 240);
        lblSeccionReportes.AutoSize = true;
        panelMenuLateral.Controls.Add(lblSeccionReportes);

        btnGraphvizCola = CrearBotonSidebar("📉 Cola (PNG)", 270, Color.FromArgb(23, 162, 184));
        btnGraphvizCola.Click += (s, e) => GenerarGraphviz(colaReproduccion.GenerarDotGraphiviz(), "cola_reproduccion");
        panelMenuLateral.Controls.Add(btnGraphvizCola);

        btnGraphvizArbol = CrearBotonSidebar("📈 Árbol (PNG)", 325, Color.FromArgb(23, 162, 184));
        btnGraphvizArbol.Click += (s, e) => GenerarGraphviz(bibliotecaArbol.GenerarDotGraphviz(), "arbol_biblioteca");
        panelMenuLateral.Controls.Add(btnGraphvizArbol);


        // 3. Panel de Contenido Principal (Derecha - Dock Fill)
        panelContenido = new Panel();
        panelContenido.Dock = DockStyle.Fill;
        panelContenido.BackColor = Color.FromArgb(250, 252, 255);

        // Elementos internos del panel de contenido
        lblTituloVentana = new Label();
        lblTituloVentana.Text = "🎵 Reproductor Digital de Música";
        lblTituloVentana.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        lblTituloVentana.ForeColor = Color.FromArgb(33, 37, 41);
        lblTituloVentana.Location = new Point(30, 25);
        lblTituloVentana.AutoSize = true;
        panelContenido.Controls.Add(lblTituloVentana);

        lblInfoActual = new Label();
        lblInfoActual.Text = "Reproduciendo: Ninguna canción en curso.";
        lblInfoActual.Location = new Point(30, 80);
        lblInfoActual.Size = new Size(580, 50);
        lblInfoActual.Font = new Font("Segoe UI", 10.5f, FontStyle.Italic);
        lblInfoActual.ForeColor = Color.FromArgb(70, 80, 95);
        panelContenido.Controls.Add(lblInfoActual);

        lblTiempoTotal = new Label();
        lblTiempoTotal.Text = "En espera: 0 canciones | Tiempo total: 0 min";
        lblTiempoTotal.Location = new Point(30, 140);
        lblTiempoTotal.AutoSize = true;
        lblTiempoTotal.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
        lblTiempoTotal.ForeColor = Color.FromArgb(45, 55, 72);
        panelContenido.Controls.Add(lblTiempoTotal);

        // Sección de Búsqueda
        Label lblBusquedaTitulo = new Label();
        lblBusquedaTitulo.Text = "Buscar canción en Biblioteca (Árbol ABB):";
        lblBusquedaTitulo.Location = new Point(30, 200);
        lblBusquedaTitulo.AutoSize = true;
        lblBusquedaTitulo.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
        panelContenido.Controls.Add(lblBusquedaTitulo);

        txtBuscarTitulo = new TextBox();
        txtBuscarTitulo.Location = new Point(30, 235);
        txtBuscarTitulo.Size = new Size(360, 26);
        txtBuscarTitulo.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
        panelContenido.Controls.Add(txtBuscarTitulo);

        btnBuscar = CrearBotonAccion("Buscar", new Point(405, 233), new Size(110, 32), Color.FromArgb(90, 105, 120), Color.White);
        btnBuscar.Click += BtnBuscar_Click;
        panelContenido.Controls.Add(btnBuscar);

        lblResultadoBusqueda = new Label();
        lblResultadoBusqueda.Text = "Resultado de búsqueda: [Esperando consulta...]";
        lblResultadoBusqueda.Location = new Point(30, 285);
        lblResultadoBusqueda.Size = new Size(500, 90);
        lblResultadoBusqueda.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
        lblResultadoBusqueda.ForeColor = Color.FromArgb(80, 90, 105);
        panelContenido.Controls.Add(lblResultadoBusqueda);


        // 4. Agregar los paneles al Formulario (Primero Fill, luego Left para asegurar correcta distribución)
        this.Controls.Add(panelContenido);
        this.Controls.Add(panelMenuLateral);
    }

    // Métodos auxiliares para crear botones limpios
    private Button CrearBotonSidebar(string texto, int y, Color colorFondo)
    {
        Button btn = new Button();
        btn.Text = texto;
        btn.Location = new Point(15, y);
        btn.Size = new Size(210, 38);
        btn.BackColor = colorFondo;
        btn.ForeColor = Color.White;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.TextAlign = ContentAlignment.MiddleLeft;
        btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        btn.Cursor = Cursors.Hand;
        return btn;
    }

    private Button CrearBotonAccion(string texto, Point ubicacion, Size tamanio, Color colorFondo, Color colorTexto)
    {
        Button btn = new Button();
        btn.Text = texto;
        btn.Location = ubicacion;
        btn.Size = tamanio;
        btn.BackColor = colorFondo;
        btn.ForeColor = colorTexto;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
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
        int tiempototal = colaReproduccion.CalculartiempoTotal();
        lblTiempoTotal.Text = $"Canciones en espera en la cola: {cancionesRestantes} | Tiempo total estimado: {tiempototal} min";
    }

    private void GenerarGraphviz(string contenidoDot, string nombreArchivo)
    {
        try
        {
            string archivodot = nombreArchivo + ".dot";
            string archivopng = nombreArchivo + ".png";
            File.WriteAllText(archivodot, contenidoDot);
            var procesoInfo = new ProcessStartInfo
            {
                FileName = "dot",
                Arguments = $"-Tpng \"{archivodot}\" -o \"{archivopng}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var proceso = Process.Start(procesoInfo))
            {
                proceso.WaitForExit();
                if (proceso.ExitCode == 0)
                {
                    MessageBox.Show($"Imagen generada con éxito: {archivopng}", "Graphviz", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string error = proceso.StandardError.ReadToEnd();
                    MessageBox.Show($"Graphviz se ejecutó pero dio un error al compilar {archivopng}.", "Error de compilación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        catch (Exception)
        {
            MessageBox.Show("No se pudo encontrar o ejecutar el comando 'dot' de Graphviz.", "Graphviz No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}