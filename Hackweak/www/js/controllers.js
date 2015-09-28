﻿module.controller('AppController', function ($scope, $data) {
    $scope.doSomething = function () {
        setTimeout(function () {
            ons.notification.alert({ message: 'tapped' });
        }, 100);
    };
});

module.controller('DetailController', function ($scope, $data) {
    $scope.item = $data.selectedItem;
});

module.controller('MasterController', function ($scope, $data) {
    $scope.items = $data.items;

    $scope.showDetail = function (index) {
        var selectedItem = $data.items[index];
        $data.selectedItem = selectedItem;
        $scope.navi.pushPage('detail.html', { title: selectedItem.title });
    };
});