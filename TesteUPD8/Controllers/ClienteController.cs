using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TesteUPD8.DataBase;
using TesteUPD8.Models;

namespace TesteUPD8.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public void DadosAlterar(Cliente cliente)
        {
            Atualizar(cliente);
        }
        public IActionResult Atualizar(Cliente cliente)
        {
            using (var _context = new Context())
            {
                ViewBag.Clientes = _context.Cliente.Where(c => c.CPF == cliente.CPF).First();
            }
            return View();
        }
        public IActionResult Consultar(Cliente cliente)
        {
            if (! string.IsNullOrEmpty(cliente.CPF)|| !string.IsNullOrEmpty(cliente.Nome) || !string.IsNullOrEmpty(cliente.Estado )|| !string.IsNullOrEmpty(cliente.Cidade) || !string.IsNullOrEmpty(cliente.Endereco ) || !string.IsNullOrEmpty(cliente.DTNascimento ) || !string.IsNullOrEmpty(cliente.Sexo))
            {
                GetPartial(cliente);
            }
            else
            {
                GetAll();
            }

            return View();
        }
        public async void AtualizarCliente(Cliente cliente)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(cliente);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(GetURLApi("Update"), content);
            var responseContent = await response.Content.ReadAsStringAsync();
        } 

        [HttpPost]
        public void Excluir(Cliente cli)
        {
            using (var _context = new Context())
            {
                var cliente = _context.Cliente.Where(c => c.CPF == cli.CPF).First();
                _context.Cliente.Remove(cliente);
                _context.SaveChanges();
            }
        }
        private void GetAll()
        {
            try
            {
                using (var _context = new Context())
                {
                    ViewBag.Clientes = _context.Cliente.ToList();
                }
            }
            catch { }
        }

        private void GetPartial(Cliente cliente)
        {
            try
            {
                using (var _context = new Context())
                {
                    ViewBag.Clientes = _context.Cliente.Where(c => (c.CPF == cliente.CPF || c.Nome == cliente.Nome || c.Cidade == cliente.Cidade || c.Estado == cliente.Estado || c.DTNascimento == cliente.DTNascimento || c.Sexo == cliente.Sexo)).ToList();
                }
            }
            catch { }
        }

        public async void CadastrarCliente(Cliente cliente)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(cliente);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(GetURLApi("Insert"), content);
            var responseContent = await response.Content.ReadAsStringAsync();

        }

        private string GetURLApi(string func)
        {
            switch (func)
            {
                case "Update":
                    return "https://localhost:44344/api/Cliente/Update";
                case "Insert":
                    return "https://localhost:44344/api/Cliente/Insert";
                default:
                    return "https://localhost:44344/api/Cliente";
            }
        }
    }
}
