angular.module('app.faturamentoArea', [])
.controller('FaturamentoAreaController', faturamentoAreaController);

function faturamentoAreaController(faturamentoAreaService) {
	
    vm                      = this
    data                    = []
    vm.meses                = []
    options                 = []
    meses                   = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro']
    vm.chartFaturamento     = carregaDados
    vm.filtarPeriodo        = filtarPeriodo

	function init(){
        carregaDados()
	}

	init()

	function carregaDados(){

        zeraDados()

        faturamentoAreaService.getPeriodo().then(function(dados){
            meses.forEach(function(item, index){
                
                bruto.push([index, 0])
                despesa.push([index, 0])
                combustivel.push([index, 0])

                dados.data.forEach(item => {
                    if ((item.mes - 1) == index) {
                        bruto.splice(index, 1, [item.mes -1, item.bruto])
                        despesa.splice(index, 1, [item.mes -1,item.despesas])
                        combustivel.splice(index, 1, [item.mes -1,item.combustivel])
                    }
                })
            })

            atribuiDados(bruto, despesa, combustivel)
        })
    }

    function chartFaturamento() {
        Highcharts.chart('faturamentoDespesasCombustivel', {
            chart: {
                type: 'area'
            },
            title: {
                text: 'Area chart with negative values'
            },
            xAxis: {
                categories: meses
            },
            credits: {
                enabled: false
            },
            series: options
        });
    }

    function filtarPeriodo(inicial, final) {
        zeraDados()

        faturamentoAreaService.getPeriodo(inicial, final).then(function(dados){
            meses.forEach(function(item, index){
                
                bruto.push([index, 0])
                despesa.push([index, 0])
                combustivel.push([index, 0])

                dados.data.forEach(item => {
                    if ((item.mes - 1) == index) {
                        bruto.splice(index, 1, [item.mes -1, item.bruto])
                        despesa.splice(index, 1, [item.mes -1,item.despesas])
                        combustivel.splice(index, 1, [item.mes -1,item.combustivel])
                    }
                })
            })

            atribuiDados(bruto, despesa, combustivel)

        })
    }

    function zeraDados() {
        options         = []
        bruto           = []
        despesa         = []
        combustivel     = []
        
            return
    }

    function atribuiDados(bruto, despesa, combustivel) {
        options.push({
            name : 'Bruto',
            color : '#00FF00',
            data : bruto
        },
        {
            name: 'Despesa',
            color : '#FF0000',
            data : despesa
        },
        {
            name: 'Combustivel',
            color : '#1C1C1C',
            data : combustivel
        })

            chartFaturamento()
                return
    }
}