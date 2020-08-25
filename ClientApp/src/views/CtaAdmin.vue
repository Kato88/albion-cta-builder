<template>
  <v-container fluid>
    <v-row style="padding-bottom: 0px; padding-top: 0px;">
      <v-col cols="12" style="padding-bottom: 0px; padding-top: 0px;">
        <cta-info-bar :parties="parties" @viewclicked="partyView = !partyView"></cta-info-bar>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="3" style="padding-top: 0px;">
        <queue-lane :queue="queue"></queue-lane>
      </v-col>
      <v-col cols="9" style="padding-top: 0px;">
        <v-expand-transition>
          <v-container v-show="partyView === true">
            <v-row style="flex-wrap: nowrap; min-height: 500px; overflow-x: auto;">
              <v-col cols="3" v-for="party in parties" :key="party.name">
                <party-lane :party="party" @dropped="onPlayerDropped" @removed="onPlayerRemoved"></party-lane>
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
import QueueLane from "../components/QueueLane.vue";

@Component({
  components: {
    PartyLane,
    RoleAvatar,
    CtaInfoBar,
    RolesDistribution,
    QueueLane
  }
})
export default class CallToArms extends Vue {
  private queue: QueuePlayer[] = [];
  private picked: QueuePlayer[] = [];

  public partyView = true;

  public parties: Array<{ name: string; players: Player[] }> = [];

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

  public onPlayerDropped(e: { player: string; role: Role }) {
    const idx = this.queue.findIndex(x => x.name === e.player);
    if (idx > -1) {
      this.picked.push(this.queue[idx]);
      this.queue.splice(idx, 1);
    }
  }

  public onPlayerRemoved(e: { player: string; role: Role }) {
    const idx = this.picked.findIndex(x => x.name === e.player);
    if (idx > -1) {
      this.queue.push(this.picked[idx]);
      this.picked.splice(idx, 1);
    }
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