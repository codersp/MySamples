var myapp = angular.module('myapp', ["ngResource"]);

myapp.factory('myHttpInterceptor', function ($q) {
    return function (promise) {
        return promise.then(function response() {
            return response;
        }, function response() {
            return $q.reject(response);
        });
    };
});

myapp.config(function ($httpProvider) {
    $httpProvider.interceptors.push(function ($q) {
        return {
            // optional method
            'request': function (config) {
                return config || $q.when(config);
            },

            // optional method
            'requestError': function (rejection) {
                return $q.reject(rejection);
            },

            // optional method
            'response': function (response) {
                return response || $q.when(response);
            },

            // optional method
            'responseError': function (rejection) {
                return $q.reject(rejection);
            }
        }
    });
});

