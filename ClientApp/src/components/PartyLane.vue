<template>
  <v-card tile>
    <v-card-title>
      {{party.name}}
      <v-spacer></v-spacer>
      <span style="font-size: 14px;">{{party.players.length}}/20</span>
    </v-card-title>
    <v-list subheader dense style="min-height: 500px;">
      <template v-for="(players, category) in groupedByCategory">
        <template v-if="players.length > 0">
          <v-subheader :key="category">{{category}} - {{players.length}}</v-subheader>
          <v-list-item v-for="player in players" :key="player.name">
              <role-avatar style="width: 32px; height: 32px; min-width: 32px; margin: 0;" :src="player.role.internalName"></role-avatar>
            <v-list-item-content>
              <v-list-item-title v-text="player.name"></v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </template>
      </template>
    </v-list>
  </v-card>
</template>
<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { Party } from "../store/types";
import RoleAvatar from "./RoleAvatar.vue";
@Component({
  components: { RoleAvatar }
})
export default class PartyLane extends Vue {
  @Prop() public party!: Party;

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
}
</script>