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
    public partial class FormGestioEspais : Form
    {
        public DataTable espais;
        private bool crear = true;
        private UsuarioLogueado usuarioActual;

        public FormGestioEspais(UsuarioLogueado usuario)
        {
            InitializeComponent();
            CargarEspais();
            usuarioActual = usuario;
            radioButtonSi.CheckedChanged += RadioButtonSi_CheckedChanged;
            roundedButtonEliminarEspai.Click += RoundedButtonEliminarEspai_Click;
            comboBoxSeleccionarEspai.SelectedIndexChanged += ComboBoxSeleccionarEspai_SelectedIndexChanged;
        }

        private void FormGestioEspais_Load(object sender, EventArgs e)
        {
            try
            {
                
                radioButtonNo.Checked = true;
                textBoxNumerofiles.Enabled = false;
                textBoxNumerobutaques.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inicial: {ex.Message}");
            }
        }

        private void CargarEspais()
        {
            espais = EspaiOrm.SelectAll();

            if (!espais.Columns.Contains("id"))
                espais.Columns.Add("id", typeof(int));

            if (espais.Rows.Count == 0 || espais.Rows[0]["nom"].ToString() != "Seleccionar espai")
            {
                DataRow newRow = espais.NewRow();
                newRow["id"] = DBNull.Value;
                newRow["nom"] = "Seleccionar espai";
                newRow["capacitat"] = 0;
                newRow["ubicacio"] = "";
                newRow["butaques_fixes"] = false;
                espais.Rows.InsertAt(newRow, 0);
            }

            comboBoxSeleccionarEspai.DataSource = espais;
            comboBoxSeleccionarEspai.DisplayMember = "nom";
            comboBoxSeleccionarEspai.ValueMember = "id";
            comboBoxSeleccionarEspai.SelectedIndex = espais.Rows.Count > 0 ? 0 : -1;
        }


        private void ComboBoxSeleccionarEspai_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Evitar manejo durante inicialización
                if (comboBoxSeleccionarEspai.SelectedIndex == -1) return;

                // Manejar selección de la fila "Seleccionar espai"
                if (comboBoxSeleccionarEspai.SelectedIndex == 0 ||
                    Convert.ToInt32(comboBoxSeleccionarEspai.SelectedValue) == -1)
                {
                    crear = true;
                    LimpiarCampos();
                    return;
                }

                // Obtener fila seleccionada con verificación
                if (!(comboBoxSeleccionarEspai.SelectedItem is DataRowView row))
                {
                    MessageBox.Show("Error al cargar datos del espacio");
                    return;
                }

                crear = false;

                // Cargar datos con manejo de nulls
                textBoxNomEspai.Text = row["nom"]?.ToString() ?? "";
                textBoxCapacitat.Text = row["capacitat"]?.ToString() ?? "0";
                textBoxConSombraUbicacio.Text = row["ubicacio"]?.ToString() ?? "";
                textBoxMetresQuadrats.Text = row["metres_quadrats"]?.ToString() ?? "0.00";

                // Manejo seguro de booleanos y DBNull
                bool butaquesFixes = false;
                if (row["butaques_fixes"] != DBNull.Value)
                {
                    butaquesFixes = Convert.ToBoolean(row["butaques_fixes"]);
                }
                radioButtonSi.Checked = butaquesFixes;
                radioButtonNo.Checked = !butaquesFixes;

                // Manejar valores numéricos con conversión segura
                if (butaquesFixes)
                {
                    textBoxNumerofiles.Text = (row["num_files"] != DBNull.Value && Convert.ToInt32(row["num_files"]) > 0)
                        ? row["num_files"].ToString()
                        : "";

                    textBoxNumerobutaques.Text = (row["num_butaques_per_fila"] != DBNull.Value && Convert.ToInt32(row["num_butaques_per_fila"]) > 0)
                        ? row["num_butaques_per_fila"].ToString()
                        : "";
                }
                else
                {
                    textBoxNumerofiles.Text = "";
                    textBoxNumerobutaques.Text = "";
                }

                // Forzar actualización de estado de controless
                textBoxNumerofiles.Enabled = butaquesFixes;
                textBoxNumerobutaques.Enabled = butaquesFixes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar espacio: {ex.Message}");
                LimpiarCampos();
            }
        }

        private void RoundedButtonConfirmarEspai_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                string nom = textBoxNomEspai.Text.Trim();
                bool butaquesFixes = radioButtonSi.Checked;
                int? numFiles = butaquesFixes ? int.Parse(textBoxNumerofiles.Text) : (int?)null;
                int? butaquesPerFila = butaquesFixes ? int.Parse(textBoxNumerobutaques.Text) : (int?)null;

                // Validar nombre único
                if (crear && EspaiOrm.ExisteNombre(nom))
                {
                    MessageBox.Show("¡Ya existe un espacio con este nombre!");
                    return;
                }

                if (crear)
                {
                    EspaiOrm.Insert(
                        nom,
                        int.Parse(textBoxCapacitat.Text),
                        textBoxConSombraUbicacio.Text.Trim(),
                        decimal.Parse(textBoxMetresQuadrats.Text),
                        butaquesFixes,
                        numFiles,
                        butaquesPerFila
                    );
                    MessageBox.Show("Espacio creado correctamente!");
                }
                else
                {
                    DataRowView row = (DataRowView)comboBoxSeleccionarEspai.SelectedItem;
                    int id = (int)row["id"];

                    if (nom != row["nom"].ToString() && EspaiOrm.ExisteNombre(nom))
                    {
                        MessageBox.Show("¡Ya existe un espacio con este nombre!");
                        return;
                    }

                    EspaiOrm.Update(
                        id,
                        nom,
                        int.Parse(textBoxCapacitat.Text),
                        textBoxConSombraUbicacio.Text.Trim(),
                        decimal.Parse(textBoxMetresQuadrats.Text),
                        butaquesFixes,
                        numFiles,
                        butaquesPerFila
                    );
                    MessageBox.Show("Cambios guardados!");
                }

                CargarEspais();
                if (crear) LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void RoundedButtonCrearEspai_Click(object sender, EventArgs e)
        {
            crear = true;
            LimpiarCampos();
            comboBoxSeleccionarEspai.SelectedIndex = 0;
        }

        private void LimpiarCampos()
        {
            textBoxNomEspai.Text = "";
            textBoxCapacitat.Text = "";
            textBoxConSombraUbicacio.Text = "";
            textBoxMetresQuadrats.Text = "";
            radioButtonNo.Checked = true;
            textBoxNumerofiles.Text = "";
            textBoxNumerobutaques.Text = "";
        }

        private bool ValidarCampos()
        {
            var errores = new StringBuilder();

            if (string.IsNullOrWhiteSpace(textBoxNomEspai.Text))
                errores.AppendLine("Nombre es obligatorio");

            if (!int.TryParse(textBoxCapacitat.Text, out int cap) || cap <= 0)
                errores.AppendLine("Capacidad debe ser un número positivo");

            if (!decimal.TryParse(textBoxMetresQuadrats.Text, out decimal m2) || m2 <= 0)
                errores.AppendLine("Metros cuadrados deben ser un número positivo");

            if (radioButtonSi.Checked)
            {
                if (!int.TryParse(textBoxNumerofiles.Text, out int files) || files <= 0)
                    errores.AppendLine("Número de filas no válido");

                if (!int.TryParse(textBoxNumerobutaques.Text, out int but) || but <= 0)
                    errores.AppendLine("Butaques por fila no válido");

                if (int.TryParse(textBoxCapacitat.Text, out int capacidad) && (files * but) > capacidad)
                    errores.AppendLine("La capacidad debe ser mayor o igual al total de butaques");
            }

            if (errores.Length > 0)
            {
                MessageBox.Show(errores.ToString(), "Errores de validación");
                return false;
            }
            return true;
        }

        private void RadioButtonSi_CheckedChanged(object sender, EventArgs e)
        {
            bool activar = radioButtonSi.Checked;
            textBoxNumerofiles.Enabled = activar;
            textBoxNumerobutaques.Enabled = activar;

            if (!activar)
            {
                textBoxNumerofiles.Text = "";
                textBoxNumerobutaques.Text = "";
            }
        }

        private void RoundedButtonEliminarEspai_Click(object sender, EventArgs e)
        {
            if (comboBoxSeleccionarEspai.SelectedIndex <= 0)
            {
                MessageBox.Show("Selecciona un espacio para eliminar");
                return;
            }

            try
            {
                DataRowView row = (DataRowView)comboBoxSeleccionarEspai.SelectedItem;
                int id = (int)row["id"];

                if (EspaiOrm.TieneEventosAsociados(id))
                {
                    MessageBox.Show("No se puede eliminar el espacio porque tiene eventos asociados");
                    return;
                }

                DialogResult resp = MessageBox.Show(
                    "¿Estás seguro de eliminar este espacio?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo);

                if (resp == DialogResult.Yes)
                {
                    EspaiOrm.DeleteEspaiCompleto(id);
                    CargarEspais();
                    LimpiarCampos();
                    MessageBox.Show("Espacio eliminado!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error eliminando: {ex.Message}");
            }
        }

        private void FormGestioEspais_Load_1(object sender, EventArgs e)
        {
            this.AutoScroll = false;
        }
    }
}