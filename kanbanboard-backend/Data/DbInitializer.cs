using System;
using System.Globalization;
using System.Linq;

namespace KanbanBoardApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(KanbanBoardContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
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

            /*var cards = new Card[]
            {
                new Card{LaneID=1, Title="Fix 1", Description="megjavítani a kódot", Deadline=DateTime.Parse("2020-01-21", CultureInfo.CurrentCulture), Order=0},
                new Card{LaneID=1, Title="Fix 2", Description="rossz minden", Deadline=DateTime.Parse("2020-03-21", CultureInfo.CurrentCulture), Order=1},
                new Card{LaneID=1, Title="Fix 3", Description="help", Deadline=DateTime.Parse("2025-01-21", CultureInfo.CurrentCulture), Order=2},
                new Card{LaneID=1, Title="Fix 4", Description="valami", Deadline=DateTime.Parse("2040-01-21", CultureInfo.CurrentCulture), Order=3},
                new Card{LaneID=3, Title="Fix 5", Description="", Deadline=DateTime.Parse("2020-01-21", CultureInfo.CurrentCulture), Order=0},
                new Card{LaneID=4, Title="Fix 6", Description="", Deadline=DateTime.Parse("2029-01-21", CultureInfo.CurrentCulture), Order=0},
                new Card{LaneID=2, Title="Fix 7", Description="", Deadline=DateTime.Parse("2020-07-21", CultureInfo.CurrentCulture), Order=0},
                
            };

            context.Cards.AddRange(cards); */
            context.SaveChanges();            
        }
    
    }
}
