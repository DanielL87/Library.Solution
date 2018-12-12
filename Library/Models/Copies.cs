using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class CopiesClass
    {
        private int _id;
        private int _book_id;
        private int _amount;
        private int _total;

        public CopiesClass(int book_id, int amount, int total, int id=0)
        {
            _book_id = book_id;
            _amount = amount;
            _total = total;
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

        public int GetAmount()
        {
            return _amount;
        }

        public int GetTotal()
        {
            return _total;
        }

        public static void Save(int book_id, int amount, int total)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO copies (book_id, amount, total) VALUES (@book_id, @amount, @total);";
            cmd.Parameters.AddWithValue("@book_id", book_id);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@total", total);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void Update(int book_id, int amount)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE copies SET amount = " + amount + " WHERE book_id = " + book_id + ";";
            cmd.Parameters.AddWithValue("@book_id", book_id);
            cmd.Parameters.AddWithValue("@amount", amount);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void UpdateTotal(int book_id, int total)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE copies SET total = " + total + " WHERE book_id = " + book_id + ";";
            cmd.Parameters.AddWithValue("@book_id", book_id);
            cmd.Parameters.AddWithValue("@total", total);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static int GetTotalByBookId(int book_id)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT total FROM copies WHERE book_id = " + book_id + ";";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        int _total = 0;
        while(rdr.Read())
        {
            _total = rdr.GetInt32(0);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return _total;
        }

        public static int GetAmountByBookId(int book_id)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT amount FROM copies WHERE book_id = " + book_id + ";";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        int _amount = 0;
        while(rdr.Read())
        {
            _amount = rdr.GetInt32(0);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return _amount;
        }
    }
}
