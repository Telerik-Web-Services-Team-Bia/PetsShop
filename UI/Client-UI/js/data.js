/**
 * Created by Ивайло on 12.11.2015 г..
 */
var data = (function () {

    const USERNAME_STORAGE_KEY = 'username-key',
        AUTH_KEY_STORAGE_KEY = 'auth-key-key';

    var baseUrl = "https://microsoft-apiappd9f14ef7f696440a97a2766f35ce4f77.azurewebsites.net/api/";

    function userLogin(user) {
        var primi = new Promise(function (resolve, reject) {
            var reqUser = {
                username: user.email,
                password: user.password,
                grant_type: 'password'
            };
            $.ajax({
                url: baseUrl + "Account/login",
                method: 'POST',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                data: JSON.stringify(reqUser),
                success: function (user) {
                    localStorage.setItem(USERNAME_STORAGE_KEY, user.username);
                    localStorage.setItem(AUTH_KEY_STORAGE_KEY, user.access_token);
                    console.log('success');
                    resolve(user);
                },
                error: function (err) {
                    console.log('error')
                }
            });
        });
        return primi;
    }

    function userRegister(user) {
        var prom = new Promise(function (resolve, reject) {
            var reqUser = {
                email: user.email,
                password: user.password,
                confirmPassword: user.confirmPassword,
                firstName : user.firstName,
                lastName : user.lastName
                //age: user.age
            };
            $.ajax({
                url: baseUrl + "Account/Register",
                method: 'POST',
                data: JSON.stringify(reqUser),
                contentType: 'application/json',
                success: function (user) {
                    //localStorage.setItem(USERNAME_STORAGE_KEY, user.username);
                    //localStorage.setItem(AUTH_KEY_STORAGE_KEY, user.authKey);
                    resolve(user);
                }
            });
        });
        return prom;
    }

    function userLogout() {
        var promis = new Promise(function (resolve, reject) {
            localStorage.removeItem(AUTH_KEY_STORAGE_KEY);
            localStorage.removeItem(USERNAME_STORAGE_KEY);
            resolve();
        });

        return promis;
    }

    function returnUsername() {
        var username = USERNAME_STORAGE_KEY;
        return username;
    }

    function getCurrentUser() {
        var username = localStorage.getItem(USERNAME_STORAGE_KEY);
        if (!username) {
            return null;
        }
        return {
            username
        };
    }




// Example -------------------------------------------------------------------------------------
    function petsGet() {
        return jsonRequester.get(baseUrl + 'Pets')
          .then(function(res) {
            return res;
          });
    }

    function petsAdd(pet) {
        var options = {
          data: pet,
          headers: {
            'Authorization': 'Bearer ' + localStorage.getItem(LOCAL_STORAGE_AUTHKEY_KEY),           
          }
        };

        return jsonRequester.post('Pets', options)
          .then(function(resp) {
            return resp;
          });
    }

    function petById(id) {
        id = id.substring(1);
        return jsonRequester.get(baseUrl + '	Pets/' + id)
          .then(function(res) {
            return res;
          });
    }
// ----------------------------------------------------------------------------------------------    

    function usersFind() {

    }


    return {
        users: {
            login: userLogin,
            register: userRegister,
            logout: userLogout,
            find: usersFind,
            current: getCurrentUser,
            username: returnUsername
        },
        pets: {
            get: petsGet,
            add: petsAdd,
            getById: petById
        }
    };
}());

