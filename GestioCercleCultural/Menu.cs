using GestioCercleCultural.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GestioCercleCultural
{
    public partial class Menu : Form
    {
        bool sidebarExpand;
        private UsuarioLogueado _usuario;

        /// <summary>
        /// Constructor de la clase Menu.
        /// </summary>
        /// <param name="usuario"> Usuario logueado.</param>
        public Menu(UsuarioLogueado usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            this.FormClosing += new FormClosingEventHandler(Menu_FormClosing);
        }

        /// <summary>
        /// Evento que se ejecuta al cargar el formulario.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de carga del formulario.</param>
        private void Menu_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            if (_usuario.TipoUsuario != "SUPERUSUARI")
            {
                buttonGestioUsuaris.Visible = false;
                panelUsuari.Visible = false;

                buttonGestioEspais.Visible = false;
                panelEspais.Visible = false;


                panelEsdeveniments.Location = new Point(3, 165);
                panelSuport.Location = new Point(3, 221);
            }
        }

        /// <summary>
        /// Evento que se ejecuta al cerrar el formulario.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de cierre del formulario.</param>
        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de cerrar sesión.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en el botón de cerrar sesión.</param>
        private void sidebarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el logo del menú.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en el logo del menú.</param>
        private void pictureBoxLogoMenu_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de gestión de usuarios.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en el botón de gestión de usuarios.</param>
        private void buttonGestioUsuaris_Click(object sender, EventArgs e)
        {
            // hacer que solo cargue el interior del form, no su borde con la x de cerrar, minimizar y eso, solo el contenido

            panelCargarForms.Controls.Clear();

            FormGestioUsuaris formGestioUsuaris = new FormGestioUsuaris(_usuario);
            formGestioUsuaris.FormBorderStyle = FormBorderStyle.None;
            formGestioUsuaris.TopLevel = false;
            formGestioUsuaris.AutoScroll = true;
            formGestioUsuaris.Dock = DockStyle.Fill;
            panelCargarForms.Controls.Add(formGestioUsuaris);
            formGestioUsuaris.Show();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de gestión de espacios.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en el botón de gestión de espacios.</param>
        private void buttonGestioEspais_Click(object sender, EventArgs e)
        {
            panelCargarForms.Controls.Clear();

            FormGestioEspais formGestioEspais = new FormGestioEspais(_usuario);
            formGestioEspais.FormBorderStyle = FormBorderStyle.None;
            formGestioEspais.TopLevel = false;
            formGestioEspais.AutoScroll = true;
            formGestioEspais.Dock = DockStyle.Fill;
            panelCargarForms.Controls.Add(formGestioEspais);
            formGestioEspais.Show();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de gestión de eventos.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en el botón de gestión de eventos.</param>
        private void buttonGestioEsdeveniments_Click(object sender, EventArgs e)
        {
            panelCargarForms.Controls.Clear();

            FormGestioEsdeveniments formGestioEsdeveniments = new FormGestioEsdeveniments(_usuario);
            formGestioEsdeveniments.FormBorderStyle = FormBorderStyle.None;
            formGestioEsdeveniments.TopLevel = false;
            formGestioEsdeveniments.AutoScroll = true;
            formGestioEsdeveniments.Dock = DockStyle.Fill;
            panelCargarForms.Controls.Add(formGestioEsdeveniments);
            formGestioEsdeveniments.Show();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en el botón de soporte.</param>
        private void buttonReservesSuport_Click(object sender, EventArgs e)
        {
            panelCargarForms.Controls.Clear();

            FormReservesSuport formReservesSuport = new FormReservesSuport(this, _usuario);
            formReservesSuport.FormBorderStyle = FormBorderStyle.None;
            formReservesSuport.TopLevel = false;
            formReservesSuport.AutoScroll = true;
            formReservesSuport.Dock = DockStyle.Fill;
            panelCargarForms.Controls.Add(formReservesSuport);
            formReservesSuport.Show();
        }

        /// <summary>
        /// Método para cambiar el formulario que se muestra en el panel.
        /// </summary>
        /// <param name="nuevoFormulario"> El nuevo formulario que se va a mostrar.</param>
        public void CambiarFormulario(Form nuevoFormulario)
        {
            panelCargarForms.Controls.Clear();

            nuevoFormulario.FormBorderStyle = FormBorderStyle.None;
            nuevoFormulario.TopLevel = false;
            nuevoFormulario.AutoScroll = true;
            nuevoFormulario.Dock = DockStyle.Fill;

            panelCargarForms.Controls.Add(nuevoFormulario);
            nuevoFormulario.Show();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de cerrar sesión.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en el botón de cerrar sesión.</param>
        private void roundedButtonCerrarSession_Click(object sender, EventArgs e)
        {
            FormIniciarSessio formIniciarSessio = new FormIniciarSessio();
            formIniciarSessio.Show();
            this.Hide();
        }
    }
}
