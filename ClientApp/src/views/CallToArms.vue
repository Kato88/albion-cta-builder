<template>
  <v-container :fluid="!joining && cta">
    <v-row v-if="!joining && cta">
      <v-col cols="3">
        <v-card>
          <v-card-title>Pick your role</v-card-title>
          <v-card-text v-for="group in groupedRoles" :key="group.id">
            <div>{{group.key}}</div>
            <v-btn-toggle dense v-model="selectedRole">
              <div>
                <v-btn
                  :value="role.id"
                  v-for="role in group.roles"
                  :key="role.id"
                  @click="selectRole(role)"
                  icon
                  style="height: 64px; width: 64px"
                >
                  <img
                    height="64"
                    width="64"
                    :alt="role.title"
                    :title="role.title"
                    :src="`https://albiononline2d.ams3.cdn.digitaloceanspaces.com/thumbnails/orig/${role.internalName}`"
                  />
                </v-btn>
              </div>
            </v-btn-toggle>
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="9">
        <v-card>
          <v-card-text>
            <v-list subheader dense>
              <template v-for="group in groupedRoles">
                <v-subheader :key="group.key"><b>{{group.key}}s: {{group.total}}</b></v-subheader>
                <v-row :key="'row-' + group.key">
                  <v-col cols="3" v-for="role in group.roles" :key="role.id">
                    <v-list-item>
                      <v-list-item-avatar :rounded="false">
                        <img
                          :src="`https://albiononline2d.ams3.cdn.digitaloceanspaces.com/thumbnails/orig/${role.internalName}`"
                        />
                      </v-list-item-avatar>
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

<style lang="scss">
.v-list--dense .v-subheader {
  height: 0px !important;
}
</style>