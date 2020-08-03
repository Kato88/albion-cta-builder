<template>
  <v-container>
    <v-card>
      <v-card-title v-if="cta">
        {{cta.title}}
      </v-card-title>
    </v-card>
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { AppStore } from "./../store/index";
import Tab from "./components/Tab.vue";
import ProgressStepper from "./components/ProgressStepper.vue";

@Component({
  components: {}
})
export default class CallToArms extends Vue {
  get ctaId() {
    return this.$route.params.id;
  }

  get cta() {
    return this.$store.direct.state.callToArms.callToArms.find((x) => x.id === this.ctaId);
  }

  created() {

  }
}
</script>
