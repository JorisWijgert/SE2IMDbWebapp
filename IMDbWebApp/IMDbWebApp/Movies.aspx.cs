using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMDbWebApp
{
    public partial class Movies : System.Web.UI.Page
    {
        /// <summary>
        /// Maakt een lijst van alle films op alfabet gesorteerd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Films | IMDb";

            List<string[]> movies = Database.GetMovies();

            foreach (string[] movie in movies)
            {
                movieViewerForMovies userControl = (movieViewerForMovies)Page.LoadControl("~/movieViewerForMovies.ascx");

                userControl.VideoId = movie[0];
                userControl.Title = movie[1];
                if (!string.IsNullOrEmpty(movie[2]))
                {
                    userControl.ImageLoc = movie[2];
                }
                if (!string.IsNullOrEmpty(movie[3]))
                {
                    userControl.Grade = movie[3];
                }
                else
                {
                    userControl.Grade = "0,0";
                }
                if (!string.IsNullOrEmpty(movie[4]))
                {
                    userControl.AboutMovie = movie[4].Substring(0, 30) + "...";
                }
                else
                {
                    userControl.AboutMovie = "Geen omschrijving";
                }
                movieContent.Controls.Add(userControl);
                userControl.LoadData();
            }
        }
    }
}