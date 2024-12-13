<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="TicketsBySessions.aspx.cs" Inherits="Cinema.Web.Site.TicketsBySessions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .styled-add-button {
            background-color: #4CAF50; /* Зеленый цвет кнопки */
            color: white; /* Белый текст */
            border: none; /* Без границ */
            padding: 10px 20px; /* Отступы */
            font-size: 16px; /* Размер текста */
            cursor: pointer; /* Указатель */
            border-radius: 5px; /* Скругленные края */
            transition: 0.3s ease-in-out; /* Анимация при наведении */
        }
    
        .styled-add-button:hover {
            background-color: #45a049; /* Темнее на hover */
        }
    
        .form-container {
            text-align: center;
        }
    
        .form-table {
            margin: 0 auto;
            padding: 10px;
            border-collapse: separate;
            border-spacing: 10px;
        }
    
        .form-label {
            font-weight: bold;
            text-align: right;
        }
    
        .form-input {
            width: 330px;
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
        }
        .form-input::placeholder {
            color: gainsboro;
            font-size: 14px;
            font-style: italic;
            opacity: 1;
        }
    
        .form-error {
            color: #FF3300;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-bottom: 80px; margin-top: 10px;">
        <p style="display: flex; justify-content: center; align-items: center;">
            <asp:Button 
                ID="AddTicket" 
                runat="server" 
                Text="Добавить новый билет" 
                CssClass="styled-add-button" 
                OnClick="AddTicket_Click"/>
        </p>
        <asp:Panel ID="AddTicketPanel" runat="server" Visible="false">
            <table class="form-table">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="form-label">Место</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox BorderColor="Black" ID="SeatBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="Поле обязательно"
                            EnableClientScript="False"
                            ControlToValidate="SeatBox" 
                            CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" CssClass="form-label">Цена</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox BorderColor="Black" ID="PriceBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredDate" runat="server"
                            ErrorMessage="Поле обязательно"
                            EnableClientScript="False"
                            ControlToValidate="PriceBox" CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="1">
                        <asp:Button style="margin-top: 15px;" BackColor="White" ForeColor="Black" ID="SaveBtn" runat="server" 
                            Text="Сохранить" OnClick="SaveBtn_Click" />

                        <asp:Button style="margin-top: 15px;" BackColor="White" ForeColor="Black" ID="CloseSaveBtn" runat="server" 
                            Text="Скрыть" OnClick="CloseSaveBtn_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>
            <p style="text-align: center;">
                <asp:Label Font-Size="16px" ID="ErrorLabel" CssClass="form-error" runat="server"></asp:Label>
            </p>
        </asp:Panel>
    </div>

    <p style="text-align: center;">
        <asp:Label ID="SessionTitle" runat="server"></asp:Label>
    </p>
    <p style="text-align: center;">
        <asp:GridView ID="TicketsGridView" 
                runat="server" 
                HorizontalAlign="Center"
                CellPadding="8" 
                BorderWidth="2" 
                ForeColor="#66ffff" 
                BackColor="White"
                AllowSorting="True" 
                AutoGenerateColumns="False" 
                DataKeyNames="ticket_id">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ControlStyle-ForeColor="Black" SelectText="Выбор" />
                <asp:BoundField DataField="ticket_id" HeaderText="ID Билета" InsertVisible="False" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="seat" HeaderText="Место" />
                <asp:BoundField DataField="price" HeaderText="Цена" DataFormatString="{0:C}" HtmlEncode="false" />
                <asp:BoundField DataField="client" HeaderText="Владение билетом" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
