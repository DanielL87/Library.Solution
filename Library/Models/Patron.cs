using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class PatronClass
    {
        private int _id;
        private string _name;

        public PatronClass(string name, int id=0)
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
            cmd.CommandText = @"INSERT INTO patrons (name) VALUES (@name);";
            cmd.Parameters.AddWithValue("@name", name);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static int GetPatronIdByName(string name)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT id FROM patrons WHERE name = '" + name + "';";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        int id = 0;
        while(rdr.Read())
        {
            id = rdr.GetInt32(0);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return id;
        }

        public static string GetPatronNameById(int patron_id)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT name FROM patrons WHERE id = " + patron_id + ";";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        string name = "";
        while(rdr.Read())
        {
            name = rdr.GetString(0);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return name;
        }

        public static bool CheckPatronExistByName(string name)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM patrons WHERE name = '" + name + "';";
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

        public static List<BookClass> GetBooksByPatronId(int patronId)
        {
        List<BookClass> allBooks = new List<BookClass> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT books.* FROM books
            JOIN patrons_copies ON (books.id = patrons_copies.book_id)
            JOIN patrons ON (patrons_copies.patron_id = patrons.id)
            WHERE patrons.id = " + patronId + ";";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string _title = rdr.GetString(1);
            BookClass newBook = new BookClass(_title, id);
            allBooks.Add(newBook);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allBooks;
        }
    }
}