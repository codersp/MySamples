myapp.controller('Employees', function ($scope, $http) {
    $http.get('/api/Employees').success(function (data, status, headers, config) {
        $scope.EmployeeList = data;
    }).error(function (data, status, headers, config) {
        console.log(status);
    });
});