using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cinema.Web.Site
{
    public partial class BuyTicketInSession : System.Web.UI.Page
    {
        private string getFreeTicketsSql = @"
            SELECT 
                Ticket.ticket_id,
	            f.film_title,
	            Ticket.seat,
	            Ticket.price,
	            s.session_date,
	            s.start_time,
	            s.session_duration
            FROM Ticket
            Inner JOIN Session s on Ticket.sessionID = s.sessionID
            Inner JOIN Film f on s.filmID = f.filmID
            WHERE Ticket.clientID IS NULL AND Ticket.sessionID = '{0}';";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            var query = Request.QueryString["sessionId"];

            if (string.IsNullOrEmpty(query))
            {
                Response.Redirect("Sessions.aspx");
                return;
            }

            var getFreeTickets = string.Format(getFreeTicketsSql, query);

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(getFreeTickets);

                TicketsGridView.DataSource = reader;
                TicketsGridView.DataBind();
            });              
            
            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(getFreeTickets);

                if (reader.Read())
                {
                    var date = reader.GetDateTime(4).ToString("yyyy-MM-dd");
                    var time = reader.GetTimeSpan(5).ToString("hh\\:mm");
                    var film = reader.GetString(1);
                    SessionTitle.Text = $"Свободные билеты на сеанс {date} ({time}) фильма '{film}'";
                }
            });
        }

        protected void TicketsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["UserId"] == null)
                return;

            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string ticketId = TicketsGridView.DataKeys[rowIndex].Value.ToString();

                var userID = Session["UserId"];

                string updTicket = $"UPDATE Ticket SET clientID = '{userID}' WHERE ticket_id = '{ticketId}';";

                var resultUpd = SqlUtils.ExecuteNotQuery(updTicket);

                Session["LabelMessage"] = (resultUpd > 0)
                    ? "Вы успешно купили билет!"
                    : "Ошибка. Попробуйте снова";

                if (resultUpd <= 0) return;

                Response.Redirect($"HistoryTicketsPage.aspx");
            }
        }
    }
}