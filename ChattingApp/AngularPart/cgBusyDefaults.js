'use strict';

app.value('cgBusyDefaults', ['$templateCache', function ($templateCache) {
    return {
        message: 'Loading Stuff',
        backdrop: false,
        templateUrl: $templateCache.get('Background.html'),
        delay: 20000,
        minDuration: 20000
    }

}]);