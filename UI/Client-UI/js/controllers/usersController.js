var usersController = function() {

    function register(context) {
        templates.get('register')
            .then(function(template) {
                context.$element().html(template());

                $('#register-form-btn').on('click', function() {
                    var user = {
                        email: $('#email').val(),
                        password: $('#pass').val(),
                        confirmPassword: $('#confirmPassword').val(),
                        firstName: $('#first-name').val(),
                        lastName: $('#last-name').val(),
                        age: $('#age').val()
                    };
                    data.users.register(user)
                        .then(function() {
                            context.redirect('#/home');
                            document.location.reload(true);
                        });
                });
            });
    }
    return {
        register: register
    };
}();