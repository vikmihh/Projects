<template>
    <h3>Edit caffe location</h3>
            <div className="row">
                <div className="col-md-12">
                    <form >
                        <div className="form-group">
                            <label className="control-label">Location </label>
                            <input type="text" className="form-control" v-model="coordinateLocation"/>
                        </div>
                        
                        <div class="form-group">
                            <a @click="submitClicked()"  class="btn btn-primary" >Save</a>
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
import { Options, Vue } from "vue-class-component";

@Options({
    components: {
    },
    props: {
        id: String
    },
    emits: [],
})

export default class CoordinateLocationEdit extends Vue {
    id!: string;
    coordinateLocation: string = "";
    coordinateLocationsStore = useCoordinateLocationStore();
    coordinateLocationService = new CoordinateLocationService();
    errorMessage: string | null = null;

    async mounted(): Promise<void> {
        console.log("mounted");
        this.coordinateLocationsStore.$state.locations = [];
        this.coordinateLocation = (await this.coordinateLocationService.get(this.id)).location;
    }

    async submitClicked(): Promise<void>  {
        var res = await this.coordinateLocationService.put(this.id, {
            id: this.id,
            location: this.coordinateLocation
        });
        console.log("stat")
        console.log(res)
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