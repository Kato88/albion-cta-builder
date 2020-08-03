<template>
  <v-container>
    <v-card>
      <v-card-title>Open CTAs</v-card-title>
      <v-card-text>
        <v-list>
          <v-list-item v-for="cta in ctas" :key="cta.id" @click="goToCta(cta)">
            <v-list-item-content>
              <v-list-item-title v-html="cta.title"></v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { AppStore } from "./../store/index";
import Tab from "./components/Tab.vue";
import ProgressStepper from "./components/ProgressStepper.vue";
import { Cta } from "../store/cta/types";

@Component({
  components: {}
})
export default class Home extends Vue {
  get ctas(): Cta[] {
    const c = this.$store.direct.state.callToArms.callToArms;
    return c;
  }

  public goToCta(cta: Cta) {
    this.$router.push('/cta/' + cta.id);
  }
}
</script>
