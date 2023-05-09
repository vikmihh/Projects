<template>
    <h1>My account</h1>
     <tr v-if="userInCategoriesStore.userInCategories.length !=0">
            {{ userInCategoriesStore.userInCategories[0].userCategoryName }}
        </tr>
        <tr>
            {{ identityStore.$state.jwt?.firstName }}
            {{ identityStore.$state.jwt?.lastName }}
        </tr>
        
        <tr>
            {{ identityStore.$state.jwt?.email }}
        </tr>
</template>

<script lang="ts">
import { UserInCategoryService } from "@/services/UserInCategoryService";
import { useIdentityStore } from "@/stores/identity";
import { useUserCategoryStore } from "@/stores/userCategories";
import { useUserInCategoryStore } from "@/stores/userInCategoryStore";
import { Options, Vue } from "vue-class-component";

@Options({
    components: {
    },
    props:{},
    emits: [],
})
export default class UserIndex extends Vue {
    
    identityStore = useIdentityStore();
     userCategoryStore = useUserCategoryStore();
     userInCategoriesStore = useUserInCategoryStore();
    userInCategoryService = new UserInCategoryService();

     async mounted(): Promise<void> {
         console.log(await this.userInCategoryService.getUserInCategory())
           this.userInCategoriesStore.$state.userInCategories = [];
        this.userInCategoriesStore.$state.userInCategories.push(await this.userInCategoryService.getUserInCategory());
       

    }
}
</script>
