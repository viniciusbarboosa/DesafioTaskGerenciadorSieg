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
            
            Title = title;
            Description = description;
            DateDue = dateDue;
            NameCategory = nameCategory;
            NameUser = userName;
            Status = status;
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
            Title= title;
            Description = description;
            DateDue = dueDate;
            NameCategory = nameCategory;
            NameUser = nameUsers;
            Status = status;
        }

        public void UpdateStatus(StatusTask status)
        {
            Status = status;
        }

    }
}
