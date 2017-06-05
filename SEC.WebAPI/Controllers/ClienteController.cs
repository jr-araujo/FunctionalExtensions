using JRA.FunctionalExtensions;
using SEC.Cadastro;
using SEC.NucleoCompartilhado;
using SEC.WebAPI.Models;
using System;
using System.Net.Http;
using System.Web.Http;

namespace SEC.WebAPI.Controllers
{
    [RoutePrefix("api/cliente")]
    public class ClienteController : Controller
    {
        public HttpResponseMessage Post([FromBody] ClienteViewModel viewModel)
        {
            var nomeCliente = Nome.Criar(viewModel.PrimeiroNome, viewModel.SobreNome);
            var emailCliente = Email.Criar(viewModel.EmailPrincipal);
            var enderecoCliente = Endereco.Criar(viewModel.Logradouro, viewModel.Bairro,
                viewModel.Cidade, viewModel.Estado, viewModel.Cep);
            var cpfCliente = Cpf.Criar(viewModel.Cpf);

            var telefoneResidencialCliente = Telefone.Criar(viewModel.DDDTelefoneResidencial, 
                viewModel.TelefoneResidencial);
            var celularCliente = Telefone.Criar(viewModel.DDDCelular, viewModel.Celular);
            var telefoneComercial = Telefone.Criar(viewModel.DDDTelefoneResidencial,
                viewModel.TelefoneComercial);

            DominioResultante resultante = DominioResultante.CombinarDominios(nomeCliente,
                emailCliente, enderecoCliente, cpfCliente, telefoneResidencialCliente,
                telefoneComercial, celularCliente);

            if (resultante.Falhou)
                return Erro(resultante.MensagemDeErro);

            var agendaContatos = AgendaTelefonica.Criar(telefoneResidencialCliente.Objeto,
                celularCliente.Objeto, telefoneComercial.Objeto);

            var cliente = new Cliente(Guid.NewGuid(), nomeCliente.Objeto, emailCliente.Objeto,
                enderecoCliente.Objeto, cpfCliente.Objeto, agendaContatos.Objeto);

            //INTERAÇÃO COM BANCO DE DADOS.

            return Ok();
        }
    }
}
