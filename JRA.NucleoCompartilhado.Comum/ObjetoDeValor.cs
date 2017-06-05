namespace JRA.NucleoCompartilhado.Comum
{
    public abstract class ObjetoDeValor<T>
        where T : ObjetoDeValor<T>
    {
        public override bool Equals(object objetoDeValor)
        {
            var objDeValor = objetoDeValor as T;

            if (ReferenceEquals(objDeValor, null))
                return false;

            return EqualsCore(objDeValor);
        }

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ObjetoDeValor<T> objetoA,
            ObjetoDeValor<T> objetoB)
        {
            if (ReferenceEquals(objetoA, null) && ReferenceEquals(objetoB, null))
                return true;

            if (ReferenceEquals(objetoA, null) || ReferenceEquals(objetoB, null))
                return false;

            return objetoA.Equals(objetoB);
        }

        public static bool operator !=(ObjetoDeValor<T> objetoA,
            ObjetoDeValor<T> objetoB)
        {
            return !(objetoA == objetoB);
        }
    }
}
