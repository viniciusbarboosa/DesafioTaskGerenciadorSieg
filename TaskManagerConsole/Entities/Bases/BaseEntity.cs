using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerConsole.Entities.Bases
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            DateCreation = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime DateCreation { get; private set; }
        
    }
}
