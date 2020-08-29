<template>
  <v-container fluid>
    <v-row style="padding-bottom: 0px; padding-top: 0px;">
      <v-col cols="12" style="padding-bottom: 0px; padding-top: 0px;"></v-col>
    </v-row>
    <v-row>
      <v-col cols="3">
        <v-container>
          <v-row>
            <v-col cols="12">
              <v-card tile>
                <v-card-title>Your settings</v-card-title>
                <v-card-text>You can change the order via dragging</v-card-text>
                <v-list two-line>
                  <draggable :group="{name: 'picked' }" handle=".handle">
                    <v-list-item
                      v-for="(role, index) in picked"
                      :key="role.internalName + '-picked'"
                    >
                      <role-avatar class="handle" :src="role.internalName"></role-avatar>
                      <v-list-item-content>
                        <v-list-item-title>{{role.title}}</v-list-item-title>
                      </v-list-item-content>
                      <v-list-item-action style="margin-top: 0px; margin-bottom: 0px;">
                        <v-btn icon @click="remove(index)">
                          <v-icon>mdi-close</v-icon>
                        </v-btn>
                      </v-list-item-action>
                    </v-list-item>
                  </draggable>
                  <v-list-item>
                    <v-btn block>Enter queue</v-btn>
                  </v-list-item>
                </v-list>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </v-col>
      <v-col cols="9">
        <v-container>
          <v-row>
            <v-col cols="12">
              <v-card tile>
                <v-card-title>Choose your roles</v-card-title>
                <v-card-text>Choose the roles you want to play. Your selection is also saved for your next CTA aswell.</v-card-text>
                <v-card-text>
                  <v-list subheader dense>
                    <template v-for="(roles, category) in rolesGroupedByCategory">
                      <v-subheader :key="category">
                        <b>{{category}}s</b>
                      </v-subheader>
                      <v-row :key="'row-' + category">
                        <v-col
                          cols="2"
                          v-for="role in roles"
                          :key="category + '-' + role.internalName + '-overview'"
                        >
                          <v-list-item
                            @click="pick(role)"
                            :disabled="picked.filter((e) => e.internalName === role.internalName).length > 0"
                          >
                            <role-avatar :src="role.internalName"></role-avatar>
                            <v-list-item-content>
                              <v-list-item-title>{{role.title}}</v-list-item-title>
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
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
import { Role } from "./../store/cta/types";
import RoleAvatar from "./../components/RoleAvatar.vue";
import draggable from "vuedraggable";

@Component({
  components: { RoleAvatar, draggable }
})
export default class Cta extends Vue {
  public picked: any[] = [];

  get roles() {
    return this.$store.direct.state.callToArms.roles;
  }

  get rolesGroupedByCategory() {
    const grouped: any = {
      Tank: [],
      Heal: [],
      Melee: [],
      Ranged: [],
      Support: [],
      Battlemount: []
    };

    this.roles.forEach(x => {
      grouped[x.category].push(x);
    });

    return grouped;
  }

  public pick(role: Role) {
    this.picked.push(role);
  }

  public remove(index: number) {
    this.picked.splice(index, 1);
  }
}
</script>

<style lang="scss" scoped>
.handle {
  cursor: grab;
}
</style>