using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class AuthorClass
    {
        private int _id;
        private string _name;

        public AuthorClass(string name, int id=0)
        {
            _name = name;
            _id = id;
        } 

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public static void Save(string name)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO authors (name) VALUES (@name);";
            cmd.Parameters.AddWithValue("@name", name);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<AuthorClass> GetAll()
        {
            List<AuthorClass> allAuthors = new List <AuthorClass> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM authors;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string title = rdr.GetString(1);
            
                AuthorClass newAuthor = new AuthorClass(title, id);
                allAuthors.Add(newAuthor);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allAuthors;
        }

        public static AuthorClass GetAuthorByName(string name)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM authors WHERE name = '" + name + "';";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        int id = 0;
        string _name = "That Author Does Not Exist In The Database!";
        while(rdr.Read())
        {
            id = rdr.GetInt32(0);
            _name = rdr.GetString(1);
        }
        conn.Close();
        AuthorClass newAuthor = new AuthorClass(_name, id);
        if (conn != null)
        {
            conn.Dispose();
        }
        return newAuthor;
        }

        public static AuthorClass GetAuthorById(int id)
        {
        
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM authors WHERE id = " + id + ";";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        int _id = 0;
        string _name = "Dog The Non Existant Author";
        while(rdr.Read())
        {
            _id = rdr.GetInt32(0);
            _name = rdr.GetString(1);
        }
        AuthorClass newAuthor = new AuthorClass(_name, id);
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return newAuthor;
        }

        public static AuthorClass GetAuthorByBookId(int bookId)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT authors.* FROM authors
            JOIN authors_books ON (authors.id = authors_books.author_id)
            JOIN books ON (authors_books.book_id = books.id)
            WHERE books.id = " + bookId + ";";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        int id = 0;
        string name = "Dog The Non Existant Author";
        while(rdr.Read())
        {
            id = rdr.GetInt32(0);
            name = rdr.GetString(1);
        }
        AuthorClass author = new AuthorClass(name, id);
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return author;
        }

        public static bool CheckAuthorExistByName(string name)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM authors WHERE name = '" + name + "';";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            bool exists = false;
            while(rdr.Read())
            {
                if (name == rdr.GetString(1))
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return exists;
        }
    }
}
