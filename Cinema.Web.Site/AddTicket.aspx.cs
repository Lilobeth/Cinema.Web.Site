using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cinema.Web.Site
{
    public partial class AddTicket : System.Web.UI.Page
    {
        string SQL_ALL = @"
            SELECT 
              f.film_title,
              Session.sessionID,
              Session.session_date,
              Session.start_time,
              Session.session_duration,
              h.hall_name,
              g.genre_name as genre,
	          c.name_of_country as country,
	          CONCAT(d.director_surname , ' ', d.director_name) as director,
	          CONCAT(ac.category, '+') as category
            FROM Session
            Inner JOIN Film f on Session.filmID = f.filmID
            Inner JOIN Hall h on Session.hallID = h.hallID
            Inner JOIN Genre g on f.genreID = g.genreID
            Inner JOIN Country c on f.countryID = c.countryID 
            Inner JOIN Director d on f.directorID = d.directorID 
            Inner JOIN Age_category ac on f.age_categoryID = ac.age_categoryID
            ORDER BY session_date, start_time;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(SQL_ALL);
                SessionsGridView.DataSource = reader;
                SessionsGridView.DataBind();
            });
        }

        protected void Date_Check(object sender, EventArgs e)
        {
            var value = DropDownList_Sessions.SelectedValue;
            string sqlCmd = SQL_ALL.Replace("ORDER BY session_date, start_time;", "") + $" WHERE (CONVERT(varchar, session_date, 104) = '{value}')"
                + "ORDER BY start_time;";

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(SQL_ALL);
                SessionsGridView.DataSource = reader;
                SessionsGridView.DataBind();
            });
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            SessionDetails.DataSource = "";
            SessionDetails.DataBind();

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(SQL_ALL);
                SessionsGridView.DataSource = reader;
                SessionsGridView.DataBind();
            });
        }

        protected void SessionsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (GridView)sender;
            var value = (Guid)list.SelectedValue;

            string sqlSessionByID = SQL_ALL.Replace("ORDER BY session_date, start_time;", "") + $" WHERE (sessionID = '{value}');";

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(sqlSessionByID);
                SessionDetails.DataSource = reader;
                SessionDetails.DataBind();
            });
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;
        }

        protected void CloseSaveBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            AddPanel.Visible = false;
        }

        protected void AddSession_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            AddPanel.Visible = true;

            SqlUtils.CompleteConnect(() =>
            {
                var readerDropDown = SqlUtils.CompleteCommand("SELECT film_title as FilmName FROM Film");
                DropDownListFilmAdd.DataSource = readerDropDown;
                DropDownListFilmAdd.DataBind();
            });

            SqlUtils.CompleteConnect(() =>
            {
                var readerDropDown = SqlUtils.CompleteCommand(
                    "SELECT hall_name as HallName FROM Hall");
                DropDownListHall.DataSource = readerDropDown;
                DropDownListHall.DataBind();
            }); 

            SqlUtils.CompleteConnect(() =>
            {
                var readerDropDown = SqlUtils.CompleteCommand("Select Session_type.type as TypeName FROM Session_type");
                DropDownListType.DataSource = readerDropDown;
                DropDownListType.DataBind();
            });
        }
    }                         
}