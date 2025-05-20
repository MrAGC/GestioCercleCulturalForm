using System;
using System.Data;
using System.Linq;
using GestioCercleCultural.Models;
using System.Data.Entity;


namespace GestioCercleCultural.Models.Controllers
{
    internal class EsdevenimentOrm
    {
        public static void Insert(string nom, string descripcio, DateTime dataInici, DateTime dataFi,
                                int aforament, int espaiId, int usuariId, string imatge, bool perInfants)
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var newEvent = new Esdeveniment
                    {
                        nom = nom,
                        descripcio = descripcio,
                        dataInici = dataInici,
                        dataFi = dataFi,
                        aforament = aforament,
                        espai_id = espaiId,
                        usuari_id = usuariId,
                        imatge = imatge,
                        per_infants = perInfants
                    };

                    context.Esdeveniment.Add(newEvent);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear evento: {ex.Message}");
            }
        }

        public static int SelectIdByName(string nom)
        {
            using (var context = new CercleCulturalEntities1())
            {
                var evento = context.Esdeveniment
                    .FirstOrDefault(e => e.nom == nom);

                if (evento != null)
                    return evento.id;

                throw new Exception("No se encontró el evento");
            }
        }

        public static DataTable SelectEventosByIdEspai(int idEspai)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("dataInici", typeof(DateTime));
            dt.Columns.Add("dataFi", typeof(DateTime));

            using (var context = new CercleCulturalEntities1())
            {
                var eventos = context.Esdeveniment
                    .Where(e => e.espai_id == idEspai)
                    .Select(e => new {e.id, e.dataInici, e.dataFi })
                    .ToList();

                foreach (var e in eventos)
                {
                    dt.Rows.Add(e.id, e.dataInici, e.dataFi);
                }
            }
            return dt;
        }

        public static DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("nom", typeof(string));
            dt.Columns.Add("descripcio", typeof(string));
            dt.Columns.Add("dataInici", typeof(DateTime));
            dt.Columns.Add("dataFi", typeof(DateTime));
            dt.Columns.Add("aforament", typeof(int));
            dt.Columns.Add("espai_id", typeof(int));
            dt.Columns.Add("usuari_id", typeof(int));
            dt.Columns.Add("imatge", typeof(string));
            dt.Columns.Add("per_infants", typeof(bool));

            using (var context = new CercleCulturalEntities1())
            {
                var eventos = context.Esdeveniment.ToList();
                foreach (var e in eventos)
                {
                    dt.Rows.Add(
                        e.id,
                        e.nom,
                        e.descripcio,
                        e.dataInici,
                        e.dataFi,
                        e.aforament,
                        e.espai_id,
                        e.usuari_id,
                        e.imatge,
                        e.per_infants
                    );
                }
            }
            return dt;
        }

        public static void Update(int id, string nom, string descripcio, DateTime dataInici, DateTime dataFi,
                                int aforament, int espaiId, int usuariId, string imatge, bool perInfants)
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var evento = context.Esdeveniment.Find(id);
                    if (evento != null)
                    {
                        evento.nom = nom;
                        evento.descripcio = descripcio;
                        evento.dataInici = dataInici;
                        evento.dataFi = dataFi;
                        evento.aforament = aforament;
                        evento.espai_id = espaiId;
                        evento.usuari_id = usuariId;
                        evento.imatge = imatge;
                        evento.per_infants = perInfants;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualizando evento: {ex.Message}");
            }
        }


        // En EsdevenimentOrm
        public static void DeleteEventoCompleto(int eventId)
        {
            using (var context = new CercleCulturalEntities1())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // 1. Eliminar reservas relacionadas con el evento
                    var reservasEvento = context.Reserva
                        .Where(r => r.esdeveniment_id == eventId)
                        .Include(r => r.Seients)
                        .ToList();

                    // 2. Liberar asientos y eliminar relaciones
                    foreach (var reserva in reservasEvento)
                    {
                        foreach (var seient in reserva.Seients.ToList())
                        {
                            seient.estat = "DISPONIBLE";
                            reserva.Seients.Remove(seient);
                        }
                    }

                    context.Reserva.RemoveRange(reservasEvento);

                    // 3. Eliminar el evento
                    var evento = context.Esdeveniment.Find(eventId);
                    if (evento != null)
                    {
                        context.Esdeveniment.Remove(evento);
                    }

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error eliminando evento completo: {ex.Message}");
                    throw;
                }
            }
        }

        public static DataTable SelectByUser(int userId)
        {
            DataTable dt = new DataTable();

            // Crear misma estructura que SelectAll()
            dt = SelectAll().Clone();

            using (var context = new CercleCulturalEntities1())
            {
                var eventos = context.Esdeveniment
                    .Where(e => e.usuari_id == userId)
                    .ToList();

                foreach (var e in eventos)
                {
                    dt.Rows.Add(
                        e.id,
                        e.nom,
                        e.descripcio,
                        e.dataInici,
                        e.dataFi,
                        e.aforament,
                        e.espai_id,
                        e.usuari_id,
                        e.imatge,
                        e.per_infants
                    );
                }
            }
            return dt;
        }
    }
}