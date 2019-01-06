angular.module('app.grafico')
.factory('graficoService', function(api) {
    
    var graficoFactory = {};

    graficoFactory.getAll = function() {
        var ds = new api.dados();
            return ds.$get()
    };

    return graficoFactory;

});