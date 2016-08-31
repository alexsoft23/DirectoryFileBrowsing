(function () {
    angular
        .module('app')
        .factory('directory', ['$resource',
        function($resource) {
            return $resource('/api/Directory/:action', {
                action:"GetDrives"
            },
            {
                headers: {
                    'Content-Type': 'application/json'
                }
            });
        }]);
})();