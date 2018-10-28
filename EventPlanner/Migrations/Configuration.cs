namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventPlanner.EventModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EventPlanner.EventModel context)
        {
            context.Categories.AddOrUpdate(x => x.Id,
                    new Category { Name = "Birthdays" },
                    new Category { Name = "Reminders" },
                    new Category { Name = "To-Do" },
                    new Category { Name = "Appointments"}
                );
        }
    }
}
