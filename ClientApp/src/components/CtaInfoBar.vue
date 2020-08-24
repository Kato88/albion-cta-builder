<template>
  <v-container fluid style="padding-bottom: 0px; padding-top: 0px;">
    <v-row style="padding-bottom: 0px; padding-top: 0px;">
      <v-col cols="12" style="padding-bottom: 0px; padding-top: 0px;">
        <v-card tile dense>
          <v-card-text style="padding-bottom: 0px; padding-top: 0px;">
            <v-row class="cta-header">
              <v-col cols="1">CTA Name</v-col>
              <v-col cols="1">Bring hammers</v-col>
              <v-col cols="1">2 exta Sets</v-col>
              <v-col cols="1">Total: {{totalCount}}</v-col>
              <v-col
                cols="1"
                v-for="(group, category) in countsByCategory"
                :key="'total' + category"
              >{{category}}: {{group.length}}</v-col>
              <v-col cols="1"></v-col>
              <v-col cols="1" class="text-right">
                <v-btn icon @click="onViewGridClicked">
                  <v-icon>mdi-view-grid</v-icon>
                </v-btn>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { Party } from "../store/types";
@Component({
  components: {}
})
export default class CtaInfoBar extends Vue {
  @Prop() parties!: Party[];

  get totalCount() {
    let total = 0;

    this.parties.forEach(p => {
      total += p.players.length;
    });

    return total;
  }

  get countsByCategory() {
    const grouped: any = {
      Tank: [],
      Heal: [],
      Melee: [],
      Ranged: [],
      Support: [],
      Battlemount: []
    };

    this.parties.forEach(p => {
      p.players.forEach(pl => {
        if (grouped[pl.role.category]) {
          grouped[pl.role.category].push(p);
        } else {
          grouped[pl.role.category] = [];
          grouped[pl.role.category].push(p);
        }
      });
    });

    return grouped;
  }

  public onViewGridClicked() {
    this.$emit("viewclicked");
  }
}
</script>
