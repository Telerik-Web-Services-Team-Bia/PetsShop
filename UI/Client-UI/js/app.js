(function () {
    var sammyApp = Sammy('#viewContainer', function () {

        this.get('#/', function(){
                this.redirect('#/home');
            });

        this.get('#/home', homeController.all);
        this.get('#/register', usersController.register);
        this.get('#/pets/:id', petsController.details);       
        this.get('#/pets', petsController.all);
        this.get('#/addPet', petsController.add);
    });

    sammyApp.run('#/');   
}());
