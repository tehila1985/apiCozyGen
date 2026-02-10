using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Test
{
    public class DatabaseFixture : IDisposable
    {
        public myDBContext Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<myDBContext>()
                .UseSqlServer("Server = LAPTOP-LDNABVH4\\SQLEXPRESS;Database=Tests;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;
            Context = new myDBContext(options);
            Context.Database.EnsureCreated();
        }

        public void ClearDatabase()
        {
            Context.ChangeTracker.Clear();
            // סדר המחיקה קריטי למניעת שגיאות Foreign Key
            if (Context.OrderItems.Any()) Context.OrderItems.RemoveRange(Context.OrderItems);
            if (Context.Orders.Any()) Context.Orders.RemoveRange(Context.Orders);
            if (Context.Products.Any()) Context.Products.RemoveRange(Context.Products);
            if (Context.Categories.Any()) Context.Categories.RemoveRange(Context.Categories);
            if (Context.Users.Any()) Context.Users.RemoveRange(Context.Users);
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}

