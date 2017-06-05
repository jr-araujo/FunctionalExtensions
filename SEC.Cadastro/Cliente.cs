using JRA.NucleoCompartilhado.Comum;
using SEC.NucleoCompartilhado;
using System;

namespace SEC.Cadastro
{
    public class Cliente : Entidade
    {
        public Nome Nome { get; }
        public Email EmailPrincipal { get; }
        public Endereco Endereco { get; }
        public Cpf Cpf { get; }
        public AgendaTelefonica AgendaContatos { get; }

        public Cliente(Guid id, Nome nome, Email emailPrincipal, Endereco endereco,
            Cpf cpf, AgendaTelefonica agendaContatos)
        {
            if (nome == null)
                throw new ArgumentNullException("O Nome do Cliente é Obrigatório");

            if (emailPrincipal == null)
                throw new ArgumentNullException("O Email Principal do Cliente é Obrigatório");

            if (endereco == null)
                throw new ArgumentNullException("O Endereço do Cliente é Obrigatório");

            if (cpf == null)
                throw new ArgumentNullException("O CPF do Cliente é Obrigatório");

            if (agendaContatos == null)
                throw new ArgumentNullException("A Agenda de Contatos do Cliente é Obrigatório");

            Id = id;
            Nome = nome;
            EmailPrincipal = emailPrincipal;
            Endereco = endereco;
            Cpf = cpf;
            AgendaContatos = agendaContatos;
        }
    }
}