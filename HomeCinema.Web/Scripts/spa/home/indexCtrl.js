(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope','apiService', 'notificationService'];

    function indexCtrl($scope, apiService, notificationService) {
        $scope.pageClass = 'page-home';
        $scope.loadingMovies = true;
        $scope.loadingGenres = true;
        $scope.isReadOnly = true;

        $scope.latestMovies = [];
        $scope.loadData = loadData;

        function loadData() {
            apiService.get('/api/movies/latest', null,
                        moviesLoadCompleted,
                        moviesLoadFailed);

            apiService.get("/api/genres/", null,
                genresLoadCompleted,
                genresLoadFailed);
        }

        function moviesLoadCompleted(result) {
            $scope.latestMovies = result.data;
            $scope.loadingMovies = false;
        }

        function genresLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function moviesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function genresLoadCompleted(result) {
            var genres = result.data;
            Morris.Bar({
                element: "genres-bar",
                data: genres,
                xkey: "Name",
                ykeys: ["NumberOfMovies"],
                labels: ["Number Of Movies"],
                barRatio: 0.4,
                xLabelAngle: 55,
                hideHover: "auto",
                resize: 'true'
            });
            //.on('click', function (i, row) {
            //    $location.path('/genres/' + row.ID);
            //    $scope.$apply();
            //});

            $scope.loadingGenres = false;
        }

        loadData();
    }

})(angular.module('homeCinema'));