<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="Cinema.Web.Site.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }
        .container {
            max-width: 1000px;
            margin: 20px auto;
            padding: 20px;

        }
        .news-item {
            border-bottom: 1px solid #ddd;
            padding: 20px 0;
        }
        .news-item:last-child {
            border-bottom: none;
        }
        .news-title {
            font-size: 24px;
            color: #333;
        }
        .news-date {
            font-size: 14px;
            color: #888;
        }
        .news-content {
            margin-top: 10px;
            font-size: 16px;
            color: #555;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
    <div class="news-item">
        <div class="news-title">Премьера!</div>
        <div class="news-date">Дата: 15 ноября 2024 г.</div>
        <div class="news-content">
            Приглашаем вас на долгожданную премьеру блокбастера «Мир завтрашнего дня»! Эффекты, захватывающий сюжет и игра звездных актеров — этот фильм нельзя пропустить. Билеты уже в продаже, поспешите забронировать!
        </div>
    </div>

    <div class="news-item">
        <div class="news-title">Детский утренник «Путешествие в мир кино»</div>
        <div class="news-date">Дата: 18 ноября 2024 г.</div>
        <div class="news-content">
            Каждое воскресенье приглашаем юных зрителей и их родителей на детский утренник с мастер-классами и конкурсами. В программе – просмотр анимационного фильма и сюрпризы для всех участников! Начало мероприятия в 11:00. Подробности на кассе.
        </div>
    </div>

    <div class="news-item">
        <div class="news-title">Чёрная пятница: скидки на билеты!</div>
        <div class="news-date">Дата: 24 ноября 2024 г.</div>
        <div class="news-content">
            В честь Чёрной пятницы только 24 ноября скидки до 50% на все билеты! Не упустите шанс посмотреть любимые фильмы по выгодной цене. Скидка действует и на новинки проката.
        </div>
    </div>

    <div class="news-item">
        <div class="news-title">Ретроспектива фильмов Стэнли Кубрика</div>
        <div class="news-date">Дата: 1 - 10 декабря 2024 г.</div>
        <div class="news-content">
            С 1 по 10 декабря в нашем кинотеатре пройдет ретроспектива фильмов Стэнли Кубрика! Это уникальная возможность увидеть культовые шедевры на большом экране и заново открыть для себя творчество великого режиссера. Билеты уже в продаже.
        </div>
    </div>

    <div class="news-item">
        <div class="news-title">Открытие VIP-зала!</div>
        <div class="news-date">Дата: 15 декабря 2024 г.</div>
        <div class="news-content">
            Рады сообщить об открытии нового VIP-зала с комфортными креслами и эксклюзивным меню. Для первых посетителей предусмотрены приятные подарки. Узнайте больше на нашей стойке информации и забронируйте свои места заранее!
        </div>
    </div>

    <div class="news-item">
        <div class="news-title">Специальный показ «Звездные войны: Новый взгляд»</div>
        <div class="news-date">Дата: 20 декабря 2024 г.</div>
        <div class="news-content">
            К 45-летию легендарной франшизы «Звездные войны» состоится специальный показ отреставрированной версии первого фильма! Мы также подготовили тематические декорации и фотозоны для фанатов.
        </div>
    </div>
</div>

</asp:Content>
