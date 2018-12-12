using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class JoinBookAuthorClass
    {
        private int _id;
        private int _book_id;
        private int _author_id;

        public JoinBookAuthorClass(int book_id, int author_id, int id=0)
        {
            _book_id = book_id;
            _author_id = author_id;
            _id = id;
        } 

        public int GetId()
        {
            return _id;
        }

        public int GetBookId()
        {
            return _book_id;
        }

        public int GetAuthorId()
        {
            return _author_id;
        }

        public static void Save(int author_id, int book_id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO authors_books (author_id , book_id) VALUES (@author_id , @book_id);";
            cmd.Parameters.AddWithValue("@author_id", author_id);
            cmd.Parameters.AddWithValue("@book_id", book_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
    public class JoinPatronBookClass
    {
        private int _id;
        private int _book_id;
        private int _patron_id;

        public JoinPatronBookClass(int book_id, int patron_id, int id=0)
        {
            _book_id = book_id;
            _patron_id = patron_id;
            _id = id;
        } 

        public int GetId()
        {
            return _id;
        }

        public int GetBookId()
        {
            return _book_id;
        }

        public int GetPatronId()
        {
            return _patron_id;
        }

        public static void SavePatronCopy(int patron_id, int book_id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO patrons_copies (patron_id , book_id) VALUES (@patron_id , @book_id);";
            cmd.Parameters.AddWithValue("@patron_id", patron_id);
            cmd.Parameters.AddWithValue("@book_id", book_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeletePatronCopy(int patron_id, int book_id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM patrons_copies WHERE patron_id = " + patron_id + " AND book_id = " + book_id + ";";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}
