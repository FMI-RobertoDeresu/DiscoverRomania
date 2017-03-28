using System;

namespace JRS.DR.Models
{
    public abstract class EntityComplexBase<T> : EntityBase<T>
    {
        public DateTime Created { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public void PreInsert()
        {
            Created = DateTime.Now;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }
    }
}