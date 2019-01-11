angular.module('app.veiculo', [])
.controller('VeiculoController', veiculoController);

function veiculoController(veiculoService, $state) {
    vm           = this
    vm.novo      = novo
    
    function novo(fabricante, modelo, anoFabricacao, anoModelo) {

        if (vm.form.$invalid) {
            toastr.error("Erro! Revise seus dados e tente novamente.","ERRO")
            return
        } 

            veiculoService.save(fabricante, modelo, anoFabricacao, anoModelo)
            .then(function(resposta) {
                if (resposta.sucesso) {
                    toastr.success("Dados inseridos com sucesso","SUCESSO")
                }
                $state.go("home")
            })
    }
}