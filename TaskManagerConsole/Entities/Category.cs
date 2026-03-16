using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities.Bases;

namespace TaskManagerConsole.Entities
{
    public class Category : BaseEntity
    {
        public Category(string name,string color)
        {
            this.Name = name;
            this.Color = color;
        }
        
        public string Name { get; private set; }
        public string Color {  get; private set; }
    }
}
