app.controller("LoaningApplicationController", function ($scope, LoaningApplicationService) {
    $scope.otpFunc = function () {
        window.location.href = "/Home/UserLandingPage";
    }

    $scope.loginFunc = function () {

        if ($scope.loginEmail == null || $scope.loginPass == null) {
            Swal.fire({ title: "Error", text: "Please fill out the required fields", icon: "error" });
        }
        else {
            window.location.href = "/Home/UserLandingPage";
        }
    }

    $scope.validationFunc = function () {

        const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

        if ($scope.fName == null) {
            Swal.fire({ title: "Error", text: "Please enter your first name", icon: "error" });
        }
        else if ($scope.lName == null) {
            Swal.fire({ title: "Error", text: "Please enter your last name", icon: "error" });
        }
        else if ($scope.uEmail == null) {
            Swal.fire({ title: "Error", text: "Please enter your email", icon: "error" });
        }
        else if ($scope.uPhone == null) {
            Swal.fire({ title: "Error", text: "Please enter your phone number", icon: "error" });
        }
        else if ($scope.uBirthdate == null) {
            Swal.fire({ title: "Error", text: "Please enter your your birthday", icon: "error" });
        }
        else if ($scope.uAddress == null) {
            Swal.fire({ title: "Error", text: "Please enter your address", icon: "error" });
        }
        else if ($scope.uPass == null) {
            Swal.fire({ title: "Error", text: "Please enter your password", icon: "error" });
        }
        else if ($scope.mName && $scope.mName.length >= 30) {
            Swal.fire({ title: "Error", text: "Please enter valid middle name", icon: "error" });
        }
        else if ($scope.fName.length >= 30 || $scope.lName.length >= 30) {
            Swal.fire({ title: "Error", text: "Please enter a valid name", icon: "error" });
        }
        else if (!emailRegex.test($scope.uEmail)) {
            Swal.fire({ title: "Error", text: "Invalid Email", icon: "error" });
        }
        else if ($scope.uPhone.length != 11 || isNaN($scope.uPhone)) {
            Swal.fire({ title: "Error", text: "Invalid Phone Number", icon: "error" });
        }
        else if ($scope.uPass.length < 8) {
            Swal.fire({ title: "Error", text: "Password must contain at least 8 characters!", icon: "error" });
        }
        else if ($scope.uPass != $scope.confirmPass) {
            Swal.fire({ title: "Error", text: "Passwords do not match!", icon: "error" });
        }
        else {
            window.location.href = "/Home/OTP";
        }
    }

    $scope.regFunc = function () {

        var userSearch = userInfo.find(Search => Search.Email === $scope.uEmail);

        if (userSearch == undefined) {
            userInfo.push({
                Firstname: $scope.fName,
                Middlename: $scope.mName,
                Lastname: $scope.lName,
                Email: $scope.uEmail,
                Phone: $scope.uPhone,
                Address: $scope.uAddress,
                Username: $scope.uEmail,
                Password: $scope.uPass,
            });
            var sessionString = JSON.stringify(userInfo)
            sessionStorage.setItem("credentials", sessionString);
            window.location.href = "/Home/LoginPage";
        } else {
            Swal.fire({ title: "Error", text: "Email already exists!", icon: "error" });
        }
    }
});