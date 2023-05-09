<template>
    <h1>Login</h1>

    <div class="row">
        <div class="col-md-12">

            <div v-if="errorMsg.length >0 && errorMsg != 'Fill'" class="text-danger validation-summary-errors" data-valmsg-summary="true">
                <ul>
                    <li>{{ errorMsg }}</li>
                </ul>
            </div>

            <div>
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

                    <input @click="loginClicked()" type="submit" value="Login" class="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>

</template>

<script lang="ts">
import router from "@/router";
import { IdentityService } from "@/services/IdentityService";
import { useIdentityStore } from "@/stores/identity";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class Login extends Vue {
    identityStore = useIdentityStore();

    email: string = '';
    password: string = '';
    errorMsg: string = '';


    identityService = new IdentityService();

    async loginClicked(): Promise<void> {
        this.errorMsg = "";
        if (this.isFormCorrectlyFilled()) {

            console.log('submitClicked');
            var res = await this.identityService.login(this.email, this.password);
            console.log(res);
            console.log(res.data);
            if (res.status >= 300) {
                this.errorMsg = res.status + ' ' + res.data;
            } else {
                this.identityStore.$state.jwt = res.data!;
            }
            if (res.status === 200) {
                router.push('/')
                console.log('pushed')
            }

        } else {
            this.errorMsg = 'Fill';

        }

    }

    isFormCorrectlyFilled(): boolean {
        return this.email.length > 0 &&
            this.password.length > 0
    }

}
</script>

