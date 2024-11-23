<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Sessions.aspx.cs" Inherits="Cinema.Web.Site.Sessions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <p style="text-align: center;">
    Сеансы
</p>
<p style="text-align:center">
    <asp:DropDownList ID="DropDownList_Sessions" runat="server" DataSourceID="SqlDataSource1" DataTextField="session_date" DataValueField="session_date">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cinemaConnectionString %>" SelectCommand="SELECT DISTINCT CONVERT(varchar, session_date, 104) as 'session_date' FROM [Session]"></asp:SqlDataSource>
    <asp:Button BackColor="White" ForeColor="Black" ID="Button1" runat="server" Text="Поиск" OnClick="Date_Check"/>
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
    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sessionID" OnSelectedIndexChanged="SessionsGridView_SelectedIndexChanged">
    <Columns>
     <asp:CommandField ShowSelectButton="True" />
     <asp:BoundField DataField="film_title" HeaderText="Название фильма" />
     <asp:BoundField DataField="sessionID" HeaderText="sessionID" InsertVisible="False" ReadOnly="True" SortExpression="sessionID" Visible="False" />
     <asp:BoundField DataField="session_date" DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" HeaderText="Дата сеанса"/>
     <asp:BoundField DataField="start_time" HeaderText="Время начала сеанса" DataFormatString="{0:hh\:mm}" HtmlEncode="false" />
     <asp:BoundField DataField="session_duration" HeaderText="Длительность" />
     <asp:BoundField DataField="hall_name" HeaderText="Зал"/>
    </Columns>
</asp:GridView>
</p>
<p>
    <asp:DetailsView ID="SessionDetails" runat="server" 
        AutoGenerateRows="False" 
        DataKeyNames="sessionID"  
        Height="50px" 
        Width="500px"
        CellPadding="8"
        HorizontalAlign="Center">
        <Fields>
            <asp:BoundField DataField="genre" HeaderText="Жанр"  />
            <asp:BoundField DataField="country" HeaderText="Страна"  />
            <asp:BoundField DataField="director" HeaderText="Режиссер"  />
            <asp:BoundField DataField="category" HeaderText="Возрастное ограничение" />

        </Fields>
    </asp:DetailsView>
</p>

</asp:Content>
