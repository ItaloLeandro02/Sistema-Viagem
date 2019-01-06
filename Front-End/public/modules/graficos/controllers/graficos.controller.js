angular.module('app.grafico', [])
.controller('GraficoController', graficoController);

function graficoController(graficoService) {
	
    vm                 = this
    vm.rendaPorVeiculo = []
    vm.modeloVeiculo   = []
    vm.meses           = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro']

	function init(){
        carregaDados()
	}

	init()

	function carregaDados(){
		graficoService.getAll().then(function(dados){			
            vm.dataset = dados.data
				vm.dataset.forEach(item => {
                    vm.rendaPorVeiculo.push(item.valorTotalLiquido)
                    vm.modeloVeiculo.push(item.veiculo.modelo)
                });
                console.log(vm.rendaPorVeiculo)
                var faturamentoVeiculo = Highcharts.chart('faturamento-veiculo', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Faturamento Por Veiculo'
                    },
                    xAxis: {
                        categories: vm.meses
                    },
                    yAxis: {
                        title: {
                            text: 'Mês'
                        }
                    },
                    series: [{
                        name: [vm.modeloVeiculo],
                        data: vm.rendaPorVeiculo
                    },
                    {
                        name: [vm.modeloVeiculo],
                        data: vm.rendaPorVeiculo
                    }]
                });
		})
	}

        

}