using JRA.FunctionalExtensions;
using JRA.NucleoCompartilhado.Comum;
using System;
using System.Text.RegularExpressions;

namespace SEC.NucleoCompartilhado
{
    public sealed class Email : ObjetoDeValor<Email>
    {
        public string Valor { get; }

        private Email(string valor)
        {
            Valor = valor;
        }

        public static DominioResultante<Email> Criar(string email)
        {
            return new DominioResultante<Email>()
                .GarantirQue(() => EnderecoEmailValido(email), "Endereço de email inválido")
                .Mapear(() => new Email(email));

            //if (!(ValidarEnderecoDeEmail(email)))
            //    throw new InvalidOperationException("Endereço de email inválido");

            //return new Email(email);
        }

        protected override bool EqualsCore(Email other)
        {
            return Valor == other.Valor;
        }

        protected override int GetHashCodeCore()
        {
            return Valor.GetHashCode();
        }

        private static bool EnderecoEmailValido(string emailAddress)
        {
            string regexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Match matches = Regex.Match(emailAddress, regexPattern);
            return matches.Success;
        }
    }

}
