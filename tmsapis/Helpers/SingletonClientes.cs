using Commons.Models.Administracion;
using Commons.Models.Generico;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace Commons.Helpers
{
    public class SingletonClientes
    {
        private readonly adminDBContext _context;
        //private readonly ConcurrentDictionary<string,Cliente> _clienteSingleton = new();
        private DateTime ultimaModificacion;
        private readonly TimeSpan minIntervaloRefresco = TimeSpan.FromHours(12);
        private object _mutex = new object();
        private bool isRefresh = false;

        public SingletonClientes(adminDBContext context)
        {
            //_context = context;
            //var clientes = GetClientes().GetAwaiter().GetResult();

            //foreach(var i in clientes)
            //{
            //    _clienteSingleton.TryAdd(i.Uiid.ToString(), i);
            //}
            //ultimaModificacion = DateTime.UtcNow;

        }
  

        //public async Task<IEnumerable<Cliente>> GetClientes()
        //{
        //    return await _context.Clientes.Where(c => c.Estatus == true).ToListAsync();
        //}

        //public async Task<Cliente> GetCliente(string uiid)
        //{
        //    return await _context.Clientes.FirstOrDefaultAsync(c => c.Estatus == true && c.Uiid.Equals(uiid));
        //}

        //public Cliente GetClientesSingleton(string uiid)
        //{
        //    Cliente cliente = null;
        //    bool forzarRefresh = DateTime.UtcNow - ultimaModificacion > minIntervaloRefresco;
        //    var existeCliente = _clienteSingleton.TryGetValue(uiid,out cliente);
        //    if (!existeCliente)
        //    {
        //        var nuevoCliente = GetCliente(uiid).GetAwaiter().GetResult();

        //        if(nuevoCliente!= null)
        //            _clienteSingleton.TryAdd(uiid,nuevoCliente);

        //        return nuevoCliente;
        //    }
        //    if (forzarRefresh)
        //    {
        //        if (!isRefresh)
        //        {
        //            lock (_mutex)
        //            {
        //                if (isRefresh)
        //                {
        //                    _clienteSingleton.Clear();
        //                    var clientes = GetClientes().GetAwaiter().GetResult();
        //                    foreach (var i in clientes)
        //                    {
        //                        _clienteSingleton.TryAdd(i.Uiid.ToString(), i);
        //                    }
        //                    isRefresh = true;
        //                }
        //                else
        //                {
        //                    isRefresh = false;
        //                }

        //            }
        //        }   

        //    }
        //    return cliente;
        //}
    }
}
