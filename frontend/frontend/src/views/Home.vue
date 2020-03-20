<template>
    <div class="page-container">
        <md-app md-mode="reveal">
            <md-app-toolbar>
                <span class="md-title"></span>
            </md-app-toolbar>
            <md-app-content>
                <md-content>
                    <md-empty-state md-icon="pan_tool">
                    </md-empty-state>
                    <md-card class="md-elevation-0">
                        <md-card-header>
                            <md-card-header-text>
                                <div class="md-layout md-gutter md-alignment-center-center">
                                    <div class="md-display-3">Brasileiros em casa</div>
                                </div>
                            </md-card-header-text>
                        </md-card-header>
                        <md-card-content>
                            <div class="md-layout md-gutter md-alignment-center-center">
                                <div class="md-layout-item md-size-50" style="text-align:center">
                                    <p class="md-subheading md-alignment-center-center">Todos em casa para ajudarmos ao
                                        combate do COVID-19!<br />
                                        <br />
                                        Por essa razão foi criada esta página que pretende mostrar quantos de nós já
                                        estão em casa.
                                        <br />
                                        O objetivo desta página não é o de ter informação exata, mas o de ajudar a
                                        sensibilizar para esta questão tão importante neste momento.</p>
                                    <br />
                                    <br />
                                    <md-button class="md-raised md-accent" style="width: 300px;min-height: 60px;">
                                        QUERO PARTICIPAR
                                    </md-button>
                                </div>
                            </div>
                        </md-card-content>
                    </md-card>
                </md-content>
                <md-content>
                    <div class="md-layout md-alignment-center-center">
                        <div class="md-layout-item md-size-25" style="text-align:center">
                            <md-card class="md-accent md-counter">
                                <md-card-header>
                                    <div class="md-title md-counter-title">300.000</div>
                                    <div class="md-subhead md-counter-subtitle">Brasileiros e contando...</div>
                                </md-card-header>
                            </md-card>
                        </div>
                    </div>
                </md-content>
                <md-content>
                    <div class="md-layout md-alignment-center-center">
                        <center>
                            <highcharts :constructor-type="'mapChart'" :options="mapOptions"
                                class="md-layout-item map-padding"></highcharts>
                        </center>
                    </div>
                </md-content>
            </md-app-content>
        </md-app>
    </div>
</template>
<script>
export default {
  name: 'Home',
  data: () => ({
    menuVisible: false,
    mapOptions: {
      chart: {
        map: 'myMapName',
        width: 600,
        height: 600
      },
      title: {
        text: ''
      },
      legend: {
        enabled: false
      },
      series: [{
        name: 'Estados',
        color: '#E0E0E0',
        enableMouseTracking: false
      },
      {
        type: 'mapbubble',
        name: 'Pessoas em casa',
        joinBy: ['hc-key'],
        data: [
          ['br-sp', 1],
          ['br-ma', 2],
          ['br-pa', 3],
          ['br-sc', 4],
          ['br-ba', 5],
          ['br-ap', 6],
          ['br-ms', 7],
          ['br-mg', 8],
          ['br-go', 9],
          ['br-rs', 10],
          ['br-to', 11],
          ['br-pi', 12],
          ['br-al', 13],
          ['br-pb', 14],
          ['br-ce', 15],
          ['br-se', 16],
          ['br-rr', 17],
          ['br-pe', 18],
          ['br-pr', 19],
          ['br-es', 20],
          ['br-rj', 21],
          ['br-rn', 22],
          ['br-am', 23],
          ['br-mt', 24],
          ['br-df', 25],
          ['br-ac', 26],
          ['br-ro', 27]

        ],
        minSize: 1,
        maxSize: '12%',
        tooltip: {
          pointFormat: '{point.properties.woe-name}: {point.z}'
        }
      }]
    }
  })
};
</script>

<style lang="scss">
    .md-counter-title {
        font-size: 56px !important;
        padding: 5px 5px 15px 5px;
    }

    .md-counter-subtitle {
        font-size: 18px !important;
        opacity: 1 !important;
    }

    .map-padding {
        margin: 80px;
    }
</style>
