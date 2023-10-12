using apiAuthores.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apiAuthores.Controllers.v2
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SignInManager<IdentityUser> SignInManager;

        public CuentasController(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            Configuration = configuration;
            SignInManager = signInManager;
        }

        private RespuestaDTO ConstruirToken(CredencialesDTO credenciales)
        {
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["llavejwt"]));

            var expires = DateTime.UtcNow.AddDays(1);
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>() { new Claim("email", credenciales.Email) };
            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims, expires: expires, signingCredentials: creds);

            return new RespuestaDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expires
            };
        }

        [HttpGet]
        public ActionResult<RespuestaDTO> RenovarToken()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            return ConstruirToken(new CredencialesDTO() { Email = email });
        }

        [HttpPut("HacerAdmin")]
        public async Task<ActionResult> HecerAdmin(EditarAdminDTO editarAdmin)
        {
            var usuario = await UserManager.FindByEmailAsync(editarAdmin.Email);
            await UserManager.AddClaimAsync(usuario, new Claim("esAdmin", "1"));

            return NoContent();
        }

        [HttpPut("RemoverAdmin")]
        public async Task<ActionResult> RemoverAdmin(EditarAdminDTO editarAdmin)
        {
            var usuario = await UserManager.FindByEmailAsync(editarAdmin.Email);
            await UserManager.RemoveClaimAsync(usuario, new Claim("esAdmin", "1"));

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<RespuestaDTO>> Registrar(CredencialesDTO credenciales)
        {
            var usuario = new IdentityUser
            {
                UserName = credenciales.Email,
                Email = credenciales.Email
            };

            var result = await UserManager.CreateAsync(usuario, credenciales.Password);

            return result.Succeeded ? ConstruirToken(credenciales) : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaDTO>> Login(CredencialesDTO credenciales)
        {
            var result = await SignInManager.PasswordSignInAsync(credenciales.Email, credenciales.Password, isPersistent: false, lockoutOnFailure: false);

            return result.Succeeded ? ConstruirToken(credenciales) : BadRequest("Login incorrecto");
        }
    }
}
