namespace SEC.WebAPI.Models
{
    public class ClienteViewModel
    {
        public string PrimeiroNome { get; set; }
        public string SobreNome { get; set; }
        public string Cpf { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int Cep { get; set; }
        public string EmailPrincipal { get; set; }
        public int DDDTelefoneResidencial { get; set; }
        public string TelefoneResidencial { get; set; }
        public int DDDCelular { get; set; }
        public string Celular { get; set; }
        public int DDDTelefoneComercial { get; set; }
        public string TelefoneComercial { get; set; }
    }
}