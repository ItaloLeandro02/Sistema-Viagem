angular.module('app.faturamento-uf')
.factory('faturamentoUfService', function(api) {
    
    var faturamentoUfFactory = {};

    faturamentoUfFactory.getAll = function() {
        var ds = new api.faturamentoUf();
            return ds.$get()
    };

    return faturamentoUfFactory;

});