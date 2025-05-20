using System;
using System.Data;
using System.Linq;
using GestioCercleCultural.Models;
using System.Data.Entity;

namespace GestioCercleCultural.Models.Controllers
{
    internal class ReservaOrm
    {
        public static void InsertEspai(int usuariId, string tipus, int? espaiId, int? esdevenimentId,
                                DateTime? dataInici, DateTime? dataFi)
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var novaReserva = new Reserva
                    {
                        usuari_id = usuariId,
                        dataReserva = DateTime.Now,
                        estat = "CONFIRMAT",
                        tipus = tipus,
                        espai_id = espaiId,
                        esdeveniment_id = esdevenimentId,
                        dataInici = dataInici,
                        dataFi = dataFi
                    };

                    context.Reserva.Add(novaReserva);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creant reserva d'espai: {ex.Message}");
            }
        }

        public static void InsertEvent(int usuariId, string tipus, int? espaiId, int? esdevenimentId,
                                DateTime? dataInici, DateTime? dataFi, int? numPlaces, string estat = "PENDENT")
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var novaReserva = new Reserva
                    {
                        usuari_id = usuariId,
                        tipus = tipus,
                        espai_id = espaiId,
                        esdeveniment_id = esdevenimentId,
                        dataInici = dataInici,
                        dataFi = dataFi,
                        numPlaces = numPlaces,
                        estat = estat
                    };

                    context.Reserva.Add(novaReserva);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creant reserva d'esdeveniment: {ex.Message}");
            }
        }

        public static DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("usuari_id", typeof(int));
            dt.Columns.Add("dataReserva", typeof(DateTime));
            dt.Columns.Add("estat", typeof(string));
            dt.Columns.Add("tipus", typeof(string));
            dt.Columns.Add("espai_id", typeof(int));
            dt.Columns.Add("esdeveniment_id", typeof(int));
            dt.Columns.Add("dataInici", typeof(DateTime));
            dt.Columns.Add("dataFi", typeof(DateTime));
            dt.Columns.Add("numPlaces", typeof(int));

            using (var context = new CercleCulturalEntities1())
            {
                var reservas = context.Reserva.ToList();
                foreach (var r in reservas)
                {
                    dt.Rows.Add(
                        r.id,
                        r.usuari_id,
                        r.dataReserva,
                        r.estat,
                        r.tipus,
                        r.espai_id,
                        r.esdeveniment_id,
                        r.dataInici,
                        r.dataFi,
                        r.numPlaces
                    );
                }
            }
            return dt;
        }

        public static void Update(int id, int usuariId, string tipus, int? espaiId, int? esdevenimentId,
                                DateTime? dataInici, DateTime? dataFi, int? numPlaces, string estat)
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var reserva = context.Reserva.Find(id);
                    if (reserva != null)
                    {
                        reserva.usuari_id = usuariId;
                        reserva.tipus = tipus;
                        reserva.espai_id = espaiId;
                        reserva.esdeveniment_id = esdevenimentId;
                        reserva.dataInici = dataInici;
                        reserva.dataFi = dataFi;
                        reserva.numPlaces = numPlaces;
                        reserva.estat = estat;

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error actualitzant reserva: {ex.Message}");
            }
        }

        public static void Delete(int id)
        {
            try
            {
                using (var context = new CercleCulturalEntities1())
                {
                    var reserva = context.Reserva.Find(id);
                    if (reserva != null)
                    {
                        context.Reserva.Remove(reserva);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminant reserva: {ex.Message}");
            }
        }


        // En ReservaOrm
        public static void DeleteReservaCompleto(int reservaId)
        {
            using (var context = new CercleCulturalEntities1())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var reserva = context.Reserva
                        .Include(r => r.Seients)
                        .FirstOrDefault(r => r.id == reservaId);

                    if (reserva != null)
                    {
                        // Liberar asientos
                        foreach (var seient in reserva.Seients.ToList())
                        {
                            seient.estat = "DISPONIBLE";
                            reserva.Seients.Remove(seient);
                        }

                        context.Reserva.Remove(reserva);
                        context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error eliminando reserva completa: {ex.Message}");
                    throw;
                }
            }
        }

    }
}