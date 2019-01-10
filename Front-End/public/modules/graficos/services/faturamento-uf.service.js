angular.module('app.faturamento-uf')
.factory('faturamentoUfService', function(api) {
    
    var faturamentoUfFactory = {};

    faturamentoUfFactory.getAll = function(dataInicial, dataFinal) {
        if (dataInicial == null && dataFinal == null) {
            dataInicial = '01/01/1970',
            dataFinal   = '31/12/2999'
        }

            var ds = new api.faturamentoUf();
                return ds.$get({ dataInicial : dataInicial.toLocaleString(), dataFinal : dataFinal.toLocaleString()})
    };

    return faturamentoUfFactory;

});