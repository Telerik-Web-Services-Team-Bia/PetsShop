/**
 * Created by ������ on 12.11.2015 �..
 */
var data = (function () {

        const USERNAME_STORAGE_KEY = 'username-key',
            ACCESS_TOKEN_STORAGE_KEY = 'access-token',
            USER_NAME = 'user-name';

    var baseUrl = "https://microsoft-apiappd9f14ef7f696440a97a2766f35ce4f77.azurewebsites.net/api/";

    function userLogin(user) {
        var promise = new Promise(function (resolve, reject) {
            var reqUser = "username=" + user.email + "&password=" + user.password + "&grant_type=password";
            $.ajax({
                url: baseUrl + "Account/login",
                method: 'POST',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                data: reqUser,
                success: function (user) {
                    localStorage.setItem(USERNAME_STORAGE_KEY, user.userName);
                    localStorage.setItem(ACCESS_TOKEN_STORAGE_KEY, user.access_token);

                    var toIndex = user.userName.indexOf('@');
                    var username = user.userName.substring(0,toIndex);
                    localStorage.setItem(USER_NAME, username );
                    resolve(user);
                },
                error: function (err) {
                    reject(user);
                }
            });
        });

        return promise;
    }


    function userRegister(user) {
        var options = {
          data: user
        };

        return jsonRequester.post(baseUrl + "Account/Register", options)
            .then(function(resp) {
                return resp;
            });
    }

    function userLogout() {
        var promise = new Promise(function(resolve, reject) {
          localStorage.removeItem(USERNAME_STORAGE_KEY);
          localStorage.removeItem(ACCESS_TOKEN_STORAGE_KEY);
          resolve();
        });

        return promise;
    }

    function hasUser() {
        return !!localStorage.getItem(USERNAME_STORAGE_KEY) &&
      !!localStorage.getItem(ACCESS_TOKEN_STORAGE_KEY);
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
            username : username
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


    return {
        users: {
            login: userLogin,
            register: userRegister,
            logout: userLogout,
            current: getCurrentUser,
            username: returnUsername,
            hasUser: hasUser
        },
        pets: {
            get: petsGet,
            add: petsAdd,
            getById: petById
        }
    };
}());

