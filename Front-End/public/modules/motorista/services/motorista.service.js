angular.module('app.motorista')
.factory('motoristaService', function(api) {
    
    var motoristaFactory = {};

    motoristaFactory.save = function(nome, apelido) {
        var ds = new api.motorista();
            ds.nome     = nome
            ds.apelido  = apelido
                return ds.$save()
    };

    return motoristaFactory;

});