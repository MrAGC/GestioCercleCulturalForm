using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using GestioCercleCultural.Models;
using GestioCercleCultural.Models.Controllers;
using System.Text.RegularExpressions;
using System.Text.Json;
using RestSharp;
using System.Configuration;
using System.Globalization;

namespace GestioCercleCultural
{
    public partial class FormGestioEsdeveniments : Form
    {
        private DataTable esdeveniments;
        private bool crear = true;
        private DataTable espais;
        private UsuarioLogueado usuarioActual;
        private List<TimeSpan> todasHoras = new List<TimeSpan>();
        private List<TimeSpan> horasOcupadas = new List<TimeSpan>();
        private int? currentEventId = null;

        public FormGestioEsdeveniments(UsuarioLogueado usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            ConfigureTextBoxes();
            InicializarHoras();
        }

        private void InicializarHoras()
        {
            for (int hora = 8; hora <= 22; hora++)
            {
                todasHoras.Add(new TimeSpan(hora, 0, 0));
                todasHoras.Add(new TimeSpan(hora, 30, 0));
            }
            todasHoras.Add(new TimeSpan(23, 0, 0));
        }

        private void ConfigureTextBoxes()
        {
            void SuppressEnter(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    ProcessTabKey(true);
                }
            }

            textBoxNomEsdeveniment.KeyDown += SuppressEnter;
            textBoxConSombraDescripcioIA.KeyDown += SuppressEnter;
            textBoxConSombraPlaces.KeyDown += SuppressEnter;
        }

        private void FormGestioEsdeveniments_Load(object sender, EventArgs e)
        {
            this.AutoScroll = false;
            textBoxConSombraUbicacioEsdeveniment.ReadOnly = true;
            CargarDatosIniciales();
            ConfigurarEventos();
            roundedButtonGenerarDescripcioIA.Visible = usuarioActual.TipoUsuario == "SUPERUSUARI";
            textBoxConSombraUbicacioEsdeveniment.ReadOnly = true;
            textBoxConSombraPlaces.SoloNumeros = true;

        }

