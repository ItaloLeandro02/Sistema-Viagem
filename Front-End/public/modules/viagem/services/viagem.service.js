angular.module('app.viagem')
.factory('viagemService', function(api) {
    
    var viagemFactory = {};

    viagemFactory.getAll = function() {
        var ds = new api.viagem();
            ds.idViagem         = idViagem
            ds.dataLancamento   = dataLancamento
            ds.historico        = historico
            ds.valor            = valor
                return ds.$save()
    };

    viagemFactory.save = function(idViagem, dataLancamento, historico, valor) {
        var ds = new api.despesa();
            ds.idViagem         = idViagem
            ds.dataLancamento   = dataLancamento
            ds.historico        = historico
            ds.valor            = valor
                return ds.$save()
    };

    return viagemFactory;

});