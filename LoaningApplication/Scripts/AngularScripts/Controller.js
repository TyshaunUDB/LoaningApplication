app.controller("LoaningApplicationController", function ($scope, LoaningApplicationService) {

    $scope.loginRedirect = function () {
        window.location.href = "/Home/LoginPage";
    }

    });