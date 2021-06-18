var RegisterController = function ($scope, $http, $window) {
    //$scope.user = {
    //    title: "",
    //    firstName: "",
    //    lastName: "",
    //    email: "",
    //    password: "",
    //    confirmpassword: ""
    //}
    $scope.registerUser = function () {
        var requestdata = $scope.user;
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Account/Register",
            data: JSON.stringify(requestdata)
        }).then(function (success) {
            requestdata.userId = success.data;
            createUser(requestdata, success);
        }, function (error) {
            alert(error.data.Message + "\n" + error.data.ModelState[""][0]);
        });
    }

    function createUser(requestdata) {
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Teacher/AddUser",
            data: JSON.stringify(requestdata)
        }).then(function (success) {
        alert("Registration " + success.xhrStatus + "!");
        window.location.replace("/AttendanceRegisterUI/#!/Login");
        }, function (error) {
            alert(error.data.Message + "\n" + error.data.ModelState[""][0]);
        });
    }
};