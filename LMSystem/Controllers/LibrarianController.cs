using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class LibrarianController : Controller
    {
        private readonly IConfiguration _config;

        public LibrarianController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            var librarians = new List<LibrarianModel>();
            string connStr = _config.GetConnectionString("DefaultConnection");

            using (var con = new SqliteConnection(connStr))
            {
                var cmd = new SqliteCommand("SELECT * FROM Librarians", con);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        librarians.Add(new LibrarianModel
                        {
                            LibrarianId = Convert.ToInt32(reader["LibrarianId"]),
                            Name = reader["Name"].ToString(),
                            Age = Convert.ToInt32(reader["Age"]),
                            Phone = reader["Phone"].ToString()
                        });
                    }
                }
            }
            return View(librarians);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(LibrarianModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string connStr = _config.GetConnectionString("DefaultConnection");
            using (var con = new SqliteConnection(connStr))
            {
                var cmd = new SqliteCommand("INSERT INTO Librarians (Name, Age, Phone) VALUES (@Name, @Age, @Phone)", con);
                cmd.Parameters.AddWithValue("@Name", model.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Age", model.Age);
                cmd.Parameters.AddWithValue("@Phone", model.Phone ?? (object)DBNull.Value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var librarian = new LibrarianModel();
            string connStr = _config.GetConnectionString("DefaultConnection");

            using (var con = new SqliteConnection(connStr))
            {
                var cmd = new SqliteCommand("SELECT * FROM Librarians WHERE LibrarianId=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        librarian.LibrarianId = Convert.ToInt32(reader["LibrarianId"]);
                        librarian.Name = reader["Name"].ToString();
                        librarian.Age = Convert.ToInt32(reader["Age"]);
                        librarian.Phone = reader["Phone"].ToString();
                    }
                }
            }
            return View(librarian);
        }

        [HttpPost]
        public IActionResult Edit(LibrarianModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string connStr = _config.GetConnectionString("DefaultConnection");
            using (var con = new SqliteConnection(connStr))
            {
                var cmd = new SqliteCommand("UPDATE Librarians SET Name=@Name, Age=@Age, Phone=@Phone WHERE LibrarianId=@id", con);
                cmd.Parameters.AddWithValue("@Name", model.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Age", model.Age);
                cmd.Parameters.AddWithValue("@Phone", model.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@id", model.LibrarianId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            string connStr = _config.GetConnectionString("DefaultConnection");
            using (var con = new SqliteConnection(connStr))
            {
                var cmd = new SqliteCommand("DELETE FROM Librarians WHERE LibrarianId=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
