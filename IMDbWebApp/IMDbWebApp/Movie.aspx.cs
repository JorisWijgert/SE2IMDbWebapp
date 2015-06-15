using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMDbWebApp
{
    public partial class Movie : System.Web.UI.Page
    {
        private bool inList { get; set; }
        public bool listExists { get; set; }
        public float userGrade { get; set; }
        private int movieId { get; set; }

        /// <summary>
        /// Als er niet is ingelogd: vermelding inloggen verplicht.
        /// Als er geen film, of een onjuiste film is opgegeven: doorverwijzen naar errorpage
        /// Anders: film laten zien
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSelectAValue.Visible = false;
            if (string.IsNullOrEmpty(Session["user"] as string))
            {
                Page.Title = "Inloggen vereist | IMDb";
                pnlNotLoggedIn.Visible = true;
            }
            else
            {
                string movId = Request.QueryString["movId"];

                if (string.IsNullOrEmpty(movId))
                {
                    Response.Redirect("Errorpage.aspx");
                }
                try
                {
                    movieId = Convert.ToInt32(movId);
                    FillMoviePanel(movieId);
                    FillUserPanel(movieId, Session["user"] as string);
                    FillSequelPanel(movieId);
                    FillCast(movieId);
                }
                catch (Exception ex)
                {
                    Response.Redirect("Errorpage.aspx?exception=" + ex.Message);
                }
                finally
                {
                    pnlMovieInfo.Visible = true;
                    pnlUserActions.Visible = true;
                }
            }
        }

        /// <summary>
        /// Vult de panel met de data van de gekozen film
        /// </summary>
        /// <param name="movieId"></param>
        private void FillMoviePanel(int movieId)
        {
            string[] movie = Database.GetSingleMovie(movieId);
            if (string.IsNullOrEmpty(movie[0]))
            {
                Response.Redirect("Errorpage.aspx");
            }
            lblTitle.Text = movie[1];
            Page.Title = movie[1] + " | IMDb";
            imgCover.ImageUrl = movie[2];
            lblLength.Text = movie[3];
            lblRlsDate.Text = movie[4];
            lblGrade.Text = movie[5];
            lblSummary.Text = movie[6];
            lblGenre.Text = movie[7];
        }

        /// <summary>
        /// Vult de panel met acties die de gebruiker kan uitvoeren: toevoegen aan lijst en beoordelen
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="username"></param>
        private void FillUserPanel(int movieId, string username)
        {
            try
            {
                if (Database.CheckInList(movieId, username) == "Y")
                {
                    inList = true;
                    listExists = true;
                }
                else
                {
                    inList = false;
                    listExists = true;
                }
                userGrade = Database.GetUserGrade(movieId, username);
                rblGrade.SelectedValue = Convert.ToString(Math.Round(userGrade));
            }
            catch (InvalidCastException)
            {
                userGrade = 0;
            }
            catch (NullReferenceException)
            {
                inList = false;
                listExists = false;
            }
            btnAddRmvListTextUpdate();
        }

        /// <summary>
        /// Vult de panel met de vervolgfilm (user control movieViewerForMovies)
        /// </summary>
        /// <param name="movieId"></param>
        private void FillSequelPanel(int movieId)
        {
            string[] sequel = Database.GetSequel(movieId);
            pnlSequel.Visible = true;
            movieViewerForMovies userControl = (movieViewerForMovies)Page.LoadControl("~/movieViewerForMovies.ascx");
            if (!string.IsNullOrEmpty(sequel[0]))
            {
                userControl.VideoId = sequel[0];
                userControl.Title = sequel[1];
                if (!string.IsNullOrEmpty(sequel[2]))
                {
                    userControl.ImageLoc = sequel[2];
                }
                if (!string.IsNullOrEmpty(sequel[3]))
                {
                    userControl.Grade = sequel[3];
                }
                else
                {
                    userControl.Grade = "0,0";
                }
                if (!string.IsNullOrEmpty(sequel[4]))
                {
                    userControl.AboutMovie = sequel[4].Substring(0, 30) + "...";
                }
                else
                {
                    userControl.AboutMovie = "Geen omschrijving";
                }
                pnlSequel.Controls.Add(userControl);
                userControl.LoadData();
            }
            else
            {
                Label noSequel = new Label();
                noSequel.Text = "Geen vervolgfilms";
                pnlSequel.Controls.Add(noSequel);
            }
        }
        /// <summary>
        /// Vult de panel met cast en acteurs (user control: castViewerForMovies)
        /// </summary>
        /// <param name="movieId"></param>
        private void FillCast(int movieId)
        {
            pnlCastActor.Visible = true;

            List<string[]> Casts = Database.GetCast(movieId);
            if (Casts.Any())
            {
                foreach (string[] cast in Casts)
                {
                    castViewerForMovies userControl = (castViewerForMovies)Page.LoadControl("~/castViewerForMovies.ascx");

                    userControl.Role = cast[0];
                    userControl.Firstname = cast[1];
                    if (!string.IsNullOrEmpty(cast[2]))
                    {
                        userControl.Betweenname = cast[2];
                    }
                    else
                    {
                        userControl.Betweenname = " ";
                    }
                    userControl.Surname = cast[3];
                    if (!string.IsNullOrEmpty(cast[4]))
                    {
                        userControl.BirthDate = cast[4];
                    }
                    else
                    {
                        userControl.BirthDate = "Geen geboortedatum bekend";
                    }
                    if (!string.IsNullOrEmpty(cast[5]))
                    {
                        userControl.BirthPlace = cast[5];
                    }
                    else
                    {
                        userControl.BirthPlace = "Geen geboorteplaats bekend";
                    }
                    if (!string.IsNullOrEmpty(cast[6]))
                    {
                        userControl.BirthCountry = cast[6];
                    }
                    else
                    {
                        userControl.BirthCountry = "Geen geboorteland bekend";
                    }
                    if (!string.IsNullOrEmpty(cast[7]))
                    {
                        userControl.Biography = cast[7].Substring(0, 30) + "...";
                    }
                    else
                    {
                        userControl.Biography = "Geen biografie bekend";
                    }
                    pnlCastActor.Controls.Add(userControl);
                    userControl.LoadData();
                }
            }
            else
            {
                Label noCast = new Label();
                noCast.Text = "Geen cast";
                pnlCastActor.Controls.Add(noCast);
            }
        }
        /// <summary>
        /// Updaten van de knop btnListAddRmv
        /// Zodat er toegevoegd of verwijderd wordt.
        /// </summary>
        private void btnAddRmvListTextUpdate()
        {
            if (inList)
            {
                btnListAddRmv.Text = "Verwijder van lijst";
            }
            else
            {
                btnListAddRmv.Text = "Toevoegen aan lijst";
            }
        }

        /// <summary>
        /// Toevoegen of verwijderen van de film aan de lijst van de gebruiker
        /// Als de record nog niet bestaat: insert
        /// Als de record wel bestaat: update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnListAddRmv_Click(object sender, EventArgs e)
        {
            if (listExists && btnListAddRmv.Text == "Toevoegen aan lijst")
            {
                Database.UpdateInUserList("Y", movieId, Session["user"] as string);
                inList = true;
            }
            else if (listExists && btnListAddRmv.Text == "Verwijder van lijst")
            {
                Database.UpdateInUserList("N", movieId, Session["user"] as string);
                inList = false;
            }
            else if (!listExists && btnListAddRmv.Text == "Toevoegen aan lijst")
            {
                Database.InsertInUserList("Y", movieId, Session["user"] as string);
                inList = true;
            }
            btnAddRmvListTextUpdate();
        }

        /// <summary>
        /// Bijwerken of toevoegen van een cijfer over de film door een gebruiker
        /// Als de record al bestaat: update
        /// Als de record niet bestaat: insert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCommit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rblGrade.SelectedValue))
            {
                if (listExists)
                {
                    Database.UpdateInGrades(rblGrade.SelectedValue, movieId, Session["user"] as string);
                }
                else
                {
                    Database.InsertInGrades(rblGrade.SelectedValue, movieId, Session["user"] as string);
                }
            }
            else
            {
                lblSelectAValue.Visible = true;
            }
        }
    }
}