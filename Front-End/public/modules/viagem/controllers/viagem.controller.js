angular.module('app.viagem', [])
.controller('ViagemController', viagemController);

function viagemController(viagemService, $state) {
    vm           = this
    vm.novo      = nova
    
    function nova(idViagem, dataLancamento, historico, valor) {

        if (vm.form.$invalid) {
            toastr.error("Erro! Revise seus dados e tente novamente.","ERRO")
            return
        } 

        viagemService.save(idViagem, dataLancamento, historico, valor)
            .then(function(resposta) {
                if (resposta.sucesso) {
                    toastr.success("Dados inseridos com sucesso","SUCESSO")
                }
                $state.go("home")
            })
    }
}