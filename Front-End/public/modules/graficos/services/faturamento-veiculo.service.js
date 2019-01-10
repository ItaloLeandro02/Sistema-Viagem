angular.module('app.faturamento-veiculo')
.factory('faturamentoVeiculoService', function(api) {
    
    var faturamentoVeiculoFactory = {};

    faturamentoVeiculoFactory.getAll = function() {
        var ds = new api.faturamentoVeiculo();
            return ds.$get()
    };

    return faturamentoVeiculoFactory;

});