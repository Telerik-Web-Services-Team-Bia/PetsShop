(function () {
    var sammyApp = Sammy('#viewContainer', function () {

        this.get('#/', function(){
                this.redirect('#/home');
            });

        this.get('#/home', homeController.all);
        this.get('#/register', usersController.register);
        this.get('#/pets/:id', petsController.details);       
        this.get('#/pets', petsController.all);


        this.get('#/liveChat', chatController.init);
        

        this.get('#/users', usersController.all);
        this.get('#/users/:id', usersController.register);

        this.get('#/addPet', petsController.add);

    });


    if(localStorage.getItem('user-name') == null){
        $('#chat').hide();
    }
    else{
        $('#chat').show();
    }
    sammyApp.run('#/');

}());
