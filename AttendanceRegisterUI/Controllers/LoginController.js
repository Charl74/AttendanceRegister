var LoginController = function ($scope, $http, $window) {
    $scope.loginUser = function () {
        var login = $scope.login;
        var loginData = "grant_type=password&username=" + login.username + "&password=" + login.password;

        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/token",
            data: loginData,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (response) {
            $window.localStorage.setItem('token', response.data.access_token);
            alert("Login Success!");
            window.location.replace("/AttendanceRegisterUI/#!/AR");
        }, function (error) {
            alert(error.data.error + "\n" + error.data.error_description + "\n" + "If you haven't used the system before, please click on the 'Register' link!");
            $scope.login = {};
            $scope.formlogin.$setUntouched();
        });
    }
};