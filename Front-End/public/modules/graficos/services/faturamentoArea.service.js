angular.module('app.faturamentoArea')
.factory('faturamentoAreaService', function(api) {
    
    var faturamentoAreaFactory = {};

    faturamentoAreaFactory.getPeriodo = function(inicial, final) {
        if (inicial == null || final == null)
        {
            inicial = new Date('01-01-1970')
            final   = new Date('12-31-2999')
        }
        
        var ds = new api.faturamentoArea();
            return ds.$get({ dataInicial: inicial.toLocaleDateString(), dataFinal: final.toLocaleDateString() })
    };

    return faturamentoAreaFactory;

});