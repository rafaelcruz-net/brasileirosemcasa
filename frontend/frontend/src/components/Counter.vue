<template>
    <div>
        <div class="md-layout md-alignment-center-center">
            <div class="md-layout-item md-medium-size-33 md-small-size-50 md-xsmall-size-100 md-large-size-25 md-xlarge-size-20"
                style="text-align:center">
                <md-card class="md-primary">
                    <md-card-header>
                        <md-card-header-text>
                            <div class="md-title md-counter-title">{{counter}}</div>
                            <div class="md-subhead md-counter-subtitle">Brasileiros em casa</div>
                        </md-card-header-text>
                    </md-card-header>
                </md-card>
            </div>
            <div class="md-layout-item md-medium-size-33 md-small-size-50 md-xsmall-size-100 md-large-size-25 md-xlarge-size-20"
                style="text-align:center">
                <md-card class="md-accent">
                    <md-card-header>
                        <div class="md-title md-counter-title">{{casoConfirmadosCounter}}</div>
                        <div class="md-subhead md-counter-subtitle">Casos Confirmados</div>
                    </md-card-header>
                </md-card>
            </div>
            <div class="md-layout-item md-medium-size-33 md-small-size-50 md-xsmall-size-100 md-large-size-25 md-xlarge-size-20"
                style="text-align:center">
                <md-card style="background-color:#EC407A">
                    <md-card-header>
                        <div class="md-title md-counter-title">{{novosCasos.total}}</div>
                        <div class="md-subhead md-counter-subtitle">Novos Casos no dia {{novosCasos.data}}</div>
                    </md-card-header>
                </md-card>
            </div>
        </div>
    </div>
</template>

<script>
export default {
  name: 'Counter',
  data () {
    return {
      counter: 0,
      casoConfirmadosCounter: 0,
      novosCasos: 0

    };
  },
  mounted () {
    this.$store.dispatch('pessoa/getCounter').then(r => {
      this.counter = r.data.count;
      this.casoConfirmadosCounter = r.data.totalCase;
      this.novosCasos = r.data.newCases;
    });
  },
  methods: {

  }
};
</script>

<style lang="scss">
    .md-counter-title {
        font-size: 56px !important;
        margin-bottom: 10px !important;
    }

    .md-counter-subtitle {
        opacity: 1 !important;
    }
</style>
