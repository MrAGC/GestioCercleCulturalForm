using System;
using System.Data;
using System.Linq;
using GestioCercleCultural.Models;

namespace GestioCercleCultural.Models.Controllers
{
    internal class SeientsOrm
    {
        /// <summary>
        /// Crea un nou seient a la base de dades.
        /// </summary>
        /// <param name="espaiId"> ID de l'espai on es troba el seient</param>
        /// <param name="numerat"> Indica si el seient és numerat</param>
        /// <param name="fila"> Fila del seient</param>
        /// <param name="columna"> Columna del seient</param>
        /// <param name="estat"> Estat del seient (per defecte "DISPONIBLE")</param>
        public static void Insert(int espaiId, bool numerat, string fila, string columna, string estat = "DISPONIBLE")
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var nouSeient = new Seients
                    {
                        espai_id = espaiId,
                        numerat = numerat,
                        fila = string.IsNullOrEmpty(fila) ? null : fila,
                        columna = string.IsNullOrEmpty(columna) ? null : columna,
                        estat = estat
                    };

                    context.Seients.Add(nouSeient);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creant seient: {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna tots els seients de la base de dades.
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("espai_id", typeof(int));
            dt.Columns.Add("numerat", typeof(bool));
            dt.Columns.Add("fila", typeof(string));
            dt.Columns.Add("columna", typeof(string));
            dt.Columns.Add("estat", typeof(string));

            using (var context = new CercleCulturalEntities1())
            {
                var seients = context.Seients.ToList();
                foreach (var s in seients)
                {
                    dt.Rows.Add(
                        s.id,
                        s.espai_id,
                        s.numerat,
                        s.fila,
                        s.columna,
                        s.estat
                    );
                }
            }
            return dt;
        }

        /// <summary>
        /// Actualitza un seient existent a la base de dades.
        /// </summary>
        /// <param name="id"> ID del seient a actualitzar</param>
        /// <param name="espaiId"> ID de l'espai on es troba el seient</param>
        /// <param name="numerat"> Indica si el seient és numerat</param>
        /// <param name="fila"> Fila del seient</param>
        /// <param name="columna"> Columna del seient</param>
        /// <param name="estat"> Estat del seient</param>
        public static void Update(int id, int espaiId, bool numerat, string fila, string columna, string estat)
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var seient = context.Seients.Find(id);
                    if (seient != null)
                    {
                        seient.espai_id = espaiId;
                        seient.numerat = numerat;
                        seient.fila = string.IsNullOrEmpty(fila) ? null : fila;
                        seient.columna = string.IsNullOrEmpty(columna) ? null : columna;
                        seient.estat = estat;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualitzant seient: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un seient de la base de dades.
        /// </summary>
        /// <param name="id"> ID del seient a eliminar</param>
        public static void Delete(int id)
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var seient = context.Seients.Find(id);
                    if (seient != null)
                    {
                        context.Seients.Remove(seient);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminant seient: {ex.Message}");
            }
        }
    }
}