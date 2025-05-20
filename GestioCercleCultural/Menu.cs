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

        public Menu(UsuarioLogueado usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            this.FormClosing += new FormClosingEventHandler(Menu_FormClosing);
        }
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
        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

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

        private void pictureBoxLogoMenu_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

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

        private void roundedButtonCerrarSession_Click(object sender, EventArgs e)
        {
            FormIniciarSessio formIniciarSessio = new FormIniciarSessio();
            formIniciarSessio.Show();
            this.Hide();
        }
    }
}
