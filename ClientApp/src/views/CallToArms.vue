<template>
  <v-container :fluid="!joining && cta">
    <v-row v-if="!joining && cta">
      <v-col cols="2">
        <v-card>
          <v-card-title>{{cta.title}}</v-card-title>
            <v-list dense>
              <v-subheader style="padding-bottom: 10px; padding-top: 10px;"><b>CTA Information</b></v-subheader>
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title>
                    Setup: {{cta.setup}}
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title>
                    Participants: {{totalPlayersCount}}
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title>
                    Hammers: {{cta.bringHammers ? 'YES!' : 'Leave them at home'}}
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-title>
                    Extra sets: {{cta.extraSets}}
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
              <v-subheader style="padding-bottom: 10px; padding-top: 10px;"><b>Setup ratio</b></v-subheader>
              <v-list-item v-for="group in groupedRoles" :key="group-key">
                <v-list-item-content>
                  <v-list-item-title>
                    {{group.key}}: <span v-if="totalPlayersCount > 0">{{Number.parseFloat((group.total * 100) / totalPlayersCount).toFixed(0)}}%</span><span v-else>0%</span>
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </v-list>    
        </v-card>
      </v-col>
      <v-col cols="10">
        <v-card>
          <v-card-title>Pick your role by clicking on the icon of your weapon</v-card-title>
          <v-card-text>
            <v-list subheader dense>
              <template v-for="group in groupedRoles">
                <v-subheader :key="group.key"><b>{{group.key}}s: {{group.total}}</b></v-subheader>
                <v-row :key="'row-' + group.key">
                  <v-col cols="2" v-for="role in group.roles" :key="role.id">
                    <v-list-item>
                      <v-btn @click="selectRole(role)" icon>
                      <v-list-item-avatar :rounded="false">
                        <img
                          v-if="role.internalName && role.internalName.startsWith('http')"
                          :src="role.internalName"
                        />
                        <img
                          v-else
                          :src="`https://render.albiononline.com/v1/item/${role.internalName}`"
                        />
                      </v-list-item-avatar>
                      </v-btn>
                      <v-list-item-content>
                        <v-list-item-title>{{role.players.length}} {{role.title}}</v-list-item-title>
                        <v-list-item-subtitle>
                          <div v-for="player in role.players" :key="player.name">{{player.name}}</div>
                        </v-list-item-subtitle>
                      </v-list-item-content>
                    </v-list-item>
                  </v-col>
                </v-row>
              </template>
            </v-list>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-row v-if="!joining && !cta">
      <v-col cols="4">
        <v-card>
          <v-card-text>
            Sadly there is no CTA under this address any more. All CTAs a cleared on a daily basis. Good luck next time.
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-overlay :value="joining">
        <v-progress-circular indeterminate size="64"></v-progress-circular>
      </v-overlay>
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

  get totalPlayersCount() {
    if (!this.cta) {
      return 0;
    }

    let count = 0;

    this.cta.roles.forEach(x => {
      count += x.players.length;
    });

    return count;
  }

  get joining() {
    return this.$store.direct.state.callToArms.joining;
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

  get playerGuild() {
    return this.$store.direct.state.callToArms.playerGuild;
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
      playerName: this.playerName,
    });
  }

  

  created() {
    this.$store.direct.dispatch.join(this.ctaId);
  }
}
</script>

<style lang="scss">
.v-list--dense .v-subheader {
  height: 0px !important;
}
</style>