        private void CargarDatosIniciales()
        {
            try
            {
                CargarEspais();
                CargarEsdeveniments();
                CargarHorasDisponibles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inicial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarEventos()
        {
            dateTimePickerConSombraDiaEsdeveniment.ValueChanged += (s, ev) => CargarHorasDisponibles();
            comboBoxConSombraEspaiEsdeveniment.SelectedIndexChanged += ComboBoxEspai_SelectedIndexChanged;
            comboBoxConSombraHoraEsdevenimentInici.SelectedIndexChanged += ComboBoxHoraInicio_SelectedIndexChanged;
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

            comboBoxConSombraEspaiEsdeveniment.DataSource = espais;
            comboBoxConSombraEspaiEsdeveniment.DisplayMember = "nom";
            comboBoxConSombraEspaiEsdeveniment.ValueMember = "id";
            comboBoxConSombraEspaiEsdeveniment.SelectedIndex = espais.Rows.Count > 0 ? 0 : -1;
        }

        private void CargarEsdeveniments()
        {
            esdeveniments = usuarioActual.TipoUsuario == "SUPERUSUARI"
                ? EsdevenimentOrm.SelectAll()
                : EsdevenimentOrm.SelectByUser(usuarioActual.Id);

            if (!esdeveniments.Columns.Contains("id"))
                esdeveniments.Columns.Add("id", typeof(int));

            if (esdeveniments.Rows.Count == 0 || esdeveniments.Rows[0]["nom"].ToString() != "Seleccionar esdeveniment")
            {
                DataRow newRow = esdeveniments.NewRow();
                newRow["id"] = DBNull.Value;
                newRow["nom"] = "Seleccionar esdeveniment";
                newRow["descripcio"] = "";
                newRow["dataInici"] = DateTime.Now;
                newRow["dataFi"] = DateTime.Now;
                newRow["aforament"] = 0;
                newRow["espai_id"] = DBNull.Value;
                newRow["usuari_id"] = DBNull.Value;
                newRow["per_infants"] = false;
                esdeveniments.Rows.InsertAt(newRow, 0);
            }

            comboBoxSeleccionarEsdeveniment.DataSource = esdeveniments;
            comboBoxSeleccionarEsdeveniment.DisplayMember = "nom";
            comboBoxSeleccionarEsdeveniment.ValueMember = "id";
            comboBoxSeleccionarEsdeveniment.SelectedIndex = esdeveniments.Rows.Count > 0 ? 0 : -1;
        }

        private void CargarHorasDisponibles()
        {
            if (comboBoxConSombraEspaiEsdeveniment.SelectedIndex <= 0 ||
                !int.TryParse(comboBoxConSombraEspaiEsdeveniment.SelectedValue?.ToString(), out int idEspai))
                return;

            DateTime fecha = dateTimePickerConSombraDiaEsdeveniment.Value.Date;
            DataTable eventos = EsdevenimentOrm.SelectEventosByIdEspai(idEspai);
            horasOcupadas.Clear();

            foreach (DataRow row in eventos.Rows)
            {
                DateTime inicio = (DateTime)row["dataInici"];
                DateTime fin = (DateTime)row["dataFi"];
                int eventoId = (int)row["id"];

                if (inicio.Date == fecha && (!currentEventId.HasValue || eventoId != currentEventId.Value))
                {
                    horasOcupadas.Add(inicio.TimeOfDay);
                    horasOcupadas.Add(fin.TimeOfDay);
                }
            }

            var horasInicio = todasHoras
                .Where(h => !IntervaloSolapado(h, h.Add(TimeSpan.FromMinutes(30)), horasOcupadas))
                .ToList();

            ActualizarComboHoras(comboBoxConSombraHoraEsdevenimentInici, horasInicio);
        }

        private void ComboBoxHoraInicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxConSombraHoraEsdevenimentInici.SelectedItem == null) return;

            var horaInicio = TimeSpan.Parse(comboBoxConSombraHoraEsdevenimentInici.Text);
            TimeSpan limiteMaximo = todasHoras.Last();

            var proximoEvento = horasOcupadas
                .Where((t, i) => i % 2 == 0 && t > horaInicio)
                .OrderBy(t => t)
                .FirstOrDefault();

            if (proximoEvento != default) limiteMaximo = proximoEvento;

            var horasFin = todasHoras
                .Where(h => h > horaInicio && h <= limiteMaximo)
                .Where(h => !IntervaloSolapado(horaInicio, h, horasOcupadas))
                .ToList();

            if (!crear && currentEventId.HasValue)
            {
                var horaActual = TimeSpan.Parse(comboBoxConSombraHoraEsdevenimentFi.Text);
                if (horaActual > horaInicio && !horasFin.Contains(horaActual))
                {
                    horasFin.Add(horaActual);
                    horasFin = horasFin.OrderBy(t => t).ToList();
                }
            }

            ActualizarComboHoras(comboBoxConSombraHoraEsdevenimentFi, horasFin);
        }

        private bool IntervaloSolapado(TimeSpan inicio, TimeSpan fin, List<TimeSpan> ocupadas)
        {
            for (int i = 0; i < ocupadas.Count; i += 2)
            {
                if (inicio < ocupadas[i + 1] && fin > ocupadas[i])
                    return true;
            }
            return false;
        }

        private void ActualizarComboHoras(ComboBoxConSombra combo, List<TimeSpan> horas)
        {
            combo.Items.Clear();
            foreach (var hora in horas.OrderBy(h => h))
                combo.Items.Add(hora.ToString(@"hh\:mm"));

            combo.SelectedIndex = combo.Items.Count > 0 ? 0 : -1;
        }

