var materialApp = angular
.module('materialApp', [
    'materialApp.routes',
    'ui.router',
    'ngMaterial',
    'ngResource',
    'ngMessages',
    'appCtrl',
    'app.faturamento-veiculo',
    'app.comissao',
    'app.faturamento-uf',
    'app.mapa-brasil',
    'app.motorista',
    'app.veiculo',
    'app.viagem'
    

]).config(function($mdThemingProvider) {
  
    $mdThemingProvider.theme('default')
    .primaryPalette('blue')
    .accentPalette('orange');
});