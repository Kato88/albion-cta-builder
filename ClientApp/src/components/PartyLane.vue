<template>
  <v-card tile>
    <v-card-title>
      {{party.name}}
      <v-spacer></v-spacer>
      <span style="font-size: 14px;">{{party.players.length}}/20</span>
    </v-card-title>

    <v-list subheader dense style="min-height: 500px;">
      <draggable
        :group="{name: 'player', pull: false, put: true }"
        :sort="false"
        :ghostClass="'droparea'"
        dragClass="droparea"
        @change="log"
        @add="add"
        style="height: 100%; min-height: 500px;"
      >
        <template v-for="(players, category) in groupedByCategory">
          <template>
            <v-subheader style="height: 15px !important;" :key="category">{{category}} - {{players.length}}</v-subheader>
            <v-fade-transition :key="category + '-fade'" class="py-0" group hide-on-leave>
              <v-list-item v-for="(player, index) in players" :key="player.name">
                <role-avatar
                  style="width: 32px; height: 32px; margin: 0px;"
                  :src="player.role.iconUrl"
                ></role-avatar>
                <v-list-item-content>
                  <v-list-item-title v-text="player.name"></v-list-item-title>
                </v-list-item-content>
                <v-list-item-action style="margin-top: 0px; margin-bottom: 0px;">
                  <v-btn icon @click="remove(player, index)">
                    <v-icon>mdi-close</v-icon>
                  </v-btn>
                </v-list-item-action>
              </v-list-item>
            </v-fade-transition>
          </template>
        </template>
      </draggable>
    </v-list>
  </v-card>
</template>
<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { Party } from "../store/types";
import RoleAvatar from "./RoleAvatar.vue";
import draggable from "vuedraggable";
import { QueuePlayer, Role, Player } from "../store/cta/types";
@Component({
  components: { RoleAvatar, draggable }
})
export default class PartyLane extends Vue {
  @Prop() public party!: Party;

  public dropList: QueuePlayer[] = [];

  get groupedByCategory() {
    const grouped: any = {
      Tank: [],
      Heal: [],
      Melee: [],
      Ranged: [],
      Support: [],
      Battlemount: []
    };

    this.party.players.forEach(p => {
      if (grouped[p.role.category]) {
        grouped[p.role.category].push(p);
      } else {
        grouped[p.role.category] = [];
        grouped[p.role.category].push(p);
      }
    });

    return grouped;
  }

  public log(e: any) {
    console.log("party lane", e);
  }

  public add(e: any) {
    const dataTransfer = e.originalEvent.dataTransfer as DataTransfer;
    const playerName = dataTransfer.getData("player") as string;
    const roleName = dataTransfer.getData("role") as string;
    const role = this.$store.direct.state.callToArms.roles.find(
      x => x.internalName === roleName
    );

    if (!role) {
      return;
    }

    console.log(role);
    console.log(
      `moving player '${playerName}' as '${role.internalName}' to '${this.party.name}'`
    );
    this.$emit("dropped", {
      player: playerName,
      role: role
    });

    this.party.players.push({
      name: playerName,
      role: role
    });

    // e.item.remove();
  }

  public remove(player: Player, index: number) {
    const idx = this.party.players.findIndex(x => x.name === player.name);
    if (idx > -1) {
      this.party.players.splice(idx, 1);
    }

    this.$emit("removed", {
      player: player.name,
      role: player.role
    });
  }
}
</script>

<style lang="scss" scoped>
.droparea {
  display: none !important;
}
</style>