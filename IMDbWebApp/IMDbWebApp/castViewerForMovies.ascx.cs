using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMDbWebApp
{
    public partial class castViewerForMovies : System.Web.UI.UserControl
    {
        public string Role { get; set; }
        public string Firstname { get; set; }
        public string Betweenname { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string BirthCountry { get; set; }
        public string Biography { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Bij aanroep worden deze labels gevuld
        /// </summary>
        public void LoadData()
        {
            lblRole.Text = Role;
            lblFtName.Text = Firstname;
            lblBtwnName.Text = Betweenname;
            lblSurname.Text = Surname;
            lblBirthDate.Text = BirthDate;
            lblBirthPlace.Text = BirthPlace;
            lblBirthCountry.Text = BirthCountry;
            lblBio.Text = Biography;
        }
    }
}