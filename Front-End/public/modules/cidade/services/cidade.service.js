angular.module('app.cidade')
.factory('cidadeService', function(api) {
    
    var cidadeFactory = {};

    cidadeFactory.getAll = function(coluna) {
        var ds = new api.cidade();
            return ds.$get({coluna : coluna})
    };

    cidadeFactory.getCidade = function(cidade, coluna) {
        if(cidade.length < 3)
            cidade = null
       
        var ds = new api.returnCidade();
            return ds.$get({cidade : cidade, coluna})
    };
    return cidadeFactory;

});