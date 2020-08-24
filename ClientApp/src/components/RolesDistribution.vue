<template>
  <v-container>
    <v-row style="flex-wrap: nowrap; min-height: 500px; overflow-x: auto;">
      <v-col cols="12">
        <v-card>
          <v-card-title>Overview by roles</v-card-title>
          <v-card-text>
            <v-list subheader dense>
              <template v-for="(group, category) in groupedByRoleByCategory">
                <v-subheader :key="category">
                  <b>{{category}}s</b>
                </v-subheader>
                <v-row :key="'row-' + category">
                  <v-col
                    cols="2"
                    v-for="(players, role) in group"
                    :key="category + '-' + role + '-overview'"
                  >
                    <v-list-item>
                      <v-btn icon>
                        <v-list-item-avatar :rounded="false">
                          <role-avatar :src="roleIcons[role]"></role-avatar>
                        </v-list-item-avatar>
                      </v-btn>
                      <v-list-item-content>
                        <v-list-item-title>{{players.length}} {{role}}</v-list-item-title>
                        <v-list-item-subtitle>
                          <div v-for="player in players" :key="player.name">{{player.name}}</div>
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
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import RoleAvatar from "./RoleAvatar.vue";
import { Party } from "../store/types";
@Component({
  components: { RoleAvatar }
})
export default class RolesDistribution extends Vue {
  @Prop() parties!: Party[];

  get groupedByRoleByCategory() {
    const grouped: any = {
      Tank: {},
      Heal: {},
      Melee: {},
      Ranged: {},
      Support: {},
      Battlemount: {}
    };

    this.$store.direct.state.callToArms.roles.forEach(p => {
      if (!grouped[p.category][p.title]) {
        grouped[p.category][p.title] = [];
      }
    });

    this.parties.forEach(p => {
      p.players.forEach(pl => {
        grouped[pl.role.category][pl.role.title].push(pl);
      });
    });

    return grouped;
  }

  get roleIcons() {
    const icons: any = {};

    this.$store.direct.state.callToArms.roles.forEach(r => {
      icons[r.title] = r.internalName;
    });

    return icons;
  }
}
</script>