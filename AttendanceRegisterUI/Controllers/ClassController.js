var ClassController = function ($scope, $http, $window) {
    var classlist = {
        DayOfWeek:"",
        Grade:"",
        Subject: ""
    };
    var token = $window.localStorage.getItem('token');
    $scope.UpdateValid = false;
    LoadExistingClasses();
    $scope.orderByField = "DayOfWeek";
    $scope.reverseSort = false;

    function CLCheck() {
        if (classlist.length > 0) {
            $scope.CLValid = true;
        } else {
            $scope.CLValid = false;
        }
    }

    function LoadExistingClasses() {
        $http({
            method: "Get",
            url: "/AttendanceRegisterAPI/api/Classes/GetClasses",
            headers: { 'Authorization': 'Bearer ' + token }
        }).then(function (response) {
            if (response) {
                classlist = response.data.ClassList;
                $scope.classlist = classlist;
                CLCheck()
                alert(response.data.StatusMessage);
            };
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
    }
    $scope.AddClass = function (newClass) {
        if (newClass) {
            if (classlist.filter(x => x.DayOfWeek == newClass.DayOfWeek && x.Grade == newClass.Grade && x.Subject == newClass.Subject).length === 0) {
                classlist.push(newClass);
            }
            $scope.class = {};
            $scope.formClass.$setUntouched();
            $scope.classlist = classlist;
        }
        CLCheck();
        $scope.UpdateValid = true;
    };
    $scope.RemoveClass = function (key) {
        classlist.splice(key, 1);
        $scope.classlist = classlist;
        CLCheck();
        $scope.UpdateValid = true;
    }
    $scope.SubmitClassList = function () {
        classlist = {};
        var requestData = JSON.stringify($scope.classlist);
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Classes/PostClasses",
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token },
            data: requestData
        }).then(function (response) {
            if (response) {
                classlist = response.data.ClassList;
                $scope.classlist = classlist;
                CLCheck()
                alert("Class List Updated!");
                $scope.UpdateValid = false;
            };
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
    }
};