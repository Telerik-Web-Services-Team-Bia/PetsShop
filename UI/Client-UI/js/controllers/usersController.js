var usersController = function () {

    var register = function (context) {
        templates.get('register')
            .then(function (template) {
                context.$element().html(template());

                $('#register-form-btn').on('click', function() {
                    var user = {
                        email: $('#userEmail').val(),
                        password: $('#pass').val(),
                        confirmPassword: $('#confirmPassword').val(),
                        firstName: $('#first-name').val(),
                        lastName: $('#last-name').val(),
                        age: $('#age').val()
                    };

                    if(!validator.validateUserEmail(user.email)) {
                        toastr.error('Email is not valid');
                        return;
                    }

                    if(!validator.validatePasswordLength(user.password)) {
                        toastr.error('Password must be at least 6 characters long');
                        return;
                    } 

                    if(!validator.validatePasswordConformation(user.password, user.confirmPassword)) {
                        toastr.error('Passwords does not match');
                        return;
                    } 

                    if(user.age == '') {
                        toastr.error('Age is required');
                        return;
                    } 

                    data.users.register(user)
                        .then(function() {
                            context.redirect('#/home');
                            toastr.success('Successfully registered!');
                        })
                        .catch(function(resp) {
                            toastr.error('User with that username already exists!')
                        });
                });
            });
    }
    
    var login = function () {
        var user = {
            email: $('#email').val(),
            password: $('#password').val()
        }
        
        data.users.login(user)
            .then(function() {
                $('#login-form').hide();
                $('#logout-form').show();
                document.location.reload(true);
            }, function(err) {
                $('#login-form').trigger("reset");
                toastr.error('Invalid username or password');
            });
    };
    
    function logout() {
        data.users.logout()
            .then(function() {
                $('#login-form').show();
                $('#logout-form').hide();
                document.location.reload(true);
             });
    }
    
    if(data.users.hasUser()) {
        $('#login-form').addClass('hidden');
    } else {
        $('#logout-form').addClass('hidden');
    }
    
    $('#btn-login').on('click', function() {
        login();
    });
    
    $('#btn-logout').on('click', function() {
        logout();
    });

    return {
        register: register,
        login: login,
        logout: logout
    };
}();