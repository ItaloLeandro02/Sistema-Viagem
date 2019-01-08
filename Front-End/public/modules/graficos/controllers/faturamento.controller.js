angular.module('app.faturamento', [])
.controller('FaturamentoController', faturamentoController);

function faturamentoController(faturamentoService) {
	
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
		faturamentoService.getAll().then(function(dados){			
        
            function extraiData(veiculo) {
                let faturamentoVeiculo = dados.data.filter(function(item) {
                    return item.modelo == veiculo
                })

                let faturamentoMensal = [0,0,0,0,0,0,0,0,0,0,0,0]
                faturamentoVeiculo.forEach(function(item, index) {
                    faturamentoMensal.splice(index, 1, [(item.mes - 1), item.total])
                })

                return faturamentoMensal
            }
                    dados.data.forEach(item => {
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
                    chartFaturamento()
        })
    }

    function chartFaturamento() {
        Highcharts.chart('faturamento-veiculo', {
            chart: {
                type: 'column'
            },
            title: {
                text: 'Faturamento Por Veiculo',
            },
            xAxis: {
                categories: meses
            },
            yAxis: {
                title: {
                    text: 'Faturamento'
                }
            },
            series: options
        });
    }
}