angular.module('app.faturamento-uf', [])
.controller('FaturamentoUfController', faturamentoUfController);

function faturamentoUfController(faturamentoUfService) {
	
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
        /*
        options = []

        faturamentoUfService.getAll().then(function(dados){

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
        */
       chartFaturamentoUf()
    }

    function chartFaturamentoUf() {
        var grafico = Highcharts.chart('faturamento-uf', {
            chart: {
                type: 'area'
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