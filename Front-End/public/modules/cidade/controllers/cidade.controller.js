angular.module('app.cidade', [])
.controller('CidadeController', cidadeController);

function cidadeController(cidadeService) {
	
    vm              = this
    vm.avancar      = avancar
    vm.retornar     = retornar
    vm.index        = 0
    vm.buscaCidade  = buscaCidade

	function init(){
        carregaDados()
	}

	init()

	function carregaDados(){
        coluna = 0
       cidadeService.getAll(coluna).then(function(dados) {
            vm.dataset =  dados.data
            return vm.dataset
       })
    }
    function avancar() {
        coluna += 10
        vm.index += 1
        cidadeService.getAll(coluna).then(function(dados) {
            vm.dataset =  dados.data
            return vm.dataset
       })
    }
    function retornar() {
        coluna      -= 10
        vm.index    -= 1
        if (coluna < 0) {
            coluna      = 0
            vm.index    = 0
        }
        cidadeService.getAll(coluna).then(function(dados) {
            vm.dataset =  dados.data
            return vm.dataset
       })
    }
    function buscaCidade(cidade) {
        if (coluna < 0) {
            coluna = 0
        }
        coluna += 10 

        cidadeService.getCidade(cidade,coluna).then(function(dados) {
            vm.dataset = dados.data
            return vm.dataset
        })
    }
}