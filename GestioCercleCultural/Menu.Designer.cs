namespace GestioCercleCultural
{
    partial class Menu
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
            this.components = new System.ComponentModel.Container();
            this.sidebar = new System.Windows.Forms.Panel();
            this.panelSuport = new System.Windows.Forms.Panel();
            this.buttonReservesSuport = new System.Windows.Forms.Button();
            this.panelEspais = new System.Windows.Forms.Panel();
            this.buttonGestioEspais = new System.Windows.Forms.Button();
            this.panelEsdeveniments = new System.Windows.Forms.Panel();
            this.buttonGestioEsdeveniments = new System.Windows.Forms.Button();
            this.panelUsuari = new System.Windows.Forms.Panel();
            this.buttonGestioUsuaris = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxLogoMenu = new System.Windows.Forms.PictureBox();
            this.sidebarTimer = new System.Windows.Forms.Timer(this.components);
            this.panelCargarForms = new System.Windows.Forms.Panel();
            this.roundedButtonCerrarSession = new RoundedButton();
            this.sidebar.SuspendLayout();
            this.panelSuport.SuspendLayout();
            this.panelEspais.SuspendLayout();
            this.panelEsdeveniments.SuspendLayout();
            this.panelUsuari.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.White;
            this.sidebar.Controls.Add(this.roundedButtonCerrarSession);
            this.sidebar.Controls.Add(this.panelSuport);
            this.sidebar.Controls.Add(this.panelEspais);
            this.sidebar.Controls.Add(this.panelEsdeveniments);
            this.sidebar.Controls.Add(this.panelUsuari);
            this.sidebar.Controls.Add(this.panel2);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.Location = new System.Drawing.Point(0, 0);
            this.sidebar.MaximumSize = new System.Drawing.Size(240, 569);
            this.sidebar.MinimumSize = new System.Drawing.Size(74, 569);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(240, 569);
            this.sidebar.TabIndex = 0;
            // 
            // panelSuport
            // 
            this.panelSuport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.panelSuport.Controls.Add(this.buttonReservesSuport);
            this.panelSuport.Location = new System.Drawing.Point(3, 333);
            this.panelSuport.Name = "panelSuport";
            this.panelSuport.Padding = new System.Windows.Forms.Padding(57, 0, 0, 0);
            this.panelSuport.Size = new System.Drawing.Size(234, 45);
            this.panelSuport.TabIndex = 5;
            // 
            // buttonReservesSuport
            // 
            this.buttonReservesSuport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReservesSuport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReservesSuport.Image = global::GestioCercleCultural.Properties.Resources.imgReserves;
            this.buttonReservesSuport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReservesSuport.Location = new System.Drawing.Point(-43, -23);
            this.buttonReservesSuport.Name = "buttonReservesSuport";
            this.buttonReservesSuport.Padding = new System.Windows.Forms.Padding(57, 0, 0, 0);
            this.buttonReservesSuport.Size = new System.Drawing.Size(283, 93);
            this.buttonReservesSuport.TabIndex = 2;
            this.buttonReservesSuport.Text = "                 Reserves i suport";
            this.buttonReservesSuport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReservesSuport.UseVisualStyleBackColor = true;
            this.buttonReservesSuport.Click += new System.EventHandler(this.buttonReservesSuport_Click);
            // 
            // panelEspais
            // 
            this.panelEspais.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.panelEspais.Controls.Add(this.buttonGestioEspais);
            this.panelEspais.Location = new System.Drawing.Point(3, 221);
            this.panelEspais.Name = "panelEspais";
            this.panelEspais.Padding = new System.Windows.Forms.Padding(57, 0, 0, 0);
            this.panelEspais.Size = new System.Drawing.Size(234, 45);
            this.panelEspais.TabIndex = 3;
            // 
            // buttonGestioEspais
            // 
            this.buttonGestioEspais.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGestioEspais.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGestioEspais.Image = global::GestioCercleCultural.Properties.Resources.imgEspais;
            this.buttonGestioEspais.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGestioEspais.Location = new System.Drawing.Point(-43, -23);
            this.buttonGestioEspais.Name = "buttonGestioEspais";
            this.buttonGestioEspais.Padding = new System.Windows.Forms.Padding(55, 0, 0, 0);
            this.buttonGestioEspais.Size = new System.Drawing.Size(283, 93);
            this.buttonGestioEspais.TabIndex = 2;
            this.buttonGestioEspais.Text = "                  Gestió d\'espais";
            this.buttonGestioEspais.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGestioEspais.UseVisualStyleBackColor = true;
            this.buttonGestioEspais.Click += new System.EventHandler(this.buttonGestioEspais_Click);
            // 
            // panelEsdeveniments
            // 
            this.panelEsdeveniments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.panelEsdeveniments.Controls.Add(this.buttonGestioEsdeveniments);
            this.panelEsdeveniments.Location = new System.Drawing.Point(3, 277);
            this.panelEsdeveniments.Name = "panelEsdeveniments";
            this.panelEsdeveniments.Padding = new System.Windows.Forms.Padding(57, 0, 0, 0);
            this.panelEsdeveniments.Size = new System.Drawing.Size(234, 45);
            this.panelEsdeveniments.TabIndex = 4;
            // 
            // buttonGestioEsdeveniments
            // 
            this.buttonGestioEsdeveniments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGestioEsdeveniments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGestioEsdeveniments.Image = global::GestioCercleCultural.Properties.Resources.imgEvents;
            this.buttonGestioEsdeveniments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGestioEsdeveniments.Location = new System.Drawing.Point(-43, -22);
            this.buttonGestioEsdeveniments.Name = "buttonGestioEsdeveniments";
            this.buttonGestioEsdeveniments.Padding = new System.Windows.Forms.Padding(55, 0, 0, 0);
            this.buttonGestioEsdeveniments.Size = new System.Drawing.Size(283, 93);
            this.buttonGestioEsdeveniments.TabIndex = 2;
            this.buttonGestioEsdeveniments.Text = "                 Gestió d\'esdeveniments";
            this.buttonGestioEsdeveniments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGestioEsdeveniments.UseVisualStyleBackColor = true;
            this.buttonGestioEsdeveniments.Click += new System.EventHandler(this.buttonGestioEsdeveniments_Click);
            // 
            // panelUsuari
            // 
            this.panelUsuari.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.panelUsuari.Controls.Add(this.buttonGestioUsuaris);
            this.panelUsuari.Location = new System.Drawing.Point(3, 165);
            this.panelUsuari.Name = "panelUsuari";
            this.panelUsuari.Padding = new System.Windows.Forms.Padding(57, 0, 0, 0);
            this.panelUsuari.Size = new System.Drawing.Size(234, 45);
            this.panelUsuari.TabIndex = 1;
            // 
            // buttonGestioUsuaris
            // 
            this.buttonGestioUsuaris.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGestioUsuaris.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGestioUsuaris.Image = global::GestioCercleCultural.Properties.Resources.imgAvatarDeUsuario;
            this.buttonGestioUsuaris.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGestioUsuaris.Location = new System.Drawing.Point(-43, -22);
            this.buttonGestioUsuaris.Name = "buttonGestioUsuaris";
            this.buttonGestioUsuaris.Padding = new System.Windows.Forms.Padding(57, 0, 0, 0);
            this.buttonGestioUsuaris.Size = new System.Drawing.Size(283, 93);
            this.buttonGestioUsuaris.TabIndex = 2;
            this.buttonGestioUsuaris.Text = "                 Gestió d\'usuaris";
            this.buttonGestioUsuaris.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGestioUsuaris.UseVisualStyleBackColor = true;
            this.buttonGestioUsuaris.Click += new System.EventHandler(this.buttonGestioUsuaris_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBoxLogoMenu);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(237, 133);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(83, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "CULTURAL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(83, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "CERCLE";
            // 
            // pictureBoxLogoMenu
            // 
            this.pictureBoxLogoMenu.Image = global::GestioCercleCultural.Properties.Resources.imgLogo;
            this.pictureBoxLogoMenu.Location = new System.Drawing.Point(3, 31);
            this.pictureBoxLogoMenu.Name = "pictureBoxLogoMenu";
            this.pictureBoxLogoMenu.Size = new System.Drawing.Size(69, 69);
            this.pictureBoxLogoMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogoMenu.TabIndex = 0;
            this.pictureBoxLogoMenu.TabStop = false;
            this.pictureBoxLogoMenu.Click += new System.EventHandler(this.pictureBoxLogoMenu_Click);
            // 
            // sidebarTimer
            // 
            this.sidebarTimer.Interval = 7;
            this.sidebarTimer.Tick += new System.EventHandler(this.sidebarTimer_Tick);
            // 
            // panelCargarForms
            // 
            this.panelCargarForms.Location = new System.Drawing.Point(243, 0);
            this.panelCargarForms.Name = "panelCargarForms";
            this.panelCargarForms.Size = new System.Drawing.Size(821, 569);
            this.panelCargarForms.TabIndex = 1;
            // 
            // roundedButtonCerrarSession
            // 
            this.roundedButtonCerrarSession.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(54)))));
            this.roundedButtonCerrarSession.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButtonCerrarSession.FlatAppearance.BorderSize = 0;
            this.roundedButtonCerrarSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButtonCerrarSession.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.roundedButtonCerrarSession.ForeColor = System.Drawing.Color.Black;
            this.roundedButtonCerrarSession.Location = new System.Drawing.Point(56, 507);
            this.roundedButtonCerrarSession.Name = "roundedButtonCerrarSession";
            this.roundedButtonCerrarSession.Size = new System.Drawing.Size(120, 50);
            this.roundedButtonCerrarSession.TabIndex = 6;
            this.roundedButtonCerrarSession.Text = "Cerrar sessión";
            this.roundedButtonCerrarSession.UseVisualStyleBackColor = false;
            this.roundedButtonCerrarSession.Click += new System.EventHandler(this.roundedButtonCerrarSession_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1064, 569);
            this.Controls.Add(this.panelCargarForms);
            this.Controls.Add(this.sidebar);
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.sidebar.ResumeLayout(false);
            this.panelSuport.ResumeLayout(false);
            this.panelEspais.ResumeLayout(false);
            this.panelEsdeveniments.ResumeLayout(false);
            this.panelUsuari.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelUsuari;
        private System.Windows.Forms.Button buttonGestioUsuaris;
        private System.Windows.Forms.Panel panelEspais;
        private System.Windows.Forms.Button buttonGestioEspais;
        private System.Windows.Forms.PictureBox pictureBoxLogoMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer sidebarTimer;
        private System.Windows.Forms.Panel panelSuport;
        private System.Windows.Forms.Button buttonReservesSuport;
        private System.Windows.Forms.Panel panelEsdeveniments;
        private System.Windows.Forms.Button buttonGestioEsdeveniments;
        private System.Windows.Forms.Panel panelCargarForms;
        private RoundedButton roundedButtonCerrarSession;
    }
}