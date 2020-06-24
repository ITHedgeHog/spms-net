var simplemde = new SimpleMDE({
    autosave: {
        enabled: false,
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

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/authoringHub")
    .build();
connection.start().catch(err => console.error(err));

connection.on("ReceiveText", (text) => {
    //editor.value = text;
    //editor.focus();
    //editor.setSelectionRange(editor.value.length, editor.value.length);

    console.log(text);
    simplemde.value(text);
    simplemde.focus();
});

function change() {
    connection.invoke("SendMessage", simplemde.value()).catch(err => console.error(err));
}