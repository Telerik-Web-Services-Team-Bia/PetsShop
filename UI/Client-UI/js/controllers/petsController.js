var petsController = function () {

    function all(context) {
        var category = getUrlParameter('Category');
        var sortBy = getUrlParameter('sortBy');
        var pets;

        data.pets.get(category, sortBy)
            .then(function (resPets) {
                pets = resPets;
                return templates.get('pets');
            })
            .then(function (template) {
                context.$element().html(template(pets));
                categoriesController.all(context);

                $('#sortBy').change(function () {
                    var url = window.location.href;

                    if ($("#sortBy").val() != 'Select') {
                        if (sortBy !== undefined && sortBy !== '') {
                            url = url.replace(sortBy, $("#sortBy").val());
                        } else if (category !== undefined && category !== '') {
                            url += '&sortBy=' + encodeURIComponent($("#sortBy").val());
                        } else {
                            url += '?sortBy=' + encodeURIComponent($("#sortBy").val());
                        }

                    }

                    url = url.replace(/\&$/, '');
                    window.location.href = url;
                });

                $('.thumbnail').on('click', function (ev) {
                    var url = $(ev.target).parents('.thumbnail').find('a').attr('href');
                    window.location.href = url;
                })

                $('.rating').barrating({
                    theme: 'bootstrap-stars',
                    readonly: true
                }); 
            });        
    }

    function petDetails(context) {
        var pet;
        var id = this.params.id;
        data.pets.getById(id)
            .then(function (res) {
                pet = res[0];
                return templates.get('petDetails');
            })
            .then(function (template) {
                context.$element().html(template(pet));

                $('#rating').barrating({
                    theme: 'bootstrap-stars',
                    onSelect:function(value) {

                        if (data.users.hasUser()) {
                            var rating = {
                                petId: id.substring(1),
                                value: value
                            };

                            data.ratings.add(rating)
                                .then(function () {
                                    toastr.success('Pet rated!');
                                })
                                .catch(function (resp) {
                                    toastr.error(resp);
                                });                                                   
                        } else {
                            toastr.error('Only registered users can rate');
                        }
                    }
                });
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
                            .then(function () {
                                toastr.success('Successfully added pet!');
                                context.redirect('#/pets');
                            })
                            .catch(function (resp) {
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