var petsController = function () {

    function all(context) {
        var pets;
        data.pets.get()
            .then(function (resPets) {
                pets = resPets;
                return templates.get('pets');
            })
            .then(function (template) {
                context.$element().html(template(pets));
            });
    }

    function petDetails(context) {
        var pet;
        data.pets.getById(this.params.id)
            .then(function (res) {
                pet = res[0];
                return templates.get('petDetails');
            })
            .then(function (template) {
                context.$element().html(template(pet));
            });
    }

    function add(context) {

        if (data.users.hasUser()) {
            templates.get('addPet')
                .then(function (template) {
                    context.$element().html(template());

                    $('#submitOffer').on('click', function () {

                        var pet = {
                            Category: $('#category').val(),
                            Species: $('#species').val(),
                            Name: $('#name').val(),
                            Color: $('#color').val(),
                            BirthDate: $('#birthdate').val(),
                            IsVaccinated: $('input[name=vaccinated]:checked', '#petForm').val(),
                            Description: $('#description').val(),
                            Price: $('#price').val(),
                            Image: $('#image').val()
                        }; 

                        // TODO validations

                        data.pets.add(pet)
                            .then(function() {
                                toastr.success('Successfully added pet!');
                                context.redirect('#/pets');
                            })
                            .catch(function(resp) {
                                toastr.error('Error adding pet offer!')
                            });
                    });
                });
        } else {
            context.redirect('#/pets');
            toastr.error('You should be logged to upload an offer');
        }
    }

    return {
        all: all,
        add: add,
        details: petDetails
    };
}();