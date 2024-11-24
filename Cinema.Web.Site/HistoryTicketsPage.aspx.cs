using System;

namespace Cinema.Web.Site
{
    public partial class HistoryTicketsPage : System.Web.UI.Page
    {
        private string getTicketsByUser = @"
            SELECT 
	            Ticket.ticket_id,
	            f.film_title,
	            CONCAT(ag.category, '+') as category,
	            s.session_duration,
	            s.session_date,
	            s.start_time,
                h.hall_name,
	            Ticket.seat,
	            Ticket.price
            FROM Ticket
            Inner JOIN Session s on Ticket.sessionID = s.sessionID
            Inner JOIN Hall h on s.hallID = h.hallID
            Inner JOIN Film f on s.filmID = f.filmID
            Inner JOIN Age_category ag ON f.age_categoryID = ag.age_categoryID
            WHERE Ticket.clientID = '{0}';";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack || Session["UserId"] == null) return;

            var userID = Session["UserId"];

            var getQuery = string.Format(getTicketsByUser, userID);

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(getQuery);
                HistoryTicketsGridView.DataSource = reader;
                HistoryTicketsGridView.DataBind();
            });
        }
    }
}