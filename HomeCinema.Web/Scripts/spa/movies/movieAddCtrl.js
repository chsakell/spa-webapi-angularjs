(function (app) {
    'use strict';

    app.controller('movieAddCtrl', movieAddCtrl);

    movieAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService', 'fileUploadService'];

    function movieAddCtrl($scope, $location, $routeParams, apiService, notificationService, fileUploadService) {

        $scope.pageClass = 'page-movies';
        $scope.movie = { GenreId: 1, Rating: 1, NumberOfStocks: 1 };

        $scope.genres = [];
        $scope.isReadOnly = false;
        $scope.AddMovie = AddMovie;
        $scope.prepareFiles = prepareFiles;
        $scope.openDatePicker = openDatePicker;
        $scope.changeNumberOfStocks = changeNumberOfStocks;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        var movieImage = null;

        function loadGenres() {
            apiService.get('/api/genres/', null,
            genresLoadCompleted,
            genresLoadFailed);
        }

        function genresLoadCompleted(response) {
            $scope.genres = response.data;
        }

        function genresLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function AddMovie() {
            AddMovieModel();
        }

        function AddMovieModel() {
            apiService.post('/api/movies/add', $scope.movie,
            addMovieSucceded,
            addMovieFailed);
        }

        function prepareFiles($files) {
            movieImage = $files;
        }

        function addMovieSucceded(response) {
            notificationService.displaySuccess($scope.movie.Title + ' has been submitted to Home Cinema');
            $scope.movie = response.data;

            if (movieImage) {
                fileUploadService.uploadImage(movieImage, $scope.movie.ID, redirectToEdit);
            }
            else
                redirectToEdit();
        }

        function addMovieFailed(response) {
            console.log(response);
            notificationService.displayError(response.statusText);
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.datepicker.opened = true;
        };

        function redirectToEdit() {
            $location.url('movies/edit/' + $scope.movie.ID);
        }

        function changeNumberOfStocks($vent)
        {
            var btn = $('#btnSetStocks'),
            oldValue = $('#inputStocks').val().trim(),
            newVal = 0;

            if (btn.attr('data-dir') == 'up') {
                newVal = parseInt(oldValue) + 1;
            } else {
                if (oldValue > 1) {
                    newVal = parseInt(oldValue) - 1;
                } else {
                    newVal = 1;
                }
            }
            $('#inputStocks').val(newVal);
            $scope.movie.NumberOfStocks = newVal;
            console.log($scope.movie);
        }

        loadGenres();
    }

})(angular.module('homeCinema'));