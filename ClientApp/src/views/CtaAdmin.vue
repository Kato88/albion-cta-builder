<template>
  <v-container fluid>
    <v-row style="padding-bottom: 0px; padding-top: 0px;">
      <v-col cols="12" style="padding-bottom: 0px; padding-top: 0px;">
        <v-container fluid style="padding-bottom: 0px; padding-top: 0px;">
          <v-row style="padding-bottom: 0px; padding-top: 0px;">
            <v-col cols="12" style="padding-bottom: 0px; padding-top: 0px;">
              <v-card tile dense>
                <v-card-text style="padding-bottom: 0px; padding-top: 0px;">
                  <v-row>
                    <v-col cols="2">Tanks: 2</v-col>
                    <v-col cols="2">Heals: 4</v-col>
                    <v-col cols="2">Ranged: 2</v-col>
                    <v-col cols="2">Melee: 4</v-col>
                    <v-col cols="2">Support: 4</v-col>
                    <v-col cols="2">Battlemounts: 0</v-col>
                  </v-row>
                </v-card-text>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
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
                  <v-btn icon>
                    <v-icon>mdi-magnify</v-icon>
                  </v-btn>
                </v-card-title>
                <v-list dense two-line subheader style="min-height: 500px;">
                  <v-menu offset-x v-for="(item, queueIndex) in queue" :key="item.name">
                    <template v-slot:activator="{ on }">
                      <v-list-item v-on="on">
                        <v-list-item-content>
                          <v-list-item-title v-text="item.name"></v-list-item-title>
                          <v-list-item-subtitle>
                            <v-list-item-avatar
                              style="width: 32px; height: 32px; min-width: 32px; margin: 0;"
                              v-for="role in item.roles"
                              :key="role"
                            >
                              <img
                                :src="`https://albiononline2d.ams3.cdn.digitaloceanspaces.com/thumbnails/orig/`"
                              />
                            </v-list-item-avatar>
                          </v-list-item-subtitle>
                        </v-list-item-content>
                      </v-list-item>
                    </template>
                    <v-list dense>
                      <v-menu offset-x v-for="role in item.roles" :key="role">
                        <template v-slot:activator="{ on }">
                          <v-list-item v-on="on">
                            <v-list-item-avatar style="width: 40px; height: 40px; min-width: 40px;">
                              <img
                                :src="`https://albiononline2d.ams3.cdn.digitaloceanspaces.com/thumbnails/orig/`"
                              />
                            </v-list-item-avatar>
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
        <v-container>
          <v-row style="flex-wrap: nowrap; min-height: 500px; overflow-x: auto;">
            <v-col cols="3" v-for="party in parties" :key="party.name">
              <v-card tile>
                <v-card-title>{{party.name}} <v-spacer></v-spacer> <span style="font-size: 14px;">{{party.players.length}}/20</span></v-card-title>
                <v-list dense style="min-height: 500px;">
                  <v-list-item v-for="player in party.players" :key="player.name">
                    <v-list-item-avatar
                      style="width: 32px; height: 32px; min-width: 32px; margin: 0;"
                    >
                      <img
                        :src="`https://albiononline2d.ams3.cdn.digitaloceanspaces.com/thumbnails/orig/${player.role}`"
                      />
                    </v-list-item-avatar>
                    <v-list-item-content>
                      <v-list-item-title v-text="player.name"></v-list-item-title>
                    </v-list-item-content>
                  </v-list-item>
                </v-list>
              </v-card>
            </v-col>
            <v-col cols="2">
              <v-btn block text transparent @click="addParty">+</v-btn>
            </v-col>
          </v-row>
        </v-container>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { AppStore } from "./../store/index";
import Tab from "./components/Tab.vue";
import ProgressStepper from "./components/ProgressStepper.vue";
import { Role, QueuePlayer } from "../store/cta/types";

@Component({
  components: {}
})
export default class CallToArms extends Vue {
  public queue: QueuePlayer[] = [];

  public parties = [
    {
      name: "Party 1",
      players: [
        {
          name: "Lugzi",
          role: "T8_2H_MACE_MORGANA"
        },
        {
          name: "Erdnussflipz",
          role: "T8_2H_RAM_KEEPER"
        },
        {
          name: "LosHomos",
          role: "T8_2H_ICEGAUNTLETS_HELL"
        }
      ]
    },
    {
      name: "Party 2",
      players: [
        {
          name: "Eldrisgaming",
          role: "T8_MAIN_RAPIER_MORGANA"
        }
      ]
    }
  ];

  public addParty() {
    this.parties.push({
      name: `Party ${this.parties.length + 1}`,
      players: []
    });
  }

  public moveToParty(item: any, index: number, role: string, party: any) {
    console.log("move to party", item, role, party);
    party.players.push({
      name: item.player,
      role: role
    });

    this.queue.splice(index, 1);
  }

  created() {
    const playersCount = Math.ceil(Math.random() * 20);
    for (let i = 0; i < playersCount; i++) {
      const numOfRoles = Math.ceil(Math.random()  * 5);
      const queuePlayer = {
        name: this.makeid(10),
        roles: []
      };

      const roles = [...this.$store.direct.state.callToArms.roles];
      for (let j = 0; j < numOfRoles; j++) {
        const roleIndex = Math.ceil(Math.random() * roles.length);
        const r = roles.slice(roleIndex, roleIndex + 1);
        // @ts-ignore
        queuePlayer.roles.push(r[0] as Role);
      }

      this.queue.push(queuePlayer);
    }

    console.log(this.queue);
  }

  private makeid(length: number) {
   var result           = '';
   var characters       = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
   var charactersLength = characters.length;
   for ( var i = 0; i < length; i++ ) {
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
</style>