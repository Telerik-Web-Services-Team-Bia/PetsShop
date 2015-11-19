var categoriesController = function () {

    function all() {
        var categories;
        data.categories.get()
            .then(function (resCategories) {
                categories = resCategories;
                return templates.get('categoriesMenu');
            })
            .then(function (template) {
                $('#category-menu-container').append(template(categories))
            });
    }

    return {
        all: all,
    };
}();