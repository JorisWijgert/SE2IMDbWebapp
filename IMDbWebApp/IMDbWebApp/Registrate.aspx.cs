using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMDbWebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        /// <summary>
        /// Als er al is ingelogd: doorverwijzen naar homepage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Registreren | IMDb";
            if (!string.IsNullOrEmpty(Session["user"] as string))
            {
                Response.Redirect("Index.aspx");
            }
            errorLabels.Visible = false;
            success.Visible = false;
        }

        /// <summary>
        /// Zorgt dat een gebruiker zich registreert
        /// Bij foute data: fout tonen en velden leegmaken
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCommit_Click(object sender, EventArgs e)
        {
            int successReg = 0;
            try
            {
                successReg = Database.RegistrateUser(tbFirstName.Text, tbLastName.Text, rblGender.SelectedValue,
                    Convert.ToInt32(tbBirthYear.Text), tbCountry.Text, tbPostCode.Text, tbEmail.Text, tbPassword.Text);
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException)
            {
                errorLabels.Visible = true;
                lblErrorMessage.Text = "Registreren niet gelukt, mogelijk is het e-mailadres al geregistreerd.";
            }
            catch (FormatException)
            {
                errorLabels.Visible = true;
                emptyTextBoxes();
                lblErrorMessage.Text = "Registeren niet gelukt, niet alle velden zijn (correct) ingevuld.";
            }
            if (successReg == 1)
            {
                success.Visible = true;
                fields.Visible = false;
            }
            else
            {
                errorLabels.Visible = true;
                emptyTextBoxes();
            }
        }
        private void emptyTextBoxes()
        {
            tbFirstName.Text = string.Empty;
            tbLastName.Text = string.Empty;
            rblGender.SelectedValue = "M";
            tbBirthYear.Text = string.Empty;
            tbCountry.Text = string.Empty;
            tbPostCode.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbPassword.Text = string.Empty;
        }
    }
}