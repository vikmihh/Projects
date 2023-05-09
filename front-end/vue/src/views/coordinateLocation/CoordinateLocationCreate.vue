<template>
    <h3>Create new caffe location</h3>
            <div className="row">
                <div className="col-md-12">
                    <form >
                        <div className="form-group">
                            <label className="control-label">Location </label>
                            <input type="text" v-model="coordinateLocation" className="form-control"/>
                        </div>
                        
                        <div class="form-group">
                            <a @click="submitClicked()" class="btn btn-primary" >Create</a>
                        </div>
                        <RouterLink to="/location">Back to main</RouterLink>
                    </form>
                </div>
            </div>

</template>

<script lang="ts">
import router from "@/router";
import { CoordinateLocationService } from "@/services/CoordinateLocationService";
import { useCoordinateLocationStore } from "@/stores/coordinateLocations";
import { Vue } from "vue-class-component";


export default class CoordinateLocationCreate extends Vue {
    coordinateLocation!: string;
    coordinateLocationService = new CoordinateLocationService();
    coordinateLocationsStore = useCoordinateLocationStore();
    errorMessage: string | null = null;

    async submitClicked(): Promise<void>  {
        console.log('submitClicked');
        var res = await this.coordinateLocationService.add({
            location: this.coordinateLocation
        });
         if (res.status >= 300) {
                this.errorMessage = res.status + ' ' + res.errorMsg;
                alert("Please fill all the fields!")
            } else {
                this.coordinateLocationsStore.$state.locations =
                    await this.coordinateLocationService.getAll();

                this.$router.push('/location');
            }
        }
}
</script>