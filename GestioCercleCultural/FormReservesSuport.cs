using GestioCercleCultural.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;


namespace GestioCercleCultural
{
    public partial class FormReservesSuport : Form
    {
        private UsuarioLogueado usuarioActual;
        private readonly Menu menuPrincipal;
        private List<Esdeveniment> eventos;
        private int currentEventIndex = 0;


        /// <summary>
        /// Constructor de la clase FormReservesSuport.
        /// </summary>
        /// <param name="menu"> Formulario principal.</param>
        /// <param name="usuario"> Usuario logueado.</param>
        public FormReservesSuport(Menu menu, UsuarioLogueado usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            menuPrincipal = menu;
            CargarEventos();
            CargarEventoActual();
            ConfigurarBotonesFlecha();
        }

        /// <summary>
        /// Carga los eventos desde la base de datos.
        /// </summary>
        private void CargarEventos()
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    eventos = context.Esdeveniment
                        .Include(e => e.Espai) // Carga relacionada
                        .Where(e => e.dataInici > DateTime.Now)
                        .OrderBy(e => e.dataInici)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando eventos: {ex.Message}");
                eventos = new List<Esdeveniment>();
            }
        }

        /// <summary>
        /// Carga el evento actual en los controles del formulario.
        /// </summary>
        private void CargarEventoActual()
        {
            if (eventos == null || eventos.Count == 0)
            {
                MostrarMensajeSinEventos();
                return;
            }

            var eventoActual = eventos[currentEventIndex];

            labelEventoNombre.Text = eventoActual.nom;
            labelDescripcion.Text = eventoActual.descripcio;

            // Ahora Espai está disponible
            labelUbicacion.Text = eventoActual.Espai?.ubicacio ?? "No especificada";
            labelFecha.Text = eventoActual.dataInici.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Muestra un mensaje cuando no hay eventos próximos.
        /// </summary>
        private void MostrarMensajeSinEventos()
        {
            labelEventoNombre.Text = "No hay eventos próximos";
            labelDescripcion.Visible = false;
            labelUbicacion.Visible = false;
            labelFecha.Visible = false;
            roundedButtonReservar.Enabled = false;
        }

        /// <summary>
        /// Configura la visibilidad de los botones de flecha.
        /// </summary>
        private void ConfigurarBotonesFlecha()
        {
            pictureBoxFechaDerecha.Visible = eventos?.Count > 1;
            pictureBoxIzquierda.Visible = eventos?.Count > 1;
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en la flecha derecha.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en la flecha derecha.</param>
        private void pictureBoxFechaDerecha_Click(object sender, EventArgs e)
        {
            if (currentEventIndex < eventos.Count - 1)
            {
                currentEventIndex++;
                CargarEventoActual();
            }
            ActualizarEstadoFlechas();
        }

        /// <summary>
        /// Actualiza la visibilidad de las flechas según el índice del evento actual.
        /// </summary>
        private void ActualizarEstadoFlechas()
        {
            pictureBoxIzquierda.Visible = currentEventIndex > 0;
            pictureBoxFechaDerecha.Visible = currentEventIndex < eventos.Count - 1;
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de reservar.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en el botón de reservar.</param>
        private void roundedButtonReservar_Click(object sender, EventArgs e)
        {
            var formReserva = new FormReservaUsuari(usuarioActual, eventos[currentEventIndex]);

            menuPrincipal.CambiarFormulario(formReserva);
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en la flecha izquierda.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en la flecha izquierda.</param>
        private void pictureBoxIzquierda_Click(object sender, EventArgs e)
        {
            if (currentEventIndex > 0)
            {
                currentEventIndex--;
                CargarEventoActual();
            }
            ActualizarEstadoFlechas();
        }
    }
}