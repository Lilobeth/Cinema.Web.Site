using System;
using System.Linq;
using System.Net;
using System.Web.UI;

namespace Cinema.Web.Site
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) { return; }

            if (Session["UserId"] is null)
                return;

            var id = (Guid)Session["UserId"];
            string getUser = $"SELECT * FROM Client WHERE clientID = '{id}'";

            string name = "";
            string email = "";
            string phone = "";

            SqlUtils.CompleteConnect(() =>
            {
                var reader = SqlUtils.CompleteCommand(getUser);

                if (reader != null)
                {
                    name = $"{reader.GetString(1)} {reader.GetString(2)}";
                    email = reader.GetString(3);
                    phone = reader.GetString(4);
                }
            });

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(phone))
            {
                return;
            }

            NameBox.Text = name;
            EmailBox.Text = email;
            PhoneBox.Text = phone;
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid || Session["UserId"].ToString() == null)
            {
                return;
            }

            var names = NameBox.Text.Split(' ').ToList();
            var name = names[0];
            var surname = names[1];

            var phone = PhoneBox.Text;

            var id = Session["UserId"].ToString();

            string UPD_USER = @"
                UPDATE Client 
                SET client_surname = '{0}', client_name = '{1}', 
                    phone_number ='{2}' WHERE clientID = '{3}';";

            var updUser = string.Format(UPD_USER, surname, name, phone, id);

            int resultUpd = SqlUtils.ExecuteNotQuery(updUser);

            ErrorLabel.Text = (resultUpd > 0) 
                ? "Вы успешно обновили даннные" 
                : "Ошибка сохранения. Попробуйте снова";
        }
    }
}