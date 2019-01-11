angular.module('app.veiculo')
.factory('veiculoService', function(api) {
    
    var veiculoFactory = {};

    veiculoFactory.save = function(fabricante, modelo, anoFabricacao, anoModelo) {
        var ds = new api.veiculo();
            ds.fabricante       = fabricante
            ds.modelo           = modelo
            ds.anoFabricacao    = anoFabricacao
            ds.anoModelo        =  anoModelo
                return ds.$save()
    };

    return veiculoFactory;

});