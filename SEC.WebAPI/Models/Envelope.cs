using System;
using System.Runtime.Serialization;

namespace SEC.WebAPI.Models
{
    public class EnvelopeRetorno<T> : ISerializable
    {
        public T Resultado { get; }
        public bool Sucesso => string.IsNullOrWhiteSpace(MensagemDeErro);
        public string MensagemDeErro { get; }
        public DateTime OcorreuEm { get; }

        protected internal EnvelopeRetorno(T resultado, string mensagemDeErro)
        {
            Resultado = resultado;
            MensagemDeErro = mensagemDeErro;
            OcorreuEm = DateTime.UtcNow;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Sucesso", Sucesso);

            if (!Sucesso)
            {
                info.AddValue("MensagemDeErro", MensagemDeErro);
                info.AddValue("OcorreuEm", OcorreuEm);
            }
            else
            {
                info.AddValue("Resultado", Resultado);
            }
        }
    }

    public class EnvelopeRetorno : EnvelopeRetorno<string>
    {
        public EnvelopeRetorno(string mensagemDeErro)
            : base(string.Empty, mensagemDeErro)
        {
        }

        public static EnvelopeRetorno<T> Ok<T>(T result)
        {
            return new EnvelopeRetorno<T>(result, string.Empty);
        }

        public static EnvelopeRetorno Ok()
        {
            return new EnvelopeRetorno(string.Empty);
        }

        public static EnvelopeRetorno Erro(string mensagemDeErro)
        {
            return new EnvelopeRetorno(mensagemDeErro);
        }
    }
}