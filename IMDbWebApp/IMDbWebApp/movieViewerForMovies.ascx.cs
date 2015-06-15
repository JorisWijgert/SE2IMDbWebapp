using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMDbWebApp
{
    //Hulpbron: code van Rick
    /// <summary>
    /// Zorgt ervoor dat de user control gevuld wordt met data van een film
    /// </summary>
    public partial class movieViewerForMovies : System.Web.UI.UserControl
    {
        public string Title { get; set; }
        public string ImageLoc { get; set; }
        public string AboutMovie { get; set; }
        public string Grade { get; set; }
        public string VideoId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void LoadData()
        {
            movCImage.Src = ImageLoc;
            movCImage.Alt = Title;
            movCTitle.InnerText = Title;
            movCAbout.InnerText = AboutMovie;
            movCGrade.InnerText = Grade;
            movContainLink.HRef = "movie.aspx?movid=" + VideoId;
        }
    }
}