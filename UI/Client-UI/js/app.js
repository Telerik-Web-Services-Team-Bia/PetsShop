(function () {

    var sammyApp = Sammy('#viewContainer', function () {
        var $content = $('#viewContainer');

        this.get('#/', function(){
                this.redirect('#/home');
            });

        this.get('#/home', homeController.all);
        this.get('#/pets/:id', petsController.details);
        this.get('#/register', usersController.register);
        this.get('#/pets', petsController.all);
        this.get('#/threads/add', threadsController.add);
        

        this.get('#/users', usersController.all);
        this.get('#/users/:id', usersController.register);
        this.get('#/notifications', notificationsController.all);
    });

    $(function () {
        sammyApp.run('#/');

        if (data.users.current()) {
            $('#btn-login').hide();
            $('#tb-user').hide();
            $('#tb-pass').hide();
            $('#reg-user').hide();
            $('#username-nav').html(localStorage.getItem('username-key'));
            $('#greeting').html(localStorage.getItem('username-key'));
            $('#btn-logout').on('click', function () {
                data.users.logout();
                document.location.reload();
                location = '#/';
                toastr.info('Good Buy');
            });
        } else {
            $('#btn-logout').hide();
            $('#username-nav').hide();
            $('#btn-login').on('click', function () {
                var user = {
                    email: $('#tb-user').val(),
                    password: $('#tb-pass').val()
                };
                data.users.login(user)
                    .then(function () {
                        location = '#/';
                        document.location.reload(true);
                        toastr.success('Hello');
                    });
            });
        }
    });
}());
