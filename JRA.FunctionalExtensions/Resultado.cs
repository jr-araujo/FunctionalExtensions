using System;
using System.Collections.Generic;
using System.Linq;

namespace JRA.FunctionalExtensions
{
    internal sealed class DominioResultanteBase
    {
        public bool Falha { get; }
        public bool Sucesso => !Falha;

        private readonly string _mensagemDeErro;

        public string MensagemDeErro
        {
            get
            {
                if (Sucesso)
                    throw new InvalidOperationException("Não deve existir Mensagem de Erro para operações de Sucesso.");

                return _mensagemDeErro;
            }
        }

        public DominioResultanteBase(bool falha, string mensagemDeErro)
        {
            if (falha)
            {
                if (string.IsNullOrEmpty(mensagemDeErro))
                    throw new ArgumentNullException(nameof(MensagemDeErro), "Deve existir uma Mensagem de Erro para operações de Erro.");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(mensagemDeErro))
                    throw new ArgumentException("Não deve existir Mensagem de Erro para operações de Sucesso.", nameof(MensagemDeErro));
            }

            Falha = falha;
            _mensagemDeErro = mensagemDeErro;
        }
    }

    public struct DominioResultante
    {
        private static readonly DominioResultante OkResult = new DominioResultante(false, null);
        
        private readonly DominioResultanteBase _resultadoBase;

        public bool Falhou => _resultadoBase.Falha;
        public bool Sucesso => _resultadoBase.Sucesso;
        public string MensagemDeErro => _resultadoBase.MensagemDeErro;

        private DominioResultante(bool falha, string mensagemDeErro)
        {
            _resultadoBase = new DominioResultanteBase(falha, mensagemDeErro);
        }

        public static DominioResultante Ok()
        {
            return OkResult;
        }

        public static DominioResultante RegistrarFalha(string error)
        {
            return new DominioResultante(true, error);
        }

        public static DominioResultante<T> Ok<T>(T value)
        {
            return new DominioResultante<T>(false, value, null);
        }

        public static DominioResultante<T> Falhar<T>(string error)
        {
            return new DominioResultante<T>(true, default(T), error);
        }

        public static DominioResultante ObterPrimeiraFalhaOuSucesso(params DominioResultante[] results)
        {
            foreach (DominioResultante result in results)
            {
                if (result.Falhou)
                    return RegistrarFalha(result.MensagemDeErro);
            }

            return Ok();
        }
       
        public static DominioResultante CombinarDominios(string errorMessagesSeparator,
            params DominioResultante[] results)
        {
            List<DominioResultante> failedResults = results.Where(x => x.Falhou).ToList();

            if (!failedResults.Any())
                return Ok();

            string errorMessage = string.Join(errorMessagesSeparator, failedResults.Select(x => x.MensagemDeErro).ToArray());

            return RegistrarFalha(errorMessage);
        }

        public static DominioResultante CombinarDominios(params DominioResultante[] results)
        {
            return CombinarDominios(", ", results);
        }

        public static DominioResultante CombinarDominios<T>(params DominioResultante<T>[] results)
        {
            return CombinarDominios(", ", results);
        }

        public static DominioResultante CombinarDominios<T>(string errorMessagesSeparator, params DominioResultante<T>[] results)
        {
            DominioResultante[] untyped = results.Select(result => (DominioResultante)result).ToArray();
            return CombinarDominios(errorMessagesSeparator, untyped);
        }
    }

    public class DominioResultante<T>
    {
        private readonly DominioResultanteBase _resultadoBase;
        
        public bool Sucesso => _resultadoBase.Sucesso;
        public string MensagemDeErro => _resultadoBase.MensagemDeErro;
        public T Objeto { get; private set; }

        public DominioResultante()
        {
            _resultadoBase = new DominioResultanteBase(false, "");
        }

        internal DominioResultante(bool temFalha, T objeto, string mensagemDeErro)
        {
            _resultadoBase = new DominioResultanteBase(temFalha, mensagemDeErro);
            Objeto = objeto;
        }

        private DominioResultante<T> RegistrarFalha(string mensagemDeErro)
        {
            return new DominioResultante<T>(true, default(T), mensagemDeErro);
        }

        private DominioResultante<T> RegistrarSucesso(T value)
        {
            return new DominioResultante<T>(false, value, "");
        }

        public DominioResultante<T> GarantirQue(Func<bool> predicate, string mensagemDeErro)
        {
            if (!predicate())
                return RegistrarFalha(mensagemDeErro);

            return this;
        }
        
        public DominioResultante<T> Mapear(Func<T> map)
        {
            if (Sucesso)
                Objeto = map();

            return this;
        }

        public static implicit operator DominioResultante(DominioResultante<T> resultado)
        {
            if (resultado.Sucesso)
                return DominioResultante.Ok();
            else
                return DominioResultante.RegistrarFalha(resultado.MensagemDeErro);
        }
    }
}