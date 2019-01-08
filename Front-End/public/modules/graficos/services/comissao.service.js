angular.module('app.comissao')
.factory('comissaoService', function(api) {
    
    var comissaoFactory = {};

    comissaoFactory.getAll = function() {
        var ds = new api.dados();
            return ds.$get()
    };

    return comissaoFactory;

});