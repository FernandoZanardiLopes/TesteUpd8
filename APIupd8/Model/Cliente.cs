using System;
using System.ComponentModel.DataAnnotations;

namespace APIupd8.Model
{
    public class Cliente
    {
        [Key]
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DTNascimento { get; set; }
        public string Sexo { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
    }
}
