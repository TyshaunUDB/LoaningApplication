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
});