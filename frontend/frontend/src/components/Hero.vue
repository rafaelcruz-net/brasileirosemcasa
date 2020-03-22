<template>
    <div>
        <div class="md-layout md-alignment-center-center">
            <div class="md-layout-item md-medium-size-33 md-small-size-50 md-xsmall-size-100 md-large-size-50 md-xlarge-size-50"
                style="text-align:center">
                <img src="/img/logo.png" width="200" height="200" style="padding:10px">
            </div>
        </div>
        <md-card class="md-elevation-0">
            <md-card-content>
                <div class="md-layout md-alignment-center-center">
                    <div class="md-layout-item md-medium-size-33 md-small-size-50 md-xsmall-size-100 md-large-size-50 md-xlarge-size-50"
                        style="text-align:center">
                        <p class="md-subheading md-alignment-center-center">Todos em casa para ajudarmos
                            ao
                            combate do COVID-19!<br />
                            <br />
                            Por essa razão foi criada esta página que pretende mostrar quantos de nós já
                            estamos em casa.
                            <br />
                            O objetivo desta página não é o de ter informação exata, mas o de ajudar a
                            sensibilizar para esta questão tão importante neste momento.</p>
                        <br />
                        <br />
                        <md-button class="md-raised md-accent" @click="showDialog = true">
                            QUERO DIZER QUE ESTOU EM CASA
                        </md-button>
                    </div>
                </div>
            </md-card-content>
        </md-card>
         <md-dialog ref="dialog" :md-active.sync="showDialog" md-fullscreen class="md-primary">
                <loading :active.sync="sending" :can-cancel="false" :is-full-page="false" :color="'#ff5252'" :width='72'
                    :height='72'></loading>
                <md-dialog-title>Sou um brasileiro em casa</md-dialog-title>
                <md-dialog-content>

                    <div class="md-layout md-gutter">
                        <div class="md-layout-item md-medium-size-100 md-small-size-100 md-xsmall-size-100 md-large-size-100 md-xlarge-size-100" style="text-align:center">

                            <md-field :class="getValidationClass('uf')">
                                <label>Seu Estado</label>
                                <md-select v-model="form.uf" name="uf" id="uf" required @md-selected="onEstadoSelected">
                                    <md-option value="">Selecione</md-option>
                                    <md-option v-for="estado in estados" :value="estado.uf" :key="estado.id">
                                        {{estado.nome}}
                                    </md-option>
                                </md-select>
                                <span class="md-error" v-if="!$v.form.uf.required && $v.form.uf.$dirty">Estado é um
                                    campo
                                    obrigatório</span>
                            </md-field>
                            <md-field :class="getValidationClass('cidade')">
                                <label>Sua Cidade</label>
                                <md-select v-model="form.cidade" name="cidade" id="cidade" required>
                                    <md-option value="">Selecione</md-option>
                                    <md-option v-for="cidade in cidades" :value="cidade.id" :key="cidade.id">
                                        {{cidade.nome}}
                                    </md-option>
                                </md-select>
                                <span class="md-error" v-if="!$v.form.cidade.required && $v.form.cidade.$dirty">Cidade é
                                    um
                                    campo obrigatório</span>
                            </md-field>
                            <md-field :class="getValidationClass('quantidadePessoasCasa')" >
                                <label>Quantas pessoas em casa?</label>
                                <md-input type="number" v-model="form.quantidadePessoasCasa" required
                                    name="quantidadePessoasCasa" id="quantidadePessoasCasa" />
                                <span class="md-error"
                                    v-if="!$v.form.quantidadePessoasCasa.required && $v.form.quantidadePessoasCasa.$dirty">Quantidade
                                    de Pessoas é um campo obrigatório</span>
                                <span class="md-error"
                                    v-else-if="!$v.form.quantidadePessoasCasa.minValue && $v.form.quantidadePessoasCasa.$dirty">Quantidade
                                    de Pessoas deve ser no minimo 1 pessoa</span>
                                <span class="md-error"
                                    v-else-if="!$v.form.quantidadePessoasCasa.maxValue && $v.form.quantidadePessoasCasa.$dirty">Quantidade
                                    de Pessoas não deve ser maior que 10 pessoas</span>
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
                        <md-button class="md-primary md-raised" @click="saveDialog">Salvar</md-button>
                        <md-button class="md-accent md-raised" @click="showDialog = false">Fechar</md-button>
                    </md-dialog-actions>
                </md-dialog-actions>
            </md-dialog>
    </div>
</template>

<script>
import {
  validationMixin
} from 'vuelidate';
import {
  required,
  minValue,
  maxValue
} from 'vuelidate/lib/validators';
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/vue-loading.css';
import Pessoa from '../model/Pessoa';

export default {
  name: 'Hero',
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
        minValue: minValue(1),
        maxValue: maxValue(10)
      },
      dataInicioQuarentena: {
        required
      }
    }
  },
  data () {
    return {
      estados: [],
      cidades: [],
      showDialog: false,
      sending: false,

      form: {
        uf: '',
        cidade: '',
        quantidadePessoasCasa: '',
        dataInicioQuarentena: ''
      }
    };
  },
  mounted () {
    this.$store.dispatch('estado/getEstado').then(r => {
      this.estados = r.data;
    });
  },
  methods: {
    onEstadoSelected (value) {
      this.sending = true;
      this.$store.dispatch('estado/getCidade', value).then(r => {
        this.cidades = r.data;
        this.sending = false;
      });
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

<style lang="scss" scoped>

</style>
