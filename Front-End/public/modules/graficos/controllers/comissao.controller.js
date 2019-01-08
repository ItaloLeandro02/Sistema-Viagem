angular.module('app.comissao', [])
.controller('ComissaoController', comissaoController);

function comissaoController(comissaoService) {
	
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
		comissaoService.getAll().then(function(dados){			
        
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
                    chartComissoes()
        })
    }

    function chartComissoes() {
        Highcharts.chart('comissoes', {
            chart: {
                type: 'bar'
            },
            title: {
                text: 'Stacked bar chart'
            },
            xAxis: {
                categories: ['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Total fruit consumption'
                }
            },
            legend: {
                reversed: true
            },
            plotOptions: {
                series: {
                    stacking: 'normal'
                }
            },
            series: [{
                name: 'John',
                data: [5, 3, 4, 7, 2]
            }, {
                name: 'Jane',
                data: [2, 2, 3, 2, 1]
            }, {
                name: 'Joe',
                data: [3, 4, 4, 2, 5]
            }]
        });
    }
}