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
            controller: 'GraficoController',
            controllerAs: 'vm',
            params: {
                title: "Faturamento Por Veiculo"
            }
        });

    $locationProvider.html5Mode(true);

});