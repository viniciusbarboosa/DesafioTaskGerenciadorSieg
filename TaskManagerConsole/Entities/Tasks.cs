using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities.Bases;
using TaskManagerConsole.Entities.Types;

namespace TaskManagerConsole.Entities
{
    public class Tasks : BaseEntity
    {
        public Tasks(string title,string description,DateTime dateDue,string nameCategory,string userName,StatusTask status)
        {
            
            this.Title = title;
            this.Description = description;
            this.DateDue = dateDue;
            this.NameCategory = nameCategory;
            this.NameUser = userName;
            this.Status = status;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DateDue { get; private set; }
        public DateTime DateCompletion { get; private set; }
        public StatusTask Status { get; private set; }
        public string NameCategory { get; private set; }
        public string NameUser { get; private set; }

        public void UpdateTask(string title, string description,DateTime dueDate,string nameCategory,string nameUsers,StatusTask status)
        {
            this.Title= title;
            this.Description = description;
            this.DateDue = dueDate;
            this.NameCategory = nameCategory;
            this.NameUser = nameUsers;
            this.Status = status;
        }

        public void UpdateStatus(StatusTask status)
        {
            this.Status = status;
        }

    }
}
