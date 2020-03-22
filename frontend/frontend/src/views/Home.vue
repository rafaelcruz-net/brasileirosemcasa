<template>
  <div>
    <div class="page-container">
      <md-app md-mode="reveal">
        <md-app-toolbar>
          <span class="md-title">
            <img src="/img/logo.png" width="96" height="96" style="padding:10px">
          </span>
        </md-app-toolbar>
        <md-app-content>
          <md-content>
            <Hero />
          </md-content>
          <md-content>
            <Counter />
          </md-content>
          <md-content>
            <Map />
          </md-content>
          <md-content>
            <PeopleStackedBar />
          </md-content>
        </md-app-content>
      </md-app>
    </div>
    <footer style="min-height:60px">
      <div class="md-layout md-alignment-center-space-between footer">
        <div class="md-layout-item md-size-25 md-alignment-center-center">
          <p class="md-body-2">Fonte dos casos de COVID-19: <br /> <a
              href='https://labs.wesleycota.com/sarscov2/br/#'>https://labs.wesleycota.com/sarscov2/br/#</a></p>
        </div>

        <div class="md-layout-item md-size-10 md-alignment-center-right">
          <p class="md-body-2">Feito com
            <md-avatar class="md-avatar-icon md-small md-accent">
              <md-icon>favorite</md-icon>
            </md-avatar>
            por</p>
          <div>
            <img src="/img/logo-rodape.png" width="220" height="150" />
          </div>
        </div>
      </div>
    </footer>
    <fab :position="position" :bg-color="bgColor" :actions="fabActions" icon-size="large" main-icon="blur_on"  main-tooltip="Atendimento virtual" @showChatbot="showChatbot"></fab>
    <modal name="chatbot" classes="chat" transition="scale" :adaptive="true" width="75%"  height="75%">
       <div slot="top-right" class="ct-top-right" @click="$modal.hide('chatbot')"> FECHAR ATENDIMENTO </div>
      <iframe src="https://chatbot-corona.web.app/" style="border: none; margin: 0; padding: 0; overflow: hidden; height:100%;  width:100%;  z-index: 999999;" ></iframe>
    </modal>
  </div>
</template>
<script>
import Hero from '@/components/Hero';
import Counter from '@/components/Counter';
import Map from '@/components/Map';
import PeopleStackedBar from '@/components/PeopleStackedBar';
import fab from 'vue-fab';

export default {
  name: 'Home',
  components: {
    Hero,
    Counter,
    Map,
    PeopleStackedBar,
    fab
  },

  data: () => ({
    menuVisible: false,
    bgColor: '#00C853',
    position: 'bottom-right',
    fabActions: [{
      name: 'showChatbot',
      icon: 'tag_faces',
      tooltip: 'Iniciar atendimento virtual com a Bia',
      color: '#AA00FF'
    }]

  }),
  mounted () {

  },
  methods: {
    showChatbot () {
      this.$modal.show('chatbot');
    }
  }
};
</script>

<style lang="scss">
  .map-padding {
    margin-top: 40px;
    margin-bottom: 40px;
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

  .footer {
    min-height: inherit;
    padding: 20px;
  }

  .chat {
    background-color: transparent;
    box-shadow: 0 2px 20px 0 rgba(0, 0, 0, 0.4);
  }
   .ct-top-right {
    cursor: pointer;
    padding-top: 20px;
    padding-right: 30px;
    font-weight: 600;
    color: white;
    text-shadow: 0 0px 20px black;
  }

  .scale-enter-active,
  .scale-leave-active {
    transition: all 0.5s;
  }

  .scale-enter,
  .scale-leave-active {
    opacity: 0;
    transform: scale(0.3) translateY(24px);
  }
</style>
