app.service("SKMalandayWebsiteService", function ($http) {



    this.AddUserService = function (userInfo) {

        return $http({

            method: "POST",

            url: "/SKMalanday/VerifyConnection",

            data: userInfo

        });

    };


});