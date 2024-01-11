using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimeApp
{
    internal class StudyTimeDAO
    {
        string connectionString = "datasource=studydb.czmok0ea8zww.eu-north-1.rds.amazonaws.com;port=3306;username=admin;password=kiko123454321;database=studyDB;";

        public List<StudyTime> getAllTimes()
        {
            // Start with an empty list
            List<StudyTime> returnThis = new List<StudyTime>();
            
            // Connect to MySQL
            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            // Define statement to fetch all studytimes
            MySqlCommand command = new MySqlCommand("SELECT * FROM days", connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    StudyTime test = new StudyTime
                    {
                        ID = reader.GetInt32(0),
                        Date = reader.GetDateTime(1),
                        Time = reader.GetString(2),
                        Notes = reader.GetString(3),
                        Summary = reader.GetString(4),
                    };
                    returnThis.Add(test);
                }
            }
            connection.Close();

            return (returnThis);
        }
        public List<StudyTime> searchStudies(String searchText)
        {
            // Start with an empty list
            List<StudyTime> returnThis = new List<StudyTime>();

            // Connect to MySQL
            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            String searchWildPhrase = "%" + searchText + "%";

            // Define statement to fetch all studytimes
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM days WHERE NOTES LIKE @search OR SUMMARY LIKE @search";
            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    StudyTime test = new StudyTime
                    {
                        ID = reader.GetInt32(0),
                        Date = reader.GetDateTime(1),
                        Time = reader.GetString(2),
                        Notes = reader.GetString(3),
                        Summary = reader.GetString(4),
                    };
                    returnThis.Add(test);
                }
            }
            connection.Close();

            return (returnThis);
        }

        public List<StudyTime> searchDates(String searchText)
        {
            // Start with an empty list
            List<StudyTime> returnThis = new List<StudyTime>();

            // Connect to MySQL
            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            // Define statement to fetch all studytimes
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM days WHERE DATE = @search";
            command.Parameters.AddWithValue("@search", searchText);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    StudyTime test = new StudyTime
                    {
                        ID = reader.GetInt32(0),
                        Date = reader.GetDateTime(1),
                        Time = reader.GetString(2),
                        Notes = reader.GetString(3),
                        Summary = reader.GetString(4),
                    };
                    returnThis.Add(test);
                }
            }
            connection.Close();

            return (returnThis);
        }

        internal int addStudy(StudyTime newStudy)
        {
            // Connect to MySQL
            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            // Define statement to fetch all studytimes
            MySqlCommand command = new MySqlCommand("INSERT INTO days (`DATE`, `STUDY_TIME`, `NOTES`, `SUMMARY`) VALUES (@date, @studytime, @notes, @summary)", connection);
            command.Parameters.AddWithValue("@date", newStudy.Date);
            command.Parameters.AddWithValue("@studytime", newStudy.Time);
            command.Parameters.AddWithValue("@notes", newStudy.Notes);
            command.Parameters.AddWithValue("@summary", newStudy.Summary);

            int newRows = command.ExecuteNonQuery();
            connection.Close();

            return (newRows);
        }

        internal int deleteStudy(int id)
        {
            throw new NotImplementedException();
        }
    }
}
