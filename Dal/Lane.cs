using KanbanBoardApi.Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoardApi.Dal
{
    public class Lane
    {
        public Lane()
        {  
        }
        
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Order { get; set; }
        public ICollection<Card> Cards { get; set; }

    }
}
