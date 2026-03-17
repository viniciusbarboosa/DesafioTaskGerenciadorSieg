using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories.interfaces
{
    public interface IRepository<T>
    {
        public void Create(T typeRepositoty);

        public void Update(List<T> typeRepositoty);

        public List<T> Get();
    }
}
