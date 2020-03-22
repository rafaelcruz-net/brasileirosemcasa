<template>
    <div class="md-layout md-alignment-center-center map-padding">
        <div
            class="md-layout-item md-medium-size-65 md-small-size-100 md-xsmall-size-100 md-large-size-45 md-xlarge-size-30">
            <center>
                <highcharts :constructor-type="'mapChart'" :options="mapOptions"></highcharts>
            </center>
        </div>
    </div>
</template>

<script>
export default {
  name: 'Map',
  data () {
    return {
      mapOptions: {
        chart: {
          map: 'myMapName',
          height: '100%'
        },
        title: {
          text: ''
        },
        legend: {
          enabled: true,
          title: {
            text: 'Densidade de casos de COVID-19'
          }
        },
        mapNavigation: {
          enabled: true,
          buttons: {
            zoomIn: {
              align: 'right'
            },
            zoomOut: {
              align: 'right'
            }
          }
        },
        colorAxis: [{
          minColor: '#FFEBEE',
          maxColor: '#B71C1C',
          startOnTick: false,
          endOnTick: false,
          type: 'logarithmic',
          min: 10,
          max: 10000,
          showInLegend: true,
          labels: {
            align: 'center',
            enabled: true
          }

        }],

        series: [{
          name: 'Casos de Covid-19',
          dataLabels: {
            enabled: false
          },
          type: 'map',
          data: [],
          colorAxis: 0,
          showInLegend: false,
          states: {
            hover: {
              color: '#BA68C8'
            }
          }
        },
        {
          type: 'mapbubble',
          name: 'Pessoas em casa',
          joinBy: ['hc-key'],
          data: [],
          minSize: 5,
          maxSize: '8%',
          colorAxis: null,
          color: '#42A5F5',
          tooltip: {
            pointFormat: '{point.properties.woe-name}: {point.z}'
          },
          showInLegend: false

        }
        ]
      }
    };
  },
  mounted () {
    this.$store.dispatch('pessoa/getMap').then(r => {
      let map = [];
      r.data.map.forEach(e => {
        map.push([e.uf, parseInt(e.qtd)]);
      });
      this.mapOptions.series[1].data = map;
    });

    this.$store.dispatch('covid/getMapPerState').then(r => {
      let map = [];
      r.data.map.perState.forEach(e => {
        map.push([e.uf, parseInt(e.total[0])]);
      });
      this.mapOptions.series[0].data = map;
    });
  }
};
</script>

<style lang="scss" scoped>

</style>
