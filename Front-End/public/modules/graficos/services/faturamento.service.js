angular.module('app.faturamento')
.factory('faturamentoService', function(api) {
    
    var faturamentoFactory = {};

    faturamentoFactory.getAll = function() {
        var ds = new api.faturamentoVeiculo();
            return ds.$get()
    };

    return faturamentoFactory;

});