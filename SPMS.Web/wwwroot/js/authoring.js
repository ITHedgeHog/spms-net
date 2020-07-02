var simplemde = new SimpleMDE({
    autosave: {
        enabled: false
    },
    element: $("#Content")[0]
});

simplemde.codemirror.on("keyup", function () {
    change();
});

simplemde.codemirror.on("keyHandled", function () {
    change();
});

simplemde.codemirror.on("click", function (stuff) {
    console.log(stuff);
});

function getChannel() {
    return  "authoring-" + $("#Id").val();
}

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/authoringHub")
    .build();
connection.start().then(function() {
    connection.invoke("JoinGroup", getChannel()).catch((err) => { console.error(err)});
}).catch(function (err) { console.error(err) });

connection.on("ReceiveText", (text) => {
    //editor.value = text;
    //editor.focus();
    //editor.setSelectionRange(editor.value.length, editor.value.length);

    console.log(text);
    simplemde.value(text);
    simplemde.focus();
});

connection.on("AddPlayer",
    (text) => {
        $('#author-' + text).removeClass("d-none");
    });

connection.on("RemovePlayer",
    (text) => {
        $('#author-' + text).addClass("d-none");
    });

connection.on("disconnected",
    function() {
        setTimeout(function() {
                connection.start().then(function() {
                    connection.invoke("JoinGroup", getChannel()).catch((err) => { console.error(err) });
                }).catch(function(err) { console.error(err) });
            },
            5000);
    }); // Restart connection after 5 seconds.

function change() {
    connection.invoke("SendMessage", simplemde.value(), getChannel()).catch(err => console.error(err));
}

function autoSave() {
    var elements = $('#authorpost').find('[data-autosave=true]').add('[form=authorpost][data-autosave=true]');
    if (elements.length > 0) {
        $('#saving').toggleClass('d-none');
        var data = elements.serialize() + encodeURI('&Content=' + simplemde.value());
       
        $.post('/player/author/post/autosave', data, function (value) {
            $('#lastSave').removeClass('d-none');
            var today = new Date();
            var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
            var time = (today.getHours() < 10 ? '0' : '') + today.getHours() + ":" + (today.getMinutes() < 10 ? '0' : '') + today.getMinutes() + ":" + (today.getSeconds() < 10 ? '0' : '') + today.getSeconds();
            var dateTime = date + ' ' + time;
            $('#lastSaveTime').text(dateTime);
            $('#saving').toggleClass('d-none');
        });
    }
}

function leaveAuthoringPage() {
    autoSave();
    connection.server.leaveGroup(getChannel());
}