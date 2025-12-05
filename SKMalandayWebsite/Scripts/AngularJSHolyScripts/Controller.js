app.controller("SKMalandayController", function ($scope, SKMalandayWebsiteService) {
    $scope.newMultiArray = [];
    $scope.isEditing = false;
    $scope.editIndex = -1;


    $scope.GetAllUsers = function () {
        SKMalandayWebsiteService.GetUserListService()
            .then(function (response) {
               

                if (response.data) {
                    var users = response.data;
                    for (var i = 0; i < users.length; i++) {
                        if (users[i].birthday) {
                            var timestamp = parseInt(users[i].birthday.replace(/\/Date\((.*?)\)\//gi, "$1"));
                            users[i].birthday = new Date(timestamp);
                        }
                    }
                    $scope.newMultiArray = response.data;
                }
            })
            .catch(function (error) {
                console.error("Error fetching users:", error);
            });
    };    
    
    $scope.AddUser = function () {
        if ($scope.registrationForm.$invalid) {
            alert("Please fill out all required fields correctly.");
            return;
        }

        var user = {
            fullName: $scope.fullName,
            birthDate: $scope.birthDate, 
            gender: $scope.gender,
            address: $scope.address,
            contactNo: $scope.contactNo,
            email: $scope.email,
            interest: $scope.interest
        };

        SKMalandayWebsiteService.AddUserService(user)
            .then(function (response) {
                var data = response.data;
                if (data && data.success) {
                    alert(data.message || "User Registered Successfully!");
                    $scope.fullName = "";
                    $scope.birthDate = "";
                    $scope.gender = "";
                    $scope.address = "";
                    $scope.contactNo = "";
                    $scope.email = "";
                    $scope.interest = "";
                    $scope.registrationForm.$setPristine();
                    $scope.registrationForm.$setUntouched();
                    if ($scope.GetAllUsers) $scope.GetAllUsers();
                } else {
                    var errMsg = (data && data.message) ? data.message : "Something went wrong while saving.";
                    alert(errMsg);
                }
            })
            .catch(function (error) {
                var msg = "Server error. Please try again later.";
                try {
                    if (error.data) {
                        if (error.data.message) msg = error.data.message;
                        else msg = JSON.stringify(error.data);
                    } else if (error.statusText) {
                        msg = error.status + " " + error.statusText;
                    }
                } catch (e) {  }

                alert(msg);
                console.error("HTTP error:", error);
            });
    };


    $scope.editUser = function (index) {
        var user = $scope.newMultiArray[index];
        $scope.fullName = user.fullName;
        $scope.birthDate = new Date(user.birthday);
        $scope.gender = user.gender;
        $scope.address = user.address;
        $scope.contactNo = user.contactNo;
        $scope.email = user.email;
        $scope.interest = user.interest;

        $scope.isEditing = true;
        $scope.editIndex = index;
    };

    $scope.cancelEdit = function () {
        $scope.isEditing = false;
        $scope.editIndex = -1;
        $scope.resetForm();
    };

    $scope.removeUser = function (index) {
        if (confirm("Are you sure you want to delete this user?")) {
            $scope.newMultiArray.splice(index, 1);
        }
    };

    $scope.deleteData = function () {
        if (confirm("Are you sure you want to delete all users?")) {
            $scope.newMultiArray = [];
        }
    };

    $scope.resetForm = function () {
        $scope.fullName = "";
        $scope.birthDate = "";
        $scope.gender = "";
        $scope.address = "";
        $scope.contactNo = "";
        $scope.email = "";
        $scope.interest = "";
        $scope.registrationForm.$setPristine();
        $scope.registrationForm.$setUntouched();
    };
   
    $scope.interestLabels = ["Test A", "Test B", "Test C"];
    $scope.interestData = [10, 20, 30];
   
    $scope.projectTitle = "";
    $scope.tempImage = null;
    $scope.projectList = [];

    $scope.previewImage = function (input) {
        if (!input.files || !input.files[0]) return;

        const reader = new FileReader();
        reader.onload = function (e) {
            $scope.$apply(function () {
                $scope.tempImage = e.target.result;
            });
        };
        reader.readAsDataURL(input.files[0]);
    };

    $scope.removePreview = function () {
        $scope.tempImage = null;
        document.getElementById("imageInput").value = "";
    };

    $scope.addToList = function () {
        if (!$scope.tempImage) {
            alert("Please upload an image first.");
            return;
        }

        const title = $scope.projectTitle || "Untitled Project";

        $scope.projectList.push({
            title: title,
            image: $scope.tempImage
        });

        $scope.tempImage = null;
        $scope.projectTitle = "";
        document.getElementById("imageInput").value = "";
    };

});

