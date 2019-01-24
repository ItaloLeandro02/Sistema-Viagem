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
      api.faturamentoVeiculo   = $resource(api.baseUrl + 'viagem/faturamento-veiculo', {},
        {update: {
          method: 'PUT'
        }
      })

      api.comissao   = $resource(api.baseUrl + 'viagem/comissao', {},
        {update: {
          method: 'PUT'
        }
      })

      api.faturamentoUf   = $resource(api.baseUrl + 'viagem/faturamento-uf', {},
        {update: {
          method: 'PUT'
        }
      })

      api.mapaBrasil   = $resource(api.baseUrl + 'viagem/mapa-brasil', {},
        {update: {
          method: 'PUT'
        }
      })

      api.mapaBrasil   = $resource(api.baseUrl + 'viagem/mapa-brasil', {},
        {update: {
          method: 'PUT'
        }
      })

      api.motorista   = $resource(api.baseUrl + 'motorista', {},
        {update: {
          method: 'PUT'
        }
      })

      api.veiculo   = $resource(api.baseUrl + 'veiculo', {},
        {update: {
          method: 'PUT'
        }
      })

      api.viagem   = $resource(api.baseUrl + 'viagem', {},
        {update: {
          method: 'PUT'
        }
      })

      api.faturamentoArea   = $resource(api.baseUrl + 'viagem/bruto-despesas-combustivel', {},
        {update: {
          method: 'PUT'
        }
      })

      api.cidade   = $resource(api.baseUrl + 'cidade', {},
        {update: {
          method: 'PUT'
        }
      })

      api.returnCidade   = $resource(api.baseUrl + 'cidade/return-cidade', {},
        {update: {
          method: 'PUT'
        }
      })

      return api;
    }

})();
