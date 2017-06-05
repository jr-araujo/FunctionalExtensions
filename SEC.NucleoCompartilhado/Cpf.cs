using JRA.FunctionalExtensions;
using JRA.NucleoCompartilhado.Comum;
using System;

namespace SEC.NucleoCompartilhado
{
    public class Cpf : ObjetoDeValor<Cpf>
    {
        public string Numero { get; }

        private Cpf(string numero)
        {
            Numero = numero;
        }

        public static DominioResultante<Cpf> Criar(string numeroDocumento)
        {
            return new DominioResultante<Cpf>()
                .GarantirQue(() => DocumentoValido(numeroDocumento), "CPF está inválido !")
                .Mapear(() => new Cpf(numeroDocumento));

            //if (DocumentoValido(numeroDocumento))
            //    return new Cpf(numeroDocumento);

            //throw new InvalidOperationException("Documento do CPF está inválido !");
        }

        private static bool DocumentoValido(string numeroDocumento)
        {
            return true;
        }

        protected override bool EqualsCore(Cpf other)
        {
            return Numero == other.Numero;
        }

        protected override int GetHashCodeCore()
        {
            return Numero.GetHashCode();
        }
    }

}
