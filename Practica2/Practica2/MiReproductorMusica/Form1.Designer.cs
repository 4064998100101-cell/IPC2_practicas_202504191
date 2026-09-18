partial class Form1
{
    /// <summary>
    /// Variable del diseñador necesaria.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Limpiar los recursos que se estén usando.
    /// </summary>
    /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Código generado por el Diseñador de Windows Forms

    /// <summary>
    /// Método necesario para admitir el Diseñador. No se puede modificar
    /// el contenido de este método con el editor de código.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
        this.components = new System.ComponentModel.Container();
        
        // Inicialización de Paneles
        this.panel1 = new System.Windows.Forms.Panel();          // Barra superior azul

        // Inicialización de Controles Visuales (PictureBoxes / Botones de ventana)
        this.pictureBox3 = new System.Windows.Forms.PictureBox(); // Minimizar / Maximizar
        this.btcerrar = new System.Windows.Forms.PictureBox();     // Botón cerrar

        this.SuspendLayout();

        // 
        // panel1 (Barra superior de la ventana sin bordes)
        // 
        this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
        this.panel1.Location = new System.Drawing.Point(0, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(4, 450);
        this.panel1.TabIndex = 1;
        this.panel1.BackColor = System.Drawing.Color.Transparent;

        // 
        // btcerrar (Botón de cerrar arriba a la derecha)
        // 
        this.btcerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btcerrar.Location = new System.Drawing.Point(980, 10);
        this.btcerrar.Name = "btcerrar";
        this.btcerrar.Size = new System.Drawing.Size(16, 16);
        this.btcerrar.TabIndex = 0;
        this.btcerrar.TabStop = false;
        this.btcerrar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btcerrar.BackgroundImage")));
        this.btcerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.btcerrar.Cursor = System.Windows.Forms.Cursors.Hand;
        this.btcerrar.BackColor = System.Drawing.Color.Transparent;

        // 
        // pictureBox3 (Botón minimizar/maximizar arriba a la derecha)
        // 
        this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.pictureBox3.Location = new System.Drawing.Point(955, 10);
        this.pictureBox3.Name = "pictureBox3";
        this.pictureBox3.Size = new System.Drawing.Size(16, 16);
        this.pictureBox3.TabIndex = 3;
        this.pictureBox3.TabStop = false;
        this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

        // 
        // panel2 (Menú lateral izquierdo)
        // 

        // 
        // panel3 (Panel de contenido principal a la derecha)
        // 

        // 
        // panelcontenedor (Contenedor que alberga el menú y el panel de contenido debajo de la barra superior)
        // 

        // Añadir paneles internos al contenedor general

        // Añadir controles flotantes de la barra superior
        this.panel1.Controls.Add(this.pictureBox3);
        this.panel1.Controls.Add(this.btcerrar);

        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(276, 442);
        this.Controls.Add(this.panel1);
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.MinimumSize = new System.Drawing.Size(700, 450);
        
        ((System.ComponentModel.ISupportInitialize)(this.btcerrar)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.PictureBox btcerrar;
    private System.Windows.Forms.PictureBox pictureBox3;
}