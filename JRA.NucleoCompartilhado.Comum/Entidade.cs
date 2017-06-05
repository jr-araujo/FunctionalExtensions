using System;

namespace JRA.NucleoCompartilhado.Comum
{
    public abstract class Entidade
    {
        public virtual Guid Id { get; protected set; }

        public override bool Equals(object entidade)
        {
            var entidadeComparacao = entidade as Entidade;

            if (ReferenceEquals(entidadeComparacao, null))
                return false;

            if (ReferenceEquals(this, entidadeComparacao))
                return true;

            if (GetType() != entidadeComparacao.GetType())
                return false;

            if (!Transitorio() &&
                !entidadeComparacao.Transitorio() &&
                Id.Equals(entidadeComparacao.Id))
                return true;

            if (Transitorio() || entidadeComparacao.Transitorio())
                return false;

            return Id.Equals(entidadeComparacao.Id);
        }

        public virtual bool Transitorio()
        {
            return Id.Equals(default(long));
        }

        public static bool operator ==(Entidade entidadeA, Entidade entidadeB)
        {
            if (ReferenceEquals(entidadeA, null) && ReferenceEquals(entidadeB, null))
                return true;

            if (ReferenceEquals(entidadeA, null) || ReferenceEquals(entidadeB, null))
                return false;

            return entidadeA.Equals(entidadeB);
        }

        public static bool operator !=(Entidade entidadeA, Entidade entidadeB)
        {
            return !(entidadeA == entidadeB);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }        
    }
}
