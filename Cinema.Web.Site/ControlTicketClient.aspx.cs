using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cinema.Web.Site
{
    public partial class ControlTicketClient : System.Web.UI.Page
    {
        private string getTicketsSql = @"
            SELECT 
                Ticket.ticket_id,
                f.film_title,
                s.session_duration,
                s.session_date,
                s.start_time,
				h.hall_name,
                Ticket.seat,
                Ticket.price,
	            CONCAT(cl.client_surname, ' ', cl.client_name) as client
            FROM Ticket
            Inner JOIN Client cl on Ticket.clientID = cl.clientID
            Inner JOIN Session s on Ticket.sessionID = s.sessionID
			Inner JOIN Hall h on s.hallID = h.hallID
            Inner JOIN Film f on s.filmID = f.filmID;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack || Session["UserId"] == null)
                return;

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(getTicketsSql);

                TicketsGridView.DataSource = reader;
                TicketsGridView.DataBind();
            });
        }

        protected void TicketsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (GridView)sender;
            var value = (Guid)list.SelectedValue;

            Session["TicketId"] = value;

            var getTicketDetails = getTicketsSql.Replace(";", " ") + $" WHERE ticket_id = '{value}'";

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CleanExecuteReader(getTicketDetails);
                TicketDetails.DataSource = reader;
                TicketDetails.DataBind();
            });

            DropDownList_Control.Visible = true;
            SaveBtn.Visible = true;
            CloseDropDown.Visible = true;
        }

        protected void CloseDropDown_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                return;

            TicketDetails.DataSource = "";
            TicketDetails.DataBind();

            DropDownList_Control.Visible = false;
            SaveBtn.Visible = false;
            CloseDropDown.Visible = false;
            Session.Remove("TicketId");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                return;

            var ticketID = Session["TicketId"];

            var value = DropDownList_Control.SelectedValue;

            if (value == "Отказать")
            {
                var upd = $"UPDATE Ticket SET clientID = NULL WHERE ticket_id = '{ticketID}'";

                var rowUpd = SqlUtils.ExecuteNotQuery(upd);

                if (rowUpd > 0)
                {
                    Session["LabelMessage"] = "В броне билета успешно отказано";
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    return;
                }
            }

            else if (value == "Принять бронь")
            {
                Session["LabelMessage"] = "Бронь билета успешно принята";
                Response.Redirect(Request.RawUrl);
            }

            DropDownList_Control.Visible = false;
            SaveBtn.Visible = false;
            CloseDropDown.Visible = false;
            Session.Remove("TicketId");
        }
    }
}