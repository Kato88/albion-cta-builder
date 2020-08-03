<template>
  <v-container>
    <v-row>
      <v-col cols="4">
        <v-card>
          <v-card-title>Your role</v-card-title>
          <v-card-text>
            <v-text-field v-model="playerName" required label="Your name"></v-text-field>
          </v-card-text>
          <v-card-text v-for="group in groupedRoles" :key="group.id">
            <div>{{group.key}}s</div>
            <v-btn-toggle dense v-model="selectedRole">
              <div>
                <v-btn
                  :value="role.id"
                  v-for="role in group.roles"
                  :key="role.id"
                  @click="selectRole(role)"
                >{{role.title}}</v-btn>
              </div>
            </v-btn-toggle>
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="8">
        <v-row>
          <v-col cols="6" v-for="group in groupedRoles" :key="group.key">
            <v-card>
              <v-card-title>{{group.key}} ({{group.total}})</v-card-title>
              <v-card-text>
                <v-expansion-panels multiple accordion>
                  <v-expansion-panel v-for="role in group.roles" :key="role.title">
                    <v-expansion-panel-header>{{role.title}}: {{role.players.length}}</v-expansion-panel-header>
                    <v-expansion-panel-content><span v-for="player in role.players" :key="player.name">{{player.name}}, </span></v-expansion-panel-content>
                  </v-expansion-panel>
                </v-expansion-panels>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { AppStore } from "./../store/index";
import Tab from "./components/Tab.vue";
import ProgressStepper from "./components/ProgressStepper.vue";
import { Role } from "../store/cta/types";

@Component({
  components: {}
})
export default class CallToArms extends Vue {
  public selectedRole = "";

  get ctaId() {
    return this.$route.params.id;
  }

  get cta() {
    return this.$store.direct.state.callToArms.callToArms.find(
      x => x.id === this.ctaId
    );
  }

  get groupedRoles() {
    if (!this.cta) {
      return [];
    }

    const grouped: Array<{ key: string; total: number; roles: Role[] }> = [];

    this.cta.roles.forEach(x => {
      let g = grouped.find(group => group.key === x.category);
      if (!g) {
        g = {
          key: x.category,
          roles: [],
          total: 0
        };

        grouped.push(g);
      }

      g.total += x.players.length;
      g.roles.push(x);
    });

    return grouped;
  }

  get playerName() {
    return this.$store.direct.state.callToArms.playerName;
  }

  set playerName(val: string) {
    window.localStorage.setItem("playerName", val);
    this.$store.direct.commit.SET_PLAYER_NAME(val);
  }

  public selectRole(role: Role) {
    if (!this.playerName) {
      this.selectedRole = "";
      alert("set your name!");
      return;
    }

    this.$store.direct.dispatch.selectRole({
      ctaId: this.ctaId,
      roleId: role.id,
      playerName: this.playerName
    });
  }

  created() {
    this.$store.direct.dispatch.join(this.ctaId);
  }
}
</script>
