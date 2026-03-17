using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;
using TaskManagerConsole.Repositories.interfaces;

namespace TaskManagerConsole.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {

        public void Create(Category category)
        {
            JsonFileHelper.WriteFile(category,"categoria.json");
        }

        public void Update(List<Category> listCategory)
        {
            JsonFileHelper.UpdateFile(listCategory, "categoria.json");
        }

        public List<Category> Get()
        {
            var categorys = JsonFileHelper.GetFile<Category>("categoria.json");
            return categorys;
        }

    }

}
