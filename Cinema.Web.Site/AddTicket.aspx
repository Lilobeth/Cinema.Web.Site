<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddTicket.aspx.cs" Inherits="Cinema.Web.Site.AddTicket" %>
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
    <h4 style="text-align: center;">Сеансы</h4>

    <div style="margin-bottom: 80px; margin-top: 10px;">
        <p style="display: flex; justify-content: center; align-items: center;">
            <asp:Button 
                ID="AddSession" 
                runat="server" 
                Text="Добавить новый сеанс" 
                CssClass="styled-add-button" 
                OnClick="AddSession_Click"/>
        </p>
        <asp:Panel ID="AddPanel" runat="server" Visible="false">
            <table class="form-table">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="form-label">Дата сеанса</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox BorderColor="Black" TextMode="Date" ID="DateSessiontBox1" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="Поле обязательно"
                            EnableClientScript="False"
                            ControlToValidate="DateSessiontBox1" CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ErrorMessage="Не верный формат даты" 
                            EnableClientScript="False" 
                            ControlToValidate="DateSessiontBox1" 
                            CssClass="form-error"
                            ValidationExpression="[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" CssClass="form-label">Время начала</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox BorderColor="Black" TextMode="Time" ID="TimeBox" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredDate" runat="server"
                            ErrorMessage="Поле обязательно"
                            EnableClientScript="False"
                            ControlToValidate="TimeBox" CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" CssClass="form-label">Длительность</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox BorderColor="Black" ID="DurationtBox" TextMode="Time" runat="server" CssClass="form-input"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="Поле обязательно"
                            EnableClientScript="False"
                            ControlToValidate="DurationtBox" 
                            CssClass="form-error">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" CssClass="form-label">Выберите фильм</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList
                            ID="DropDownListFilmAdd" 
                            runat="server" 
                            AutoPostBack="True"  
                            DataTextField="FilmName" 
                            DataValueField="FilmName" 
                            Width="250px"
                        />   
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" CssClass="form-label">Выберите зал</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList
                            ID="DropDownListHall" 
                            runat="server" 
                            DataTextField="HallName" 
                            DataValueField="HallName" 
                            Width="350px"
                        />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" CssClass="form-label">Выберите тип сеанса</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList
                            ID="DropDownListType" 
                            runat="server" 
                            AutoPostBack="True"  
                            DataTextField="TypeName" 
                            DataValueField="TypeName" 
                            Width="300px"
                        />
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
            <asp:Label Font-Size="16px" ID="ErrorLabel" CssClass="form-error" runat="server"></asp:Label>
        </asp:Panel>
    </div>

    <p style="text-align:center">
        <asp:DropDownList BackColor="White" ID="DropDownList_Sessions" runat="server" DataSourceID="SqlDataSource1" DataTextField="session_date" DataValueField="session_date">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT CONVERT(varchar, session_date, 104) as 'session_date' FROM [Session]"></asp:SqlDataSource>
        <asp:Button BackColor="White" ForeColor="Black" ID="Button4" runat="server" Text="Поиск" OnClick="Date_Check"/>
        <asp:Button BackColor="White" ForeColor="Black" ID="Button5" runat="server" Text="Сброс" OnClick="Reset_Click"/>
    </p>

    <p style="text-align: center;">
        <asp:GridView 
            ID="SessionsGridView" runat="server" 
            HorizontalAlign="Center"
            CellPadding="8" 
            BorderWidth="2" 
            ForeColor="#996633"
            BackColor="White"
            EditRowStyle-BackColor="#ffccff"
            AlternatingRowStyle-BackColor="#ffccff"
            RowStyle-BackColor="#ffccff"
            OnRowCommand="GridViewTickets_RowCommand"
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="sessionID" OnSelectedIndexChanged="SessionsGridView_SelectedIndexChanged">
            <Columns>
                 <asp:CommandField ShowSelectButton="True" ControlStyle-ForeColor="Black" />
                 <asp:BoundField DataField="film_title" HeaderText="Название фильма" />
                 <asp:BoundField DataField="sessionID" HeaderText="sessionID" InsertVisible="False" ReadOnly="True" Visible="False" />
                 <asp:BoundField DataField="session_date" DataFormatString="{0:dd.MM.yyyy}" HtmlEncode="false" HeaderText="Дата сеанса"/>
                 <asp:BoundField DataField="start_time" HeaderText="Время начала сеанса" DataFormatString="{0:hh\:mm}" HtmlEncode="false" />
                 <asp:BoundField DataField="session_duration" HeaderText="Длительность" />
                 <asp:BoundField DataField="hall_name" HeaderText="Зал"/>
            </Columns>
        </asp:GridView>
    </p>
    <asp:Panel ID="EditSessionPanel" runat="server" Visible="false">
        <h4 style="text-align: center;">Редактирование сеанса</h4>
        <table class="form-table">
            <tr>
                <td>
                    <asp:Label ID="LabelEditFilm" runat="server" CssClass="form-label">Фильм</asp:Label>
                </td>
                <td>
                    <asp:DropDownList
                        ID="DropDownListEditFilm" 
                        runat="server" 
                        DataTextField="FilmName" 
                        DataValueField="FilmName" 
                        Width="250px"
                    />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelEditDate" runat="server" CssClass="form-label">Дата сеанса</asp:Label>
                </td>
                <td>
                    <asp:TextBox BorderColor="Black" TextMode="Date" ID="EditDateSessiontBox" runat="server" CssClass="form-input"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ErrorMessage="Поле обязательно"
                        EnableClientScript="False"
                        ControlToValidate="EditDateSessiontBox" CssClass="form-error">
                    </asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                        ErrorMessage="Не верный формат даты" 
                        EnableClientScript="False" 
                        ControlToValidate="EditDateSessiontBox" 
                        CssClass="form-error"
                        ValidationExpression="[0-9]{4}-(0[1-9]|1[012])-(0[1-9]|1[0-9]|2[0-9]|3[01])">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelEditTime" runat="server" CssClass="form-label">Время начала</asp:Label>
                </td>
                <td>
                    <asp:TextBox BorderColor="Black" TextMode="Time" ID="EditTimeBox" runat="server" CssClass="form-input"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ErrorMessage="Поле обязательно"
                        EnableClientScript="False"
                        ControlToValidate="EditTimeBox" CssClass="form-error">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelEditDuration" runat="server" CssClass="form-label">Длительность</asp:Label>
                </td>
                <td>
                    <asp:TextBox BorderColor="Black" TextMode="Time" ID="EditDurationtBox" runat="server" CssClass="form-input"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ErrorMessage="Поле обязательно"
                        EnableClientScript="False"
                        ControlToValidate="EditDurationtBox" CssClass="form-error">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelEditHall" runat="server" CssClass="form-label">Зал</asp:Label>
                </td>
                <td>
                    <asp:DropDownList
                        ID="DropDownListEditHall" 
                        runat="server" 
                        DataTextField="HallName" 
                        DataValueField="HallName" 
                        Width="350px"
                    />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelEditType" runat="server" CssClass="form-label">Тип сеанса</asp:Label>
                </td>
                <td>
                    <asp:DropDownList
                        ID="DropDownListEditType" 
                        runat="server" 
                        DataTextField="TypeName" 
                        DataValueField="TypeName" 
                        Width="300px"
                    />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="UpdateSessionBtn" runat="server" Text="Обновить" OnClick="UpdateSessionBtn_Click"  CssClass="styled-add-button" />
                    <asp:Button ID="CancelEditBtn" runat="server" Text="Отмена" OnClick="CancelEdit_Click" CssClass="styled-add-button" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
