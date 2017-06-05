using JRA.FunctionalExtensions;
using JRA.NucleoCompartilhado.Comum;
using System;

namespace SEC.NucleoCompartilhado
{
    public sealed class AgendaTelefonica : ObjetoDeValor<AgendaTelefonica>
    {
        public Telefone TelefonePrincipal { get; }
        public Telefone TelefoneComercial { get; }
        public Telefone Celular { get; }

        private AgendaTelefonica(Telefone telefonePrincipal,
            Telefone telefoneComercial, Telefone celular)
        {
            //if (telefonePrincipal == null)
            //    throw new InvalidOperationException("O telefone principal é obrigatório");

            //if (telefoneComercial == null)
            //    throw new InvalidOperationException("O telefone comercial é obrigatório");

            //if (celular == null)
            //    throw new InvalidOperationException("O celular é obrigatório");

            TelefonePrincipal = telefonePrincipal;
            TelefoneComercial = telefoneComercial;
            Celular = celular;
        }

        public static DominioResultante<AgendaTelefonica> Criar(Telefone telefonePrincipal,
            Telefone telefoneComercial, Telefone celular)
        {
            return new DominioResultante<AgendaTelefonica>()
                .GarantirQue(() => telefonePrincipal != null, "O Número Principal precisa ser preenchido !")
                .GarantirQue(() => telefoneComercial != null, "O Número Comercial precisa ser preenchido !")
                .GarantirQue(() => celular != null, "O Número do Celular precisa ser preenchido!")
                .Mapear(() => new AgendaTelefonica(telefonePrincipal, telefoneComercial, celular));

            //return new AgendaTelefonica(telefonePrincipal, telefoneComercial, celular);
        }

        protected override bool EqualsCore(AgendaTelefonica other)
        {
            return TelefonePrincipal == other.TelefonePrincipal
                    && TelefoneComercial == other.TelefoneComercial
                    && Celular == other.Celular;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = TelefonePrincipal.GetHashCode();
                hashCode = (hashCode * 397) ^ TelefoneComercial.GetHashCode();
                hashCode = (hashCode * 397) ^ Celular.GetHashCode();

                return hashCode;
            }
        }
    }

}
