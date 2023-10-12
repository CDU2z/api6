using apiAuthores.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiAuthores.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : Controller
    {
        private readonly AppDBContext context;

        public LibrosController(AppDBContext dbContext) => context = dbContext;

        [HttpGet]
        [HttpGet("default")] //asi responde a las 2 rutas; y asi se puede a poner a varias rutas.
        public async Task<ActionResult<Libro>> Get()
        {
            return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> GetById(int id)
        {
            return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Libro>>> GetList()
        {
            return await context.Libros.Include(x => x.Autor).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            bool existe = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);
            if (!existe)
                return NotFound();

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Libro libro)
        {
            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Libros.AnyAsync(x => x.Id == id);
            if (!existe)
                return NotFound();

            context.Remove(new Libro() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}