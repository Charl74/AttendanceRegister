var StudentController = function ($scope, $http, $window) {
    var studentList = [];
    $scope.UpdateValid = false;
    $scope.showClassSearch = false;
    $scope.SearchText = "";
    $scope.student = {
        ClassId: "",
        GradeName: "",
        LastName: "",
        FirstName: "",
        Title: ""
    };

    var token = $window.localStorage.getItem('token');
    LoadExistingClasses();
    $scope.showStudentDetails = false;

    function LoadExistingStudents(classID) {
        var requestData = JSON.stringify(classID);
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Student/GetStudents",
            headers: { 'Content-Type': 'application/json','Authorization': 'Bearer ' + token },
            data: requestData
        }).then(function (response) {
            if (response) {
                studentList = response.data.StudentList;
                $scope.studentList = studentList;
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
        if (studentList.length > 0) {
            $scope.SValid = true;
        } else {
            $scope.SValid = false;
        }
    }

    $scope.AddStudent = function (newStudent) {
        if (newStudent) {
            if (studentList.filter(x => x.Title == newStudent.Title && x.FirstName == newStudent.FirstName && x.LastName == newStudent.LastName && x.GradeName == newStudent.GradeName).length === 0) {
                //studentList.push(newStudent);
                var requestData = JSON.stringify(newStudent);
                $http({
                    method: "Post",
                    url: "/AttendanceRegisterAPI/api/Student/PostAddStudent",
                    headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token },
                    data: requestData
                }).then(function (response) {
                    if (response) {
                        studentList = response.data.StudentList;
                        $scope.studentList = studentList;
                        SLCheck()
                    };
                }, function (error) {
                    alert(error.data.Message + "\n" + error.statusText);
                    if (error.statusText === "Unauthorized") {
                        window.location.replace("/AttendanceRegisterUI/#!/Login");
                    }
                });
            };
            //$scope.UpdateValid = true;
        }
                $scope.formStudent.$setUntouched();
                $scope.formStudent.$setPristine();
                $scope.student = {
                    ClassId: newStudent.ClassId,
                    GradeName: newStudent.GradeName
                };
                SLCheck();
    }

    $scope.RemoveStudent = function (key, value) {
        //studentList.push(newStudent);
        var requestData = JSON.stringify(value);
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Student/PostRemoveStudent",
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token },
            data: requestData
        }).then(function (response) {
            if (response) {
                studentList = response.data.StudentList;
                $scope.studentList = studentList;
                SLCheck()
            };
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
        //studentList.splice(key, 1);
        //$scope.studentList = studentList;
        //SLCheck();
        //$scope.UpdateValid = true;
    };

    //$scope.SubmitStudentList = function () {
    //    studentList = {};

    //}

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
        //var searchList = [];
        $scope.hideList = false;

        angular.forEach($scope.classlist, function (classRecord) {
            if (classRecord.Subject.toLowerCase().indexOf(searchText.toLowerCase()) >= 0 ||
                classRecord.DayOfWeek.toLowerCase().indexOf(searchText.toLowerCase()) >= 0 ) {
                output.push(classRecord);
            }
        });
        $scope.filterClassName = output;
    };

    $scope.fillClassTextBox = function (classID, classOption) {
        $scope.SearchText = classOption;
        $scope.hideList = true;
        $scope.showStudentDetails = true;
        $scope.student.ClassId = classID;
        $scope.student.GradeName = $scope.classlist.filter(x => x.ClassId == classID)[0].Grade;

        LoadExistingStudents(classID);
        $scope.UpdateValid = false;
    };
};