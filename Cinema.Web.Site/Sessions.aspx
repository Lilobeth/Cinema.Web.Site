<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Sessions.aspx.cs" Inherits="Cinema.Web.Site.Sessions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<p style="text-align: center;">
    Сеансы
</p>
<p style="text-align:center">
    <asp:DropDownList
        ID="DropDownList_Sessions" 
        runat="server" 
        AutoPostBack="True"  
        DataTextField="session_date" 
        DataValueField="session_date" 
        Width="200px"
        OnSelectedIndexChanged="DropDownList_SessionDateIndexChanged"
    />
    <asp:Button BackColor="White" ForeColor="Black" ID="ResetButton" runat="server" Text="Сброс" OnClick="Reset_Click"/>
</p>
<p style="text-align: center;">
<asp:GridView 
    ID="SessionsGridView" runat="server" 
    HorizontalAlign="Center"
    CellPadding="8" 
    BorderWidth="2" 
    ForeColor="#66ffff" 
    BackColor="White"
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sessionID">
    <Columns>
     <asp:CommandField ShowSelectButton="True" />
     <asp:BoundField DataField="film_title" HeaderText="Название фильма" />
     <asp:BoundField DataField="sessionID" HeaderText="sessionID" InsertVisible="False" ReadOnly="True" SortExpression="sessionID" Visible="False" />
     <asp:BoundField DataField="session_date" DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" HeaderText="Дата сеанса"/>
     <asp:BoundField DataField="start_time" HeaderText="Время начала сеанса" />
     <asp:BoundField DataField="session_duration" HeaderText="Длительность" />
     <asp:BoundField DataField="hall_name" HeaderText="Наименование зала"/>
    </Columns>
</asp:GridView>
</p>
</asp:Content>
