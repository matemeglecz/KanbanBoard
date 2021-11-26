using KanbanBoardApi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoardApi.Data
{
    public class Lane
    {
        public Lane()
        {  
        }

        public Lane(string title)
        {
            Title = title;
        }

        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Order { get; set; }
        public ICollection<Card> Cards { get; set; }

    }
}
