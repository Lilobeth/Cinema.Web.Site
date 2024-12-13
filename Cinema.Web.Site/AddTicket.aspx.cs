using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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
              Session_type.type as type_,
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
            Inner JOIN Session_type on Session.session_typeID = Session_type.session_typeID
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

            SqlUtils.CompleteConnect(() =>
            {
                var readerDropDown = SqlUtils.CleanExecuteReader("SELECT film_title as FilmName FROM Film");
                DropDownListFilmAdd.DataSource = readerDropDown;
                DropDownListFilmAdd.DataBind();
            });
                      
            SqlUtils.CompleteConnect(() =>
            {
                var readerDropDown = SqlUtils.CleanExecuteReader("SELECT film_title as FilmName FROM Film");
                DropDownListEditFilm.DataSource = readerDropDown;
                DropDownListEditFilm.DataBind();
            });

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader("SELECT hall_name as HallName FROM Hall");
                DropDownListHall.DataSource = reader;
                DropDownListHall.DataBind();
            });
                              
            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader("SELECT hall_name as HallName FROM Hall");
                DropDownListEditHall.DataSource = reader;
                DropDownListEditHall.DataBind();
            });

            SqlUtils.CompleteConnect(() =>
            {
                var readerDropDown = SqlUtils.CleanExecuteReader("Select Session_type.type as TypeName FROM Session_type");
                DropDownListType.DataSource = readerDropDown;
                DropDownListType.DataBind();
            });  

            SqlUtils.CompleteConnect(() =>
            {
                var readerDropDown = SqlUtils.CleanExecuteReader("Select Session_type.type as TypeName FROM Session_type");
                DropDownListEditType.DataSource = readerDropDown;
                DropDownListEditType.DataBind();
            });
        }

        protected void Date_Check(object sender, EventArgs e)
        {
            var value = DropDownList_Sessions.SelectedValue;
            string sqlCmd = SQL_ALL.Replace("ORDER BY session_date, start_time;", "") + $" WHERE (CONVERT(varchar, session_date, 104) = '{value}')"
                + "ORDER BY start_time;";

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(sqlCmd);
                SessionsGridView.DataSource = reader;
                SessionsGridView.DataBind();
            });
        }

        protected void Reset_Click(object sender, EventArgs e)
        {
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

            var sqlGetSession = SQL_ALL.Replace("ORDER BY session_date, start_time;", $"WHERE sessionID = '{value.ToString()}';");

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(sqlGetSession);

                if (reader.Read())
                {
                    EditDateSessiontBox.Text = reader.GetDateTime(2).ToString("yyyy-MM-dd");
                    EditTimeBox.Text = reader.GetTimeSpan(3).ToString("hh\\:mm");
                    EditDurationtBox.Text = reader.GetTimeSpan(4).ToString("hh\\:mm");

                    DropDownListEditFilm.SelectedValue = reader.GetString(0);
                    DropDownListEditHall.SelectedValue = reader.GetString(5);
                    DropDownListEditType.SelectedValue = reader.GetString(7);

                    Session["SessionID"] = reader.GetGuid(1);
                }
            });

            EditSessionPanel.Visible = true;
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            var date = string.Format(DateSessiontBox1.Text, "yyyy-MM-dd HH:mm:ss.fff");
            var startTime = string.Format(TimeBox.Text, "HH:MM:SS.fff");
            var duration = string.Format(DurationtBox.Text, "HH:MM:SS.fff");

            var film = DropDownListFilmAdd.SelectedValue;
            var hall = DropDownListHall.SelectedValue;
            var type = DropDownListType.SelectedValue;

            var film_id = SqlUtils.ExecuteScalar<Guid>($"SELECT * FROM Film WHERE film_title = '{film}'");
            var hall_id = SqlUtils.ExecuteScalar<Guid>($"SELECT * FROM Hall WHERE hall_name = '{hall}'");
            var type_id = SqlUtils.ExecuteScalar<Guid>($"SELECT * FROM Session_type WHERE type = '{type}'");

            var sqlInsert = $"INSERT INTO Session VALUES (NEWID(), '{date}', '{startTime}', '{duration}', '{film_id}', '{hall_id}', '{type_id}');";

            var resultAdd = SqlUtils.ExecuteNotQuery(sqlInsert);

            if (resultAdd > 0)
            {
                Session["LabelMessage"] = "Вы успешно добавили сеанс!";
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                ErrorLabel.Text = "Ошибка добавления. Попробуйте снова";
                return;
            }
        }

        protected void CloseSaveBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            AddPanel.Visible = false;
        }               
        
        protected void CancelEdit_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            EditSessionPanel.Visible = false;
        }

        protected void AddSession_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            AddPanel.Visible = true;
        }

        protected void UpdateSessionBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            var date = string.Format(EditDateSessiontBox.Text, "yyyy-MM-dd HH:mm:ss.fff");
            var startTime = string.Format(EditTimeBox.Text, "HH:MM:SS.fff");
            var duration = string.Format(EditDurationtBox.Text, "HH:MM:SS.fff");

            var film = DropDownListEditFilm.SelectedValue;
            var hall = DropDownListEditHall.SelectedValue;
            var type = DropDownListEditType.SelectedValue;

            var film_id = SqlUtils.ExecuteScalar<Guid>($"SELECT * FROM Film WHERE film_title = '{film}'");
            var hall_id = SqlUtils.ExecuteScalar<Guid>($"SELECT * FROM Hall WHERE hall_name = '{hall}'");
            var type_id = SqlUtils.ExecuteScalar<Guid>($"SELECT * FROM Session_type WHERE type = '{type}'");

            var sqlInsert = $"UPDATE Session SET session_date = '{date}', start_time = '{startTime}', session_duration = '{duration}', filmID = '{film_id}', " +
                            $"hallID = '{hall_id}', session_typeID = '{type_id}' WHERE sessionID = '{Session["SessionID"].ToString()}';";

            Session.Remove("SessionID");

            var resultUpd = SqlUtils.ExecuteNotQuery(sqlInsert);

            if (resultUpd > 0)
            {
                Session["LabelMessage"] = "Вы успешно обновили сеанс!";
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                ErrorLabel.Text = "Ошибка обновления. Попробуйте снова";
                return;
            }
        }
    }                         
}