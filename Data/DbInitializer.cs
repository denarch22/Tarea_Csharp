using MyFirstApi.Models;

namespace MyFirstApi.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Todos.Any()) return;

            db.Todos.AddRange(
                new Todo { Title = "Aprender .NET" },
                new Todo { Title = "Configurar PostgreSQL" },
                new Todo { Title = "Probar en Postman", IsCompleted = true }
            );
            db.SaveChanges();
        }
    }
}
