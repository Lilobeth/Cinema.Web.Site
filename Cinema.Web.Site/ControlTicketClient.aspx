<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ControlTicketClient.aspx.cs" Inherits="Cinema.Web.Site.ControlTicketClient" %>
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
     <h4 style="text-align: center;">Заявки на покупку билетов</h4>
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
                DataKeyNames="ticket_id" OnSelectedIndexChanged="TicketsGridView_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ControlStyle-ForeColor="Black" SelectText="Выбор" />
                <asp:BoundField DataField="ticket_id" HeaderText="ID Билета" InsertVisible="False" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="film_title" HeaderText="Фильм" />
                <asp:BoundField DataField="session_duration" HeaderText="Длительность фильма" />

                <asp:BoundField DataField="session_date" DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" HeaderText="Дата сеанса"/>
                <asp:BoundField DataField="start_time" HeaderText="Время начала сеанса" DataFormatString="{0:hh\:mm}" HtmlEncode="false" />

                <asp:BoundField DataField="hall_name" HeaderText="Зал"/>
                <asp:BoundField DataField="seat" HeaderText="Место" />
                <asp:BoundField DataField="price" HeaderText="Цена" DataFormatString="{0:C}" HtmlEncode="false" />
                <asp:BoundField DataField="client" HeaderText="Владение билетом" />
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:DetailsView ID="TicketDetails" runat="server" 
            AutoGenerateRows="False" 
            DataKeyNames="ticket_id"  
            Height="50px" 
            Width="500px"
            CellPadding="8"
            HorizontalAlign="Center">
            <Fields>
                <asp:BoundField DataField="film_title" HeaderText="Фильм" />
                <asp:BoundField DataField="session_duration" HeaderText="Длительность фильма" />
                <asp:BoundField DataField="client" HeaderText="Владение билетом" />
            </Fields>
        </asp:DetailsView>
    </p>
    <p style="text-align: center;">
        <asp:DropDownList Visible="false" BackColor="White" ID="DropDownList_Control" runat="server">
            <asp:ListItem Text="Принять бронь" Value="Принять бронь"></asp:ListItem>
            <asp:ListItem Text="Отказать" Value="Отказать"></asp:ListItem>
        </asp:DropDownList>


        <asp:Button style="margin-top: 15px;" BackColor="White" ForeColor="Black" ID="SaveBtn" runat="server" 
                    Text="Сохранить" OnClick="SaveBtn_Click" Visible="false"/>

        <asp:Button style="margin-top: 15px; text-align: center;" BackColor="White" ForeColor="Black" ID="CloseDropDown" runat="server" 
                    Text="Скрыть" OnClick="CloseDropDown_Click" Visible="false"/>
    </p>
</asp:Content>
