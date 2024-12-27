app.controller("LoaningApplicationController", function ($scope, LoaningApplicationService) {
    $scope.otpFunc = function () {
        window.location.href = "/Home/UserLandingPage";
    }

    $scope.loginFunc = function () {

        if ($scope.loginEmail == null || $scope.loginPass == null) {
            Swal.fire({ title: "Error", text: "Please fill out the required fields", icon: "error" });
        }
        else {
            var email = $scope.loginEmail;
            var pass = $scope.loginPass;

            var postData = LoaningApplicationService.logIn(email, pass);
            postData.then(function (ReturnedData) {
                var returnValue = Number(ReturnedData.data);
                if (returnValue == 0) {
                    Swal.fire({ title: "Error", text: "Invalid username or password", icon: "error" });
                }
                else if (returnValue == 1) {
                    $scope.loginSession();
                    window.location.href = "/Home/UserLandingPage";
                }
                else if (returnValue == 2) {
                    $scope.loginSession();
                    window.location.href = "/Home/Accounts";
                }
                else if (returnValue == 3) {
                    $scope.loginSession();
                    window.location.href = "/Home/EmployeeDashboard";
                }
                else if (returnValue == 4) {
                    $scope.loginSession();
                    window.location.href = "/Home/LenderDashboard";
                }
            });
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
            LoaningApplicationService.emailExist($scope.uEmail).then(function (ReturnedData) {
                var returnValue = Number(ReturnedData.data);
                if (returnValue == 1) {
                    Swal.fire({ title: "Error", text: "Email already exists!", icon: "error" });
                }
                else {
                    var uFirstName = $scope.fName;
                    var uMiddleName = $scope.mName;
                    var uLastName = $scope.lName;
                    var uEmail = $scope.uEmail;
                    var uPhone = $scope.uPhone;
                    var uBirthdate = $scope.uBirthdate;
                    var uAddress = $scope.uAddress;
                    var uPassword = $scope.uPass;

                    var postData = LoaningApplicationService.regUser(uFirstName, uMiddleName, uLastName, uEmail, uPhone, uBirthdate, uAddress, uPassword);

                    $scope.regSession();

                    window.location.href = "/Home/UserLandingPage";
                }
            });
        }
    }

    var loginInfo = [];

    $scope.loginSession = function () {
        loginInfo.push($scope.loginEmail);
        var sessionString = (loginInfo)
        sessionStorage.setItem("logged in", sessionString);
    }

    $scope.regSession = function () {
        loginInfo.push($scope.uEmail);
        var sessionString = (loginInfo)
        sessionStorage.setItem("logged in", sessionString);
    }

    $scope.logoutSession = function () {
        sessionStorage.removeItem("logged in");
    }

    $scope.getUsers = function () {
        LoaningApplicationService.getUsers().then(function (response) {
            if (response.data.success) {
                $scope.users = response.data.data.map(accounts => ({
                    AccountID: accounts.accountID,
                    FirstName: accounts.firstName,
                    MiddleName: accounts.middleName,
                    LastName: accounts.lastName,
                    EmailAddress: accounts.emailAddress,
                    PhoneNumber: accounts.phoneNumber,
                    BirthDate: new Date(parseInt(accounts.birthDate.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric'
                    }),
                    Address: accounts.Address,
                    RoleName: accounts.RoleName,
                    StatusName: accounts.StatusName,
                    UpdateAt: new Date(parseInt(accounts.updateAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                    CreateAt: new Date(parseInt(accounts.createAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                }));
                initAccTable();
            } else {
                console.error('Failed to load accounts:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading accounts:', error);
        });
    };

    function initAccTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#accountsTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 50,
                    columns: [
                        { title: "Account ID" },
                        { title: "First Name" },
                        { title: "Middle Name" },
                        { title: "Last Name" },
                        { title: "Email" },
                        { title: "Contact" },
                        { title: "Birthday" },
                        { title: "Address" },
                        { title: "Role" },
                        { title: "Status" },
                        { title: "Last Updated" },
                        { title: "Created" },
                        { title: "Action" }
                    ],
                })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    $scope.editAcc = function (account) {
        $scope.editAccID = account.AccountID;
        $scope.editFirstName = account.FirstName;
        $scope.editMiddleName = account.MiddleName;
        $scope.editLastName = account.LastName;
        $scope.editEmail = account.EmailAddress;
        $scope.editPhone = account.PhoneNumber;
        $scope.editBirthday = account.BirthDate;
        $scope.editAddress = account.Address;

        const modalElement = document.getElementById('editAccModal');
        const modalInstance = M.Modal.init(modalElement);
        modalInstance.open();

        setTimeout(() => {
            M.updateTextFields();
        }, 0);
    }

    $scope.updateAcc = function () {
        var editAccID = $scope.editAccID;
        var editFirstName = $scope.editFirstName;
        var editMiddleName = $scope.editMiddleName;
        var editLastName = $scope.editLastName;
        var editPhone = $scope.editPhone;
        var editBirthday = $scope.editBirthday;
        var editAddress = $scope.editAddress;

        var postData = LoaningApplicationService.updateAcc(editAccID, editFirstName, editMiddleName, editLastName, editPhone, editBirthday, editAddress);

        window.location.href = "/Home/Accounts";
    }

    $scope.deleteAcc = function () {
        var deleteAccID = $scope.editAccID;
        var postData = LoaningApplicationService.deleteAcc(deleteAccID);
        window.location.href = "/Home/Accounts";
    }

    $scope.appOpen = function () {

        var getInfo = sessionStorage.getItem("logged in");
        LoaningApplicationService.loanExist(getInfo).then(function (ReturnedData) {
            var returnValue = Number(ReturnedData.data);
            if (returnValue == 1) {
                Swal.fire({ title: "Error", text: "You already have an active loan!", icon: "error" });
            }
            else {
                var LoanAmount = $scope.loanAmount;
                var LoanMonths = $scope.loanMonths;

                const modalElement = document.getElementById('appModal');
                const modalInstance = M.Modal.init(modalElement);
                modalInstance.open();
            }
        });
    }

    $scope.loggedinData = function () {
        var getInfo = sessionStorage.getItem("logged in");
        LoaningApplicationService.loggedinData(getInfo).then(function (response) {
            if (response.data.success) {
                $scope.loggedinusers = response.data.data.map(loggedInAcc => ({
                    FirstName: loggedInAcc.firstName,
                    MiddleName: loggedInAcc.middleName,
                    LastName: loggedInAcc.lastName,
                    EmailAddress: loggedInAcc.emailAddress,
                    PhoneNumber: loggedInAcc.phoneNumber,
                    BirthDate: new Date(parseInt(loggedInAcc.birthDate.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric'
                    }),
                    Address: loggedInAcc.Address
                }));
            } else {
                console.error('Failed to load account data:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading account data:', error);
        });
    };
    $scope.loanInfo = function () {
        var getInfo = sessionStorage.getItem("logged in");
        LoaningApplicationService.loanInfo(getInfo).then(function (response) {
            if (response.data.success) {
                $scope.loanInformation = response.data.data.map(loggedinLoan => ({
                    LoanID: loggedinLoan.loanID,
                    LoanAmount: loggedinLoan.loanAmount,
                    Pending: loggedinLoan.Pending,
                    PaymentMonth: loggedinLoan.paymentMonth,
                    AmountPaid: loggedinLoan.amountPaid,
                    DueDate: new Date(parseInt(loggedinLoan.dueDate.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric'
                    })
                }));
            } else {
                console.error('Failed to load account data:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading account data:', error);
        });
    };

    $scope.getGovID = function (input) {
        $scope.$apply(function () {
            $scope.govID = input.files[0];
        });
    };
    $scope.getCompID = function (input) {
        $scope.$apply(function () {
            $scope.compID = input.files[0];
        });
    };
    $scope.getPayslip = function (input) {
        $scope.$apply(function () {
            $scope.paySlip = input.files[0];
        });
        $scope.$apply(function () {
            $scope.sssTin = input.files[0];
        });
    };
    $scope.getSSSTin = function (input) {
        $scope.$apply(function () {
            $scope.sssTin = input.files[0];
        });
    };

    $scope.loanApply = function () {
        var formData = new FormData();

        formData.append("email", sessionStorage.getItem("logged in"));
        formData.append("LoanAmount", $scope.loanAmount);
        formData.append("LoanMonths", $scope.loanMonths);
        formData.append("GovID", $scope.govID);
        formData.append("CompanyID", $scope.compID);
        formData.append("Payslip", $scope.paySlip);
        formData.append("SSSTin", $scope.sssTin);

        LoaningApplicationService.loanApply(formData).then(function (response) {
            window.location.href = "/Home/UserLandingPage";
        }).catch(function (error) {
            console.error('Error submitting loan application:', error);
        });
    }
    $scope.getLoans = function () {
        LoaningApplicationService.getLoans().then(function (response) {
            if (response.data.success) {
                $scope.loans = response.data.data.map(loanList => ({
                    LoanID: loanList.loanID,
                    StatusIDStatusID: loanList.statusID,
                    Email: loanList.Applicant,
                    Status: loanList.StatusName,
                    LoanAmount: loanList.loanAmount,
                    LoanTerm: loanList.loanTerm,
                    StartDate: new Date(parseInt(loanList.startDate.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric'
                    }),
                    DueDate: new Date(parseInt(loanList.dueDate.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric'
                    }),
                    AmountPaid: loanList.amountPaid,
                    GovID: loanList.GovtIDPic,
                    CompID: loanList.CompIDPic,
                    PaySlip: loanList.payslipPic,
                    TinSSS: loanList.tinSSS,
                    UpdateAt: loanList.updateAt,
                    CreateAt: loanList.createAt,
                    UpdateAt: new Date(parseInt(loanList.updateAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                    CreateAt: new Date(parseInt(loanList.createAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                }));
                initLoansTable();
            } else {
                console.error('Failed to load loans:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading loans:', error);
        });
    };

    function initLoansTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#loansTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 50,
                    columns: [
                        { title: "Loan ID" },
                        { title: "Applicant Email" },
                        { title: "Status" },
                        { title: "Amount" },
                        { title: "Term" },
                        { title: "Start Date" },
                        { title: "Next Due Date" },
                        { title: "Amount Paid" },
                        { title: "Documents" },
                        { title: "Last Updated" },
                        { title: "Created" },
                        { title: "Action" }
                    ],
                })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    $scope.$watch('loans', function () {
        setTimeout(function () {
            M.Materialbox.init(document.querySelectorAll('.materialboxed'));
        }, 0);
    });

    $scope.checkLoan = function () {

        var getInfo = sessionStorage.getItem("logged in");
        LoaningApplicationService.loanExist(getInfo).then(function (ReturnedData) {
            var returnValue = Number(ReturnedData.data);
            if (returnValue == 1) {
                const modalElement = document.getElementById('myLoan');
                const modalInstance = M.Modal.init(modalElement);
                modalInstance.open();
            }
            else {
                Swal.fire({ title: "Error", text: "You don't have any active loans!", icon: "error" });
            }
        });
    }

    $scope.openPayment = function (loanInformation) {
        var paymentLoanID = loanInformation[0].LoanID;
        var getInfo = sessionStorage.getItem("logged in");

        LoaningApplicationService.paymentCheck(getInfo, paymentLoanID).then(function (ReturnedData) {
            var returnValue = Number(ReturnedData.data);
            if (returnValue == 1) {
                Swal.fire({ title: "Error", text: "You already paid for this month!", icon: "error" });
            }
            else if (returnValue == 2) {
                Swal.fire({ title: "Error", text: "Your loan is still pending for approval!", icon: "error" });
            }
            else {
                const modalElement = document.getElementById('paymentModal');
                const modalInstance = M.Modal.init(modalElement);
                modalInstance.open();
            }
        });
    }

    $scope.getPaymentProof = function (input) {
        $scope.$apply(function () {
            $scope.paymentProof = input.files[0];
        });
    };

    $scope.payment = function (loanInformation) {
        var formData = new FormData();

        formData.append("email", sessionStorage.getItem("logged in"));
        formData.append("LoanID", loanInformation[0].LoanID);
        formData.append("PaymentProof", $scope.paymentProof);

        LoaningApplicationService.payment(formData).then(function (response) {
            window.location.href = "/Home/UserLandingPage";
        }).catch(function (error) {
            console.error('Error submitting loan application:', error);
        });
    }

    $scope.updateLoan = function (list) {
        var editLoanID = list.LoanID;
        var editStatusID = list.StatusID;

        var postData = LoaningApplicationService.updateLoan(editLoanID, editStatusID);

        window.location.href = "/Home/Loans";
    }
    $scope.deleteAcc = function () {
        var deleteAccID = $scope.editAccID;
        var postData = LoaningApplicationService.deleteAcc(deleteAccID);
        window.location.href = "/Home/Accounts";
    }

    $scope.getaccStat = function () {
        LoaningApplicationService.getaccStat().then(function (response) {
            if (response.data.success) {
                $scope.accstat = response.data.data.map(accountstatus => ({
                    StatusID: accountstatus.statusID,
                    StatusName: accountstatus.statusName,
                    UpdateAt: new Date(parseInt(accountstatus.updateAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                    CreateAt: new Date(parseInt(accountstatus.createAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                }));
                initAccStatTable();
            } else {
                console.error('Failed to load accounts:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading accounts:', error);
        });
    };

    function initAccStatTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#accountstatusTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 50,
                    columns: [
                        { title: "Status ID" },
                        { title: "Status Name" },
                        { title: "Last Updated" },
                        { title: "Created" },
                        { title: "Action" }
                    ],
                })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    $scope.editAccStat = function (accountstatus) {
        $scope.editAccStatID = accountstatus.StatusID;
        $scope.editAccStatName = accountstatus.StatusName;

        const modalElement = document.getElementById('editAccStatModal');
        const modalInstance = M.Modal.init(modalElement);
        modalInstance.open();

        setTimeout(() => {
            M.updateTextFields();
        }, 0);
    }

    $scope.updateAccStat = function () {
        var editAccStatID = $scope.editAccStatID;
        var editAccStatName = $scope.editAccStatName;

        var postData = LoaningApplicationService.updateAccStat(editAccStatID, editAccStatName);

        window.location.href = "/Home/AccountStatus";
    }

    $scope.deleteAccStat = function () {
        var deleteAccStatID = $scope.editAccStatID;
        var postData = LoaningApplicationService.deleteAccStatID(deleteAccStatID);
        window.location.href = "/Home/AccountStatus";
    }

    $scope.getRoles = function () {
        LoaningApplicationService.getRoles().then(function (response) {
            if (response.data.success) {
                $scope.role = response.data.data.map(roles => ({
                    RoleID: roles.roleID,
                    RoleName: roles.roleName,
                    UpdateAt: new Date(parseInt(roles.updateAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                    CreateAt: new Date(parseInt(roles.createAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                }));
                initRolesTable();
            } else {
                console.error('Failed to load accounts:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading accounts:', error);
        });
    };

    function initRolesTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#rolesTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 50,
                    columns: [
                        { title: "Role ID" },
                        { title: "Role Name" },
                        { title: "Last Updated" },
                        { title: "Created" },
                        { title: "Action" }
                    ],
                })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    $scope.getDisbursement = function () {
        LoaningApplicationService.getDisbursement().then(function (response) {
            if (response.data.success) {
                $scope.disbursement = response.data.data.map(disburse => ({
                    DisbursementID: disburse.disbursementID,
                    LoanID: disburse.loanID,
                    DisbursementDate: new Date(parseInt(disburse.disbursementDate.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                    DisbursedAmount: disburse.disbursedAmount,
                    DisbursementMethod: disburse.disbursementMethod,
                    UpdateAt: new Date(parseInt(disburse.updateAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                    CreateAt: new Date(parseInt(disburse.createAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                }));
                initDisbursementTable();
            } else {
                console.error('Failed to load accounts:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading accounts:', error);
        });
    };

    function initDisbursementTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#disbursementTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 50,
                    columns: [
                        { title: "Disbursement ID" },
                        { title: "Loan ID" },
                        { title: "Disbursement Date" },
                        { title: "Disbursed Amount" },
                        { title: "Disbursement Method" },
                        { title: "Last Updated" },
                        { title: "Created" },
                        { title: "Action" }
                    ],
                })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    $scope.getLoanStatus = function () {
        LoaningApplicationService.getLoanStatus().then(function (response) {
            if (response.data.success) {
                $scope.loanstat = response.data.data.map(loanstatus => ({
                    LoanStatusID: loanstatus.loanstatusID,
                    LoanStatusName: loanstatus.loanstatusName,
                    UpdateAt: new Date(parseInt(loanstatus.updateAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                    CreateAt: new Date(parseInt(loanstatus.createAt.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    }),
                }));
                initLoanStatusTable();
            } else {
                console.error('Failed to load accounts:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading accounts:', error);
        });
    };

    function initLoanStatusTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#loanstatusTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 50,
                    columns: [
                        { title: "LoanStatus ID" },
                        { title: "Loan Status" },
                        { title: "Last Updated" },
                        { title: "Created" },
                        { title: "Action" }
                    ],
                })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }

    $scope.getLogs = function () {
        LoaningApplicationService.getLogs().then(function (response) {
            if (response.data.success) {
                $scope.log = response.data.data.map(logs => ({
                    ActionID: logs.actionID,
                    AccountID: logs.accountID,
                    ActionDesc: logs.actionDesc,
                    ActionDate: new Date(parseInt(logs.actionDate.match(/\d+/)[0])).toLocaleString('en-PH', {
                        month: 'short',
                        day: 'numeric',
                        year: 'numeric',
                        hour: 'numeric',
                        minute: 'numeric',
                        hour12: true
                    })
                }));
                initLogsTable();
            } else {
                console.error('Failed to load accounts:', response.data.message);
            }
        }).catch(error => {
            console.error('Error loading accounts:', error);
        });
    };

    function initLogsTable() {
        if (typeof $ !== 'undefined' && $.fn.DataTable) {
            setTimeout(function () {
                $('#logsTable').DataTable({
                    destroy: true,
                    paging: true,
                    searching: true,
                    ordering: true,
                    responsive: true,
                    autoWidth: false,
                    fixedHeader: true,
                    pageLength: 50,
                    columns: [
                        { title: "Action ID" },
                        { title: "Account Email" },
                        { title: "Action Description" },
                        { title: "Action Date" }
                    ],
                })
            });
        } else {
            console.error('jQuery or DataTables is not loaded.');
        }
    }
});