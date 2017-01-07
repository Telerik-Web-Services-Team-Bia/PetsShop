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
                pet = res;
                pet.BirthDate = new Date(pet.BirthDate).toDateString();
                return templates.get('petDetails');
            })
            .then(function (template) {
                context.$element().html(template(pet));

                gapi.savetodrive.go('drive');

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

                if (pet.Seller !== data.users.current().username) {
                    $('#delete').hide();

                    if (data.users.hasUser()) {
                        $('#buy').on('click', function() {
                            // magic
                            var emailUrl = "https://api.elasticemail.com/v2/email/send?apikey=46d23ca0-3dd2-4d87-bb50-479e9ab5db20&subject=Pet%20bought&from=petstorepiss@gmail.com" +
                                "&to=" + pet.Seller +
                                "&bodyText=" + "Dear%20seller,%20your%20" + pet.Category + '%20' + pet.Name + '%20was%20sold%20for%20' + pet.Price + '$%20to%20' + data.users.current().username;

                            data.email.send(emailUrl)
                                .then(function () {
                                    data.pets.delete(id)
                                        .then(function() {
                                            context.redirect('#/pets');
                                            toastr.success('Pet bought, email was sent to seller!');
                                        })
                                        .catch(function (resp) {
                                            toastr.error(resp);
                                        });

                                })
                                .catch(function (resp) {
                                    toastr.error(resp);
                                });
                        })
                    } else {
                        $('#buy').hide();
                    }

                } else {
                    $('#buy').hide();
                    $('#delete').on('click', function() {
                        data.pets.delete(id)
                            .then(function() {
                                context.redirect('#/pets');
                                toastr.warning('Offer deleted!');
                            })
                            .catch(function (resp) {
                                toastr.error(resp);
                            });
                    })
                }
            });
    }

    function add(context) {

        if (data.users.hasUser()) {
            templates.get('addPet')
                .then(function (template) {
                    context.$element().html(template());

                    var image;

                    $('#image').change(function () {
                            var preview = document.querySelector('img');
                            var file    = document.querySelector('input[type=file]').files[0];
                            var reader  = new FileReader();

                            reader.onloadend = function () {
                                preview.src = reader.result;
                                image = reader.result;
                            }

                            if (file) {
                                reader.readAsDataURL(file);
                            } else {
                                preview.src = "";
                            }
                        })

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
                            image: image.split(',')[1]
                        };

                        console.log(pet);

                        if (!validator.validateTextField(pet.Category)) {
                            toastr.error('Category is required');
                            return;
                        }

                        if (!validator.validateTextField(pet.Species)) {
                            toastr.error('Species is required');
                            return;
                        }

                        if (!validator.validateTextField(pet.Price)) {
                            toastr.error('Price is required');
                            return;
                        }

                        if (!validator.validatePrice(pet.Price)) {
                            toastr.error('Price has to be larger or equal to zero');
                            return;
                        }

                        if (!validator.validateDate(pet.BirthDate)) {
                            toastr.error('Birthdate has to be before today');
                            return;
                        }

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