namespace CSP
{
    public interface IVariable<T> where T : IComparable<T>
    {
        T value { get; set; }
        int ID { get; set; }

        List<T> Domain { get; set; }
    }
    public class Variable<T> : IVariable<T> where T : IComparable<T>
    {
        public T value { get; set; }
        public int ID { get; set; }
        public List<T> Domain { get; set; }

        public Variable(T value, int iD, List<T> domain)
        {
            this.value = value;
            ID = iD;
            Domain = domain;
        }
    }
}
