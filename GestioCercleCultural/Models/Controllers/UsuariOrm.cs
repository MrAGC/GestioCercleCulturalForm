using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace GestioCercleCultural.Models.Controllers
{
    internal class UsuariOrm
    {
        /// <summary>
        /// Encripta una cadena de texto utilizando el algoritmo MD5.
        /// </summary>
        /// <param name="input"> Cadena de texto a encriptar.</param>
        /// <returns></returns>
        private static string EncriptarMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                foreach (byte b in data) sBuilder.Append(b.ToString("x2"));
                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// Realiza el login de un usuario.
        /// </summary>
        /// <param name="email"> Email del usuario.</param>
        /// <param name="contrasenya"> Contraseña del usuario.</param>
        /// <returns></returns>
        public static UsuarioLogueado Login(string email, string contrasenya)
        {
            try
            {
                string hash = EncriptarMD5(contrasenya);

                using (var context = new CercleCulturalEntities1())
                {
                    var usuario = context.Usuari
                        .FirstOrDefault(u => u.email == email && u.contrasenya == hash);

                    if (usuario != null)
                    {
                        if (usuario.tipusUsuari != "NORMAL")
                        {
                            return new UsuarioLogueado
                            {
                                Id = usuario.id,
                                Nombre = usuario.nom,
                                Email = usuario.email,
                                TipoUsuario = usuario.tipusUsuari
                            };
                        }
                        Console.WriteLine("El usuario no tiene permisos para acceder.");
                    }
                    else
                    {
                        Console.WriteLine("Email o contraseña incorrectos.");
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Inserta un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="nom"> Nombre del usuario.</param>
        /// <param name="email"> Email del usuario.</param>
        /// <param name="contrasenya"> Contraseña del usuario.</param>
        /// <param name="tipusUsuari"> Tipo de usuario (ADMIN o NORMAL).</param>
        /// <param name="idioma"> Idioma del usuario.</param>
        public static void Insert(string nom, string email, string contrasenya, string tipusUsuari, string idioma)
        {
            try
            {
                string hash = EncriptarMD5(contrasenya);

                using (var context = new CercleCulturalEntities1())
                {
                    var newUser = new Usuari
                    {
                        nom = nom,
                        email = email,
                        contrasenya = hash,
                        tipusUsuari = tipusUsuari,
                        idioma = idioma
                    };

                    context.Usuari.Add(newUser);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Verifica si un email ya existe en la base de datos.
        /// </summary>
        /// <param name="email"> Email a verificar.</param>
        /// <returns></returns>
        public static bool EmailExists(string email)
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    return context.Usuari.Any(u => u.email == email);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verificando email: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Selecciona todos los usuarios de la base de datos.
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("nom", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("tipusUsuari", typeof(string));
            dt.Columns.Add("idioma", typeof(string));

            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var usuarios = context.Usuari
                        .Select(u => new { u.id, u.nom, u.email, u.tipusUsuari, u.idioma })
                        .ToList();

                    foreach (var u in usuarios)
                    {
                        dt.Rows.Add(u.id, u.nom, u.email, u.tipusUsuari, u.idioma);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error obteniendo usuarios: {ex.Message}");
            }

            return dt;
        }

        /// <summary>
        /// Actualiza la información de un usuario en la base de datos.
        /// </summary>
        /// <param name="id"> ID del usuario a actualizar.</param>
        /// <param name="nom"> Nombre del usuario.</param>
        /// <param name="email"> Email del usuario.</param>
        /// <param name="tipusUsuari"> Tipo de usuario (ADMIN o NORMAL).</param>
        /// <param name="idioma"> Idioma del usuario.</param>
        public static void Update(int id, string nom, string email, string tipusUsuari, string idioma)
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var usuario = context.Usuari.Find(id);
                    if (usuario != null)
                    {
                        usuario.nom = nom;
                        usuario.email = email;
                        usuario.tipusUsuari = tipusUsuari;
                        usuario.idioma = idioma;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un usuario y todas sus reservas y eventos asociados.
        /// </summary>
        /// <param name="usuariId"> ID del usuario a eliminar.</param>
        /// <exception cref="Exception"> Lanza una excepción si ocurre un error durante la eliminación.</exception>
        public static void DeleteAll(int usuariId)
        {
            using (var context = new CercleCulturalEntities1())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // 1. Eliminar mensajes del usuario
                    var mensajes = context.Mensajes
                        .Where(m => m.usuari_id == usuariId)
                        .ToList();
                    context.Mensajes.RemoveRange(mensajes);

                    // 2. Obtener eventos creados por el usuario
                    var eventos = context.Esdeveniment
                        .Where(e => e.usuari_id == usuariId)
                        .ToList();

                    // 3. Eliminar reservas de esos eventos
                    var reservasEventos = context.Reserva
                        .Where(r => eventos.Select(e => e.id).Contains(r.esdeveniment_id.Value))
                        .Include(r => r.Seients)
                        .ToList();

                    foreach (var reserva in reservasEventos)
                    {
                        foreach (var seient in reserva.Seients.ToList())
                        {
                            seient.estat = "DISPONIBLE";
                            reserva.Seients.Remove(seient);
                        }
                        context.Reserva.Remove(reserva);
                    }

                    // 4. Eliminar los eventos del usuario
                    context.Esdeveniment.RemoveRange(eventos);

                    // 5. Eliminar reservas directas del usuario
                    var reservasUsuario = context.Reserva
                        .Where(r => r.usuari_id == usuariId)
                        .Include(r => r.Seients)
                        .ToList();

                    foreach (var reserva in reservasUsuario)
                    {
                        foreach (var seient in reserva.Seients.ToList())
                        {
                            seient.estat = "DISPONIBLE";
                            reserva.Seients.Remove(seient);
                        }
                        context.Reserva.Remove(reserva);
                    }

                    // 6. Finalmente eliminar el usuario
                    var usuario = context.Usuari.Find(usuariId);
                    if (usuario != null)
                    {
                        context.Usuari.Remove(usuario);
                    }

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Mostrar el error completo incluyendo inner exception
                    throw new Exception($"Error completo: {ex.Message}\nInner exception: {ex.InnerException?.Message}");
                }
            }
        }
    }
}