        private void ComboBoxEspai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxConSombraEspaiEsdeveniment.SelectedIndex <= 0)
            {
                textBoxConSombraUbicacioEsdeveniment.Text = "";
                textBoxConSombraPlaces.Text = "";
                checkBoxButaques.Checked = false;
                return;
            }

            DataRowView row = (DataRowView)comboBoxConSombraEspaiEsdeveniment.SelectedItem;

            // Establecer capacidad máxima
            int capacidadMaxima = (int)row["capacitat"];
            textBoxConSombraPlaces.MaxLength = capacidadMaxima.ToString().Length;

            textBoxConSombraUbicacioEsdeveniment.Text = row["ubicacio"].ToString();
            checkBoxButaques.Checked = (bool)row["butaques_fixes"];
            checkBoxButaques.Enabled = false;

            textBoxConSombraPlaces.Text = capacidadMaxima.ToString(); // Valor por defecto
            CargarHorasDisponibles();
        }

        private void TextBoxConSombraPlaces_Leave(object sender, EventArgs e)
        {
            if (comboBoxConSombraEspaiEsdeveniment.SelectedIndex <= 0) return;

            DataRowView row = (DataRowView)comboBoxConSombraEspaiEsdeveniment.SelectedItem;
            int maxCap = (int)row["capacitat"];

            if (!int.TryParse(textBoxConSombraPlaces.Text, out int valor) || valor <= 0)
                textBoxConSombraPlaces.Text = "1";
            else if (valor > maxCap)
                textBoxConSombraPlaces.Text = maxCap.ToString();
        }

        private void ComboBoxSeleccionarEsdeveniment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSeleccionarEsdeveniment.SelectedIndex == 0)
            {
                crear = true;
                currentEventId = null;
                LimpiarCampos();
                return;
            }

            DataRowView row = (DataRowView)comboBoxSeleccionarEsdeveniment.SelectedItem;
            crear = false;
            currentEventId = (int)row["id"];

            textBoxNomEsdeveniment.Text = row["nom"].ToString();
            textBoxConSombraDescripcioIA.Text = row["descripcio"].ToString();
            textBoxConSombraPlaces.Text = row["aforament"].ToString();
            checkBoxButaques.Checked = (bool)row["per_infants"];

            DateTime inicio = (DateTime)row["dataInici"];
            DateTime fin = (DateTime)row["dataFi"];
            dateTimePickerConSombraDiaEsdeveniment.Value = inicio;
            comboBoxConSombraHoraEsdevenimentInici.Text = inicio.ToString("HH:mm");
            comboBoxConSombraHoraEsdevenimentFi.Text = fin.ToString("HH:mm");
            comboBoxConSombraEspaiEsdeveniment.SelectedValue = row["espai_id"];
        }

        private void LimpiarCampos()
        {
            textBoxNomEsdeveniment.Text = "";
            textBoxConSombraDescripcioIA.Text = "";
            textBoxConSombraPlaces.Text = "";
            checkBoxButaques.Checked = false;
            dateTimePickerConSombraDiaEsdeveniment.Value = DateTime.Today;

            comboBoxConSombraHoraEsdevenimentInici.Items.Clear();
            comboBoxConSombraHoraEsdevenimentFi.Items.Clear();

            if (comboBoxConSombraEspaiEsdeveniment.Items.Count > 0)
                comboBoxConSombraEspaiEsdeveniment.SelectedIndex = 0;
        }

        private void roundedButtonEliminarEvent_Click_1(object sender, EventArgs e)
        {
            if (comboBoxSeleccionarEsdeveniment.SelectedIndex == 0) return;

            DataRowView row = (DataRowView)comboBoxSeleccionarEsdeveniment.SelectedItem;
            int id = (int)row["id"];
            EsdevenimentOrm.DeleteEventoCompleto(id);
            LimpiarCampos();
            CargarEsdeveniments();
        }

        private void roundedButtonConfirmarEvento_Click_1(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                DateTime dataInici = dateTimePickerConSombraDiaEsdeveniment.Value.Date.Add(
                    TimeSpan.Parse(comboBoxConSombraHoraEsdevenimentInici.Text)
                );

                DateTime dataFi = dateTimePickerConSombraDiaEsdeveniment.Value.Date.Add(
                    TimeSpan.Parse(comboBoxConSombraHoraEsdevenimentFi.Text)
                );

                int espaiId = (int)comboBoxConSombraEspaiEsdeveniment.SelectedValue;

                if (crear)
                {
                    EsdevenimentOrm.Insert(
                        textBoxNomEsdeveniment.Text.Trim(),
                        textBoxConSombraDescripcioIA.Text.Trim(),
                        dataInici,
                        dataFi,
                        int.Parse(textBoxConSombraPlaces.Text),
                        espaiId,
                        usuarioActual.Id,
                        null,
                        checkBoxPerInfants.Checked
                    );

                    int eventId = EsdevenimentOrm.SelectIdByName(textBoxNomEsdeveniment.Text.Trim());
                    ReservaOrm.InsertEspai(
                        usuarioActual.Id,
                        "ESPAI",
                        espaiId,
                        eventId,
                        dataInici,
                        dataFi
                    );

                    MessageBox.Show("Evento creado correctamente!");
                }
                else
                {
                    DialogResult respuesta = MessageBox.Show(
                        "¿Confirmas los cambios?",
                        "Confirmación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (respuesta == DialogResult.Yes)
                    {
                        DataRowView row = (DataRowView)comboBoxSeleccionarEsdeveniment.SelectedItem;
                        int id = (int)row["id"];

                        EsdevenimentOrm.Update(
                            id,
                            textBoxNomEsdeveniment.Text.Trim(),
                            textBoxConSombraDescripcioIA.Text.Trim(),
                            dataInici,
                            dataFi,
                            int.Parse(textBoxConSombraPlaces.Text),
                            espaiId,
                            usuarioActual.Id,
                            null,
                            checkBoxPerInfants.Checked
                        );
                        MessageBox.Show("Cambios guardados!");
                    }
                }

                CargarEsdeveniments();
                if (crear) LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            var errores = new StringBuilder();

            if (string.IsNullOrWhiteSpace(textBoxNomEsdeveniment.Text))
                errores.AppendLine("El campo 'Nom' es obligatorio.");

            if (dateTimePickerConSombraDiaEsdeveniment.Value < DateTime.Today)
                errores.AppendLine("La fecha no puede ser anterior al día actual.");

            if (comboBoxConSombraHoraEsdevenimentInici.SelectedItem == null ||
                comboBoxConSombraHoraEsdevenimentFi.SelectedItem == null)
                errores.AppendLine("Seleccione un horario válido.");

            if (TimeSpan.Parse(comboBoxConSombraHoraEsdevenimentFi.Text) <=
                TimeSpan.Parse(comboBoxConSombraHoraEsdevenimentInici.Text))
                errores.AppendLine("La hora final debe ser posterior a la de inicio.");

            if (!int.TryParse(textBoxConSombraPlaces.Text, out int aforo) || aforo <= 0)
                errores.AppendLine("Aforo no válido.");

            if (comboBoxConSombraEspaiEsdeveniment.SelectedIndex <= 0)
                errores.AppendLine("Seleccione un espacio válido.");

            if (errores.Length > 0)
            {
                MessageBox.Show(errores.ToString(), "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void roundedButtonCrearEsdeveniment_Click_1(object sender, EventArgs e)
        {
            crear = true;
            currentEventId = null;
            LimpiarCampos();
            CargarEsdeveniments();
        }

        private void textBoxConSombraPlaces_Leave_1(object sender, EventArgs e)
        {
            if (comboBoxConSombraEspaiEsdeveniment.SelectedIndex <= 0) return;

            DataRowView row = (DataRowView)comboBoxConSombraEspaiEsdeveniment.SelectedItem;
            int maxCap = (int)row["capacitat"];

            if (!int.TryParse(textBoxConSombraPlaces.Text, out int valor) || valor <= 0)
            {
                textBoxConSombraPlaces.Text = "1";
            }
            else if (valor > maxCap)
            {
                textBoxConSombraPlaces.Text = maxCap.ToString();
            }
        }

        private async void roundedButtonGenerarDescripcioIA_Click(object sender, EventArgs e)
        {
            // Validar campo obligatorio.
            if (string.IsNullOrWhiteSpace(textBoxConSombraInformacioAdicional.Text))
            {
                MessageBox.Show("Debes escribir una descripción base para generar la versión mejorada con IA.",
                                "Campo obligatorio",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Recoger TODOS los datos del formulario
                var datosEvento = new StringBuilder();
                datosEvento.AppendLine("=== DATOS DEL EVENTO ===");
                datosEvento.AppendLine($"• Nombre: {textBoxNomEsdeveniment.Text.Trim()}");
                datosEvento.AppendLine($"• Espacio: {comboBoxConSombraEspaiEsdeveniment.Text}");
                datosEvento.AppendLine($"• Fecha: {dateTimePickerConSombraDiaEsdeveniment.Value:dd/MM/yyyy}");
                datosEvento.AppendLine($"• Horario: {comboBoxConSombraHoraEsdevenimentInici.Text} a {comboBoxConSombraHoraEsdevenimentFi.Text}");
                datosEvento.AppendLine($"• Aforo: {textBoxConSombraPlaces.Text} personas");
                datosEvento.AppendLine($"• Información base: {textBoxConSombraInformacioAdicional.Text.Trim()}");

                // Construir prompt mejorado
                string promptIA = $@"
        ACTÚA COMO REDACTOR PROFESIONAL PARA EVENTOS CULTURALES.
        GENERA UNA DESCRIPCIÓN USANDO ESTOS DATOS:

        {datosEvento}

        REQUISITOS:
        - Estilo: {(new[] { "formal", "coloquial", "narrativo" }[new Random().Next(3)])}
        - Longitud: 180-200 caracteres
        - Estructura: 2-3 frases impactantes
        - Incluir: Localización, fecha/hora y aforo
        - Add emojis relevantes (max 2)
        - Formato JSON válido ÚNICAMENTE con 'descripcion'

        EJEMPLO VÁLIDO:
        {{
            ""descripcion"": ""Concierto de jazz nocturno 🎷 en el Auditorio Municipal (18/11/2024, 20:30). Disfruta de la Big Band con repertorio clásico y aforo exclusivo para {textBoxConSombraPlaces.Text} personas. ¡Reserva ya tu experiencia musical!""
        }}";

                var client = new RestClient("https://api.groq.com/openai/v1/chat/completions");
                var request = new RestRequest("", Method.Post);

                string apiKey = ConfigurationManager.AppSettings["GroqApiKey"];
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    MessageBox.Show("Error de configuración: Clave API no encontrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                request.AddHeader("Authorization", $"Bearer {apiKey}");
                request.AddHeader("Content-Type", "application/json");

                request.AddJsonBody(new
                {
                    model = "llama3-8b-8192",
                    messages = new[] { new { role = "user", content = promptIA } },
                    temperature = 0.7,
                    max_tokens = 250,
                    top_p = 0.9
                });

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    using (JsonDocument jsonResponse = JsonDocument.Parse(response.Content))
                    {
                        if (!jsonResponse.RootElement.TryGetProperty("choices", out var choices) || choices.GetArrayLength() == 0)
                        {
                            MessageBox.Show("La IA no respondió correctamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var content = choices[0].GetProperty("message").GetProperty("content").GetString();
                        var match = Regex.Match(content, @"\{.*?\}", RegexOptions.Singleline);

                        if (match.Success)
                        {
                            using (JsonDocument jsonDescripcion = JsonDocument.Parse(match.Value))
                            {
                                if (jsonDescripcion.RootElement.TryGetProperty("descripcion", out var descripcion))
                                {
                                    textBoxConSombraDescripcioIA.Text = descripcion.GetString();
                                    textBoxConSombraInformacioAdicional.Text = ""; // Limpiar campo después de generar
                                }
                                else
                                {
                                    MessageBox.Show("Formato de respuesta incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se encontró descripción en la respuesta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Error en la API: {response.StatusCode}\n{response.Content}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error crítico: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
