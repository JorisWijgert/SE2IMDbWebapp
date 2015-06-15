using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace IMDbWebApp
{
    /// <summary>
    /// De klasse waarin de interactie met de database gebeurd
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// Maakt een parameter aan om SQL-injectie te voorkomen
        /// </summary>
        /// <param name="command">databasecommando</param>
        /// <param name="parameterName">de naam van de parameter</param>
        /// <param name="parameterValue">de waarde van de parameter</param>
        private static void AddParameterWithValue(this DbCommand command, string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            parameter.DbType = System.Data.DbType.AnsiString;
            parameter.Direction = System.Data.ParameterDirection.Input;
            command.Parameters.Add(parameter);
        }
        #region read data
        #region get user(s)
        /// <summary>
        /// Inloggen als gebruiker
        /// </summary>
        /// <param name="mailadress"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string LogInUser(string mailadress, string password)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    return null;
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    return null;
                }
                com.Connection = con;
                com.CommandText = "SELECT emailadres FROM gebruiker WHERE emailadres = :1 AND wachtwoord = :2";
                AddParameterWithValue(com, "mailadres", mailadress);
                AddParameterWithValue(com, "lastname", password);
                return (string)com.ExecuteScalar();
            }
        }
        #endregion
        #region get Movies
        /// <summary>
        /// Geeft alle films terug die in de database staan
        /// </summary>
        /// <returns></returns>
        public static List<string[]> GetMovies()
        {
            List<string[]> movies = new List<string[]>();
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    return null;
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    return null;
                }
                com.Connection = con;
                com.CommandText = "SELECT video.videoId, titel, cover, cijfer, samenvatting FROM video INNER JOIN film ON film.videoId = video.videoId ORDER BY titel";
                DbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    string[] moviedata = new string[5];
                    for (int i = 0; i < moviedata.Length; i++)
                    {
                        moviedata[i] = Convert.ToString(reader[i]);
                    }
                    movies.Add(moviedata);
                }
                return movies;
            }
        }

        /// <summary>
        /// Geeft de 2 best gewaardeerde filsm
        /// </summary>
        /// <returns></returns>
        public static List<string[]> GetBestMovies()
        {
            List<string[]> movies = new List<string[]>();
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    return null;
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    return null;
                }
                com.Connection = con;
                com.CommandText = "SELECT * FROM(SELECT video.videoId, titel, cover, cijfer, samenvatting FROM video INNER JOIN film ON film.videoId = video.videoId ORDER BY cijfer DESC ) WHERE rownum <= 2";
                DbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    string[] moviedata = new string[5];
                    for (int i = 0; i < moviedata.Length; i++)
                    {
                        moviedata[i] = Convert.ToString(reader[i]);
                    }
                    movies.Add(moviedata);
                }
                return movies;
            }
        }
        /// <summary>
        /// Geeft de informatie van de opgevraagde film
        /// </summary>
        /// <param name="movId"></param>
        /// <returns></returns>
        public static string[] GetSingleMovie(int movId)
        {
            string[] movie = new string[8];

            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    return null;
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    return null;
                }
                com.Connection = con;
                com.CommandText = "SELECT video.videoId, titel, cover, to_char(lengte, 'HH24:MI:SS'), to_char(releasedatum, 'DD-MM-YYYY'), cijfer, samenvatting, genre FROM FILM INNER JOIN VIDEO ON FILM.VIDEOID = VIDEO.VIDEOID WHERE video.videoId = :1";
                AddParameterWithValue(com, "videoId", movId);
                DbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < movie.Length; i++)
                    {
                        movie[i] = Convert.ToString(reader[i]);
                    }
                }
                return movie;
            }
        }

        /// <summary>
        /// Geeft het vervolg van de opgevraagde film
        /// </summary>
        /// <param name="movId"></param>
        /// <returns></returns>
        public static string[] GetSequel(int movId)
        {
            string[] movie = new string[5];

            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    return null;
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    return null;
                }
                com.Connection = con;
                com.CommandText = "SELECT video.videoId, titel, cover, cijfer, samenvatting FROM video INNER JOIN film ON film.videoId = video.videoId WHERE film.videoId = (SELECT vervolgfilm FROM film WHERE videoId = :1)";
                AddParameterWithValue(com, "videoId", movId);
                DbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < movie.Length; i++)
                    {
                        movie[i] = Convert.ToString(reader[i]);
                    }
                }
                return movie;
            }
        }
        #endregion
        #region get user review data

        /// <summary>
        /// Bekijkt of de gebruiker al in de tabel beoordeling staat met de ingevoerde film en geeft dan terug of deze ook in de lijst staat
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string CheckInList(int movie, string user)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    return null;
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    return null;
                }
                com.Connection = con;
                com.CommandText = "SELECT inMarkeerlijst FROM beoordeling INNER JOIN gebruiker ON beoordeling.gebruikerId = gebruiker.gebruikerId WHERE videovideoId = :1 AND gebruiker.emailadres = :2";
                AddParameterWithValue(com, "movieId", movie);
                AddParameterWithValue(com, "user", user);
                return (string)com.ExecuteScalar();
            }
        }

        /// <summary>
        /// Geeft het cijfer wat de gebruiker aan de film heeft gegeven
        /// </summary>
        /// <param name="movie"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static float GetUserGrade(int movie, string user)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    return 0;
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    return 0;
                }
                com.Connection = con;
                com.CommandText = "SELECT cijfer FROM beoordeling INNER JOIN gebruiker ON beoordeling.gebruikerId = gebruiker.gebruikerId WHERE videovideoId = :1 AND gebruiker.emailadres = :2";
                AddParameterWithValue(com, "movieId", movie);
                AddParameterWithValue(com, "user", user);
                var returnvalue = com.ExecuteScalar();
                return (float)returnvalue;
            }
        }

        /// <summary>
        /// Geeft de cast met acteurs op die in de ingevoerde film meespelen
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        #endregion
        #region get cast
        public static List<String[]> GetCast(int movieId)
        {
            List<string[]> casts = new List<string[]>();
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    return null;
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    return null;
                }
                com.Connection = con;
                com.CommandText = "SELECT rol, voornaam, tussenvoegsel, achternaam, TO_CHAR(geboortedatum, 'DD-MM-YYYY'), geboorteplaats, geboorteland, biografie FROM CAST INNER JOIN ACTEUR ON cast.acteurID = acteur.acteurID WHERE filmvideoId = :1";
                AddParameterWithValue(com, "videoId", movieId);
                DbDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    string[] moviedata = new string[8];
                    for (int i = 0; i < moviedata.Length; i++)
                    {
                        moviedata[i] = Convert.ToString(reader[i]);
                    }
                    casts.Add(moviedata);
                }
                return casts;
            }
        }

        #endregion
        #endregion
        #region insert/update data

        /// <summary>
        /// Registreert een gebruiker
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="gender"></param>
        /// <param name="yearOfBirth"></param>
        /// <param name="country"></param>
        /// <param name="postcode"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int RegistrateUser(string firstname, string lastname, string gender, int yearOfBirth, string country, string postcode, string email, string password)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    return 0;
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    return 0;
                }
                com.Connection = con;
                com.CommandText = "INSERT INTO gebruiker (voornaam, achternaam, geslacht, geboortejaar, land, postcode, emailadres, wachtwoord) VALUES (:1, :2, :3, :4, :5, :6, :7, :8)";
                AddParameterWithValue(com, "firstname", firstname);
                AddParameterWithValue(com, "lastname", lastname);
                AddParameterWithValue(com, "gender", gender);
                AddParameterWithValue(com, "yearOfBirth", yearOfBirth);
                AddParameterWithValue(com, "country", country);
                AddParameterWithValue(com, "postcode", postcode);
                AddParameterWithValue(com, "email", email);
                AddParameterWithValue(com, "password", password);
                return com.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Werkt bij of de film in de lijst van de gebruiker staat
        /// </summary>
        /// <param name="inListValue"></param>
        /// <param name="movieId"></param>
        /// <param name="user"></param>
        public static void UpdateInUserList(string inListValue, int movieId, string user)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {
                    
                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {
                    
                }
                com.Connection = con;
                com.CommandText = "UPDATE beoordeling SET inMarkeerlijst = :1 WHERE videovideoId = :2 AND gebruikerId = (SELECT gebruikerId FROM gebruiker WHERE emailadres = :3)";
                AddParameterWithValue(com, "inList", inListValue);
                AddParameterWithValue(com, "movId", movieId);
                AddParameterWithValue(com, "user", user);

                com.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Voegt een record toe zodat een film in de lijst van de gebruiker kan staan
        /// </summary>
        /// <param name="inListValue"></param>
        /// <param name="movieId"></param>
        /// <param name="user"></param>
        public static void InsertInUserList(string inListValue, int movieId, string user)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {

                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {

                }
                com.Connection = con;
                com.CommandText = "INSERT INTO beoordeling (gebruikerId, inMarkeerlijst, videovideoId, isFilm) SELECT gebruikerId, :1, :2, 'Y' FROM gebruiker WHERE emailadres = :3";
                AddParameterWithValue(com, "inList", inListValue);
                AddParameterWithValue(com, "movId", movieId);
                AddParameterWithValue(com, "user", user);

                com.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Werkt de beoordeling van de film bij van de gebruiker
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="movieId"></param>
        /// <param name="user"></param>
        public static void UpdateInGrades(string grade, int movieId, string user)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {

                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {

                }
                com.Connection = con;
                com.CommandText = "UPDATE beoordeling SET cijfer = :1 WHERE videovideoId = :2 AND gebruikerId = (SELECT gebruikerId FROM gebruiker WHERE emailadres = :3)";
                AddParameterWithValue(com, "grade", grade);
                AddParameterWithValue(com, "movId", movieId);
                AddParameterWithValue(com, "user", user);

                com.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Voegt een record toe waarin de beoordeling van de gebruiker staat over de gekozen film. De film staat dan niet in de lijst van de gebruiker
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="movieId"></param>
        /// <param name="user"></param>
        public static void InsertInGrades(string grade, int movieId, string user)
        {
            using (DbConnection con = OracleClientFactory.Instance.CreateConnection())
            {
                if (con == null)
                {

                }
                con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
                con.Open();
                DbCommand com = OracleClientFactory.Instance.CreateCommand();
                if (com == null)
                {

                }
                com.Connection = con;
                com.CommandText = "INSERT INTO beoordeling (gebruikerId, inMarkeerlijst, cijfer, videovideoId, isFilm) SELECT gebruikerId, 'N', :1, :2, 'Y' FROM gebruiker WHERE emailadres = :3";
                AddParameterWithValue(com, "grade", grade);
                AddParameterWithValue(com, "movId", movieId);
                AddParameterWithValue(com, "user", user);

                com.ExecuteNonQuery();
            }
        }
        #endregion
    }
}