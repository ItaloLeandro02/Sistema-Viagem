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
        options = []
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

        // switch (mes) {
        //     case 'Janeiro':
        //             count = 1
        //         break;

        //     case 'Fevereiro':
        //         count = 2
        //     break;
            
        //     case 'Março':
        //         count = 3
        //     break;

        //     case 'Abril':
        //         count = 4
        //     break;
            
        //     case 'Maio':
        //         count = 5
        //     break;
            
        //     case 'Junho':
        //         count = 6
        //     break;
            
        //     case 'Julho':
        //         count = 7
        //     break;
            
        //     case 'Agosto':
        //         count = 8
        //     break;

        //     case 'Setembro':
        //         count = 9
        //     break;

        //     case 'Outubro':
        //         count = 10
        //     break;

        //     case 'Novembro':
        //         count = 11
        //     break;

        //     case 'Dezembro':
        //         count = 12
        //     break;    
        
        //     default:
        //         break;
        // }

        comissaoService.getPeriodo(inicial, final).then(function(dados) {
            // options     = []
            // comissao    = []
            // total       = []
            // dados.data.forEach(item => {
            //     if (item.mes == count) {
            //         //vm.nomes.push(item.nome)
            //         total.push(item.total),
            //         comissao.push(item.comissao)
            //     }
            //     else {
            //         comissao.push(0)
            //         total.push(0)
            //     }
            // });
        
            //     options.push({
            //         name : 'Comissão',
            //         data : comissao
            //     },
            //     {
            //         name: 'Faturamento',
            //         data : total
            //     })
            // chartComissao()
        })
    }
}