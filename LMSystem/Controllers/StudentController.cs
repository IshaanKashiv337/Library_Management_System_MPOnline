using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _config;

        public StudentController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            var students = new List<StudentModel>();
            string connStr = _config.GetConnectionString("DefaultConnection");

            using (var con = new SqliteConnection(connStr))
            {
                var cmd = new SqliteCommand("SELECT * FROM Students", con);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new StudentModel
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            StudentName = reader["Student_Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone_Number"].ToString()
                        });
                    }
                }
            }
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string connStr = _config.GetConnectionString("DefaultConnection");
            using (var con = new SqliteConnection(connStr))
            {
                var cmd = new SqliteCommand("INSERT INTO Students (Student_Name, Email, Phone_Number) VALUES (@Name, @Email, @Phone)", con);
                cmd.Parameters.AddWithValue("@Name", model.StudentName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", model.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", model.Phone ?? (object)DBNull.Value);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var student = new StudentModel();
            string connStr = _config.GetConnectionString("DefaultConnection");

            using (var con = new SqliteConnection(connStr))
            {
                var cmd = new SqliteCommand("SELECT * FROM Students WHERE StudentId=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        student.StudentId = Convert.ToInt32(reader["StudentId"]);
                        student.StudentName = reader["Student_Name"].ToString();
                        student.Email = reader["Email"].ToString();
                        student.Phone = reader["Phone_Number"].ToString();
                    }
                }
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(StudentModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string connStr = _config.GetConnectionString("DefaultConnection");
            using (var con = new SqliteConnection(connStr))
            {
                var cmd = new SqliteCommand("UPDATE Students SET Student_Name=@Name, Email=@Email, Phone_Number=@Phone WHERE StudentId=@id", con);
                cmd.Parameters.AddWithValue("@Name", model.StudentName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", model.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", model.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@id", model.StudentId);
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
                var cmd = new SqliteCommand("DELETE FROM Students WHERE StudentId=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}
