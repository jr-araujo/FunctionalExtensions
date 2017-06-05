using JRA.FunctionalExtensions;
using JRA.NucleoCompartilhado.Comum;

namespace SEC.NucleoCompartilhado
{
    public sealed class Endereco : ObjetoDeValor<Endereco>
    {
        public string Logradouro { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Estado { get; }
        public int Cep { get; }

        private Endereco(string logradouro, string bairro, string cidade, string estado, int cep)
        {
            //if (cep < 01000000 || cep > 99999999)
            //    throw new InvalidOperationException("Cep inválido");

            //if (string.IsNullOrWhiteSpace(logradouro))
            //    throw new InvalidOperationException("O logradouro é obrigatório");

            //if (string.IsNullOrWhiteSpace(bairro))
            //    throw new InvalidOperationException("O bairro é obrigatório");

            //if (string.IsNullOrWhiteSpace(cidade))
            //    throw new InvalidOperationException("A cidade é obrigatória");

            //if (string.IsNullOrWhiteSpace(estado))
            //    throw new InvalidOperationException("O estado é obrigatório");

            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
        }

        public static DominioResultante<Endereco> Criar(string logradouro, string bairro, string cidade, string estado,
            int cep)
        {
            //return new Endereco(logradouro, bairro, cidade, estado, cep);

            return new DominioResultante<Endereco>()
                .GarantirQue(() => !(cep < 01000000 || cep > 99999999), "Cep inválido")
                .GarantirQue(() => !string.IsNullOrWhiteSpace(logradouro), "O Logradouro é obrigatório")
                .GarantirQue(() => !string.IsNullOrWhiteSpace(bairro), "O Bairro é obrigatório")
                .GarantirQue(() => !string.IsNullOrWhiteSpace(cidade), "A Cidade é obrigatório")
                .GarantirQue(() => !string.IsNullOrWhiteSpace(estado), "O Estado é obrigatório")
                .Mapear(() => new Endereco(logradouro, bairro, cidade, estado, cep));            
        }

        protected override bool EqualsCore(Endereco other)
        {
            return Logradouro == other.Logradouro
                    && Bairro == other.Bairro
                    && Cidade == other.Cidade
                    && Estado == other.Estado
                    && Cep == other.Cep;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Logradouro.GetHashCode();
                hashCode = (hashCode * 397) ^ Bairro.GetHashCode();
                hashCode = (hashCode * 397) ^ Cidade.GetHashCode();
                hashCode = (hashCode * 397) ^ Estado.GetHashCode();
                hashCode = (hashCode * 397) ^ Cep.GetHashCode();

                return hashCode;
            }
        }
    }
}
