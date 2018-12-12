using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class BookClass
    {
        private int _id;
        private string _title;

        public BookClass(string title, int id=0)
        {
            _title = title;
            _id = id;
        } 

        public int GetId()
        {
            return _id;
        }

        public string GetTitle()
        {
            return _title;
        }

        public static void Save(string title)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO books (title) VALUES (@title);";
            cmd.Parameters.AddWithValue("@title", title);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static BookClass GetBookByTitle(string title)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM books WHERE title = '" + title + "';";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        int id = 0;
        string _title = "";
        while(rdr.Read())
        {
            id = rdr.GetInt32(0);
            _title = rdr.GetString(1);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        BookClass book = new BookClass(_title, id);
        return book;
        }

        public static bool CheckBookExistByTitle(string title)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM books WHERE title = '" + title + "';";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            bool exists = false;
            while(rdr.Read())
            {
                if (title == rdr.GetString(1))
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
