<template>
    
</template>

<script lang="ts">
import { Vue, Component, Prop } from "vue-property-decorator";
@Component({
  components: {}
})
export default class Cta extends Vue {
  @Prop() src!: string;

  get iconUrl() {
    if (this.src && this.src.startsWith("http")) {
      return this.src;
    }

    return `https://albiononline2d.ams3.cdn.digitaloceanspaces.com/thumbnails/orig/${this.src}`;
  }
}
</script>