<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Films.aspx.cs" Inherits="Cinema.Web.Site.Films" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p style="text-align: center;">Фильмы в прокате</p>
    <p style="text-align: center;">
        
        <asp:DropDownList BackColor="White" ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="genre_name" DataValueField="genre_name">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [genre_name] FROM [Genre]"></asp:SqlDataSource>
        <asp:DropDownList BackColor="White" ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="name_of_country" DataValueField="name_of_country">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [name_of_country] FROM [Country]"></asp:SqlDataSource>
        
    </p>
        <p style="text-align: center;">
        <asp:Button BackColor="White" ForeColor="Black" ID="Button1" runat="server" Text="Поиск по жанру" OnClick="Genre_search"/>
        <asp:Button BackColor="White" ForeColor="Black" ID="Button2" runat="server" Text="Поиск по стране" OnClick="Country_search"/>
        <asp:Button BackColor="White" ForeColor="Black" ID="Button3" runat="server" Text="Поиск по жанру и стране" OnClick="Genre_Country_search"/>
        <asp:Button BackColor="White" ForeColor="Black" ID="ResetButton" runat="server" Text="Сброс" OnClick="Reset_Click"/>
    </p>

    <p style="text-align: center;">
        <asp:GridView ID="FilmsGridView" runat="server" 
            HorizontalAlign="Center"
            CellPadding="8" 
            BorderWidth="2" 
            ForeColor="#66ffff" 
            BackColor="White"
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="filmID">
            <Columns>
                <asp:CommandField ShowSelectButton="False" />
                <asp:BoundField DataField="filmID" HeaderText="filmID" InsertVisible="False" ReadOnly="True" SortExpression="filmID" Visible="False" />
                <asp:BoundField DataField="film_title" HeaderText="Название фильма"/>
                <asp:BoundField DataField="duration" HeaderText="Длительность" />
                <asp:BoundField DataField="genre" HeaderText="Жанр" />
                <asp:BoundField DataField="country" HeaderText="Страна"/>
                <asp:BoundField DataField="director" HeaderText="Режиссер" />
                <asp:BoundField DataField="category" HeaderText="Возрастное ограничение" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
