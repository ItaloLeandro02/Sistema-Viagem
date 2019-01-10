angular.module('app.mapa-brasil')
.factory('mapaBrasilService', function(api) {
    
    var mapaBrasilFactory = {};

    mapaBrasilFactory.getAll = function() {
        var ds = new api.mapaBrasil();
            return ds.$get()
    };


    return mapaBrasilFactory;

});