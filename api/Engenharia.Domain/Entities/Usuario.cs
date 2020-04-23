using System;

namespace Engenharia.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NomeAbreviado { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }
        public string Setor { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string AlteradoPor { get; set; }

        //Foreign key Empresa Standard
        public int empresaId { get; set; }
        //public Empresa Empresa { get; set; }
    }
}
