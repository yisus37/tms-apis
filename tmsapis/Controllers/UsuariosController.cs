
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Commons.Models.Generico;
using Commons.ViewModels;
using Commons.Models.Administracion;
using System.Text.RegularExpressions;
using Commons.Helpers;
using Commons.Models.Utilites;

namespace Commons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : TMSControllerBase
    {
        public UsuariosController(adminDBContext contextAdmin, genericoDBContext contextGenerico, IHttpContextAccessor contextAccessor, IConfiguration configuration, SingletonClientes singletonClientes, IHostEnvironment environment) : base(contextAdmin, contextGenerico, contextAccessor, configuration, singletonClientes, environment)
        { }

        // GET: api/Usuarios
        [HttpGet]
        public ActionResult<object> GetUsuarios()
        {
            var paies = _contextGenerico.Pais.ToList();
            return Ok(paies);
        }
    
    }
}
