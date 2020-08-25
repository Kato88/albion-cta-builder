<template>
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
                      <v-col cols="2" v-for="role in roles" :key="role.title + '-filter'">
                        <v-btn icon @click="filter = role.internalName">
                          <role-avatar
                            style="width: 40px; height: 40px;"
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
            <v-list-item v-for="item in filteredQueue" :key="item.name">
              <v-list-item-content>
                <v-list-item-title v-text="item.name"></v-list-item-title>
                <v-list-item-subtitle>
                  <draggable
                    class="tile"
                    :group="{name: 'player', pull:'clone', put:false }"
                    :set-data="setData"
                    :sort="false"
                    @change="log"
                    @onStart="log"
                    @end="onDragEnd"
                    handle=".handle"
                  >
                    <role-avatar
                      :data-player="item.name"
                      :data-role="role.internalName"
                      class="handle"
                      v-for="role in item.roles"
                      :key="item.name + '-' + role.internalName"
                      style="width: 32px; height: 32px; min-width: 32px; margin: 0;"
                      :src="role.internalName"
                    ></role-avatar>
                  </draggable>
                </v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { QueuePlayer, Role } from "../store/cta/types";
import RoleAvatar from "./RoleAvatar.vue";
import draggable from "vuedraggable";

@Component({
  components: { RoleAvatar, draggable }
})
export default class QueueLane extends Vue {
  @Prop() public queue!: QueuePlayer[];

  public filter = "";

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

  public log(e: any) {
    console.log("somethign changed", e);
  }

  public setData(dataTransfer: DataTransfer, wow: HTMLElement) {
    const playerName = wow.getAttribute("data-player");
    const roleName = wow.getAttribute("data-role");

    dataTransfer.setData("player", playerName as string);
    dataTransfer.setData("role", roleName as string);
  }

  public onDragEnd(e: any) {
      e.item.remove();
  }
}
</script>