angular.module('app.mapa-brasil', [])
.controller('MapaBrasilController', mapaBrasilController);

function mapaBrasilController(mapaBrasilService) {
	
    vm                 = this
    faturamento        = []
    despesa            = []
    liquido            = []
    estados            = ["AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RO", "RS", "RR", "SC", "SE", "SP", "TO"]
    options            = []


	function init(){
        carregaDados()
	}

	init()

	function carregaDados(){
        mapaBrasilService.getAll().then(function(dados){
            
            estados.forEach(function(item, index){
                
                if (dados.data[index]) {
                    uf = dados.data[index].uf.toLowerCase()

                    faturamento.push([('br-' + uf), dados.data[index].faturamento]),
                    despesa.push(dados.data[index].despesa),
                    liquido.push(dados.data[index].liquido)
                }
                else {
                    uf = item.toLowerCase()

                    faturamento.push([('br-' + uf), 0]),
                    despesa.push(0),
                    liquido.push(0)
                }
            });

            console.log(despesa)
        
                options.push({
                    data : faturamento,
                    states: {
                        hover: {
                            color: '#BADA55'
                        }
                    },
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}'
                    }
                })

                chartMapaBrasil()
        })
    }
    console.log(faturamento)
    function chartMapaBrasil() {
        
        // Create the chart
        Highcharts.mapChart('mapa-brasil', {
            chart: {
                map: 'countries/br/br-all'
            },
        
            title: {
                text: 'Highmaps basic demo'
            },
        
            subtitle: {
                text: 'Source map: <a href="http://code.highcharts.com/mapdata/countries/br/br-all.js">Brazil</a>'
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.key +'</b>' +
                    '<br> Faturamento: R$' + this.series.data[this.point.index].options.value + '</b>' +
                    '<br> Despesa: R$' + despesa[this.point.index] + '</b>'    +
                    '<br> Liquido: R$' + liquido[this.point.index] + '</b>'
                }
            },
            mapNavigation: {
                enabled: true,
                buttonOptions: {
                    verticalAlign: 'bottom'
                }
            },
        
            colorAxis: {
                min: 0
            },
        
            series: options
        });
    }
}