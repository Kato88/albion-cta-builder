<template>
  <v-container fluid>
    <v-row style="padding-bottom: 0px; padding-top: 0px;">
      <v-col cols="12" style="padding-bottom: 0px; padding-top: 0px;">
        <cta-info-bar :parties="parties" @viewclicked="partyView = !partyView"></cta-info-bar>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="3" style="padding-top: 0px;">
        <v-container>
          <v-row>
            <v-col cols="12">
              <v-card tile max-width="600" class="mx-auto">
                <v-card-title color="light-blue" dark>
                  Queue {{queue.length}}
                  <v-spacer></v-spacer>
                  <v-btn icon v-if="filter" @click="filter = ''">
                    <v-icon>mdi-close</v-icon>
                  </v-btn>
                  <v-menu>
                    <template v-slot:activator="{ on }">
                      <v-btn icon v-on="on">
                        <v-icon>mdi-magnify</v-icon>
                      </v-btn>
                    </template>
                    <v-card tile>
                      <v-container fluid>
                        <template v-for="(roles, category) in rolesGrouped">
                          <v-subheader :key="category">{{category}}</v-subheader>
                          <v-row :key="category + 'row'">
                            <v-col cols="2" v-for="role in roles" :key="role.title">
                              <v-btn icon @click="filter = role.internalName">
                                <role-avatar
                                  style="width: 40px; height: 40px; min-width: 40px;"
                                  :src="role.internalName"
                                ></role-avatar>
                              </v-btn>
                            </v-col>
                          </v-row>
                        </template>
                      </v-container>
                    </v-card>
                  </v-menu>
                </v-card-title>
                <v-list dense two-line subheader style="min-height: 500px;">
                  <v-menu offset-x v-for="(item, queueIndex) in filteredQueue" :key="item.name">
                    <template v-slot:activator="{ on }">
                      <v-list-item v-on="on">
                        <v-list-item-content>
                          <v-list-item-title v-text="item.name"></v-list-item-title>
                          <v-list-item-subtitle>
                            <role-avatar
                              v-for="role in item.roles"
                              :key="role.internalName"
                              style="width: 32px; height: 32px; min-width: 32px; margin: 0;"
                              :src="role.internalName"
                            ></role-avatar>
                          </v-list-item-subtitle>
                        </v-list-item-content>
                      </v-list-item>
                    </template>
                    <v-list dense>
                      <v-menu offset-x v-for="role in item.roles" :key="role.internalName">
                        <template v-slot:activator="{ on }">
                          <v-list-item v-on="on">
                            <role-avatar
                              style="width: 40px; height: 40px; min-width: 40px;"
                              :src="role.internalName"
                            ></role-avatar>
                            <v-list-item-content>{{role.title}}</v-list-item-content>
                          </v-list-item>
                        </template>
                        <v-list dense>
                          <v-list-item
                            v-for="party in parties"
                            :key="party.name"
                            @click="moveToParty(item, queueIndex, role, party)"
                          >
                            <v-list-item-title>{{party.name}}</v-list-item-title>
                          </v-list-item>
                        </v-list>
                      </v-menu>
                    </v-list>
                  </v-menu>
                </v-list>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </v-col>
      <v-col cols="9" style="padding-top: 0px;">
        <v-expand-transition>
          <v-container v-show="partyView === true">
            <v-row style="flex-wrap: nowrap; min-height: 500px; overflow-x: auto;">
              <v-col cols="3" v-for="party in parties" :key="party.name">
                <party-lane :party="party"></party-lane>
              </v-col>
              <v-col cols="2">
                <v-btn block text transparent @click="addParty">+</v-btn>
              </v-col>
            </v-row>
          </v-container>
        </v-expand-transition>
        <v-expand-transition>
          <roles-distribution v-show="!partyView" :parties="parties"></roles-distribution>
        </v-expand-transition>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { AppStore } from "./../store/index";
import Tab from "./components/Tab.vue";
import ProgressStepper from "./components/ProgressStepper.vue";
import { Role, QueuePlayer, Player } from "../store/cta/types";
import PartyLane from "../components/PartyLane.vue";
import RoleAvatar from "../components/RoleAvatar.vue";
import CtaInfoBar from "../components/CtaInfoBar.vue";
import RolesDistribution from "../components/RolesDistribution.vue";

@Component({
  components: { PartyLane, RoleAvatar, CtaInfoBar, RolesDistribution }
})
export default class CallToArms extends Vue {
  public queue: QueuePlayer[] = [];
  public filter = "";
  public partyView = true;

  public parties: Array<{ name: string; players: Player[] }> = [];

  get rolesGrouped() {
    const grouped: any = {
      Tank: [],
      Heal: [],
      Melee: [],
      Ranged: [],
      Support: [],
      Battlemount: []
    };

    this.$store.direct.state.callToArms.roles.forEach(p => {
      if (grouped[p.category]) {
        grouped[p.category].push(p);
      } else {
        grouped[p.category] = [];
        grouped[p.category].push(p);
      }
    });

    return grouped;
  }

  get filteredQueue() {
    if (!this.filter) {
      return this.queue;
    }

    const filteredQueue: QueuePlayer[] = [];

    this.queue.forEach(x => {
      if (x.roles.findIndex(y => y.internalName === this.filter) !== -1) {
        filteredQueue.push(x);
      }
    });

    return filteredQueue;
  }

  public addParty() {
    this.parties.push({
      name: `Party ${this.parties.length + 1}`,
      players: [] as Player[]
    });
  }

  public moveToParty(item: any, index: number, role: string, party: any) {
    party.players.push({
      name: item.name,
      role: role
    });

    this.queue.splice(index, 1);
  }

  created() {
    const playersCount = 20;

    for (let i = 0; i < playersCount; i++) {
      const numOfRoles = Math.ceil(Math.random() * 5);
      const queuePlayer = {
        name: this.makeid(10),
        roles: [] as Role[]
      };

      const roles = [...this.$store.direct.state.callToArms.roles];
      for (let j = 0; j < numOfRoles; j++) {
        const roleIndex = Math.floor(Math.random() * roles.length);
        queuePlayer.roles.push(roles[roleIndex]);
        roles.splice(roleIndex, 1);
      }

      this.queue.push(queuePlayer);
    }

    console.log(this.queue);
  }

  private makeid(length: number) {
    var result = "";
    var characters =
      "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
  }
}
</script>

<style lang="scss">
.v-list--dense .v-subheader {
  height: 0px !important;
}

.cta-header {
  .col {
    padding-top: 0px;
    padding-bottom: 0px;
    line-height: 36px;
  }
}
</style>