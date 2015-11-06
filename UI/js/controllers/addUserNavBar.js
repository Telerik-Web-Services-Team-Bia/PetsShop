
var loginNavBar = (function () {
    Parse.initialize("BxC62zFfCXJAfLxS90r6hwNSz0OIKtDlZ1sVeCCV", "Av5f9x57L6qsWpxohLSaXtqUD32Pblzm4dyUnYaJ");
    
    var currentUser = Parse.User.current();

    if (!currentUser) {
        var form = $('<form/>').addClass('navbar-form navbar-right').attr('role', 'login');

        var inputUserNameContainer = $('<div/>').addClass('form-group navbar-login');
        inputUserNameContainer.append('<input type="text" class="form-control" placeholder="Username" id="userNameNavBar">');
        form.append(inputUserNameContainer);

        var inputPasswordContainer = $('<div/>').addClass('form-group navbar-login');
        inputPasswordContainer.append('<input type="password" class="form-control" placeholder="Password" id="passwordNavBar">');
        form.append(inputPasswordContainer);

        form.append('<button id="buttonLoginNavBar" class="btn btn-default">Login</button>');

        $('#bs-example-navbar-collapse-1').append(form);

        $('#buttonLoginNavBar').on('click', function () {
            var userName = $('#userNameNavBar').val(),
                password = $('#passwordNavBar').val();

            Parse.User.logIn(userName, password, {
                success: function (user) {
                    console.log('User Logged');
                    location.reload();
                },
                error: function (user, error) {
                    console.log(error.message);
                    window.alert('Invalid username or password');
                }
            });
        })
    } else {       
        var logoutBtn = $('<button/>').addClass('btn btn-default navbar-btn navbar-right').html('Logout');
        
        $('#bs-example-navbar-collapse-1').append(logoutBtn);
        
        var ul = $('<ul/>').addClass('nav navbar-nav navbar-right');
        var li = $('<li/>').addClass('navbar-text').html('You are logged in as ');
        
        var userName = $('<strong/>').html(currentUser.get('username'));
        li.append(userName);
        ul.append(li);

        $('#bs-example-navbar-collapse-1').append(ul);

        logoutBtn.on('click', function () {
            Parse.User.logOut()
            console.log('logged out');
            location.reload();
        });
    }
} ());
