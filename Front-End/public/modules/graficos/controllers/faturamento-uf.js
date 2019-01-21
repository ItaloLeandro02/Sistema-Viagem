angular.module('app.faturamento-uf', [])
.controller('FaturamentoUfController', faturamentoUfController);

function faturamentoUfController(faturamentoUfService) {
	
    vm                      = this
    data                    = []
    vm.nomes                = []
    options                 = []
    meses                   = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro']
    vm.filtrarFaturamento   = filtrarFaturamento
    vm.chartFaturamentoUf   = carregaDados
    

	function init(){
        carregaDados()
	}

	init()

	function carregaDados(){
        
        options         = []
        despesa         = []
        faturamento     = []
		faturamentoUfService.getAll().then(function(dados){
            
            dados.data.forEach(item => {
                if (vm.nomes.indexOf(item.uf) == -1) {
                    vm.nomes.push(item.uf)
                }
                faturamento.push(item.faturamento),
                despesa.push(item.despesa)
            });
        
                options.push({
                    name : 'Faturamento',
                    color : '#00FF00',
                    data : faturamento
                },
                {
                    name: 'Despesa',
                    color : '#FF0000',
                    data : despesa
                })
                chartFaturamentoUf()
        })
    }

    function chartFaturamentoUf() {
        Highcharts.chart('faturamento-uf', {
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

    function filtrarFaturamento(veiculo) {
    
    }
}