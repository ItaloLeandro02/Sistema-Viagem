var router = angular.module('materialApp.routes', ['ui.router']);
router.config(function($stateProvider, $urlRouterProvider, $locationProvider) {

    $urlRouterProvider.otherwise('/');

    // UI Router States
    // Inserting Page title as State Param
    $stateProvider
        .state('home', {
            url: '/',
            templateUrl: 'home.html',
            params: {
                title: "Material Starter"
            }
        })
        .state('faturamento-veiculo', {
            url: '/faturamento-veiculo',
            templateUrl: '/modules/graficos/views/faturamento-veiculo.html',
            controller: 'FaturamentoVeiculoController',
            controllerAs: 'vm',
            params: {
                title: "Faturamento Veicular"
            }
        })
        .state('comissoes', {
            url: '/comissoes',
            templateUrl: '/modules/graficos/views/comissoes.html',
            controller: 'ComissaoController',
            controllerAs: 'vm',
            params: {
                title: "Comissões"
            }
        })
        .state('faturamento-uf', {
            url: '/faturamento-uf',
            templateUrl: '/modules/graficos/views/faturamento-uf.html',
            controller: 'FaturamentoUfController',
            controllerAs: 'vm',
            params: {
                title: "Faturamento UF"
            }
        })
        .state('mapa-brasil', {
            url: '/mapa-brasil',
            templateUrl: '/modules/graficos/views/mapa-brasil.html',
            controller: 'MapaBrasilController',
            controllerAs: 'vm',
            params: {
                title: "Dados Por UF"
            }
        })
        .state('novo-motorista', {
            url: '/novo-motorista',
            templateUrl: '/modules/motorista/views/novo-motorista.html',
            controller: 'MotoristaController',
            controllerAs: 'vm',
            params: {
                title: "Novo Motorista"
            }
        })
        .state('novo-veiculo', {
            url: '/novo-veiculo',
            templateUrl: '/modules/veiculo/views/novo-veiculo.html',
            controller: 'VeiculoController',
            controllerAs: 'vm',
            params: {
                title: "Novo Veiculo"
            }
        })
        .state('nova-viagem', {
            url: '/nova-viagem',
            templateUrl: '/modules/viagem/views/nova-viagem.html',
            controller: 'ViagemController',
            controllerAs: 'vm',
            params: {
                title: "Nova Viagem"
            }
        });


    $locationProvider.html5Mode(true);

});