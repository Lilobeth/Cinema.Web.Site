<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="BuyTicketInSession.aspx.cs" Inherits="Cinema.Web.Site.BuyTicketInSession" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Покупка билета</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                OnRowCommand="TicketsGridView_RowCommand"
                DataKeyNames="ticket_id">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ControlStyle-ForeColor="Black" SelectText="Купить" />
                <asp:BoundField DataField="ticket_id" HeaderText="ID Билета" InsertVisible="False" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="seat" HeaderText="Место" />
                <asp:BoundField DataField="price" HeaderText="Цена" DataFormatString="{0:C}" HtmlEncode="false" />
                <asp:BoundField DataField="session_duration" HeaderText="Длительность фильма" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
