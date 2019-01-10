angular.module('app.faturamento-veiculo', [])
.controller('FaturamentoVeiculoController', faturamentoVeiculoController);

function faturamentoVeiculoController(faturamentoVeiculoService) {
	
    vm                      = this
    data                    = []
    vm.nomes                = []
    options                 = []
    meses                   = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro']
    vm.filtarVeiculo        = filtrarVeiculo
    vm.chartFaturamento     = carregaDados
    

	function init(){
        carregaDados()
	}

	init()

	function carregaDados(){
        options = []

        faturamentoVeiculoService.getAll().then(function(dados){

                vm.extraiData = extraiData
                
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
                            if (vm.nomes.indexOf(item.modelo) == -1) {
                                vm.nomes.push(item.modelo)
                            }
                        });

                        vm.nomes.forEach(carro => {
                            options.push({
                                name : carro,
                                data : extraiData(carro)
                            })
                        })
                        chartFaturamento()
        })
    }

    function chartFaturamento() {
        var grafico = Highcharts.chart('faturamento-veiculo', {
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

    function filtrarVeiculo(veiculo) {
        options = []
        options.push({
            name : veiculo,
            data : vm.extraiData(veiculo)
    })
        chartFaturamento()
    }
}