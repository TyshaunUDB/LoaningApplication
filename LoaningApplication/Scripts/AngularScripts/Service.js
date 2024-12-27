app.service("LoaningApplicationService", function ($http) {

    this.logIn = function (email, pass) {
        var response = $http({
            method: "post",
            url: "/Home/logIn",
            params: {
                loginEmail: email,
                loginPass: pass
            }
        });
        return response;
    }

    this.emailExist = function (email) {
        return $http.get("/Home/emailExist", { params: { email: email } });
    };

    this.regUser = function (uFirstName, uMiddleName, uLastName, uEmail, uPhone, uBirthdate, uAddress, uPassword) {
        var response = $http({
            method: "post",
            url: "/Home/regUser",
            params: {
                fName: uFirstName,
                mName: uMiddleName,
                lName: uLastName,
                email: uEmail,
                phone: uPhone,
                birthDate: uBirthdate,
                address: uAddress,
                pass: uPassword
            }
        });
        return response;
    };

    this.getUsers = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetUsers'
        });
    };

    this.updateAcc = function (editAccID, editFirstName, editMiddleName, editLastName, editPhone, editBirthday, editAddress) {
        var response = $http({
            method: "post",
            url: "/Home/updateAcc",
            params: {
                editID: editAccID,
                editFName: editFirstName,
                editMName: editMiddleName,
                editLName: editLastName,
                editPhone: editPhone,
                editBDay: editBirthday,
                editAdress: editAddress
            }
        });
        return response;
    };

    this.deleteAcc = function (deleteAccID) {
        var response = $http({
            method: "post",
            url: "/Home/deleteAcc",
            params: {
                deleteAccID: deleteAccID
            }
        });
        return response;
    };

    this.loanExist = function (getInfo) {
        return $http.get("/Home/loanExist", { params: { email: getInfo } });
    };

    this.loggedinData = function (getInfo) {
        return $http({
            method: 'GET',
            url: '/Home/loggedinData',
            params: {
                email: getInfo
            }
        });
    };
    this.loanInfo = function (getInfo) {
        return $http({
            method: 'GET',
            url: '/Home/loanInfo',
            params: {
                email: getInfo
            }
        });
    };
    this.loanApply = function (formData) {
        return $http.post("/Home/loanApply", formData, {
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        });
    };
    this.getLoans = function () {
        return $http({
            method: 'GET',
            url: '/Home/getLoans'
        });
    };

    this.paymentCheck = function (getInfo, paymentLoanID) {
        return $http.get("/Home/paymentCheck", { params: { email: getInfo, PaymentLoanID: paymentLoanID } });
    };

    this.payment = function (formData) {
        return $http.post("/Home/payment", formData, {
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        });
    };

    this.updateLoan = function (editLoanID, editStatusID) {
        var response = $http({
            method: "post",
            url: "/Home/updateLoan",
            params: {
                EditLoanID: editLoanID,
                EditStatusID: editStatusID
            }
        });
        return response;
    };

    this.getStatuses = function () {
        return $http({
            method: 'GET',
            url: '/Home/getStatuses'
        });
    };
    this.getaccStat = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetAccstatus'
        });
    };

    this.updateAccStat = function (editAccStatID, editAccStatName) {
        var response = $http({
            method: "post",
            url: "/Home/updateAccStat",
            params: {
                editID: editAccStatID,
                editFName: editAccStatName,
            }
        });
        return response;
    };

    this.deleteAccStat = function (deleteAccStatusID) {
        var response = $http({
            method: "post",
            url: "/Home/deleteAccStat",
            params: {
                deleteAccStatusID: deleteAccStatusID
            }
        });
        return response;
    };

    this.getRoles = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetRoles'
        });
    };

    this.getDisbursement = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetDisbursement'
        });
    };

    this.getLoanStatus = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetLoanStatus'
        });
    };

    this.getLogs = function () {
        return $http({
            method: 'GET',
            url: '/Home/GetLogs'
        });
    };
});