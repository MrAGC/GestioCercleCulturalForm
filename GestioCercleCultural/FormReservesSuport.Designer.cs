namespace GestioCercleCultural
{
    partial class FormReservesSuport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelFecha = new System.Windows.Forms.Label();
            this.labelUbicacion = new System.Windows.Forms.Label();
            this.labelDescripcion = new System.Windows.Forms.Label();
            this.labelEventoNombre = new System.Windows.Forms.Label();
            this.roundedButtonReservar = new RoundedButton();
            this.pictureBoxIzquierda = new System.Windows.Forms.PictureBox();
            this.pictureBoxFechaDerecha = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIzquierda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFechaDerecha)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.labelFecha);
            this.panel1.Controls.Add(this.labelUbicacion);
            this.panel1.Controls.Add(this.labelDescripcion);
            this.panel1.Controls.Add(this.labelEventoNombre);
            this.panel1.Controls.Add(this.roundedButtonReservar);
            this.panel1.Location = new System.Drawing.Point(63, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 491);
            this.panel1.TabIndex = 61;
            // 
            // labelFecha
            // 
            this.labelFecha.AutoSize = true;
            this.labelFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFecha.Location = new System.Drawing.Point(26, 428);
            this.labelFecha.Name = "labelFecha";
            this.labelFecha.Size = new System.Drawing.Size(64, 24);
            this.labelFecha.TabIndex = 4;
            this.labelFecha.Text = "Fecha";
            // 
            // labelUbicacion
            // 
            this.labelUbicacion.AutoSize = true;
            this.labelUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUbicacion.Location = new System.Drawing.Point(26, 391);
            this.labelUbicacion.Name = "labelUbicacion";
            this.labelUbicacion.Size = new System.Drawing.Size(94, 24);
            this.labelUbicacion.TabIndex = 3;
            this.labelUbicacion.Text = "Ubicacion";
            // 
            // labelDescripcion
            // 
            this.labelDescripcion.AutoSize = true;
            this.labelDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescripcion.Location = new System.Drawing.Point(26, 161);
            this.labelDescripcion.MaximumSize = new System.Drawing.Size(650, 450);
            this.labelDescripcion.Name = "labelDescripcion";
            this.labelDescripcion.Size = new System.Drawing.Size(110, 24);
            this.labelDescripcion.TabIndex = 2;
            this.labelDescripcion.Text = "Descripcion";
            // 
            // labelEventoNombre
            // 
            this.labelEventoNombre.AutoSize = true;
            this.labelEventoNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEventoNombre.Location = new System.Drawing.Point(275, 62);
            this.labelEventoNombre.Name = "labelEventoNombre";
            this.labelEventoNombre.Size = new System.Drawing.Size(99, 31);
            this.labelEventoNombre.TabIndex = 1;
            this.labelEventoNombre.Text = "Evento";
            // 
            // roundedButtonReservar
            // 
            this.roundedButtonReservar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(54)))));
            this.roundedButtonReservar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButtonReservar.FlatAppearance.BorderSize = 0;
            this.roundedButtonReservar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButtonReservar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.roundedButtonReservar.ForeColor = System.Drawing.Color.Black;
            this.roundedButtonReservar.Location = new System.Drawing.Point(523, 417);
            this.roundedButtonReservar.Name = "roundedButtonReservar";
            this.roundedButtonReservar.Size = new System.Drawing.Size(120, 50);
            this.roundedButtonReservar.TabIndex = 0;
            this.roundedButtonReservar.Text = "Reservar";
            this.roundedButtonReservar.UseVisualStyleBackColor = false;
            this.roundedButtonReservar.Click += new System.EventHandler(this.roundedButtonReservar_Click);
            // 
            // pictureBoxIzquierda
            // 
            this.pictureBoxIzquierda.Image = global::GestioCercleCultural.Properties.Resources.imgFlechaIzquierda;
            this.pictureBoxIzquierda.Location = new System.Drawing.Point(12, 251);
            this.pictureBoxIzquierda.Name = "pictureBoxIzquierda";
            this.pictureBoxIzquierda.Size = new System.Drawing.Size(42, 41);
            this.pictureBoxIzquierda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxIzquierda.TabIndex = 63;
            this.pictureBoxIzquierda.TabStop = false;
            this.pictureBoxIzquierda.Click += new System.EventHandler(this.pictureBoxIzquierda_Click);
            // 
            // pictureBoxFechaDerecha
            // 
            this.pictureBoxFechaDerecha.Image = global::GestioCercleCultural.Properties.Resources.imgFlechaDerecha;
            this.pictureBoxFechaDerecha.Location = new System.Drawing.Point(745, 251);
            this.pictureBoxFechaDerecha.Name = "pictureBoxFechaDerecha";
            this.pictureBoxFechaDerecha.Size = new System.Drawing.Size(42, 41);
            this.pictureBoxFechaDerecha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFechaDerecha.TabIndex = 62;
            this.pictureBoxFechaDerecha.TabStop = false;
            this.pictureBoxFechaDerecha.Click += new System.EventHandler(this.pictureBoxFechaDerecha_Click);
            // 
            // FormReservesSuport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(799, 530);
            this.Controls.Add(this.pictureBoxIzquierda);
            this.Controls.Add(this.pictureBoxFechaDerecha);
            this.Controls.Add(this.panel1);
            this.Name = "FormReservesSuport";
            this.Text = "FormReservesSuport";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIzquierda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFechaDerecha)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedButton roundedButtonReservar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelEventoNombre;
        private System.Windows.Forms.Label labelFecha;
        private System.Windows.Forms.Label labelUbicacion;
        private System.Windows.Forms.Label labelDescripcion;
        private System.Windows.Forms.PictureBox pictureBoxFechaDerecha;
        private System.Windows.Forms.PictureBox pictureBoxIzquierda;
    }
}