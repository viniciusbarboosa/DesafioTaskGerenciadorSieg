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
    public class CategoryRepository : ICategoryRepository
    {

        public void CreateCategory(Category category)
        {
            JsonFileHelper.WriteFile(category);
        }

        public void UpdatesCategory(List<Category> listCategory)
        {
            JsonFileHelper.UpdateFile(listCategory);
        }

        public List<Category> GetCategory()
        {
            var categorys = JsonFileHelper.GetCategoriesFile();
            return categorys;
        }

    }

}
