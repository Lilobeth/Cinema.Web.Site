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
              h.hall_name
            FROM Session
            Inner JOIN Film f on Session.filmID = f.filmID
            Inner JOIN Hall h on Session.hallID = h.hallID
            ORDER BY session_date;";

        private const string SQL_SESSION_OPTIONS = "SELECT DISTINCT session_date FROM Session;";

        private readonly SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            Page_Load(sender, e, DropDownList_Sessions);
        }

        protected void Page_Load(object sender, EventArgs e, DropDownList DropDownList_Sessions)
        {
            if (IsPostBack) return;
            connection.Open();

            SqlCommand cmd = new SqlCommand(SQL_ALL, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            SessionsGridView.DataSource = reader;
            SessionsGridView.DataBind();

            connection.Close();

            connection.Open();

            SqlCommand cmdDd = new SqlCommand(SQL_SESSION_OPTIONS, connection);
            SqlDataReader readerDd = cmdDd.ExecuteReader();

            DropDownList_Sessions.DataSource = readerDd;
            DropDownList_Sessions.DataBind();

            connection.Close();
        }
        protected void DropDownList_SessionDateIndexChanged(object sender, EventArgs e)
        {
            DropDownList list = (DropDownList)sender;
            string value = (string)list.SelectedValue;

            string sqlCmd = SQL_ALL.Replace("ORDER BY session_date;", "") + $" WHERE (session_date= '{value}');";

            connection.Open();

            SqlCommand cmdDd = new SqlCommand(sqlCmd, connection);
            SqlDataReader readerDd = cmdDd.ExecuteReader();

            SessionsGridView.DataSource = readerDd;
            SessionsGridView.DataBind();

            connection.Close();
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand cmdDd = new SqlCommand(SQL_ALL, connection);
            SqlDataReader readerDd = cmdDd.ExecuteReader();

            SessionsGridView.DataSource = readerDd;
            SessionsGridView.DataBind();

            SessionDetails.DataSource = null;
            SessionDetails.DataBind();

            connection.Close();
        }

        protected void SessionGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (GridView)sender;
            var value = (int)list.SelectedValue;

            string sqlSessionByID = SQL_ALL.Replace("ORDER BY session_date;", "") + $" WHERE (sessionID = '{value}');";
            connection.Open();

            SqlCommand cmdDd = new SqlCommand(sqlSessionByID, connection);
            SqlDataReader reader = cmdDd.ExecuteReader();

            SessionDetails.DataSource = reader;
            SessionDetails.DataBind();

            connection.Close();
        }
    }
}