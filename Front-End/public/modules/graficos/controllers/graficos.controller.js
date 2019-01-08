angular.module('app.grafico', [])
.controller('GraficoController', graficoController);

function graficoController(graficoService) {
	
    vm                 = this
    data               = []
    nomes              = []
    options            = []
    meses              = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro']

	function init(){
        carregaDados()
	}

	init()

	function carregaDados(){
		graficoService.getAll().then(function(dados){			
            dataset = dados.data
                nome = null
            // console.log(nomes[item.id - 1])
            //         if (nomes[item.id - 1] == item.modelo)
            //         {
            //             data.push([[(item.mes - 1), item.total]])
            //         }    
                
                    //Name
                    dataset.forEach(item => {
                        if (nome != item.modelo)
                        {
                            nomes.push(item.modelo)
                                nome = item.modelo
                        }
                    });
                    
                    //Data
                    dataset.forEach(item => {
                        data.push([[(item.mes - 1), item.total]])
                    });

                    console.log(nomes, data)
                        var faturamentoVeiculo = Highcharts.chart('faturamento-veiculo', {
                            chart: {
                                type: 'column'
                            },
                            title: {
                                text: 'Faturamento Por Veiculo',
                            },
                            xAxis: {
                                categories: vm.meses
                            },
                            yAxis: {
                                title: {
                                    text: 'Mês'
                                }
                            },
                        });
		})
	}
}