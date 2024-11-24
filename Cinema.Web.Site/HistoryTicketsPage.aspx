<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="HistoryTicketsPage.aspx.cs" Inherits="Cinema.Web.Site.HistoryTicketsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>История билетов пользователя</title>
    <style>
        .form-container {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h4>История оформления билетов</h4>
        <p style="text-align: center;">
            <asp:GridView ID="HistoryTicketsGridView" 
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
                    <asp:CommandField ShowSelectButton="False" ControlStyle-ForeColor="Black" />
                    <asp:BoundField DataField="ticket_id" HeaderText="ID Билета" InsertVisible="False" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="film_title" HeaderText="Название фильма" />
                    <asp:BoundField DataField="category" HeaderText="Возрастное ограничение" />
                    <asp:BoundField DataField="session_duration" HeaderText="Длительность" />
                    <asp:BoundField DataField="session_date" DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" HeaderText="Дата сеанса"/>
                    <asp:BoundField DataField="start_time" HeaderText="Время начала сеанса" DataFormatString="{0:hh\:mm}" HtmlEncode="false" />
                    <asp:BoundField DataField="hall_name" HeaderText="Зал"/>
                    <asp:BoundField DataField="seat" HeaderText="Место" />
                    <asp:BoundField DataField="price" HeaderText="Цена" DataFormatString="{0:C}" HtmlEncode="false" />
                </Columns>
            </asp:GridView>
        </p>
    </div>
</asp:Content>
