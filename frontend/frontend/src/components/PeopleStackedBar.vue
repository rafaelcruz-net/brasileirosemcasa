<template>
    <div class="md-layout md-alignment-center-center map-padding">
        <div class="md-layout-item md-medium-size-65 md-small-size-100 md-xsmall-size-100 md-large-size-100 md-xlarge-size-100">
            <center>
                <highcharts :options="stackedOptions"></highcharts>
            </center>
        </div>
    </div>
</template>

<script>
import _ from 'lodash';
export default {
  name: 'PeopleStackedBar',
  data () {
    return {
      stackedOptions: {
        chart: {
          type: 'column',
          height: 640
        },
        title: {
          text: 'Total de pessoas em casa por estado'
        },
        xAxis: {
          categories: [],
          crosshair: true
        },
        yAxis: {
          min: 100,
          title: {
            text: ''
          }
        },
        plotOptions: {
          column: {
            stacking: 'normal',
            pointPadding: 0.0

          }
        },
        series: []
      }
    };
  },
  mounted () {
    let $this = this;

    this.$store.dispatch('pessoa/getCounterByStateAndMonth').then(r => {
      let result = r.data.result;
      $this.stackedOptions.xAxis.categories = r.data.category;
      result.forEach(element => {
        let serieData = {
          name: element.periodo,
          data: _.chain(element.valor).map('total').value()
        };

        $this.stackedOptions.series.push(serieData);
      });
    });
  }
};
</script>

<style lang="scss" scoped>

</style>
