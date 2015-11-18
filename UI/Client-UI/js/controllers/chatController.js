/**
 * Created by nikol on 18.11.2015 �..
 */
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