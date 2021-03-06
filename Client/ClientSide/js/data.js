var data = (function () {

    const USERNAME_STORAGE_KEY = 'username-key',
        ACCESS_TOKEN_STORAGE_KEY = 'access-token',
        USER_NAME = 'user-name';

    var baseUrl = "http://localhost:8089/api/";

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
            return "";
        }
        return {
            username : username
        };
    }

    function petsGet(category, sortBy) {
        var url = baseUrl + 'Pets';

        if (category !== undefined && category !== '') {
            url += ('?Category=' + category);
        };

        if (sortBy !== undefined && sortBy !== '') {
            if (category !== undefined && category !== '') {
                url += ('&sortBy=' + sortBy);
            } else {
                url += ('?sortBy=' + sortBy);
            };           
        };
        
        return jsonRequester.get(url)
          .then(function(res) {
            return res;
          });
    }

    function petsAdd(pet) {
        var options = {
          data: pet,
          headers: {
            'Authorization': 'Bearer ' + localStorage.getItem(ACCESS_TOKEN_STORAGE_KEY),           
          }
        };

        return jsonRequester.post(baseUrl + 'Pets', options)
            .then(function(resp) {
              return resp;
            });
    }

    function petById(id) {
        id = id.substring(1);
        return jsonRequester.get(baseUrl + 'Pets/' + id)
            .then(function(res) {
              return res;
            });
    }

    function petDelete(id) {
        id = id.substring(1);
        var options = {
            headers: {
              'Authorization': 'Bearer ' + localStorage.getItem(ACCESS_TOKEN_STORAGE_KEY),           
            }
        }  

        return jsonRequester.del(baseUrl + 'Pets/' + id, options)
            .then(function(res) {
              return res;
            });
    }

    function categoriesGet() {
        return jsonRequester.get(baseUrl + 'Categories')
            .then(function(res) {
              return res;
            });
    }  

    function addRating(rating) {
        var options = {
            data: rating,
            headers: {
              'Authorization': 'Bearer ' + localStorage.getItem(ACCESS_TOKEN_STORAGE_KEY),           
            }
        };

        return jsonRequester.put(baseUrl + 'Ratings', options)
            .then(function(resp) {
                return resp;
            });
    }

    function sendEmail(emailUrl) {

        return jsonRequester.post(emailUrl) 
            .then(function(resp) {
                return resp;
            })
    }


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
            getById: petById,
            delete: petDelete
        },
        categories: {
            get: categoriesGet 
        },
        ratings: {
            add: addRating
        },
        email: {
            send: sendEmail
        }
    };
}());

