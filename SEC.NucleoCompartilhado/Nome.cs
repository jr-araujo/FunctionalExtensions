using JRA.FunctionalExtensions;
using JRA.NucleoCompartilhado.Comum;
using System;

namespace SEC.NucleoCompartilhado
{
    public sealed class Nome : ObjetoDeValor<Nome>
    {
        public string PrimeiroNome { get; }
        public string Sobrenome { get; }

        private Nome(string primeiroNome, string sobreNome)
        {
            //if (string.IsNullOrWhiteSpace(primeiroNome))
            //    throw new InvalidOperationException("O Primeiro Nome é obrigatório");

            //if (string.IsNullOrWhiteSpace(sobreNome))
            //    throw new InvalidOperationException("O Sobrenome é obrigatório");

            PrimeiroNome = primeiroNome;
            Sobrenome = sobreNome;
        }

        public static DominioResultante<Nome> Criar(string primeiroNome, string sobreNome)
        {
            return new DominioResultante<Nome>()
                .GarantirQue(() => !string.IsNullOrWhiteSpace(primeiroNome), "O Primeiro Nome é obrigatório")
                .GarantirQue(() => !string.IsNullOrWhiteSpace(sobreNome), "O Sobrenome é obrigatório")
                .Mapear(() => new Nome(primeiroNome, sobreNome));
        }

        protected override bool EqualsCore(Nome other)
        {
            return PrimeiroNome == other.PrimeiroNome &&
                Sobrenome == other.Sobrenome;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = PrimeiroNome.GetHashCode();
                hashCode = (hashCode * 397) ^ Sobrenome.GetHashCode();

                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{PrimeiroNome} {Sobrenome}";
        }
    }
}
