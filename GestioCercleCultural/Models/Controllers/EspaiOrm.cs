using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

namespace GestioCercleCultural.Models.Controllers
{
    internal class EspaiOrm
    {
        public static DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            // 1. Definir estructura exacta
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("nom", typeof(string));
            dt.Columns.Add("capacitat", typeof(int));
            dt.Columns.Add("ubicacio", typeof(string));
            dt.Columns.Add("metres_quadrats", typeof(decimal));
            dt.Columns.Add("butaques_fixes", typeof(bool));
            dt.Columns.Add("num_files", typeof(int));
            dt.Columns.Add("num_butaques_per_fila", typeof(int));

            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    // 2. Cargar datos con Include para relaciones
                    var espais = context.Espai
                        .AsNoTracking()
                        .OrderBy(e => e.nom)
                        .ToList();

                    // 3. Llenar DataTable
                    foreach (var e in espais)
                    {
                        dt.Rows.Add(
                            e.id,
                            e.nom ?? "", // Manejo de nulls
                            e.capacitat,
                            e.ubicacio ?? "",
                            e.metres_quadrats,
                            e.butaques_fixes,
                            e.num_files ?? 0,
                            e.num_butaques_per_fila ?? 0
                        );
                    }
                }
            }
            // Dentro de EspaiOrm.SelectAll()
catch (Exception ex)
{
    MessageBox.Show($"Error en SelectAll: {ex.ToString()}"); // Mostrar excepción completa
    dt.Rows.Clear();
}

            return dt;
        }

        public static void Insert(string nom, int capacitat, string ubicacio, decimal metresQuadrats,
                        bool butaquesFixes, int? numFiles, int? butaquesPerFila)
        {
            using (var context = new CercleCulturalEntities1())
            {
                var nouEspai = new Espai
                {
                    nom = nom,
                    capacitat = capacitat,
                    ubicacio = ubicacio,
                    metres_quadrats = metresQuadrats,
                    butaques_fixes = butaquesFixes,
                    num_files = numFiles,
                    num_butaques_per_fila = butaquesPerFila
                };

                context.Espai.Add(nouEspai);
                context.SaveChanges(); // Genera el ID del espacio

                // Si el espacio tiene butaques fijas, crear los asientos
                if (butaquesFixes && numFiles.HasValue && butaquesPerFila.HasValue)
                {
                    for (int i = 0; i < numFiles.Value; i++)
                    {
                        char fila = (char)('A' + i); // Filas como A, B, C...
                        for (int col = 1; col <= butaquesPerFila.Value; col++)
                        {
                            var seient = new Seients
                            {
                                espai_id = nouEspai.id,
                                numerat = true,
                                fila = fila.ToString(),
                                columna = col.ToString(),
                                estat = "disponible"
                            };
                            context.Seients.Add(seient);
                        }
                    }
                    context.SaveChanges();
                }
            }
        }

        public static void Update(int id, string nom, int capacitat, string ubicacio, decimal metresQuadrats,
                                bool butaquesFixes, int? numFiles, int? butaquesPerFila)
        {
            using (var context = new CercleCulturalEntities1())
            {
                var espai = context.Espai.Find(id);
                if (espai == null) return;

                espai.nom = nom;
                espai.capacitat = capacitat;
                espai.ubicacio = ubicacio;
                espai.metres_quadrats = metresQuadrats;
                espai.butaques_fixes = butaquesFixes;
                espai.num_files = numFiles;
                espai.num_butaques_per_fila = butaquesPerFila;

                context.SaveChanges();
            }
        }

        // En EspaiOrm
        public static void DeleteEspaiCompleto(int espaiId)
        {
            using (var context = new CercleCulturalEntities1())
            using (var tx = context.Database.BeginTransaction())
            {
                try
                {
                    var espai = context.Espai
                        .Include(e => e.Esdeveniment)
                        .Include(e => e.Reserva)
                        .Include(e => e.Seients)
                        .FirstOrDefault(e => e.id == espaiId);

                    if (espai == null) return;

                    // Eliminar reservas relacionadas
                    var reservasIds = espai.Reserva.Select(r => r.id).ToList();
                    var reservas = context.Reserva
                        .Where(r => reservasIds.Contains(r.id))
                        .Include(r => r.Seients)
                        .ToList();

                    foreach (var reserva in reservas)
                    {
                        foreach (var seient in reserva.Seients.ToList())
                        {
                            seient.estat = "DISPONIBLE";
                            reserva.Seients.Remove(seient);
                        }
                    }
                    context.Reserva.RemoveRange(reservas);

                    // Eliminar eventos del espacio
                    context.Esdeveniment.RemoveRange(espai.Esdeveniment);

                    // Eliminar asientos
                    context.Seients.RemoveRange(espai.Seients);

                    // Finalmente eliminar el espacio
                    context.Espai.Remove(espai);

                    context.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    Console.WriteLine($"Error eliminando espacio: {ex.Message}");
                    throw;
                }
            }
        }


        public static bool ExisteNombre(string nombre)
        {
            using (var context = new CercleCulturalEntities1())
            {
                return context.Espai.Any(e => e.nom == nombre);
            }
        }

        public static bool TieneEventosAsociados(int espaiId)
        {
            using (var context = new CercleCulturalEntities1())
            {
                return context.Esdeveniment.Any(e => e.espai_id == espaiId);
            }
        }
    }
}