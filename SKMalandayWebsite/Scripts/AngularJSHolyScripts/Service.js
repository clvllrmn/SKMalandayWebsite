app.service('SKMalandayWebsiteService', function ($http) {
    this.AddUserService = function (user) {
        return $http({
            method: 'POST',
            url: '/SKMalanday/VerifyConnection',
            data: user
        });

    };
    this.GetInterestChartService = function (user) {
        return $http.get('/SKMalanday/GetInterestChartData');
    };
    this.GetUserListService = function () {
        return $http.get('/SKMalanday/GetUsers');
    };

    this.AddProjectService = function (formData) {
        var response = $http({
            method: "post",
            url: '/SKMalanday/VerifyConnection',
            data: formData,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        });
        return response;
        this.uploadProject = function (formData) {
            return $http.post('/SKMalanday/UploadProjectImage', formData, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
        };


        this.getProjects = function () {
            return $http.get('/SKMalanday/GetProjects');
        };
    }
});
