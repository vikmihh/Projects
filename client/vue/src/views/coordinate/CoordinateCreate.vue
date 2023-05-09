<template>
    <h3>Create new coordinate</h3>
    <div className="row">
        <div className="col-md-12">
            <form>
                <div className="form-group">
                    <label className="control-label">Index </label>
                    <input type="text" className="form-control" v-model="index" />
                </div>
                <div className="form-group">
                    <label className="control-label">Coordinate location</label>
                    <select class="form-control" data-val="true"
                        data-val-required="The ItemCategoryId field is required." id="ItemCategoryId"
                        name="ItemCategoryId" v-model="selectedCoordinateLocationId">
                        <option v-for="item of coordinateLocationsStore.locations" :value="item.id">{{ item.location }}
                        </option>

                    </select>
                </div>
                <div class="form-group">
                    <a @click="submitClicked()" type="submit"  class="btn btn-primary" >Create</a>
                </div>
                <RouterLink to="/coordinate">Back to main</RouterLink>
            </form>
        </div>
    </div>

</template>

<script lang="ts">
import router from "@/router";
import { CoordinateLocationService } from "@/services/CoordinateLocationService";
import { CoordinateService } from "@/services/CoordinateService";
import { useCoordinateLocationStore } from "@/stores/coordinateLocations";
import { useCoordinateStore } from "@/stores/coordinates";
import { Vue } from "vue-class-component";


export default class CoordinateCreate extends Vue {
    index: string = "";
    errorMessage: string | null = null;

    coordinatesStore = useCoordinateStore();
    coordinateService = new CoordinateService();
    selectedCoordinateLocationId: string = "";
    coordinateLocationsService = new CoordinateLocationService();
    coordinateLocationsStore = useCoordinateLocationStore();


    async mounted(): Promise<void> {
        console.log("mountedProcessOrder");
        this.coordinateLocationsStore.$state.locations = await this.coordinateLocationsService.getAll();

    }
    async submitClicked(): Promise<void> {
        console.log('submitClicked');
        var res = await this.coordinateService.add({
             index:this.index,
             coordinateLocationId:this.selectedCoordinateLocationId,
             coordinateLocationName:""
         })
          if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMsg;
                alert("Please fill all the fields!")
            } else {
                this.coordinatesStore.$state.coordinates =
                    await this.coordinateService.getAll();

                this.$router.push('/coordinate');
            }

    }
}
</script>