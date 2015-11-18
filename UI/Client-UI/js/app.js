(function () {
    var sammyApp = Sammy('#viewContainer', function () {
        //var $content = $('#viewContainer');

        this.get('#/', function(){
                this.redirect('#/home');
            });

        this.get('#/home', homeController.all);
        this.get('#/register', usersController.register);
        this.get('#/pets/:id', petsController.details);       
        this.get('#/pets', petsController.all);
        this.get('#/threads/add', threadsController.add);
        

        this.get('#/users', usersController.all);
        this.get('#/users/:id', usersController.register);
        this.get('#/notifications', notificationsController.all);
    });

    sammyApp.run('#/');   
}());
