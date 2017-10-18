namespace LecturasRestAPI.Controllers
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Models;

    public class MedidoresController : ApiController
    {
        private LecturasContext db = new LecturasContext();

        // GET: api/Medidores
        public IQueryable<Medidor> GetMedidores()
        {
            return db.Medidores
                .Include(m => m.ModeloDeMedidor)
                .Include(m => m.UltimaLectura.ValoresLeidos)
                ;
        }

        // GET: api/Medidores/5
        [ResponseType(typeof(Medidor))]
        public async Task<IHttpActionResult> GetMedidor(int id)
        {
            Medidor medidor = await db.Medidores
                .Include(m => m.ModeloDeMedidor)
                .Include(m => m.UltimaLectura.ValoresLeidos)
                .Include(m => m.Lecturas.Select(l => l.ValoresLeidos))
                .FirstOrDefaultAsync(m => m.Id == id)
                ;
            if (medidor == null)
            {
                return NotFound();
            }

            return Ok(medidor);
        }

        // PUT: api/Medidores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMedidor(int id, Medidor medidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medidor.Id)
            {
                return BadRequest();
            }

            db.Entry(medidor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedidorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Medidores
        [ResponseType(typeof(Medidor))]
        public async Task<IHttpActionResult> PostMedidor(Medidor medidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medidores.Add(medidor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MedidorExists(medidor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = medidor.Id }, medidor);
        }

        // DELETE: api/Medidores/5
        [ResponseType(typeof(Medidor))]
        public async Task<IHttpActionResult> DeleteMedidor(int id)
        {
            Medidor medidor = await db.Medidores.FindAsync(id);
            if (medidor == null)
            {
                return NotFound();
            }

            db.Medidores.Remove(medidor);
            await db.SaveChangesAsync();

            return Ok(medidor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedidorExists(int id)
        {
            return db.Medidores.Count(e => e.Id == id) > 0;
        }
    }
}