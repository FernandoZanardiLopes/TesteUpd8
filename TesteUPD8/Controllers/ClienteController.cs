using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TesteUPD8.DataBase;
using TesteUPD8.Models;
using RestSharp;
using System;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Threading;

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
            GetEstadosAsync();
            Thread.Sleep(5000);
            return View();
        }

        //[HttpPost]
        //public IActionResult Atualizar(Cliente cliente)
        //{
        //    if (cliente.CPF != null)
        //    {
        //        using (var _context = new Context())
        //        {
        //            ViewBag.Clientes = _context.Cliente.Where(c => c.CPF == cliente.CPF).First();
        //        }
        //    }
        //    return View();
        //}

        public IActionResult Consultar(Cliente cliente)
        {
            GetEstadosAsync();
            if (!string.IsNullOrEmpty(cliente.CPF) || !string.IsNullOrEmpty(cliente.Nome) || !string.IsNullOrEmpty(cliente.Estado) || !string.IsNullOrEmpty(cliente.Cidade) || !string.IsNullOrEmpty(cliente.Endereco) || !string.IsNullOrEmpty(cliente.DTNascimento) || !string.IsNullOrEmpty(cliente.Sexo))
            {
                GetPartial(cliente);
            }
            else
            {
                GetAll();
            }
            Thread.Sleep(5000);
            return View();
        }


        [HttpPost]
        public async void AtualizarCliente(Cliente cliente)
        {
            GetEstadosAsync();
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

        [HttpPost]
        public List<Cliente> GetCliente(Cliente cliente)
        {
            List<Cliente> list = new List<Cliente>();
            try
            {
                using (var _context = new Context())
                {
                    list = _context.Cliente.Where(c => (c.CPF == cliente.CPF || c.Nome == cliente.Nome || c.Cidade == cliente.Cidade || c.Estado == cliente.Estado || c.DTNascimento == cliente.DTNascimento || c.Sexo == cliente.Sexo)).ToList();
                    
                }
            }
            catch { }
            return list;
        }
        private void GetPartial(Cliente cliente)
        {
            try
            {
                using (var _context = new Context())
                {
                   
                    ViewBag.Clientes = _context.Cliente.Where(c => 
                    (c.CPF == (cliente.CPF  ?? "")||
                    c.Nome == (cliente.Nome ?? "") || 
                    c.Cidade == (cliente.Cidade ?? "") ||
                    c.Estado == (cliente.Estado ?? "") ||
                    c.DTNascimento == (cliente.DTNascimento ?? "") ||
                    c.Sexo == (cliente.Sexo ?? ""))).ToList();
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

        public async Task GetEstadosAsync()
        {
            using (var client = new HttpClient())
            {
                //var estados = ObterEstados();
                var states = new List<Estados>();

                var response = await client.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados");

                string apiResponse = await response.Content.ReadAsStringAsync();

                var estados = JsonConvert.DeserializeObject<Estados[]>(apiResponse);


                foreach (var item in estados)
                {
                    states.Add(new Estados
                    {
                        Sigla = item.Sigla.ToString(),
                        Nome = item.Nome.ToString()
                    });
                }
                ViewBag.Estados = states;
            }
        }

        //public List<Estados> ObterEstados()
        //{
        //    var client = new RestClient("https://servicodados.ibge.gov.br/api/v1/localidades/estados");
        //    var request = new RestRequest(Method.Get.ToString());

        //    var response = client.Execute<List<Estados>>(request);

        //    if (response.IsSuccessful)
        //    {
        //        return response.Data;
        //    }
        //    else
        //    {
        //        throw new Exception($"Falha ao obter estados: {response.ErrorMessage}");
        //    }
        //}

    }
}
