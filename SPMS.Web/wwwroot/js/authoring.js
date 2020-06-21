jQuery.fn.extend({
    autoHeight: function () {
        function autoHeight_(element) {
            return jQuery(element).css({
                'height': 'auto',
                'overflow-y': 'hidden'
            }).height(element.scrollHeight);
        }
        return this.each(function () {
            autoHeight_(this).on('input', function () {
                autoHeight_(this);
            });
        });
    }
});
$('#Content').autoHeight();
let timeOut = 60000;
let autoSaveId = 0;



function autoSave() {
    console.log('autoSave ' + autoSaveId);
    var dirtyElements = $('#authorpost').find('[data-dirty=true]').add('[form=authorpost][data-dirty=true]');
    var hiddenElements = $('#authorpost').find('[type=hidden]').add('[form=authorpost][type=hidden]');

    var elementsToPost = $.merge(dirtyElements, hiddenElements);

    console.log(dirtyElements);
    if (dirtyElements.length > 0)
    {
        //window.clearInterval(autoSaveId);
        $('#saving').toggleClass('d-none');
        var data = elementsToPost.serialize();
        $.post('post/autosave',
            data,
            function(data) {
                $('#Id').val(data);
                dirtyElements.attr('data-dirty', false);
                $('#lastSave').removeClass('d-none');
                //alert('data saved successfully');
                var today = new Date();
                var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
                var time = (today.getHours() < 10 ? '0' : '') + today.getHours() + ":" + (today.getMinutes() < 10 ? '0' : '') + today.getMinutes() + ":" + (today.getSeconds() < 10 ? '0' : '') + today.getSeconds();
                var dateTime = date + ' ' + time;
                $('#lastSaveTime').text(dateTime);
                $('#saving').toggleClass('d-none');
                //autoSaveId = window.setInterval(autoSave(), timeOut);
            });
    }
}


$(function () {
    var formElements = $('#authorpost')
        .find('input, select, textarea')
        .add('[form=authorpost]')
        .not(':disabled')
        .each(function () {
            $(this).attr('data-dirty', false).change(function () {
                $(this).attr('data-dirty', true);
            });
        });
});

autoSaveId = window.setInterval(autoSave(), timeOut); // 60 seconds
