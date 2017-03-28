namespace JRS.DR.Models
{
    public abstract class EntityBase<T>
    {
        public T Id { get; protected set; }
    }
}