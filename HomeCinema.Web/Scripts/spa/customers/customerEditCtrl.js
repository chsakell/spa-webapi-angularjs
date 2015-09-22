(function (app) {
    'use strict';

    app.controller('customerEditCtrl', customerEditCtrl);

    customerEditCtrl.$inject = ['$scope', '$modalInstance','$timeout', 'apiService', 'notificationService'];

    function customerEditCtrl($scope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateCustomer = updateCustomer;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        function updateCustomer()
        {
            console.log($scope.EditedCustomer);
            apiService.post('/api/customers/update/', $scope.EditedCustomer,
            updateCustomerCompleted,
            updateCustomerLoadFailed);
        }

        function updateCustomerCompleted(response)
        {
            notificationService.displaySuccess($scope.EditedCustomer.FirstName + ' ' + $scope.EditedCustomer.LastName + ' has been updated');
            $scope.EditedCustomer = {};
            $modalInstance.dismiss();
        }

        function updateCustomerLoadFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);

            
        };

    }

})(angular.module('homeCinema'));