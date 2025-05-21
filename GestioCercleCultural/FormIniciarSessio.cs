using GestioCercleCultural.Models;
using GestioCercleCultural.Models.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestioCercleCultural
{
    public partial class FormIniciarSessio : Form
    {

        /// <summary>
        /// Constructor de la clase FormIniciarSessio.
        /// </summary>
        public FormIniciarSessio()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Menu_FormClosing);
            textBoxContra.UseSystemPasswordChar = true;

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
        /// Evento que se ejecuta al cargar el formulario.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de carga del formulario.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        /// <summary>
        /// Evento que se ejecuta al cerrar el formulario.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de cierre del formulario.</param>
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Evento que se ejecuta al hacer clic en el botón de inicio de sesión.
        /// </summary>
        /// <param name="sender"> El objeto que envía el evento.</param>
        /// <param name="e"> Argumentos del evento de clic en el botón.</param>
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;
            string password = textBoxContra.Text;

            // Validación básica
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, completa todos los campos");
                return;
            }

            // Llamar al método de login
            UsuarioLogueado usuario = UsuariOrm.Login(email, password);

            if (usuario != null)
            {
                AbrirPantallaPrincipal(usuario);
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas o acceso no autorizado");
            }
        }

        /// <summary>
        /// Método para abrir la pantalla principal después de iniciar sesión.
        /// </summary>
        /// <param name="usuario"> El usuario que ha iniciado sesión.</param>
        private void AbrirPantallaPrincipal(UsuarioLogueado usuario)
        {
            // Ejemplo de cómo abrir otra ventana
            Menu menu = new Menu(usuario);
            menu.Show();
            this.Hide();
        }
    }
}
