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

        public FormReservesSuport(Menu menu, UsuarioLogueado usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            menuPrincipal = menu;
            CargarEventos();
            CargarEventoActual();
            ConfigurarBotonesFlecha();
        }

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


        private void MostrarMensajeSinEventos()
        {
            labelEventoNombre.Text = "No hay eventos próximos";
            labelDescripcion.Visible = false;
            labelUbicacion.Visible = false;
            labelFecha.Visible = false;
            roundedButtonReservar.Enabled = false;
        }

        private void ConfigurarBotonesFlecha()
        {
            pictureBoxFechaDerecha.Visible = eventos?.Count > 1;
            pictureBoxIzquierda.Visible = eventos?.Count > 1;
        }

        private void pictureBoxFechaDerecha_Click(object sender, EventArgs e)
        {
            if (currentEventIndex < eventos.Count - 1)
            {
                currentEventIndex++;
                CargarEventoActual();
            }
            ActualizarEstadoFlechas();
        }

        private void ActualizarEstadoFlechas()
        {
            pictureBoxIzquierda.Visible = currentEventIndex > 0;
            pictureBoxFechaDerecha.Visible = currentEventIndex < eventos.Count - 1;
        }

        private void roundedButtonReservar_Click(object sender, EventArgs e)
        {
            var formReserva = new FormReservaUsuari(usuarioActual, eventos[currentEventIndex]);

            menuPrincipal.CambiarFormulario(formReserva);
        }

        private void pictureBoxIzquierda_Click(object sender, EventArgs e)
        {
            if (currentEventIndex > 0)
            {
                currentEventIndex--;
                CargarEventoActual();
            }
            ActualizarEstadoFlechas();
        }

        private void FormReservesSuport_Load(object sender, EventArgs e)
        {
            this.AutoScroll = false;
        }
    }
}