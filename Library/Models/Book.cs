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

        public static List<BookClass> GetAll()
        {
            List<BookClass> allBooks = new List <BookClass> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM books;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string title = rdr.GetString(1);
             
                BookClass newBook = new BookClass(title, id);
                allBooks.Add(newBook);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allBooks;
        }

        public static BookClass GetBookByTitle(string title)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM books WHERE title = '" + title + "';";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        int id = 0;
        string _title = "This Book Does Not Exist in the Database!";
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

        public static BookClass GetBookById(int id)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM books WHERE id = '" + id + "';";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        int _id = 0;
        string _title = "This Book Does Not Exist in the Database!";
        while(rdr.Read())
        {
            _id = rdr.GetInt32(0);
            _title = rdr.GetString(1);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        BookClass book = new BookClass(_title, _id);
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

        public static List<BookClass> GetBooksByAuthorId(int authorId)
        {
        List<BookClass> books = new List<BookClass>{};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT books.* FROM books
            JOIN authors_books ON (books.id = authors_books.book_id)
            JOIN authors ON (authors_books.author_id = authors.id)
            WHERE authors.id = " + authorId + ";";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        
        while(rdr.Read())
        {
            int id = rdr.GetInt32(0);
            string title = rdr.GetString(1);
            BookClass book = new BookClass(title, id);
            books.Add(book);
        }
        
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return books;
        }
    }
}
