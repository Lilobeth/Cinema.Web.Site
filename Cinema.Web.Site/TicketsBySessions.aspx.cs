using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cinema.Web.Site
{
    public partial class TicketsBySessions : System.Web.UI.Page
    {
        private string getTicketsSql = @"
            SELECT 
                Ticket.ticket_id,
                f.film_title,
                Ticket.seat,
                Ticket.price,
                s.session_date,
                s.start_time,
                s.session_duration,
	            CONCAT(cl.client_surname, ' ', cl.client_name) as client
            FROM Ticket
            LEFT JOIN Client cl on Ticket.clientID = cl.clientID
            Inner JOIN Session s on Ticket.sessionID = s.sessionID
            Inner JOIN Film f on s.filmID = f.filmID
            WHERE Ticket.sessionID = '{0}';";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            var query = Request.QueryString["sessionId"];

            if (string.IsNullOrEmpty(query))
            {
                Response.Redirect("AddTicket.aspx");
                return;
            }

            var getTicket = string.Format(getTicketsSql, query);

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(getTicket);

                TicketsGridView.DataSource = reader;
                TicketsGridView.DataBind();
            });

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(getTicket);

                if (reader.Read())
                {
                    var date = reader.GetDateTime(4).ToString("yyyy-MM-dd");
                    var time = reader.GetTimeSpan(5).ToString("hh\\:mm");
                    var film = reader.GetString(1);
                    var duration = reader.GetTimeSpan(6).ToString("hh\\:mm");

                    SessionTitle.Text = $"Билеты на сеанс {date} ({time}) фильма '{film}'. Длительность фильма: {duration}";
                }
            });
        }

        protected void AddTicket_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                return;

            AddTicketPanel.Visible = true;
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            var query = Request.QueryString["sessionId"];

            if (string.IsNullOrEmpty(query))
                return;

            var getBySeat = $"SELECT * FROM Ticket WHERE sessionID = '{query}' AND seat = {SeatBox.Text}";

            var getRow = SqlUtils.ExecuteScalar<Guid?>(getBySeat);

            if (getRow != null)
            {
                ErrorLabel.Text = $"За местом (Номер - {SeatBox.Text}) уже есть билет";
                return;
            }

            var insertSQL = $"INSERT INTO Ticket VALUES (NEWID(), '{Guid.Parse(query)}', {SeatBox.Text}, {PriceBox.Text}, NULL);";

            var insertRow = SqlUtils.ExecuteNotQuery(insertSQL);

            if (insertRow > 0)
            {
                Session["LabelMessage"] = "Вы успешно добавили билет на сеанс!";
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                ErrorLabel.Text = "Ошибка обновления. Попробуйте снова";
                return;
            }
        }

        protected void CloseSaveBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] is null)
                return;

            AddTicketPanel.Visible = false;
        }
    }
}