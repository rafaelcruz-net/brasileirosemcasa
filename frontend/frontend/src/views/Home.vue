<template>
    <div class="page-container">
        <md-app md-mode="reveal">
            <md-app-toolbar>
                <span class="md-title">
                    <img src="/img/logo.png" width="96" height="96" style="padding:10px">
                </span>
            </md-app-toolbar>
            <md-app-content>
                <md-content>
                    <div class="md-layout md-alignment-center-center">
                        <div class="md-layout-item md-size-50" style="text-align:center">
                            <img src="/img/logo.png" width="200" height="200" style="padding:10px">
                        </div>
                    </div>

                    <md-card class="md-elevation-0">
                        <md-card-content>
                            <div class="md-layout md-gutter md-alignment-center-center">
                                <div class="md-layout-item md-size-50" style="text-align:center">
                                    <p class="md-subheading md-alignment-center-center">Todos em casa para ajudarmos ao
                                        combate do COVID-19!<br />
                                        <br />
                                        Por essa razão foi criada esta página que pretende mostrar quantos de nós já
                                        estamos em casa.
                                        <br />
                                        O objetivo desta página não é o de ter informação exata, mas o de ajudar a
                                        sensibilizar para esta questão tão importante neste momento.</p>
                                    <br />
                                    <br />
                                    <md-button class="md-raised md-accent" style="width: 300px;min-height: 60px;"
                                        @click="showDialog = true">
                                        QUERO DIZER QUE ESTOU EM CASA
                                    </md-button>
                                </div>
                            </div>
                        </md-card-content>
                    </md-card>
                </md-content>
                <md-content>
                    <div class="md-layout md-alignment-center-center">
                        <div class="md-layout-item"></div>
                        <div class="md-layout-item" style="text-align:center">
                            <md-card class="md-primary">
                                <md-card-header>
                                    <md-card-header-text>
                                        <div class="md-title md-counter-title">{{counter}}</div>
                                        <div class="md-subhead md-counter-subtitle">Brasileiros em casa</div>
                                    </md-card-header-text>
                                </md-card-header>
                            </md-card>
                        </div>
                        <div class="md-layout-item" style="text-align:center">
                            <md-card class="md-accent">
                                <md-card-header>
                                    <div class="md-title md-counter-title">{{casoConfirmadosCounter}}</div>
                                    <div class="md-subhead md-counter-subtitle">Casos Confirmados</div>
                                </md-card-header>
                            </md-card>
                        </div>
                        <div class="md-layout-item"></div>
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
        <md-dialog ref="dialog" :md-active.sync="showDialog" md-fullscreen class="md-primary">
            <loading :active.sync="sending" :can-cancel="false" :is-full-page="false" :color="'#ff5252'" :width='72'
                :height='72'></loading>
            <md-dialog-title>Sou um brasileiro em casa</md-dialog-title>
            <md-dialog-content>

                <div class="md-layout md-gutter">
                    <div class="md-layout-item" style="text-align:center">

                        <md-field :class="getValidationClass('uf')">
                            <label>Seu Estado</label>
                            <md-select v-model="form.uf" name="uf" id="uf" required @md-selected="onEstadoSelected">
                                <md-option value="">Selecione</md-option>
                                <md-option v-for="estado in estados" :value="estado.uf" :key="estado.id">{{estado.nome}}
                                </md-option>
                            </md-select>
                            <span class="md-error" v-if="!$v.form.uf.required && $v.form.uf.$dirty">Estado é um campo
                                obrigatório</span>
                        </md-field>
                        <md-field :class="getValidationClass('cidade')">
                            <label>Sua Cidade</label>
                            <md-select v-model="form.cidade" name="cidade" id="cidade" required>
                                <md-option value="">Selecione</md-option>
                                <md-option v-for="cidade in cidades" :value="cidade.id" :key="cidade.id">{{cidade.nome}}
                                </md-option>
                            </md-select>
                            <span class="md-error" v-if="!$v.form.cidade.required && $v.form.cidade.$dirty">Cidade é um
                                campo obrigatório</span>
                        </md-field>
                        <md-field :class="getValidationClass('quantidadePessoasCasa')" required>
                            <label>Quantas pessoas em casa?</label>
                            <md-input type="number" v-model="form.quantidadePessoasCasa" name="quantidadePessoasCasa"
                                id="quantidadePessoasCasa" />
                            <span class="md-error"
                                v-if="!$v.form.quantidadePessoasCasa.required && $v.form.quantidadePessoasCasa.$dirty">Quantidade
                                de Pessoas é um campo obrigatório</span>
                            <span class="md-error"
                                v-else-if="!$v.form.quantidadePessoasCasa.minValue && $v.form.quantidadePessoasCasa.$dirty">Quantidade
                                de Pessoas deve ser maior que zero</span>
                        </md-field>
                        <md-datepicker v-model="form.dataInicioQuarentena"
                            :class="getValidationClass('dataInicioQuarentena')">
                            <label>Desde quando em casa?*</label>
                            <span class="md-error"
                                v-if="!$v.form.dataInicioQuarentena.required && $v.form.dataInicioQuarentena.$dirty">Data
                                de ínicio de quarentena é um campo obrigatório</span>
                        </md-datepicker>

                    </div>
                </div>
            </md-dialog-content>
            <md-dialog-actions>
                <md-dialog-actions>
                    <md-button class="md-primary md-flat" @click="saveDialog">Salvar</md-button>
                    <md-button class="md-accent" @click="showDialog = false">Fechar</md-button>
                </md-dialog-actions>
            </md-dialog-actions>
        </md-dialog>
    </div>
