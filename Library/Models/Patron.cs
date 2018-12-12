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
    }
}