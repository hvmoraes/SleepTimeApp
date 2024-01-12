using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudyTimeApp
{
    internal class StudyTimeDAO
    {
        string connectionString = Environment.GetEnvironmentVariable("connectionSTR");

        public List<StudyDay> getAllDays()
        {
            // Start with an empty list
            List<StudyDay> returnThis = new List<StudyDay>();
            
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
                    StudyDay day = new StudyDay
                    {
                        ID = reader.GetInt32(0),
                        Date = reader.GetString(1),
                        Time = reader.GetString(2),
                    };

                    day.Studies = getAllStudies(day.ID);

                    returnThis.Add(day);
                }
            }
            connection.Close();

            return (returnThis);
        }

        public List<Studies> getAllStudies(int timeID)
        {
            // Start with an empty list
            List<Studies> returnThis = new List<Studies>();

            // Connect to MySQL
            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            // Define statement to fetch all studies
            MySqlCommand command = new MySqlCommand("SELECT * FROM studies WHERE days_ID = @daysid", connection);
            command.Parameters.AddWithValue("@daysid", timeID);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Studies study = new Studies
                    {
                        ID = reader.GetInt32(0),
                        TotalTime = reader.GetString(1),
                        StartTime = reader.GetString(2),
                        EndTime = reader.GetString(3),
                        Summary = reader.GetString(4),
                        Notes = reader.GetString(5),
                    };
                    returnThis.Add(study);
                }
            }
            connection.Close();

            return (returnThis);
        }
        public List<StudyDay> searchDaysText(string searchText)
        {
            // Start with an empty list
            List<StudyDay> returnThis = new List<StudyDay>();

            // Connect to MySQL
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string searchWildPhrase = "%" + searchText + "%";

            // Define statement to fetch study days with matching Notes or Summary
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT DISTINCT d.* FROM days d JOIN studies s ON d.ID = s.days_ID WHERE s.NOTES LIKE @search OR s.SUMMARY LIKE @search";
            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    StudyDay day = new StudyDay
                    {
                        ID = reader.GetInt32(0),
                        Date = reader.GetString(1),
                        Time = reader.GetString(2),
                    };
                    returnThis.Add(day);
                }
            }
            connection.Close();

            return returnThis;
        }

        public List<StudyDay> searchDates(String searchText)
        {
            // Start with an empty list
            List<StudyDay> returnThis = new List<StudyDay>();

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
                    StudyDay test = new StudyDay
                    {
                        ID = reader.GetInt32(0),
                        Date = reader.GetString(1),
                        Time = reader.GetString(2),
                    };
                    returnThis.Add(test);
                }
            }
            connection.Close();

            return (returnThis);
        }

        public static int ParseTimeStringToSeconds(string timeString)
        {
            int totalSeconds = 0;
            string[] timeUnits = timeString.Split(' ');

            foreach (string unit in timeUnits)
            {
                if (unit.Contains("hr"))
                {
                    int hours = int.Parse(unit.Replace("hr", ""));
                    totalSeconds += hours * 3600;
                }
                else if (unit.Contains("min"))
                {
                    int minutes = int.Parse(unit.Replace("min", ""));
                    totalSeconds += minutes * 60;
                }
                else if (unit.Contains("sec"))
                {
                    int seconds = int.Parse(unit.Replace("sec", ""));
                    totalSeconds += seconds;
                }
            }
            return totalSeconds;
        }

        internal int addStudyDay(StudyDay newDay, Studies newStudy)
        {
            // Connect to MySQL
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            int newRows = 0;
            int dayID;

            // Check if days contains current Date
            command.CommandText = "SELECT ID FROM days WHERE DATE = @search ORDER BY ID DESC LIMIT 1";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@search", DateTime.Now.ToString("yyyy-MM-dd"));
            command.Connection = connection;
            object result = command.ExecuteScalar();

            if (result == null)
            {
                // If the day doesn't exist, insert it first
                command.CommandText = "INSERT INTO days (DATE, STUDY_TIME) VALUES (@date, @time)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@date", newDay.Date);
                command.Parameters.AddWithValue("@time", newDay.Time);
                newRows += command.ExecuteNonQuery();

                // Retrieve the ID of the newly inserted day
                command.CommandText = "SELECT LAST_INSERT_ID()";
                command.Parameters.Clear();
                dayID = Convert.ToInt32(command.ExecuteScalar());
                MessageBox.Show("New study day added successfully!");
            }
            else
            {
                // If the day exists, use its ID
                dayID = Convert.ToInt32(result);

                // Get value of current total time in days table
                command.CommandText = "SELECT STUDY_TIME FROM days WHERE ID = @dayID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@dayID", dayID);
                command.Connection = connection;
                int currentTotal = ParseTimeStringToSeconds(Convert.ToString(command.ExecuteScalar()));

                // Update the total_time column in the specific entry of the days table
                command.CommandText = "UPDATE days SET STUDY_TIME = @newTotalTime WHERE ID = @dayID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@newTotalTime", StudyTimeApp.Form1.FormatTime(TimeSpan.FromSeconds(currentTotal + ParseTimeStringToSeconds(newStudy.TotalTime))));
                command.Parameters.AddWithValue("@dayID", dayID);
                command.ExecuteNonQuery();
            }

            // Insert the study with the obtained dayID
            command.CommandText = "INSERT INTO studies (TOTAL_TIME, START_TIME, END_TIME, SUMMARY, NOTES, days_ID) VALUES (@totaltime, @starttime, @endtime, @summary, @notes, @dayId)";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@totaltime", newStudy.TotalTime);
            command.Parameters.AddWithValue("@starttime", newStudy.StartTime);
            command.Parameters.AddWithValue("@endtime", newStudy.EndTime);
            command.Parameters.AddWithValue("@summary", newStudy.Summary);
            command.Parameters.AddWithValue("@notes", newStudy.Notes);
            command.Parameters.AddWithValue("@dayId", dayID);
            newRows += command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("New study added to day " + newDay.Date);

            return newRows;
        }

        internal int deleteStudy(int studyID)
        {
            int result;

            // Connect to MySQL
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand();

            command.CommandText = "DELETE FROM studies WHERE studies.ID = @studyid";
            command.Parameters.AddWithValue("@studyid", studyID);
            command.Connection = connection;

            result = command.ExecuteNonQuery(); 
            connection.Close();
            MessageBox.Show("Study deleted!");

            return result;
        }

        internal int deleteDay(int daysID)
        {
            int result;

            // Connect to MySQL
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand();

            // Delete all studies associated with the specified day
            command.CommandText = "DELETE FROM studies WHERE days_ID = @daysID";
            command.Parameters.AddWithValue("@daysID", daysID);
            command.Connection = connection;

            result = command.ExecuteNonQuery();

            // Now, delete the day itself
            command.CommandText = "DELETE FROM days WHERE ID = @daysID";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@daysID", daysID);

            result += command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Day and associated studies deleted!");

            return result;
        }
    }
}