</template>
<script>
import _ from 'lodash';
import {
  validationMixin
} from 'vuelidate';
import {
  required,
  minValue
} from 'vuelidate/lib/validators';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import Pessoa from '../model/Pessoa';
export default {
  name: 'Home',
  mixins: [validationMixin],
  components: {
    Loading
  },
  validations: {
    form: {
      uf: {
        required
      },
      cidade: {
        required
      },
      quantidadePessoasCasa: {
        required,
        minValue: minValue(1)
      },
      dataInicioQuarentena: {
        required
      }
    }
  },
  data: () => ({
    menuVisible: false,
    showDialog: false,
    sending: false,
    counter: 0,
    casoConfirmadosCounter: 0,
    estados: [],
    cidades: [],
    form: {
      uf: '',
      cidade: '',
      quantidadePessoasCasa: '',
      dataInicioQuarentena: ''
    },
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
        enabled: true
      },
      series: [{
        name: 'Casos de Covid-19',
        dataLabels: {
          enabled: false
        },
        type: 'map',
        data: [],
        color: '#ff5252'
      },
      {
        type: 'mapbubble',
        name: 'Pessoas em casa',
        joinBy: ['hc-key'],
        data: [],
        minSize: 5,
        maxSize: '10%',
        color: '#448aff',
        tooltip: {
          pointFormat: '{point.properties.woe-name}: {point.z}'
        }
      }
      ]
    }
  }),
  mounted () {
    this.$material.locale = {
      ...this.$material.locale,
      startYear: 1900,
      endYear: 2099,
      dateFormat: 'dd/MM/yyyy',
      days: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sabado'],
      shortDays: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
      shorterDays: ['D', 'M', 'T', 'Q', 'Q', 'S', 'S'],
      months: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro',
        'Outubro', 'Novembro', 'Dezembro'
      ],
      shortMonths: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
      shorterMonths: ['J', 'F', 'M', 'A', 'M', 'Ju', 'Ju', 'A', 'Se', 'O', 'N', 'D'],
      firstDayOfAWeek: 0
    };
    this.$store.dispatch('estado/getEstado').then(r => {
      this.estados = r.data;
    });

    this.$store.dispatch('pessoa/getCounter').then(r => {
      this.counter = r.data.count;
    });

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
        if (e.uf !== 'br-total') {
          map.push([e.uf, parseInt(e.total[0])]);
        } else {
          this.casoConfirmadosCounter = e.total[0];
        }
      });
      this.mapOptions.series[0].data = map;
    });
  },
  methods: {
    onEstadoSelected (value) {
      this.cidades = _.chain(this.estados).find(o => o.uf === value).value().cidades;
    },
    saveDialog () {
      this.$v.$touch();

      if (this.$v.$invalid) {
        return;
      }

      this.sending = true;

      let pessoa = new Pessoa();
      pessoa.cidade = this.form.cidade;
      pessoa.QuantidadePessoasCasa = parseInt(this.form.quantidadePessoasCasa);
      pessoa.dataInicioQuarentena = this.form.dataInicioQuarentena;

      this.$store.dispatch('pessoa/savePessoa', pessoa).then(r => {
        this.sending = false;
        this.form.uf = '';
        this.form.cidade = '';
        this.form.quantidadePessoasCasa = '';
        this.form.dataInicioQuarentena = '';
        this.$v.$reset();
        this.$swal({
          title: 'Obrigado',
          text: 'As informações foram enviadas com sucesso',
          icon: 'success'
        }).then(r => {
          this.showDialog = false;
        });
      });
    },
    getValidationClass (fieldName) {
      const field = this.$v.form[fieldName];
      if (field) {
        return {
          'md-invalid': field.$invalid && field.$dirty
        };
      }
    }

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

    .map-padding {
        margin: 80px;
    }

    .md-icon:after {
        width: 37px;
        height: 4px;
        position: absolute;
        left: -1px;
        bottom: -5px;
        transition: .3s cubic-bezier(.4, 0, .2, 1);
        content: normal !important;
    }
</style>
