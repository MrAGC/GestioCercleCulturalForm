using GestioCercleCultural.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace GestioCercleCultural
{
    public partial class FormReservaUsuari : Form
    {
        private readonly UsuarioLogueado _usuario;
        private readonly Esdeveniment _evento;
        private readonly bool _tieneButacas;
        private int _aforoRestante;

        public FormReservaUsuari(UsuarioLogueado usuario, Esdeveniment evento)
        {
            InitializeComponent();
            _usuario = usuario;
            _evento = evento;

            using (var context = new CercleCulturalEntities1())
            {
                var espai = context.Espai.Find(_evento.espai_id);
                _tieneButacas = espai?.butaques_fixes ?? false;
            }

            CargarDatosIniciales();
            ActualizarDisponibilidad();
        }

        private void CargarDatosIniciales()
        {
            using (var context = new CercleCulturalEntities1())
            {
                // Cargar usuarios con índice inicial 0
                comboBoxSeleccionarUsuari.DataSource = context.Usuari.ToList();
                comboBoxSeleccionarUsuari.DisplayMember = "nom";
                comboBoxSeleccionarUsuari.ValueMember = "id";
                comboBoxSeleccionarUsuari.SelectedIndex = 0;
            }
        }

        private void ActualizarDisponibilidad()
        {
            using (var context = new CercleCulturalEntities1())
            {
                var reservas = context.Reserva
                    .Where(r => r.esdeveniment_id == _evento.id)
                    .Sum(r => r.numPlaces) ?? 0;

                _aforoRestante = _evento.aforament - reservas;
            }

            comboBoxConSombraEtrades.Items.Clear();

            int maxEntradas = Math.Min(_aforoRestante, 4);
            if (maxEntradas > 0)
            {
                for (int i = 1; i <= maxEntradas; i++)
                    comboBoxConSombraEtrades.Items.Add(i);

                comboBoxConSombraEtrades.SelectedIndex = 0;
                labelEntrades.Text = $"Entrades disponibles: {_aforoRestante}";
                roundedButtonReservar.Enabled = true;
            }
            else
            {
                labelEntrades.Text = "SOLD OUT";
                comboBoxConSombraEtrades.Enabled = false;
                roundedButtonReservar.Enabled = false;
            }
        }

        private void roundedButtonReservar_Click(object sender, EventArgs e)
        {
            if (!ValidarReserva()) return;

            int numEntradas = (int)comboBoxConSombraEtrades.SelectedItem;

            using (var context = new CercleCulturalEntities1())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Verificación en tiempo real
                    var reservasActuales = context.Reserva
                        .Where(r => r.esdeveniment_id == _evento.id)
                        .Sum(r => r.numPlaces) ?? 0;

                    if (reservasActuales + numEntradas > _evento.aforament)
                    {
                        MessageBox.Show("No hay suficientes entradas disponibles");
                        ActualizarDisponibilidad();
                        return;
                    }

                    var reserva = CrearReserva(context, numEntradas);

                    context.Reserva.Add(reserva);
                    context.SaveChanges();
                    transaction.Commit();

                    ActualizarInterfazPostReserva();
                    MessageBox.Show("Reserva realizada con éxito!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private bool ValidarReserva()
        {
            if (comboBoxSeleccionarUsuari.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un usuario");
                return false;
            }

            if (comboBoxConSombraEtrades.SelectedItem == null)
            {
                MessageBox.Show("Seleccione el número de entradas");
                return false;
            }

            return true;
        }

        private Reserva CrearReserva(CercleCulturalEntities1 context, int numEntradas)
        {
            var reserva = new Reserva
            {
                usuari_id = (int)comboBoxSeleccionarUsuari.SelectedValue,
                esdeveniment_id = _evento.id,
                dataReserva = DateTime.Now,
                estat = "CONFIRMAT",
                tipus = "ESDEVENIMENT",
                numPlaces = numEntradas
            };

            if (_tieneButacas)
            {
                ReservarAsientos(context, numEntradas, reserva);
            }

            return reserva;
        }

        public Dictionary<int, string> ObtenerEstadosAsientosParaEvento(List<int> seientIds, int esdevenimentId)
        {
            var estados = new Dictionary<int, string>();

            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    // Obtener todos los asientos reservados para el evento
                    var reservados = context.Reserva
                        .Where(r => r.esdeveniment_id == esdevenimentId)
                        .SelectMany(r => r.Seients.Select(s => s.id))
                        .ToList();

                    // Obtener estados globales
                    var asientos = context.Seients
                        .Where(s => seientIds.Contains(s.id))
                        .Select(s => new { s.id, s.estat })
                        .ToList();

                    foreach (var asiento in asientos)
                    {
                        string estado = reservados.Contains(asiento.id) ?
                            $"RESERVAT (per aquest esdeveniment) | Global: {asiento.estat}" :
                            $"DISPONIBLE | Global: {asiento.estat}";

                        estados.Add(asiento.id, estado);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error obtenint estats: {ex.Message}");
            }

            return estados;
        }

        private void ReservarAsientos(CercleCulturalEntities1 context, int numEntradas, Reserva reserva)
        {
            // Obtener asientos disponibles para este evento  
            var asientosDisponibles = context.Seients
                .Where(s => s.espai_id == _evento.espai_id)
                .AsEnumerable()
                .Where(s => ObtenerEstadosAsientosParaEvento(new List<int> { s.id }, _evento.id).Values.Any(estado => estado.Contains("DISPONIBLE")))
                .OrderBy(s => s.fila)
                .ThenBy(s => s.columna)
                .Take(numEntradas)
                .ToList();

            if (asientosDisponibles.Count < numEntradas)
                throw new Exception("No hay suficientes asientos disponibles");

            foreach (var asiento in asientosDisponibles)
            {
                reserva.Seients.Add(asiento);
            }
        }

        private void ActualizarInterfazPostReserva()
        {
            // Mantener selección de usuario
            var usuarioActual = comboBoxSeleccionarUsuari.SelectedValue;

            ActualizarDisponibilidad();

            // Restaurar selección previa
            comboBoxSeleccionarUsuari.SelectedValue = usuarioActual;

            // Seleccionar primera opción en entradas si hay disponibles
            if (comboBoxConSombraEtrades.Items.Count > 0)
                comboBoxConSombraEtrades.SelectedIndex = 0;
        }
    }
}