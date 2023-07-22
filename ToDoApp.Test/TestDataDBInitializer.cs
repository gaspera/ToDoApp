namespace ToDoApp.Test
{
    public class TestDataDBInitializer
    {
        public TestDataDBInitializer()
        {
        }

        public void Seed(ToDoAppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Tasks.Add(new Data.Models.Task
            {
                Naslov = "Task 1",
                Opis = "Opis task 1",
                DatumUstvarjanja = DateTime.Now,
                Opravljeno = true
            });
            context.Tasks.Add(new Data.Models.Task
            {
                Naslov = "Task 2",
                Opis = "Opis task 2",
                DatumUstvarjanja = DateTime.Now,
                Opravljeno = true
            });
            context.Tasks.Add(new Data.Models.Task
            {
                Naslov = "Task 3",
                Opis = "Opis task 3",
                DatumUstvarjanja = DateTime.Now,
                Opravljeno = true
            });

            context.SaveChanges();
        }
    }
}
