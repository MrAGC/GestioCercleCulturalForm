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

        /// <summary>
        /// Constructor de la clase FormReservaUsuari.
        /// </summary>
        /// <param name="usuario"> El usuario logueado.</param>
        /// <param name="evento"> El evento para el cual se realiza la reserva.</param>
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

        /// <summary>
        /// Carga los datos iniciales en los controles del formulario.
        /// </summary>
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

        /// <summary>
        /// Actualiza la disponibilidad de entradas para el evento.
        /// </summary>
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

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de reservar.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic.</param>
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

        /// <summary>
        /// Valida la reserva antes de proceder.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Crea una reserva en la base de datos.
        /// </summary>
        /// <param name="context"> El contexto de la base de datos.</param>
        /// <param name="numEntradas"> Número de entradas a reservar.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Obtiene los estados de los asientos para un evento específico.
        /// </summary>
        /// <param name="seientIds"> Lista de IDs de asientos.</param>
        /// <param name="esdevenimentId"> ID del evento.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Reserva asientos para un evento específico.
        /// </summary>
        /// <param name="context"> El contexto de la base de datos.</param>
        /// <param name="numEntradas"> Número de entradas a reservar.</param>
        /// <param name="reserva"> La reserva a la que se asignarán los asientos.</param>
        /// <exception cref="Exception"> Lanza una excepción si no hay suficientes asientos disponibles.</exception>
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

        /// <summary>
        /// Actualiza la interfaz después de realizar una reserva.
        /// </summary>
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