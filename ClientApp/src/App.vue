<template>
  <v-app v-if="connected">
    <v-app-bar app color="primary" dark>
      <v-toolbar-title>CTA Builder</v-toolbar-title>
     <v-spacer></v-spacer>

      <v-btn
        @click="dialog = true"
      >
        <span class="mr-2">Create CTA</span>
      </v-btn>
    </v-app-bar>

    <v-main >
      <router-view />
    </v-main>

    <v-dialog v-model="dialog" width="500">
      <v-card>
        <v-card-title>Create new CTA</v-card-title>
        <v-card-text>
          <v-text-field v-model="newCtaTitle"></v-text-field>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
            <v-btn
              color="primary"
              text
              @click="createCta"
            >
              Create new CTA
            </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-footer color="primary">
    <v-spacer></v-spacer>
    <div style="color: white">&copy; {{ new Date().getFullYear() }}. Made with <v-icon color="red">mdi-heart</v-icon> by SirKato for ZORN</div>
  </v-footer>
  </v-app>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { AppStore } from "./store/index";
import Tab from "./components/Tab.vue";
import ProgressStepper from "./components/ProgressStepper.vue";

@Component({
  components: {}
})
export default class App extends Vue {

  public dialog = false;
  public newCtaTitle = "";

  public async createCta() {
    await this.$store.direct.dispatch.createCta(this.newCtaTitle);
    this.newCtaTitle = "";
    this.dialog = false;
  }

  get connected() {
    return this.$store.direct.state.callToArms.connected;
  }

  created() {
    this.$store.direct.dispatch.init();
  }
}
</script>
