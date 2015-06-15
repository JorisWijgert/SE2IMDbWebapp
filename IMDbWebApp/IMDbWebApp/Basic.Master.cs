using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMDbWebApp
{
    public partial class Basic : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Zorgt ervoor dat listitem 4 van het menu inloggen of uitloggen bevat aan de hand van of de gebruikersessie ingevuld is
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["user"] as string))
            {
                loginuit.InnerText = "Inloggen";
            }
            else
            {
                loginuit.InnerText = "Uitloggen";
            }
        }
    }
}