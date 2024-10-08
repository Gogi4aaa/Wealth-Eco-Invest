﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function(message, chatId, owner) {

        var queryString = window.location.search;
        var urlParams = new URLSearchParams(queryString);
        var currentId = urlParams.get("clickedChatId");

        if (currentId === chatId || currentId == null) {

            var p = document.createElement("p");
            var div = document.createElement("div");
            var div2 = document.createElement("div");
            var div3 = document.createElement("div");

            var monthsArray = ["Яну", "Феб", "Мар", "Апр", "Май", "Юни", "Юли", "Авг", "Сеп", "Окт", "Ное", "Дек"];

            var data = new Date();
            var monthName = monthsArray[data.getMonth()];
            var hours = data.getHours();
            var minutes = data.getMinutes();
            div.className = "media mb-3 text-center";
            div2.className = "media-body ml-3";
            div3.className = "rounded py-2 px-3 mb-2 bg-success";
            p.className = "small text-muted";
            var dateText = hours + ":" + minutes + " | " + data.getDate() + " " + monthName;
            p.innerText = dateText;
            div3.innerText = message;
            div2.innerText = owner;

            var mainDiv = document.getElementById("textDiv");
            mainDiv.appendChild(div);
            div.appendChild(div2);
            div2.appendChild(div3);
            div2.appendChild(p);

            var el = document.getElementById("chat-box");
            el.scrollTop = el.scrollHeight;
        }
    });

connection.start().then(function() {
    document.getElementById("sendButton").disabled = false;
}).catch(function(err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click",
    function(event) {

        var message = document.getElementById("messageInput").value;
        var chatId = document.getElementById("chatDiv").value;

        $.ajax({
            type: "GET",
            url: "/Chat/SendMessage?message=" + message + "&chatId=" + chatId,
            contentType: "application/json; charset=utf-8",
            success: function() {
                //tuka logikata za visualization na potrebitelq
                location.reload();
            },
            error: function() {

            }
        });
        event.preventDefault();
    });