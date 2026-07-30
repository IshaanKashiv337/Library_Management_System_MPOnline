using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LibraryManagement.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt and David Thomas",
                    ISBN = "978-0201616224",
                    PublishedDate = new DateTime(2021, 10, 30),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 2,
                    Title = "Design Pattern using C#",
                    Author = "Robert C. Martin",
                    ISBN = "978-0132350884",
                    PublishedDate = new DateTime(2023, 8, 1),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 3,
                    Title = "Mastering ASP.NET Core",
                    Author = "Pranaya Kumar Rout",
                    ISBN = "978-0451616235",
                    PublishedDate = new DateTime(2022, 11, 22),
                    IsAvailable = true
                },
                new Book
                {
                    BookId = 4,
                    Title = "SQL Server with DBA",
                    Author = "Rakesh Kumat",
                    ISBN = "978-4562350123",
                    PublishedDate = new DateTime(2020, 8, 15),
                    IsAvailable = true
                }
            );

            // Configure logintab
            modelBuilder.Entity<LoginModel>().ToTable("logintab");
            modelBuilder.Entity<LoginModel>().Property(l => l.id).HasColumnName("Id");
            modelBuilder.Entity<LoginModel>().Property(l => l.username).HasColumnName("Username");
            modelBuilder.Entity<LoginModel>().Property(l => l.password).HasColumnName("Password");

            // Seed logintab
            modelBuilder.Entity<LoginModel>().HasData(
                new LoginModel { id = 1, username = "admin", password = "12345" },
                new LoginModel { id = 2, username = "mycodingproject", password = "myc546" },
                new LoginModel { id = 3, username = "my", password = "myc" }
            );

            // Configure Students
            modelBuilder.Entity<StudentModel>().ToTable("Students");
            modelBuilder.Entity<StudentModel>().Property(s => s.StudentId).HasColumnName("StudentId");
            modelBuilder.Entity<StudentModel>().Property(s => s.StudentName).HasColumnName("Student_Name");
            modelBuilder.Entity<StudentModel>().Property(s => s.Email).HasColumnName("Email");
            modelBuilder.Entity<StudentModel>().Property(s => s.Phone).HasColumnName("Phone_Number");

            // Seed Students
            modelBuilder.Entity<StudentModel>().HasData(
                new StudentModel { StudentId = 1, StudentName = "Alice Johnson", Email = "alice.j@email.com", Phone = "555-0101" },
                new StudentModel { StudentId = 2, StudentName = "Bob Smith", Email = "bob.smith@email.com", Phone = "555-0102" },
                new StudentModel { StudentId = 3, StudentName = "Charlie Brown", Email = "charlie.b@email.com", Phone = "555-0103" },
                new StudentModel { StudentId = 4, StudentName = "Diana Prince", Email = "diana.p@email.com", Phone = "555-0104" },
                new StudentModel { StudentId = 5, StudentName = "Evan Wright", Email = "evan.w@email.com", Phone = "555-0105" }
            );

            // Configure Librarians
            modelBuilder.Entity<LibrarianModel>().ToTable("Librarians");

            // Seed Librarians
            modelBuilder.Entity<LibrarianModel>().HasData(
                new LibrarianModel { LibrarianId = 1, Name = "Sarah Connor", Age = 34, Phone = "555-0201" },
                new LibrarianModel { LibrarianId = 2, Name = "John Doe", Age = 28, Phone = "555-0202" },
                new LibrarianModel { LibrarianId = 3, Name = "Michael Scott", Age = 45, Phone = "555-0203" },
                new LibrarianModel { LibrarianId = 4, Name = "Ellen Ripley", Age = 39, Phone = "555-0204" },
                new LibrarianModel { LibrarianId = 5, Name = "James Bond", Age = 40, Phone = "555-0205" }
            );
        }

        public DbSet<Book> Books13 { get; set; }
        public DbSet<BorrowRecord> BorrowRecords13 { get; set; }
        public DbSet<LoginModel> LoginModels { get; set; }
        public DbSet<StudentModel> StudentModels { get; set; }
        public DbSet<LibrarianModel> LibrarianModels { get; set; }
    }
}
