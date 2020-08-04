<template>
  <v-container fluid v-if="cta">
    <v-row>
      <v-col cols="3">
        <v-card>
          <v-card-title>Pick your role in {{cta.title}}</v-card-title>
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
  public pickName = false;
  public nameInput = "";

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
    window.localStorage.setItem("player", val);
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
    this.$store.direct.dispatch.join(this.ctaId);
    this.pickName = this.playerName === "";
  }
}
</script>

<style lang="scss">
.v-list--dense .v-subheader {
  height: 0px !important;
}
</style>