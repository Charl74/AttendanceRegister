var ReportsController = function ($scope, $http, $window, $uibModal) {
    var token = $window.localStorage.getItem('token');
    $scope.showStudentSearch = false;
    $scope.studentId = 0;
    LoadAllStudents();
    function LoadAllStudents() {
        $http({
            method: "Get",
            url: "/AttendanceRegisterAPI/api/Student/GetAllStudents",
            headers: { 'Authorization': 'Bearer ' + token }
        }).then(function (response) {
            if (response) {
                studentlist = response.data.StudentList;
                if (studentlist.length > 0) {
                    $scope.showStudentSearch = true;
                }
                $scope.studentlist = studentlist;
                alert(response.data.StatusMessage);
            };
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
    }

    $scope.DailyReport = function () {
        var requestData = JSON.stringify($scope.studentId);
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Reports/GetDailyStudentReport",
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token },
            data: requestData
        }).then(function (response) {
            if (response) {
                $scope.hideDailyReport = false;
                $scope.hideReportOptions = true;
                DailyReportModal(response.data.DailyReportsList);
                $scope.SearchText = "";
                $scope.studentId = 0;
            };
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
    }

    $scope.lookupStudent = function (searchText) {
        var output = [];
        $scope.hideDailyReport = true;
        $scope.hideSearceList = false;
        $scope.showStudentSearch = true;

        angular.forEach($scope.studentlist, function (classRecord) {
            if (classRecord.FirstName.toLowerCase().indexOf(searchText.toLowerCase()) >= 0 ||
                classRecord.LastName.toLowerCase().indexOf(searchText.toLowerCase()) >= 0) {
                output.push(classRecord);
            }
        });
        $scope.filterStudentName = output;
    };

    $scope.fillStudentTextBox = function (studentId, studentName) {
        $scope.SearchText = studentName;
        $scope.hideSearceList = true;
        $scope.studentId = studentId;
        $scope.UpdateValid = false;
    };

    $scope.TermReport = function myfunction() {
        var requestData = JSON.stringify($scope.termdate);
        $http({
            method: "Post",
            url: "/AttendanceRegisterAPI/api/Reports/GetTermReport",
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer ' + token },
            data: requestData
        }).then(function (response) {
            if (response) {
                if (response.data.TermReportsList.length > 0) {
                $scope.hideReportOptions = true;
                TermReportModal(response.data.TermReportsList);
                } else {
                    alert("No Attendance Records Found For Selected Term!");
                }
            };
        }, function (error) {
            alert(error.data.Message + "\n" + error.statusText);
            if (error.statusText === "Unauthorized") {
                window.location.replace("/AttendanceRegisterUI/#!/Login");
            }
        });
    }

    function DailyReportModal(dailyReportsList) {
        $uibModal.open({
            templateUrl: 'Templates/dailyreportmodal.html',
            backdrop: true,
            windowClass: 'modal',
            controller: function ($scope, $uibModalInstance, $log) {
                $scope.dailyReportsList = dailyReportsList;
                $scope.close = function () {
                    $uibModalInstance.dismiss();
                    unHideReportOptions();
                };
            },
            resolve: {
                user: function () {
                    return $scope.selected;
                }
            }
        });
    };

    function TermReportModal(termReportsList) {
        $uibModal.open({
            templateUrl: 'Templates/termreportmodal.html',
            backdrop: true,
            windowClass: 'modal',
            controller: function ($scope, $uibModalInstance, $log) {
                $scope.termReportsList = termReportsList;
                $scope.close = function () {
                    $uibModalInstance.dismiss();
                    unHideReportOptions();
                };
            },
            resolve: {
                user: function () {
                    return $scope.selected;
                }
            }
        });
    };

    function unHideReportOptions() {
        $scope.hideReportOptions = false;
    };
};