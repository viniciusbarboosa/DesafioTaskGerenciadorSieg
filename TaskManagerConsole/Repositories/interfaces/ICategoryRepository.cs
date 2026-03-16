using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;

namespace TaskManagerConsole.Repositories.interfaces
{
    public interface ICategoryRepository
    {
        public void CreateCategory(Category category);

        public void UpdatesCategory(List<Category> listCategory);

        public List<Category> GetCategory();
    }
}
