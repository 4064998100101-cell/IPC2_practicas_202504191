using System;
using System.Drawing;
using System.Windows.Forms;
using MiReproductorMusica;

public partial class Form2 : Form
{
    private ListBox lstCancionesJson;
    private Label lblTituloForm2;
    private Button btnCerrar;

    // Constructor que recibe las canciones cargadas desde el JSON
    public Form2(Cancion[] canciones)
    {
        InitializeCompon();
        ConfigurarUI(canciones);
    }

    private void ConfigurarUI(Cancion[] canciones)
    {
        this.Text = "Biblioteca - Archivo JSON";
        this.Size = new Size(480, 420);
        this.StartPosition = FormStartPosition.CenterParent;

        // Título superior
        lblTituloForm2 = new Label();
        lblTituloForm2.Text = "Listado de Canciones Cargadas (JSON):";
        lblTituloForm2.Font = new Font("Arial", 11, FontStyle.Bold);
        lblTituloForm2.Location = new Point(20, 20);
        lblTituloForm2.AutoSize = true;
        this.Controls.Add(lblTituloForm2);

        // ListBox para mostrar los elementos detallados
        lstCancionesJson = new ListBox();
        lstCancionesJson.Location = new Point(20, 60);
        lstCancionesJson.Size = new Size(420, 260);
        this.Controls.Add(lstCancionesJson);

   
        if (canciones != null)
        {
            lstCancionesJson.Items.Add(" Titulo   |Artista      | Genero      | Duracion ");
            foreach (var c in canciones)
            {
                lstCancionesJson.Items.Add($" {c.Titulo} | {c.Artista} |{c.Genero} | {c.Duracion} min");
            }
        }

        // Botón para cerrar esta ventana modal
        btnCerrar = new Button();
        btnCerrar.Text = "Cerrar";
        btnCerrar.Location = new Point(340, 335);
        btnCerrar.Size = new Size(100, 30);
        btnCerrar.Click += (sender, e) => this.Close();
        this.Controls.Add(btnCerrar);
    }


}