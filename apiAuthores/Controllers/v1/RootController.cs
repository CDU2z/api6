using apiAuthores.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiAuthores.Controllers.v1
{
    [ApiController]
    [Route("Api")]
    [Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RootController : Controller
    {
        private readonly IAuthorizationService AuthorizationService;

        public RootController(IAuthorizationService authorizationService) => AuthorizationService = authorizationService;


        [HttpGet(Name = "ObtenerRoot")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DatoHATEOAS>>> Get()
        {
            var datosHATEOAS = new List<DatoHATEOAS>
            {
                new DatoHATEOAS(enlace: Url.Link("ObtenerRoot", new { }), descripcion: "self", metodo: "GET")
            };

            var esAdmin = await AuthorizationService.AuthorizeAsync(User, "esAdmin");
            if (esAdmin.Succeeded)
            {
                datosHATEOAS.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerAutores", new { }), descripcion: "ObtenerAutores", metodo: "GET"));

                datosHATEOAS.Add(new DatoHATEOAS(enlace: Url.Link("CrearAutores", new { }), descripcion: "CrearAutores", metodo: "GET"));
            }

            return datosHATEOAS;
        }
    }
}
