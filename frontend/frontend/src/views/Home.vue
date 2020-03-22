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
  </div>
</template>
<script>
import Hero from '@/components/Hero';
import Counter from '@/components/Counter';
import Map from '@/components/Map';
import PeopleStackedBar from '@/components/PeopleStackedBar';

export default {
  name: 'Home',
  components: {
    Hero,
    Counter,
    Map,
    PeopleStackedBar
  },

  data: () => ({
    menuVisible: false

  }),
  mounted () {

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
</style>
