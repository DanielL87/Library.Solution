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

        public static List<AuthorClass> GetAuthorByName(string name)
        {
        List<AuthorClass> allAuthors = new List<AuthorClass> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM authors WHERE name = '" + name + "';";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string _name = rdr.GetString(1);
            AuthorClass newAuthor = new AuthorClass(_name, id);
            allAuthors.Add(newAuthor);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allAuthors;
        }
    }
}
