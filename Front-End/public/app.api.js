(function ()
{
    'use strict';

    angular
        .module('materialApp')
        .factory('api', apiService);
    
    function apiService($resource)
    {

      var api = {}      

      // Base Url
      api.baseUrl = 'https://localhost:5001/api/';

      
      /* Recursos da API */ 
      api.dados   = $resource(api.baseUrl + 'viagem/faturamento-veiculo', {},
        {update: {
          method: 'PUT'
        }
      })


      

      return api;
    }

})();
