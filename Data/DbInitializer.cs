using KanbanBoardApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoardApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(KanbanBoardContext context)
        {
            // Look for any students.
            if (context.Lanes.Any())
            {
                return;   // DB has been seeded
            }

            var boards = new Lane[]
            {
                new Lane{Title="Függőben", Order=0},
                new Lane{Title="Folyamatban", Order=1},
                new Lane{Title="Kész", Order=2},
                new Lane{Title="Elhalasztva", Order=3},
            };

            context.Lanes.AddRange(boards);
            context.SaveChanges();

            var cards = new Card[]
            {
                new Card{LaneID=1, Title="Fix 1", Description="abrakadabra", Deadline=DateTime.Parse("2020-01-21"), Order=0},
                new Card{LaneID=1, Title="Fix 2", Description="asdasdasd", Deadline=DateTime.Parse("2020-03-21"), Order=1},
                new Card{LaneID=1, Title="Fix 3", Description="ijoijn", Deadline=DateTime.Parse("2025-01-21"), Order=2},
                new Card{LaneID=1, Title="Fix 4", Description="vbcxzubvuizb", Deadline=DateTime.Parse("2040-01-21"), Order=3},
                new Card{LaneID=3, Title="Fix 5", Description="abrakadabra", Deadline=DateTime.Parse("2020-01-21"), Order=0},
                new Card{LaneID=4, Title="Fix 6", Description="njcvnjc", Deadline=DateTime.Parse("2029-01-21"), Order=0},
                new Card{LaneID=2, Title="Fix 7", Description="", Deadline=DateTime.Parse("2020-07-21"), Order=0},
                
            };

            context.Cards.AddRange(cards);
            context.SaveChanges();            
        }
    
    }
}
