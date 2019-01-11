angular.module('app.motorista', [])
.controller('MotoristaController', motoristaController);

function motoristaController(motoristaService, $state) {
    vm           = this
    vm.novo      = novo
    
    function novo(nome, apelido) {

        if (vm.form.$invalid) {
            toastr.error("Erro! Revise seus dados e tente novamente.","ERRO")
            return
        } 

            motoristaService.save(nome, apelido)
            .then(function(resposta) {
                if (resposta.sucesso) {
                    toastr.success("Dados inseridos com sucesso","SUCESSO")
                }
                $state.go("home")
            })
    }
}