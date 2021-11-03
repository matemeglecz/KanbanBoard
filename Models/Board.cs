using KanbanBoardApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoardApi.Models
{
    public class Board
    {
        public Board()
        {  
        }
        
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<Card> Cards { get; set; }

    }
}
