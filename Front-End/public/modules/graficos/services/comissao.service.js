angular.module('app.comissao')
.factory('comissaoService', function(api) {
    
    var comissaoFactory = {};

    comissaoFactory.getAll = function() {
        var ds = new api.comissao();
            return ds.$get()
    };

    comissaoFactory.getPeriodo = function(inicial, final) {
        var ds = new api.comissao();
            return ds.$get({ dataInicial: inicial.toLocaleDateString(), dataFinal: final.toLocaleDateString() })
    };

    return comissaoFactory;

});