var materialApp = angular
.module('materialApp', [
    'materialApp.routes',
    'ui.router',
    'ngMaterial',
    'ngResource',
    'appCtrl',
    'app.faturamento-veiculo',
    'app.comissao',
    'app.faturamento-uf'   
    

]).config(function($mdThemingProvider) {
  
    $mdThemingProvider.theme('default')
    .primaryPalette('deep-orange')
    .accentPalette('orange');
});