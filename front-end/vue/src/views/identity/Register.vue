<template>
    <h1>Register</h1>

    <div class="row">
        <div class="col-md-12">

            <div v-if="errorMsg.length >0 && errorMsg != 'Fill'" class="text-danger validation-summary-errors" data-valmsg-summary="true">
                <ul>
                    <li>{{ errorMsg }}</li>
                </ul>
            </div>

            <div>
                <div v-if="errorMsg == 'Fill' && firstName.length < 1"
                    class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Please fill the First Name</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="FirstName">First Name</label>
                    <input v-model="firstName" class="form-control" type="text" />
                </div>

                <div v-if="errorMsg == 'Fill' && lastName.length < 1"
                    class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Please fill the Last Name</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="LastName">Last Name</label>
                    <input v-model="lastName" class="form-control" type="text" />
                </div>

                <div v-if="errorMsg == 'Fill' && email.toString().length < 1"
                    class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Please fill the email</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="Email">Email</label>
                    <input v-model="email" class="form-control" type="text" />
                </div>

                <div v-if="errorMsg == 'Fill' && password.toString().length < 1"
                    class="text-danger validation-summary-errors" data-valmsg-summary="true">
                    <ul>
                        <li>Please fill the password</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="control-label" for="Password">Password</label>
                    <input v-model="password" class="form-control" type="password" />
                </div>
                <div class="form-group">
                    <input @click="regiterClicked()" type="submit" value="Register" class="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>

</template>

<script lang="ts">
import router from "@/router";
import { IdentityService } from "@/services/IdentityService";
import { useIdentityStore } from "@/stores/identity";
import type { AxiosError } from "axios";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class Register extends Vue {
    identityStore = useIdentityStore();
    firstName: string = '';
    lastName: string = '';
    email: string = '';
    password: string = '';
    errorMsg: string = '';


    identityService = new IdentityService();

    async regiterClicked(): Promise<void> {
        this.errorMsg = "";
       
        if (this.isFormCorrectlyFilled()) {
            console.log('registeredClicked');
             var res = await this.identityService.register(this.firstName, this.lastName, this.email, this.password);
            
            if (res.status >= 300) {
                this.errorMsg = res.status + ' ' + res.errorMsg;
            } else {
                this.identityStore.$state.jwt = res.data!;
                router.push('/');
            }
        }else{
            this.errorMsg = "Fill";
        }

    }

    isFormCorrectlyFilled(): boolean {
        return this.firstName.length > 0 &&
            this.lastName.length > 0 &&
            this.email.toString().length > 0 &&
            this.password.toString().length > 0
    }

}
</script>

