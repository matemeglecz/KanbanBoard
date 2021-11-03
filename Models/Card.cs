using KanbanBoardApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KanbanBoardApi.Models
{
    public class Card
    {
        public int ID { get; set; }
        [Required]
        public int BoardID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int Order { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }
        
        [JsonIgnore]
        public Board Board { get; set; }
    }
}
