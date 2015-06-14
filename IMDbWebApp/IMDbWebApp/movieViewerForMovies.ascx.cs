using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMDbWebApp
{
    public partial class movieViewerForMovies : System.Web.UI.UserControl
    {
        public String Title { get; set; }
        public String ImageLoc { get; set; }
        public String AboutMovie { get; set; }
        public String Grade { get; set; }
        public String VideoId { get; set; }
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