angular.module('app.comissao', [])
.controller('ComissaoController', comissaoController);

function comissaoController(comissaoService) {
	
    vm                 = this
    data               = []
    total              = []
    comissao           = []
    vm.nomes           = []
    options            = []
    vm.meses           = ('Janeiro Fevereiro Março Abril Maio Junho Julho Agosto Setembro Outubro Novembro Dezembro')
    .split(' ').map(function (mes) { return { abrev: mes }; });
    vm.chartComissao   = carregaDados
    vm.filtarPeriodo   = filtarPeriodo

	function init(){
        carregaDados()
	}

	init()

	function carregaDados(){
        options     = []
        comissao    = []
        total       = []
		comissaoService.getAll().then(function(dados){
            
            dados.data.forEach(item => {
                if (vm.nomes.indexOf(item.nome) == -1) {
                    vm.nomes.push(item.nome)
                }
                total.push(item.total),
                comissao.push(item.comissao)
            });
        
                options.push({
                    name : 'Comissão',
                    data : comissao
                },
                {
                    name: 'Faturamento',
                    data : total
                })
            chartComissao()
        })
    }

    function chartComissao() {
        var chart = Highcharts.chart('comissoes', {
            chart: {
                type: 'bar'
            },
            title: {
                text: 'Comissoes'
            },
            xAxis: {
                categories: vm.nomes
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
            series: options
        });
    }

    function filtarPeriodo(inicial, final) {
        options = []
        options.push({
            name : 'Comissão',
            data : comissao
        },
        {
            name: 'Faturamento',
            data : total
        })

        comissaoService.getPeriodo(inicial, final).then(function(dados) {
            options     = []
            comissao    = []
            total       = []
                dados.data.forEach(item => {
                        if (item == null)
                        {
                            total.push(0),
                            comissao.push(0)
                        }
                        else 
                        {
                            total.push(item.total),
                            comissao.push(item.comissao)
                        }
                        
                });
        
                options.push(
                    {
                        name : 'Comissão',
                        data : comissao
                    },
                    {
                        name: 'Faturamento',
                        data : total
                    })
                    chartComissao()
        })
    }
}