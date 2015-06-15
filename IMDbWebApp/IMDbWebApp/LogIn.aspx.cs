using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMDbWebApp
{
    public partial class LogIn : System.Web.UI.Page
    {
        /// <summary>
        /// Laat de velden voor het inloggen zien of voor het uitloggen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["user"] as string))
            {
                Page.Title = "Inloggen | IMDb";
                logOut.Visible = false;
                logIn.Visible = true;
                lblResult.Visible = false;
            }
            else
            {
                Page.Title = "Uitloggen | IMDb";
                lblAccount.Text = Session["user"] as string;
                Session["user"] = null;
                logOut.Visible = true;
                logIn.Visible = false;
                lblResult.Visible = false;
            }
            
        }
        /// <summary>
        /// Zorgt dat een gebruiker kan inloggen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblResult.Visible = true;
            try
            {
                Session["user"] = Database.LogInUser(tbEmail.Text, tbPassword.Text);
                if (string.IsNullOrEmpty(Session["user"] as string))
                {
                    lblResult.Text = "Inloggen is niet gelukt";
                    tbEmail.Text = string.Empty;
                    tbPassword.Text = string.Empty;
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
            }
            catch (Exception ex)
            {
                lblResult.Text = ex.Message;
                tbEmail.Text = string.Empty;
                tbPassword.Text = string.Empty;
            }
        }
    }
}