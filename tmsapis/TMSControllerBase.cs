using Commons.Helpers;
using Commons.Models;
using Commons.Models.Administracion;
using Commons.Models.Generico;
using Commons.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Net.Http.Headers;
using System.Text;

namespace Commons
{
    public class TMSControllerBase : ControllerBase
    {
        protected readonly adminDBContext _contextAdmin;
        protected readonly genericoDBContext _contextGenerico;
        private HttpContext _httpContext;
        protected IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        //
        protected Logger _logger;
        private adminDBContext contextAdmin;
        private genericoDBContext contextGenerico;
        private IHttpContextAccessor contextAccessor;
        private SingletonClientes _singletonClientes;
        private IConfiguration configuration;
        private SingletonClientes singletonClientes;

        public TMSControllerBase(adminDBContext contextAdmin, genericoDBContext contextGenerico,
            IHttpContextAccessor contextAccessor, IConfiguration configuration, SingletonClientes singletonClientes, 
            IHostEnvironment environment) : base()
        {
            _configuration = configuration;
            _contextAdmin = contextAdmin;
            _contextGenerico = contextGenerico;
            _httpContext = contextAccessor.HttpContext;
            _singletonClientes = singletonClientes;
            _environment = environment;
            Console.WriteLine("Entorno base: " + _environment.EnvironmentName);
            if (_httpContext != null)
            {
                //if (_environment.IsDevelopment())
                {
                    if (_httpContext.Request.Headers.TryGetValue("tenant", out var tenantId))
                    {
                        //string typeMethod = _httpContext.Request.Method;
                        string nombre = ObtenerContexto(tenantId);
                        _logger = Logger.InstanciarLoggerAsync("SCAF-APIS-" + nombre, _configuration).GetAwaiter().GetResult();
                    }
                    else
                    {
                        throw new Exception("Invalid Tenant!");
                    }
                }
                //else
                //{
                //    if (_httpContext.Request.Headers.TryGetValue("Authorization", out var token) &&
                //    _httpContext.Request.Headers.TryGetValue("user", out var user)
                //    )
                //    {
                //        GetTenant(token, user).GetAwaiter().GetResult();
                //    }
                //    else
                //    {
                //        throw new Exception("Invalid Tenant!");
                //    }
                //}

            }
        }

        public TMSControllerBase(adminDBContext contextAdmin, genericoDBContext contextGenerico, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            this.contextAdmin = contextAdmin;
            this.contextGenerico = contextGenerico;
            this.contextAccessor = contextAccessor;
            this.configuration = configuration;
        }

        public TMSControllerBase(adminDBContext contextAdmin, genericoDBContext contextGenerico, IHttpContextAccessor contextAccessor)
        {
            this.contextAdmin = contextAdmin;
            this.contextGenerico = contextGenerico;
            this.contextAccessor = contextAccessor;
        }

        public TMSControllerBase(adminDBContext contextAdmin, genericoDBContext contextGenerico, IHttpContextAccessor contextAccessor, IConfiguration configuration, SingletonClientes singletonClientes) : this(contextAdmin, contextGenerico, contextAccessor, configuration)
        {
            this.singletonClientes = singletonClientes;
        }

        private string ObtenerContexto(string tenant)
        {
            //try
            //{
            //    var cliente = _singletonClientes.GetClientesSingleton(tenant);
            //    if (cliente != null)
            //    {
            //        _contextGenerico.Database.SetConnectionString(cliente.CadenaConexion);
            //        return cliente.Nombre;
            //    }
            //    else
            //    {
            //        return String.Empty;
            //        throw new Exception("Error tenant no found");
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw new Exception("Error connection string: ");
            //}
            return "ok";
        }

        private async Task GetTenant(string token,string user)
        {
            using (var client = new HttpClient())
            {

                try
                {
                    string url = _configuration["Keycloak:auth-server-url"];
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("accept", "*/*");
                    client.DefaultRequestHeaders.Add("Authorization",token);
                    string metodo = "admin/realms/SCAF/users/?username=" + user;
                    HttpResponseMessage Res = await client.GetAsync(metodo);
                    var result = await Res.Content.ReadAsStringAsync();
                    if (Res.IsSuccessStatusCode)
                    {
                        var respuesta = JsonConvert.DeserializeObject<UserInfoViewModel>(Res.Content.ReadAsStringAsync().Result);                        
                        string nombre = ObtenerContexto(respuesta.attributes.tenant.FirstOrDefault());
                        _logger = Logger.InstanciarLoggerAsync("SCAF-APIS-" + nombre, _configuration).GetAwaiter().GetResult();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
}
