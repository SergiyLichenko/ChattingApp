'use strict';

app
    .directive('ngThumb', ['$window', function ($window) {
        var helper = {
            support: !!($window.FileReader && $window.CanvasRenderingContext2D),
            isFile: function (item) {
                return angular.isObject(item) && item instanceof $window.File;
            },
            isImage: function (file) {
                var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        };

        return {
            restrict: 'A',
            template: '<canvas/>',
            link: function (scope, element, attributes) {
                if (!helper.support) return;

                var params = scope.$eval(attributes.ngThumb);

                if (!helper.isFile(params.file)) return;
                if (!helper.isImage(params.file)) return;

                var reader = new FileReader();
                reader.onload = onLoadFile;
                reader.readAsDataURL(params.file);

                function onLoadFile(event) {
                    var img = new Image();
                    img.src = event.target.result;

                    img.onload = function () {
                        var width = params.width || this.width / this.height * params.height;
                        var height = params.height || this.height / this.width * params.width;
                        scope.item.image = {
                            src: img.src,
                            height: height*2.5,
                            width: width*2.5
                        };
                        scope.$apply();
                    };
                }
            }
        };
    }]);
