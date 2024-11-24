using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cinema.Web.Site
{
    public partial class Sessions : System.Web.UI.Page
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

        private const string SQL_SESSION_OPTIONS = "SELECT DISTINCT session_date FROM Session;";

        private readonly SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);



        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (Session["UserId"] != null)
            {
                var btnField = new ButtonField
                {
                    HeaderText = "Купить билеты",
                    CommandName = "Buy",
                    Text = "Купить",
                    Visible = true,
                };

                SessionsGridView.Columns.Add(btnField);
            }

            connection.Open();

            SqlCommand cmd = new SqlCommand(SQL_ALL, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            SessionsGridView.DataSource = reader;
            SessionsGridView.DataBind();

            connection.Close();

        }
        protected void Date_Check(object sender, EventArgs e)
        {
            var value = DropDownList_Sessions.SelectedValue;
            string sqlCmd = SQL_ALL.Replace("ORDER BY session_date, start_time;", "") + $" WHERE (CONVERT(varchar, session_date, 104) = '{value}')" 
                + "ORDER BY start_time;";

            connection.Open();

            SqlCommand cmdDd = new SqlCommand(sqlCmd, connection);
            SqlDataReader readerDd = cmdDd.ExecuteReader();

            SessionsGridView.DataSource = readerDd;
            SessionsGridView.DataBind();

            connection.Close();
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            SessionDetails.DataSource = "";
            SessionDetails.DataBind();

            connection.Open();

            SqlCommand cmdDd = new SqlCommand(SQL_ALL, connection);
            SqlDataReader readerDd = cmdDd.ExecuteReader();

            SessionsGridView.DataSource = readerDd;
            SessionsGridView.DataBind();

            connection.Close();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["UserId"] == null)
                return;

            if (e.CommandName == "Buy") // Проверяем, что нажата кнопка "Купить билет"
            {
                // Получаем индекс строки, на которой нажата кнопка
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Получаем ID сеанса
                GridViewRow row = SessionsGridView.Rows[rowIndex];

                string sessionId = SessionsGridView.DataKeys[rowIndex].Value.ToString();

                Response.Redirect($"BuyTicketInSession.aspx?sessionId={sessionId}");
            }
        }

        protected void SessionsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (GridView)sender;
            var value = (Guid)list.SelectedValue;

            string sqlSessionByID = SQL_ALL.Replace("ORDER BY session_date, start_time;", "") + $" WHERE (sessionID = '{value}');";
            connection.Open();

            SqlCommand cmdDd = new SqlCommand(sqlSessionByID, connection);
            SqlDataReader reader = cmdDd.ExecuteReader();

            SessionDetails.DataSource = reader;
            SessionDetails.DataBind();

            connection.Close();
        }
    }
}