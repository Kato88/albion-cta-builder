<template>
  <v-list-item-avatar :rounded="false">
    <img :src="src" />
  </v-list-item-avatar>
</template>
<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
@Component({
  components: {}
})
export default class RoleAvatar extends Vue {
  @Prop() src!: string;
}
</script>