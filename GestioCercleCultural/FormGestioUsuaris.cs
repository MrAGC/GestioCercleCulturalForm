using GestioCercleCultural.Models;
using GestioCercleCultural.Models.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestioCercleCultural
{
    public partial class FormGestioUsuaris : Form
    {


        public DataTable usuaris;
        private Boolean crear = true;
        int idiomaIndex;
        int rolIndex;
        private UsuarioLogueado usuarioActual;

        public FormGestioUsuaris(UsuarioLogueado usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
        }

        private void InitializeComponent()
        {
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.roundedButtonEliminarUsuari = new RoundedButton();
            this.comboBoxSeleccionarUsuari = new ComboBoxConSombra();
            this.roundedButtonCrearUsuari = new RoundedButton();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxConSombraIdioma = new ComboBoxConSombra();
            this.comboBoxRol = new ComboBoxConSombra();
            this.textBoxConfirmarContrasenya = new TextBoxConSombra();
            this.textBoxContrasenya = new TextBoxConSombra();
            this.textBoxCorreu = new TextBoxConSombra();
            this.textBoxNom = new TextBoxConSombra();
            this.roundedButtonConfirmarUsuari = new RoundedButton();
            this.comboBoxConSombraReserves = new ComboBoxConSombra();
            this.labelReserves = new System.Windows.Forms.Label();
            this.roundedButtonEliminarReserva = new RoundedButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(316, 278);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(169, 20);
            this.label8.TabIndex = 25;
            this.label8.Text = "Confirmar contrasenya";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(34, 278);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 23;
            this.label6.Text = "Contrasenya";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(34, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "Correu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "Nom";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(204, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "Rol";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(174)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.roundedButtonEliminarUsuari);
            this.panel1.Controls.Add(this.comboBoxSeleccionarUsuari);
            this.panel1.Controls.Add(this.roundedButtonCrearUsuari);
            this.panel1.Location = new System.Drawing.Point(38, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 65);
            this.panel1.TabIndex = 28;
            // 
            // roundedButtonEliminarUsuari
            // 
            this.roundedButtonEliminarUsuari.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.roundedButtonEliminarUsuari.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButtonEliminarUsuari.FlatAppearance.BorderSize = 0;
            this.roundedButtonEliminarUsuari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButtonEliminarUsuari.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.roundedButtonEliminarUsuari.ForeColor = System.Drawing.Color.Black;
            this.roundedButtonEliminarUsuari.Location = new System.Drawing.Point(605, 12);
            this.roundedButtonEliminarUsuari.Name = "roundedButtonEliminarUsuari";
            this.roundedButtonEliminarUsuari.Size = new System.Drawing.Size(122, 40);
            this.roundedButtonEliminarUsuari.TabIndex = 2;
            this.roundedButtonEliminarUsuari.Text = "Eliminar Usuari";
            this.roundedButtonEliminarUsuari.UseVisualStyleBackColor = false;
            this.roundedButtonEliminarUsuari.Click += new System.EventHandler(this.roundedButtonEliminarUsuari_Click);
            // 
            // comboBoxSeleccionarUsuari
            // 
            this.comboBoxSeleccionarUsuari.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.comboBoxSeleccionarUsuari.DataSource = null;
            this.comboBoxSeleccionarUsuari.DisplayMember = "";
            this.comboBoxSeleccionarUsuari.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSeleccionarUsuari.Location = new System.Drawing.Point(11, 12);
            this.comboBoxSeleccionarUsuari.Name = "comboBoxSeleccionarUsuari";
            this.comboBoxSeleccionarUsuari.SelectedIndex = -1;
            this.comboBoxSeleccionarUsuari.SelectedItem = null;
            this.comboBoxSeleccionarUsuari.SelectedValue = null;
            this.comboBoxSeleccionarUsuari.Size = new System.Drawing.Size(300, 45);
            this.comboBoxSeleccionarUsuari.TabIndex = 0;
            this.comboBoxSeleccionarUsuari.ValueMember = "";
            this.comboBoxSeleccionarUsuari.SelectedIndexChanged += new System.EventHandler(this.comboBoxSeleccionarUsuari_SelectedIndexChanged);
            // 
            // roundedButtonCrearUsuari
            // 
            this.roundedButtonCrearUsuari.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(54)))));
            this.roundedButtonCrearUsuari.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButtonCrearUsuari.FlatAppearance.BorderSize = 0;
            this.roundedButtonCrearUsuari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButtonCrearUsuari.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.roundedButtonCrearUsuari.ForeColor = System.Drawing.Color.Black;
            this.roundedButtonCrearUsuari.Location = new System.Drawing.Point(317, 13);
            this.roundedButtonCrearUsuari.Name = "roundedButtonCrearUsuari";
            this.roundedButtonCrearUsuari.Size = new System.Drawing.Size(122, 40);
            this.roundedButtonCrearUsuari.TabIndex = 1;
            this.roundedButtonCrearUsuari.Text = "Crear Usuari";
            this.roundedButtonCrearUsuari.UseVisualStyleBackColor = false;
            this.roundedButtonCrearUsuari.Click += new System.EventHandler(this.roundedButtonCrearUsuari_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 345);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "Idioma";
            // 
            // comboBoxConSombraIdioma
            // 
            this.comboBoxConSombraIdioma.DataSource = null;
            this.comboBoxConSombraIdioma.DisplayMember = "";
            this.comboBoxConSombraIdioma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxConSombraIdioma.Location = new System.Drawing.Point(38, 368);
            this.comboBoxConSombraIdioma.Name = "comboBoxConSombraIdioma";
            this.comboBoxConSombraIdioma.SelectedIndex = -1;
            this.comboBoxConSombraIdioma.SelectedItem = null;
            this.comboBoxConSombraIdioma.SelectedValue = null;
            this.comboBoxConSombraIdioma.Size = new System.Drawing.Size(160, 45);
            this.comboBoxConSombraIdioma.TabIndex = 29;
            this.comboBoxConSombraIdioma.ValueMember = "";
            this.comboBoxConSombraIdioma.SelectedIndexChanged += new System.EventHandler(this.comboBoxConSombraIdioma_SelectedIndexChanged);
            // 
            // comboBoxRol
            // 
            this.comboBoxRol.DataSource = null;
            this.comboBoxRol.DisplayMember = "";
            this.comboBoxRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxRol.Location = new System.Drawing.Point(208, 368);
            this.comboBoxRol.Name = "comboBoxRol";
            this.comboBoxRol.SelectedIndex = -1;
            this.comboBoxRol.SelectedItem = null;
            this.comboBoxRol.SelectedValue = null;
            this.comboBoxRol.Size = new System.Drawing.Size(160, 45);
            this.comboBoxRol.TabIndex = 26;
            this.comboBoxRol.ValueMember = "";
            this.comboBoxRol.SelectedIndexChanged += new System.EventHandler(this.comboBoxRol_SelectedIndexChanged);
            // 
            // textBoxConfirmarContrasenya
            // 
            this.textBoxConfirmarContrasenya.Location = new System.Drawing.Point(320, 297);
            this.textBoxConfirmarContrasenya.Name = "textBoxConfirmarContrasenya";
            this.textBoxConfirmarContrasenya.ReadOnly = false;
            this.textBoxConfirmarContrasenya.Size = new System.Drawing.Size(276, 45);
            this.textBoxConfirmarContrasenya.SoloNumeros = false;
            this.textBoxConfirmarContrasenya.TabIndex = 19;
            // 
            // textBoxContrasenya
            // 
            this.textBoxContrasenya.Location = new System.Drawing.Point(38, 297);
            this.textBoxContrasenya.Name = "textBoxContrasenya";
            this.textBoxContrasenya.ReadOnly = false;
            this.textBoxContrasenya.Size = new System.Drawing.Size(276, 45);
            this.textBoxContrasenya.SoloNumeros = false;
            this.textBoxContrasenya.TabIndex = 17;
            // 
            // textBoxCorreu
            // 
            this.textBoxCorreu.Location = new System.Drawing.Point(38, 230);
            this.textBoxCorreu.Name = "textBoxCorreu";
            this.textBoxCorreu.ReadOnly = false;
            this.textBoxCorreu.Size = new System.Drawing.Size(276, 45);
            this.textBoxCorreu.SoloNumeros = false;
            this.textBoxCorreu.TabIndex = 16;
            // 
            // textBoxNom
            // 
            this.textBoxNom.Location = new System.Drawing.Point(38, 162);
            this.textBoxNom.Name = "textBoxNom";
            this.textBoxNom.ReadOnly = false;
            this.textBoxNom.Size = new System.Drawing.Size(276, 45);
            this.textBoxNom.SoloNumeros = false;
            this.textBoxNom.TabIndex = 14;
            // 
            // roundedButtonConfirmarUsuari
            // 
            this.roundedButtonConfirmarUsuari.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(54)))));
            this.roundedButtonConfirmarUsuari.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButtonConfirmarUsuari.FlatAppearance.BorderSize = 0;
            this.roundedButtonConfirmarUsuari.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButtonConfirmarUsuari.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.roundedButtonConfirmarUsuari.ForeColor = System.Drawing.Color.Black;
            this.roundedButtonConfirmarUsuari.Location = new System.Drawing.Point(38, 428);
            this.roundedButtonConfirmarUsuari.Name = "roundedButtonConfirmarUsuari";
            this.roundedButtonConfirmarUsuari.Size = new System.Drawing.Size(120, 50);
            this.roundedButtonConfirmarUsuari.TabIndex = 13;
            this.roundedButtonConfirmarUsuari.Text = "Confirmar";
            this.roundedButtonConfirmarUsuari.UseVisualStyleBackColor = false;
            this.roundedButtonConfirmarUsuari.Click += new System.EventHandler(this.roundedButtonConfirmarUsuari_Click);
            // 
            // comboBoxConSombraReserves
            // 
            this.comboBoxConSombraReserves.DataSource = null;
            this.comboBoxConSombraReserves.DisplayMember = "";
            this.comboBoxConSombraReserves.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.comboBoxConSombraReserves.Location = new System.Drawing.Point(320, 162);
            this.comboBoxConSombraReserves.Name = "comboBoxConSombraReserves";
            this.comboBoxConSombraReserves.SelectedIndex = -1;
            this.comboBoxConSombraReserves.SelectedItem = null;
            this.comboBoxConSombraReserves.SelectedValue = null;
            this.comboBoxConSombraReserves.Size = new System.Drawing.Size(276, 45);
            this.comboBoxConSombraReserves.TabIndex = 31;
            this.comboBoxConSombraReserves.ValueMember = "";
            // 
            // labelReserves
            // 
            this.labelReserves.AutoSize = true;
            this.labelReserves.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReserves.Location = new System.Drawing.Point(316, 139);
            this.labelReserves.Name = "labelReserves";
            this.labelReserves.Size = new System.Drawing.Size(76, 20);
            this.labelReserves.TabIndex = 32;
            this.labelReserves.Text = "Reserves";
            // 
            // roundedButtonEliminarReserva
            // 
            this.roundedButtonEliminarReserva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.roundedButtonEliminarReserva.Cursor = System.Windows.Forms.Cursors.Hand;
            this.roundedButtonEliminarReserva.FlatAppearance.BorderSize = 0;
            this.roundedButtonEliminarReserva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButtonEliminarReserva.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.roundedButtonEliminarReserva.ForeColor = System.Drawing.Color.Black;
            this.roundedButtonEliminarReserva.Location = new System.Drawing.Point(643, 162);
            this.roundedButtonEliminarReserva.Name = "roundedButtonEliminarReserva";
            this.roundedButtonEliminarReserva.Size = new System.Drawing.Size(122, 45);
            this.roundedButtonEliminarReserva.TabIndex = 3;
            this.roundedButtonEliminarReserva.Text = "Eliminar Reserva";
            this.roundedButtonEliminarReserva.UseVisualStyleBackColor = false;
            this.roundedButtonEliminarReserva.Click += new System.EventHandler(this.roundedButtonEliminarReserva_Click);
            // 
            // FormGestioUsuaris
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(799, 530);
            this.Controls.Add(this.roundedButtonEliminarReserva);
            this.Controls.Add(this.labelReserves);
            this.Controls.Add(this.comboBoxConSombraReserves);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxConSombraIdioma);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxRol);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxConfirmarContrasenya);
            this.Controls.Add(this.textBoxContrasenya);
            this.Controls.Add(this.textBoxCorreu);
            this.Controls.Add(this.textBoxNom);
            this.Controls.Add(this.roundedButtonConfirmarUsuari);
            this.Name = "FormGestioUsuaris";
            this.Load += new System.EventHandler(this.FormGestioUsuaris_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void FormGestioUsuaris_Load(object sender, EventArgs e)
        {
            CargarUsuaris();

            CargarValoresComboBox();
        }

        private void CargarValoresComboBox()
        {
            // Rol
            comboBoxRol.Items.Clear();
            comboBoxRol.Items.Add("Seleccionar rol");
            comboBoxRol.Items.AddRange(new string[] { "NORMAL", "ORGANITZADOR", "SUPERUSUARI" });
            comboBoxRol.SelectedIndex = 0;

            // Idioma
            comboBoxConSombraIdioma.Items.Clear();
            comboBoxConSombraIdioma.Items.Add("Seleccionar idioma");
            comboBoxConSombraIdioma.Items.AddRange(new string[] { "ca", "es", "en" });
            comboBoxConSombraIdioma.SelectedIndex = 0;
        }

        private void CargarUsuaris()
        {
            try
            {
                usuaris = UsuariOrm.SelectAll();

                // Añadir fila vacía para selección
                DataRow newRow = usuaris.NewRow();
                newRow["id"] = -1; // Añadir ID
                newRow["nom"] = "Seleccionar usuari";
                newRow["email"] = "";
                usuaris.Rows.InsertAt(newRow, 0);

                comboBoxSeleccionarUsuari.DataSource = usuaris;
                comboBoxSeleccionarUsuari.DisplayMember = "nom";
                comboBoxSeleccionarUsuari.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private void comboBoxSeleccionarUsuari_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSeleccionarUsuari.SelectedItem != null)
            {
                DataRowView row = (DataRowView)comboBoxSeleccionarUsuari.SelectedItem;

                // Mostrar/ocultar controles de reservas
                bool esSeleccionarUsuario = (row["email"].ToString() == "");
                comboBoxConSombraReserves.Visible = !esSeleccionarUsuario;
                labelReserves.Visible = !esSeleccionarUsuario;
                roundedButtonEliminarReserva.Visible = !esSeleccionarUsuario;

                if (esSeleccionarUsuario)
                {
                    crear = true;
                    LimpiarCampos();
                    comboBoxRol.SelectedIndex = 0;
                    comboBoxConSombraIdioma.SelectedIndex = 0;
                }
                else
                {
                    crear = false;
                    textBoxNom.Text = row["nom"].ToString();
                    textBoxCorreu.Text = row["email"].ToString();
                    comboBoxRol.SelectedItem = row["tipusUsuari"].ToString();
                    comboBoxConSombraIdioma.SelectedItem = row["idioma"].ToString();
                    textBoxContrasenya.Text = "";
                    textBoxConfirmarContrasenya.Text = "";

                    // Cargar reservas del usuario
                    CargarReservasUsuario((int)row["id"]);
                }
            }
        }

        private void CargarReservasUsuario(int userId)
        {
            try
            {
                DataTable reservas = ReservaOrm.SelectByUser(userId);

                // Crear nuevo DataTable para el combo
                DataTable dtCombo = new DataTable();
                dtCombo.Columns.Add("Display", typeof(string));
                dtCombo.Columns.Add("id", typeof(int));

                if (reservas.Rows.Count == 0)
                {
                    // Caso sin reservas
                    dtCombo.Rows.Add("No hay reservas", -1);
                }
                else
                {
                    // Añadir opción de selección
                    dtCombo.Rows.Add("Seleccionar reserva", -1);

                    // Añadir reservas reales
                    foreach (DataRow row in reservas.Rows)
                    {
                        string displayText = $"Reserva {row["id"]} - {row["tipus"]}";
                        dtCombo.Rows.Add(displayText, row["id"]);
                    }
                }

                comboBoxConSombraReserves.DataSource = dtCombo;
                comboBoxConSombraReserves.DisplayMember = "Display";
                comboBoxConSombraReserves.ValueMember = "id";
                comboBoxConSombraReserves.SelectedIndex = 0; // Seleccionar primer item
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar reservas: " + ex.Message);
            }
        }

        private void roundedButtonConfirmarUsuari_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                if (crear)
                {
                    if (UsuariOrm.EmailExists(textBoxCorreu.Text))
                    {
                        MessageBox.Show("Aquest correu ja existeix!");
                        return;
                    }

                    UsuariOrm.Insert(
                        textBoxNom.Text.Trim(),
                        textBoxCorreu.Text.Trim().ToLower(), // Normalizar email
                        textBoxContrasenya.Text,
                        comboBoxRol.SelectedItem.ToString(),
                        comboBoxConSombraIdioma.SelectedItem.ToString()
                    );
                    MessageBox.Show("Usuari Creat!");
                }
                else
                {
                    DialogResult respuesta = MessageBox.Show(
                        "¿Deseas realizar esta acción?",
                        "Confirmación",
                        MessageBoxButtons.YesNo
                    );

                    if (respuesta == DialogResult.Yes) {

                    DataRowView row = (DataRowView)comboBoxSeleccionarUsuari.SelectedItem;
                    int id = (int)row["id"];

                    // Verificar si el email ha cambiat i existeix
                    if (textBoxCorreu.Text.Trim().ToLower() != row["email"].ToString().ToLower() &&
                        UsuariOrm.EmailExists(textBoxCorreu.Text))
                    {
                        MessageBox.Show("Correu en ús per un altre usuari!");
                        return;
                    }

                    UsuariOrm.Update(
                        id,
                        textBoxNom.Text.Trim(),
                        textBoxCorreu.Text.Trim().ToLower(),
                        comboBoxRol.SelectedItem.ToString(),
                        comboBoxConSombraIdioma.SelectedItem.ToString()
                    );
                    MessageBox.Show("Canvis guardats!");
                    }
                    else
                    {
                        MessageBox.Show("Acció cancel·lada.");
                    }
                }

                CargarUsuaris();
                if (crear) LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            comboBoxConSombraReserves.Visible = false;
            labelReserves.Visible = false;
            roundedButtonEliminarReserva.Visible = false;
        }

        private void roundedButtonCrearUsuari_Click_1(object sender, EventArgs e)
        {
            crear = true;
            LimpiarCampos();
            CargarUsuaris();

            // Ocultar y resetear reservas
            comboBoxConSombraReserves.Visible = false;
            labelReserves.Visible = false;
            roundedButtonEliminarReserva.Visible = false;

            // Crear DataTable vacío para el combo
            DataTable dtVacio = new DataTable();
            dtVacio.Columns.Add("Display", typeof(string));
            dtVacio.Columns.Add("id", typeof(int));
            dtVacio.Rows.Add("No hay reservas", -1);

            comboBoxConSombraReserves.DataSource = dtVacio;
        }

        private void LimpiarCampos()
        {
            textBoxNom.Text = "";
            textBoxCorreu.Text = "";
            textBoxContrasenya.Text = "";
            textBoxConfirmarContrasenya.Text = "";
            CargarValoresComboBox();
        }
        private bool ValidarCampos()
        {
            // Acumulador de errores
            var errores = new StringBuilder();

            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(textBoxNom.Text))
                errores.AppendLine("El camp Nom és obligatori.");
            if (string.IsNullOrWhiteSpace(textBoxCorreu.Text))
                errores.AppendLine("El camp Correu és obligatori.");
            if (rolIndex <= 0) // índice 0 es "Seleccionar rol"
                errores.AppendLine("Ha de seleccionar un rol.");
            if (idiomaIndex <= 0)
                errores.AppendLine("Ha de seleccionar l'idioma.");

            // Validar formato del email siempre
            if (!Regex.IsMatch(textBoxCorreu.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errores.AppendLine("Format de correu electrònic invàlid!");

            // Comprobar existencia del email solo si se está creando
            if (crear && UsuariOrm.EmailExists(textBoxCorreu.Text))
                errores.AppendLine("Correu en ús per un altre usuari!");

            // Validar contrasenyes (solo para creación o cuando se modifican)
            if (crear || !string.IsNullOrEmpty(textBoxContrasenya.Text))
            {
                if (textBoxContrasenya.Text.Length < 6)
                    errores.AppendLine("La contrasenya ha de tenir mínim 6 caràcters!");
                if (textBoxContrasenya.Text != textBoxConfirmarContrasenya.Text)
                    errores.AppendLine("Les contrasenyes no coincideixen!");
            }

            // Si hi ha algun error, el mostrarem
            if (errores.Length > 0)
            {
                MessageBox.Show(errores.ToString(), "Error de validació");
                return false;
            }
            return true;
        }

        private void comboBoxConSombraIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí guarda el indice del idioma seleccionat
            idiomaIndex = comboBoxConSombraIdioma.SelectedIndex;


        }

        private void comboBoxRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aquí guarda el indice del rol seleccionat
            rolIndex = comboBoxRol.SelectedIndex;
        }

        private void roundedButtonEliminarUsuari_Click(object sender, EventArgs e)
        {


            if (comboBoxSeleccionarUsuari.SelectedItem == null || comboBoxSeleccionarUsuari.SelectedIndex == 0)
            {
                MessageBox.Show("Selecciona un usuari per eliminar.");
                return;
            }
            DataRowView row = (DataRowView)comboBoxSeleccionarUsuari.SelectedItem;
            int id = (int)row["id"];
            if (id == usuarioActual.Id)
            {
                MessageBox.Show("No es pot eliminar el superusuari loguejat.");
                return;
            }
            else if (row["tipusUsuari"].ToString() == "SUPERUSUARI")
            {
                MessageBox.Show("No es pot eliminar un superusuari.");
                return;
            }
            UsuariOrm.DeleteAll(id);
            CargarUsuaris();
            LimpiarCampos();

            comboBoxConSombraReserves.Visible = false;
            labelReserves.Visible = false;
            roundedButtonEliminarReserva.Visible = false;

        }

        private void roundedButtonEliminarReserva_Click(object sender, EventArgs e)
        {
            if (comboBoxConSombraReserves.SelectedItem == null ||
               (int)((DataRowView)comboBoxConSombraReserves.SelectedItem)["id"] == -1)
            {
                MessageBox.Show("Selecciona una reserva válida para eliminar");
                return;
            }

            int reservaId = (int)((DataRowView)comboBoxConSombraReserves.SelectedItem)["id"];

            try
            {
                ReservaOrm.DeleteReservaCompleto(reservaId);
                MessageBox.Show("Reserva eliminada correctamente");

                // Recargar reservas
                DataRowView usuario = (DataRowView)comboBoxSeleccionarUsuari.SelectedItem;
                CargarReservasUsuario((int)usuario["id"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar reserva: " + ex.Message);
            }
        }
    }
}

