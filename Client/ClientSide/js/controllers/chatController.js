var chatController = function(){

    var init = function(context){
        templates.get('chatTemplate')
            .then(function(template){
                context.$element().html(template());

                chatManager.initialize();
            });

    };

    return {
        init: init
    };
}();