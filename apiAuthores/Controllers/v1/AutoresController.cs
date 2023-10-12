using apiAuthores.DTOs;
using apiAuthores.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiAuthores.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "esAdmin")]
    public class AutoresController : Controller
    {
        private readonly AppDBContext context;
        private readonly IMapper mapper;

        public AutoresController(AppDBContext dbContext, IMapper iMapper)
        {
            context = dbContext;
            mapper = iMapper;
        }

        [HttpGet(Name = "ObtenerAutores")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.ToListAsync();
        } 

        [HttpPost(Name = "CrearAutores")]
        public async Task<ActionResult> Post(AutoresDTO autores)
        {
            bool existe = await context.Autores.AnyAsync(x => x.Nombre == autores.Nombres);
            if (!existe)
                return BadRequest($"Ya existe un autor con el nombre {autores.Nombres}");

            var newAutor = mapper.Map<Autor>(autores);

            context.Add(newAutor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut(Name = "ActualizarAutores")]
        public async Task<ActionResult> Put(Autor autor)
        {
            bool existe = await context.Autores.AnyAsync(x => x.Id == autor.Id);
            if (!existe)
                return NotFound();

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}", Name = "EliminarAutores")]
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
                return NotFound();

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}