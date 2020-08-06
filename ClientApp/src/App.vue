<template>
  <v-app v-if="connected">
    <v-app-bar app color="primary" dark>
      <v-toobar-title v-if="currentCta">
        <router-link to="/" style="margin-right: 20px;">
          <v-icon>mdi-arrow-left</v-icon>
        </router-link>
      </v-toobar-title>
      <v-toolbar-title>CTA Builder</v-toolbar-title>
      <v-spacer></v-spacer>

      <v-btn @click="dialog = true">
        <span class="mr-2">Create CTA</span>
      </v-btn>
    </v-app-bar>

    <v-main>
      <router-view />
    </v-main>

    <v-dialog v-model="dialog" width="500">
      <v-card>
        <v-card-title>Create new CTA</v-card-title>
        <v-card-text>Choose a title for you CTA.</v-card-text>
        <v-card-text>
          <v-text-field label="Title" v-model="newCtaTitle"></v-text-field>
        </v-card-text>
        <v-card-text>Optional give your zerg a hint on what to wear. Like 'Castle Defense', 'Open World', 'Brawler Setup' or whatever you want.</v-card-text>
        <v-card-text>
          <v-text-field label="Preferred Setup" v-model="newCtaSetup"></v-text-field>
        </v-card-text>
        <v-card-text>
          <v-switch
            v-model="newCtaHammers"
            :label="newCtaHammers ? 'Bring hammers' : 'Leave hammers at home'"
          ></v-switch>
        </v-card-text>
        <v-card-text>
          Number of sets to bring<br/>
          <v-slider
            v-model="newCtaExtraSets"
            :tick-labels="extraSetsLabels"
            :max="4"
            step="1"
            ticks="always"
            tick-size="1"
          ></v-slider>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="createCta">Create new CTA</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-dialog v-model="pickName" persistent max-width="600px">
      <v-card>
        <v-card-title>
          <span class="headline">Welcome</span>
        </v-card-title>
        <v-card-text>Welcome to the CTA Builder. It looks like you are here for the first time.</v-card-text>
        <v-card-text>
          First of all you need to enter your ingame name. You
          <b>cannot change the name you choose</b> so please make sure to use the correct one.
        </v-card-text>
        <v-card-text>
          <v-text-field v-model="nameInput" required label="Your name"></v-text-field>
        </v-card-text>
        <v-card-text>After closing this Dialog you can choose your role in this CTA on the left and see what you comrades picked on the right.</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="blue darken-1"
            text
            @click="saveName"
          >Yes save! I know that I can't change my Name after this.</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <v-footer color="primary">
      <v-spacer></v-spacer>
      <div style="color: white">
        &copy; {{ new Date().getFullYear() }}. Made with
        <v-icon color="red">mdi-heart</v-icon>by SirKato for ZORN
      </div>
    </v-footer>
  </v-app>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { AppStore } from "./store/index";
import Tab from "./components/Tab.vue";
import ProgressStepper from "./components/ProgressStepper.vue";
import { Cta } from "./store/cta/types";

@Component({
  components: {}
})
export default class App extends Vue {
  public pickName = false;
  public dialog = false;
  public newCtaTitle = "";
  public newCtaSetup = "";
  public newCtaHammers = true;
  public newCtaExtraSets = 2;
  public nameInput = "";
  public extraSetsLabels = [
    "None",
    '1 extra',
    '2 extra',
    '3 extra',
    'ALL',
  ];

  public async createCta() {
    await this.$store.direct.dispatch.createCta({
      title: this.newCtaTitle,
      setup: this.newCtaSetup,
      bringHammers: this.newCtaHammers,
      extraSets: this.newCtaExtraSets,
    });

    this.newCtaTitle = "";
    this.newCtaSetup = "";
    this.dialog = false;
  }

  get connected() {
    return this.$store.direct.state.callToArms.connected;
  }

  get currentCta(): Cta | undefined {
    const ctaId = this.$route.params.id;

    if (!ctaId) {
      return undefined;
    }

    return this.$store.direct.state.callToArms.callToArms.find(
      x => x.id === ctaId
    );
  }

  get totalPlayersCount() {
    if (!this.currentCta) {
      return 0;
    }

    let count = 0;

    this.currentCta.roles.forEach(x => {
      count += x.players.length;
    });

    return count;
  }

  get playerName() {
    return this.$store.direct.state.callToArms.playerName;
  }

  set playerName(val: string) {
    window.localStorage.setItem("player", val);
    this.$store.direct.commit.SET_PLAYER_NAME(val);
  }

  public saveName() {
    if (this.nameInput === "") {
      alert(
        "if you are to stupid to enter your name you should not join any CTA."
      );
      window.close();
    } else {
      this.playerName = this.nameInput;
      this.pickName = false;
    }
  }

  created() {
    this.$store.direct.dispatch.init();
    this.pickName = this.playerName === "";
  }
}
</script>
