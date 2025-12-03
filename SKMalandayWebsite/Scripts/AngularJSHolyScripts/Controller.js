// LOAD USERS FROM DATABASE
$scope.loadUsers = function () {
    $http.get("/User/GetUsers").then(function (response) {
        $scope.newMultiArray = response.data;
    });
};

$scope.loadUsers();

// ADD USER to database
$scope.AddUser = function () {
    var user = {
        fullName: $scope.fullName,
        birthday: $scope.birthDate,
        gender: $scope.gender,
        email: $scope.email,
        address: $scope.address,
        contactNo: $scope.contactNo,
        interest: $scope.interest
    };

    $http.post("/User/AddUser", user).then(function (response) {
        $scope.loadUsers();
        $scope.resetForm();
    });
};

// EDIT: Fill form
$scope.editUser = function (index) {
    var user = $scope.newMultiArray[index];

    $scope.editIndex = index;
    $scope.isEditMode = true;

    $scope.userID = user.userID;
    $scope.fullName = user.fullName;
    $scope.birthDate = new Date(user.birthday);
    $scope.gender = user.gender;
    $scope.email = user.email;
    $scope.address = user.address;
    $scope.contactNo = user.contactNo;
    $scope.interest = user.interest;
};

// UPDATE USER in database
$scope.updateUser = function () {

    var updatedUser = {
        userID: $scope.userID,
        fullName: $scope.fullName,
        birthday: $scope.birthDate,
        gender: $scope.gender,
        email: $scope.email,
        address: $scope.address,
        contactNo: $scope.contactNo,
        interest: $scope.interest
    };

    $http.post("/User/UpdateUser", updatedUser).then(function (response) {
        $scope.loadUsers();
        $scope.resetForm();
        $scope.isEditMode = false;
    });
};

// DELETE USER
$scope.removeUser = function (index) {
    var id = $scope.newMultiArray[index].userID;

    $http.post("/User/DeleteUser", { id: id }).then(function () {
        $scope.loadUsers();
    });
};

// RESET FORM
$scope.resetForm = function () {
    $scope.fullName = "";
    $scope.birthDate = "";
    $scope.gender = "";
    $scope.email = "";
    $scope.address = "";
    $scope.contactNo = "";
    $scope.interest = "";
};
