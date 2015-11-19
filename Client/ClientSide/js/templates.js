/**
 * Created by Ивайло on 12.11.2015 г..
 */
var templates = function() {
    var handlebars = window.handlebars || window.Handlebars,
        cache = {};

    function get(name) {
        var promise = new Promise(function(resolve, reject) {
            if (cache[name]) {
                resolve(cache[name]);
                return;
            }
            var url = "views/" + name + ".handlebars.html";
            $.get(url, function(html) {
                var template = handlebars.compile(html);
                cache[name] = template;
                resolve(template);
            });
        });

        return promise;
    }
    return {
        get: get
    };
}();
