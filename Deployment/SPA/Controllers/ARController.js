var ARController = function ($scope, $http, $window) {
    $scope.showClassSearch = false;
    $scope.SValid = false;
    var token = $window.localStorage.getItem('token');
    LoadExistingClasses();

    function LoadExistingClasses() {
        $http({
            method: "Get",
            url: "/AttendanceRegisterAPI/api/Classes/GetClasses",
            headers: { 'Authorization': 'Bearer ' + token }
        }).then(function (response) {
            if (response) {
                classlist = response.data.ClassList;
                if (classlist.length > 0) {
                    $scope.showClassSearch = true;
                }
                $scope.classlist = classlist;
                alert(response.data.StatusMessage);
            };
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
    }

    $scope.lookupClass = function (searchText) {
        var output = [];
        var searchList = [];
        $scope.hideList = false;

        angular.forEach($scope.classlist, function (classRecord) {
            if (classRecord.Subject.toLowerCase().indexOf(searchText.toLowerCase()) >= 0 ||
                classRecord.DayOfWeek.toLowerCase().indexOf(searchText.toLowerCase()) >= 0) {
                output.push(classRecord);
            }
        });
        $scope.filterClassName = output;
    };

    $scope.fillClassTextBox = function (classID, classOption) {
        $scope.SearchText = classOption;
        $scope.hideList = true;
        $scope.showStudentDetails = true;
        $scope.ClassId = classID;
        $scope.Grade = $scope.classlist.filter(x => x.ClassId == classID)[0].Grade;

        LoadExistingAttendance(classID);
        $scope.UpdateValid = false;
    };

    function LoadExistingAttendance(classID) {
        var requestData = JSON.stringify(classID);
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Attendance/GetAttendance",
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token },
            data: requestData
        }).then(function (response) {
            if (response) {
                if (response.data.AttendanceList.length > 0) {
                    classList = response.data.AttendanceList;
                    $scope.classList = classList;
                    SLCheck();
                    alert(response.data.StatusMessage);
                } else {
                    LoadExistingStudents(classID);
                }
            }
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
    }

    function LoadExistingStudents(classID) {
        var requestData = JSON.stringify(classID);
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Student/GetStudents",
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token },
            data: requestData
        }).then(function (response) {
            if (response) {
                classList = response.data.StudentList;
                classList.ClassAttended = false;
                $scope.classList = classList;
                SLCheck();
                alert(response.data.StatusMessage);
            };
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
    }

    function SLCheck() {
        if (classList.length > 0) {
            $scope.SValid = true;
        } else {
            $scope.SValid = false;
        }
    }

    $scope.UpdateAttendanceList = function () {
        var requestData = JSON.stringify($scope.classList);
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Attendance/PostClassAttendance",
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token },
            data: requestData
        }).then(function (response) {
            if (response) {
                classList = response.data.AttendanceList;
                $scope.classList = classList;
                //$scope.SValid = false;
                alert(response.data.StatusMessage);
                $scope.SearchText = "";
            };
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
    }
};