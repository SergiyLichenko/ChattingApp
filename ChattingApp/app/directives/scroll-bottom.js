'use strict';

app.directive("scrollBottom", function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            scope.$watchCollection(attr.scrollBottom, function () {
                $timeout(function () {
                    element[0].scrollTop = element[0].scrollHeight;
                });
            }, true);
        }
    }
});