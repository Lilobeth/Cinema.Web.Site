﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cinema.Web.Site
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                AdminLabel.Text = $"Hi, {Session["UserName"]}";
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTicket.aspx");
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            AdminLabel.Text = "";

            Session.Remove("UserId");
            Session.Remove("UserName");

            Response.Redirect("MainPage.aspx");
        }
    }
}