var gameList = document.getElementById("gameList");
GetGamesList();
function GetGamesList() {
    var r = new XMLHttpRequest();
	r.open("POST", "GetGames", true);
    r.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            games = JSON.parse(this.response).data;
            var inner = "";
            for (var i = 0; i < games.length; i++)
                inner += "<div class=\"listItemContainer\">"+
                    "<div class=\"listItem\" onclick=\"SetGame('" + games[i].gameName + "')\">" + games[i].gameName + (games[i].selected ? "*" : "") + "</div>" +
                    "<div class=\"listOption\" onclick=\"Edit('" + games[i].gameName + "')\"> [edit] </div>" +
                    "<div class=\"listOption\" onclick=\"Delete('" + games[i].gameName + "')\"> [delete] </div>" +
                    "</div>"; 
            gameList.innerHTML = inner;
        }
    };
    r.send();
}

function SoftReboot(){Post("SoftReboot",null,null);}
function Reboot(){Post("Reboot",null,null);}

function SetGame(name) {
    var data = new FormData();
    data.append("name", name);
    Post("SetGame", data, GetGamesList);
}

function Delete(name) {
    var data = new FormData();
    data.append("name", name);
    Post("Delete", data, GetGamesList);
}

function AddGame() {
    var addGame = document.getElementById("addGame");
    if (addGame.classList.contains("hiddenMenu")) {
        addGame.classList.remove("hiddenMenu");
    }
    else {
        var name = document.getElementById("name");
        var cmd = document.getElementById("cmd");
        var game = new FormData();
        game.append("name", name.value);
        game.append("command", cmd.value);
        Post("AddGame", game, GetGamesList);
        addGame.classList.add("hiddenMenu");
        cmd.value = "";
        name.value = "";
    }
}

function Post(url, data, onSuccess) {
	var r = new XMLHttpRequest();
	r.open("POST", "http://localhost/"+url, true);
    if (onSuccess != null) r.onreadystatechange = () => { setTimeout(onSuccess, 1); }
    if (data != null) r.send(data);
    else r.send();
}