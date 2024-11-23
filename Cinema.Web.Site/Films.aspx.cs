using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Cinema.Web.Site
{
    public partial class Films : System.Web.UI.Page
    {
        private string sqlALL = @"
            select
                filmID,
	            film_title,
	            duration,
	            g.genre_name as genre,
	            c.name_of_country as country,
	            CONCAT(d.director_surname , ' ', d.director_name) as director,
	            CONCAT(ac.category, '+') as category
            from Film f 
            JOIN Genre g on f.genreID = g.genreID
            JOIN Country c on f.countryID = c.countryID 
            JOIN Director d on f.directorID = d.directorID 
            JOIN Age_category ac on f.age_categoryID = ac.age_categoryID;";

        private const string SQL_GENRE_OPTIONS = "SELECT DISTINCT genre_name FROM Genre;";
        private const string SQL_COUNTRY_OPTIONS = "SELECT DISTINCT name_of_country FROM Country;";

        private readonly SqlConnection connection = 
            new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            connection.Open();

            SqlCommand cmd = new SqlCommand(sqlALL, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            FilmsGridView.DataSource = reader;
            FilmsGridView.DataBind();

            connection.Close();
        }

        protected void Genre_search(object sender, EventArgs e)
        {
            var value = DropDownList1.SelectedValue;
            string sqlGenres = sqlALL.Substring(0, sqlALL.Length - 1) + $" WHERE (g.genre_name = '{value}');";

            connection.Open();

            SqlCommand cmdDd = new SqlCommand(sqlGenres, connection);
            SqlDataReader readerDd = cmdDd.ExecuteReader();

            FilmsGridView.DataSource = readerDd;
            FilmsGridView.DataBind();

            connection.Close();

        }

        protected void Country_search(object sender, EventArgs e)
        {
            var value = DropDownList2.SelectedValue;
            string sqlCountries = sqlALL.Substring(0, sqlALL.Length - 1) + $" WHERE (c.name_of_country = '{value}');";

            connection.Open();

            SqlCommand cmdDd = new SqlCommand(sqlCountries, connection);
            SqlDataReader readerDd = cmdDd.ExecuteReader();

            FilmsGridView.DataSource = readerDd;
            FilmsGridView.DataBind();

            connection.Close();

        }

        protected void Genre_Country_search(object sender, EventArgs e)
        {
            var value1 = DropDownList1.SelectedValue;
            var value2 = DropDownList2.SelectedValue;
            string sqlCountries = sqlALL.Substring(0, sqlALL.Length - 1) + $" WHERE " +
                $"(g.genre_name = '{value1}' AND c.name_of_country = '{value2}');";

            connection.Open();

            SqlCommand cmdDd = new SqlCommand(sqlCountries, connection);
            SqlDataReader readerDd = cmdDd.ExecuteReader();

            FilmsGridView.DataSource = readerDd;
            FilmsGridView.DataBind();

            connection.Close();

        }


        protected void Reset_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand cmdDd = new SqlCommand(sqlALL, connection);
            SqlDataReader readerDd = cmdDd.ExecuteReader();

            FilmsGridView.DataSource = readerDd;
            FilmsGridView.DataBind();

            connection.Close();
        }
    }
}