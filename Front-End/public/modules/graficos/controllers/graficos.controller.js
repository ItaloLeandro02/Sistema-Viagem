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
                    
                    function extraiData(veiculo) {
                        let faturamentoVeiculo = dataset.filter(function(item) {
                            return item.modelo == veiculo
                        })

                        let faturamentoMensal = [0,0,0,0,0,0,0,0,0,0,0,0]
                        faturamentoVeiculo.forEach(function(item, index) {
                            faturamentoMensal.splice(index, 1, item.total)
                        })

                        return faturamentoMensal
                    }
                
                    //Name
                    dataset.forEach(item => {
                        if (nomes.indexOf(item.modelo) == -1) {
                            nomes.push(item.modelo)
                        }
                    });

                    nomes.forEach(carro => {
                        options.push({
                            name : carro,
                            data : extraiData(carro)
                        })
                    })

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
                            series: options
                        });
		})
    }
}