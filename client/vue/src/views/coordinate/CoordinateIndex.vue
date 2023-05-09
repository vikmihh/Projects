<template>
    <div>
        <h1>Table coordinates</h1>
        <p>
            <RouterLink to="/coordinate/create">Create New</RouterLink>
        </p>
        <div class="container">
            <table class="table">
                <thead>
                    <tr>
                        <th>
                            Index
                        </th>
                        <th>
                            CoordinateLocation
                        </th>

                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item of coordinatesStore.coordinates">
                        <td>
                            {{ item.index }}
                        </td>
                        <td>
                            {{ item.coordinateLocationName }}
                        </td>
                        <td>
                            <RouterLink :to="'/coordinate/edit/' + item.id">Edit</RouterLink> |
                              <a @click="deleteItem(item.id!)" class="btn btn-link">Delete</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">

import { Options, Vue } from "vue-class-component";
import { useCoordinateStore } from "@/stores/coordinates";
import { CoordinateService } from "@/services/CoordinateService";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class CoordinateIndex extends Vue {
    coordinatesStore = useCoordinateStore();
    coordinatesService = new CoordinateService();
    async mounted(): Promise<void> {
        console.log("mounted");
        this.coordinatesStore.$state.coordinates = await this.coordinatesService.getAll();

    }

     async deleteItem(id:string){
         await this.coordinatesService.delete(id);
          this.coordinatesStore.$state.coordinates =
            await this.coordinatesService.getAll();
    }
}
</script>
