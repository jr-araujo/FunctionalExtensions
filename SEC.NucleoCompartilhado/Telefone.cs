using JRA.FunctionalExtensions;
using JRA.NucleoCompartilhado.Comum;
using System;

namespace SEC.NucleoCompartilhado
{
    public sealed class Telefone : ObjetoDeValor<Telefone>
    {
        public int DDD { get; }
        public string Numero { get; }

        private Telefone(int ddd, string numero)
        {
            //if (ddd < 11 || ddd > 99)
            //    throw new InvalidOperationException("O ddd está inválido");

            //if (string.IsNullOrWhiteSpace(numero))
            //    throw new InvalidOperationException("O número é obrigatório");

            //if (numero.Length == 0 || numero.Length > 9)
            //    throw new InvalidOperationException("O número do telefone está inválido");

            Numero = numero;
            DDD = ddd;
        }

        public static DominioResultante<Telefone> Criar(int ddd, string numero)
        {
            return new DominioResultante<Telefone>()
                .GarantirQue(() => !(ddd < 11 || ddd > 99), "O DDD está inválido !")
                .GarantirQue(() => !string.IsNullOrWhiteSpace(numero), "O Número do Telefone é obrigatório !")
                .GarantirQue(() => (numero.Length > 0 || numero.Length <= 9), "O Número do Telefone está inválido !")
                .Mapear(() => new Telefone(ddd, numero));

            //return new Telefone(ddd, numero);
        }

        protected override bool EqualsCore(Telefone other)
        {
            return Numero == other.Numero
                   && DDD == other.DDD;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Numero.GetHashCode();
                hashCode = (hashCode * 397) ^ DDD.GetHashCode();

                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{DDD} {Numero}";
        }
    }
}
