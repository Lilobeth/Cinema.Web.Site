﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cinema.Web.Site
{
    public partial class MainPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LabelMessage"] != null)
            {
                LabelMessage.Text = Session["LabelMessage"].ToString();
                Session.Remove("LabelMessage");
            }

            if (Session["UserId"] != null)
            {
                DynamicNameLabel.Text = $"Hi, {Session["UserName"]}!";
                LoginLabel.Visible = false;
                PasswordLabel.Visible = false;

                TextBox1.Visible = false;
                TextBox2.Visible = false;
                RequiredLog.Enabled = false;
                RequiredPassWord.Enabled = false;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Films.aspx");
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sessions.aspx");
        }        
        
        protected void RegisterRedirect(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPage.aspx");
        }

        protected void ProfileBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
            {
                return;
            }

            Response.Redirect("Profile.aspx");
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            var login = TextBox1.Text;
            var password = TextBox2.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return;

            var getUser = $"SELECT * FROM Client WHERE login = '{login}' OR email = '{login}'";

            Guid? id = null;
            string passwordDb = "";
            string name = "";

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CompleteCommand(getUser);
                
                if (reader != null)
                {
                    id = reader.GetGuid(0);
                    name = reader.GetString(2);
                    passwordDb = reader.GetString(6);
                }
            });
 
            if (id is null || passwordDb != password)
            {
                LabelMessage.Text = "Неверный логин или пароль";
                return;
            }

            Session["UserId"] = id.Value;
            Session["UserName"] = name;

            DynamicNameLabel.Text = $"Привет, {Session["UserName"]}!";

            LoginLabel.Visible = false;
            PasswordLabel.Visible = false;

            TextBox1.Visible = false;
            TextBox2.Visible = false;
            RequiredLog.Enabled = false;
            RequiredPassWord.Enabled = false;
        }

        protected void ExitBtn_Click(object sender, EventArgs e)
        {
            Session.Remove("UserId");
            Session.Remove("UserName");

            TextBox1.Visible = true;
            TextBox2.Visible = true;
            LoginLabel.Visible = true;
            PasswordLabel.Visible = true;

            RequiredLog.Enabled = true;
            RequiredPassWord.Enabled = true;

            LabelMessage.Text = "";

            Response.Redirect("MainPage.aspx");
        }
    }
}