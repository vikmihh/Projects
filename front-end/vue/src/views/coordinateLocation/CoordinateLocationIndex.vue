<template>
    <div>
        <h1>Caffe location </h1>
        <p>
            <RouterLink to="/location/create">Create New</RouterLink>
        </p>
        <div class="container">
            <table class="table">
                <thead>
                    <tr>
                        <th>
                            Location
                        </th>
                        
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item of coordinateLocationsStore.locations" :key="item.id">
                        <td>
                            {{ item.location }}
                        </td>
                        
                        <td>
                            <RouterLink :to="'/location/edit/' + item.id ">Edit</RouterLink> |
                            
                            <a @click="deleteItem(item.id!)" class="btn btn-link">Delete</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
import { useCoordinateLocationStore } from "@/stores/coordinateLocations";
import { Options, Vue } from "vue-class-component";
import { CoordinateLocationService } from "@/services/CoordinateLocationService";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class CoordinateLocationIndex extends Vue {
    coordinateLocationsStore = useCoordinateLocationStore();
    coordinateLocationService = new CoordinateLocationService();

   

    async mounted(): Promise<void> {
        console.log("mounted");
        this.coordinateLocationsStore.$state.locations = 
            await this.coordinateLocationService.getAll();
        

    }
    async deleteItem(id:string){
         await this.coordinateLocationService.delete(id);
          this.coordinateLocationsStore.$state.locations =
            await this.coordinateLocationService.getAll();
    }
}
</script>
