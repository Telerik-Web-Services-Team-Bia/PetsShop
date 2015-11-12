var petsController = function () {

// Example ------------------------------------------------
    function all(context) {
        var pets;
        data.pets.get()
            .then(function (resPets) {
                pets = resPets;
                return templates.get('pets');
            })
            .then(function (template) {
                context.$element().html(template(pets));
                toastr.success('Hello posts');
            });
    }

    function petDetails(context) {
        var pet;
        data.pets.getById(this.params.id)
            .then(function (res) {
                pet = res[0];
                console.log(pet);
                return templates.get('petDetails');
            })
            .then(function (template) {
                context.$element().html(template(pet));
            });
    }
// -----------------------------------------------------------------
    function add(context) {
        templates.get('threadAdd')
            .then(function (template) {
                context.$element().html(template());
                $('#btn-add-thread').on('click', function () {
                    var title = $('#tb-thread-title').val();
                    data.threads.add(title)
                        .then(function () {
                            context.redirect('#/threads');
                        });
                });
            });
    }

    return {
        all: all,
        add: add,
        details: petDetails
    };
}();