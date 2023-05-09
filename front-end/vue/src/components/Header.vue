<template>
    <header>
        <nav
            class=" navbar navbar-fixed-top navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between container">
                <ul class="navbar-nav flex-grow-1">
                    <li class="nav-item">
                        <RouterLink to="/" class="nav-link text-dark" active-class="active">Home</RouterLink>
                    </li>

                    <li class="nav-item">
                        <RouterLink to="/tickets" class="nav-link text-dark" active-class="active">
                            {{ identityStore.$state.jwt?.role == 'admin' ? "Tickets" : "Buy pass" }}
                        </RouterLink>
                    </li>
                    <li class="nav-item">
                        <RouterLink to="/menuItems" class="nav-link text-dark" active-class="active">
                            Menu
                        </RouterLink>
                    </li>
                    <li class="nav-item" v-if="identityStore.$state.jwt?.role == 'admin'">
                        <RouterLink to="/itemCategories" class="nav-link text-dark" active-class="active">
                            Menu categories
                        </RouterLink>
                    </li>
                    <li class="nav-item" v-if="identityStore.$state.jwt?.role == 'admin'">
                        <RouterLink to="/userCategory" class="nav-link text-dark" active-class="active">
                            User categories
                        </RouterLink>
                    </li>
                    <li class="nav-item" v-if="identityStore.$state.jwt?.role == 'admin'">
                        <RouterLink to="/couponCategory" class="nav-link text-dark" active-class="active">
                            Coupons
                        </RouterLink>
                    </li>
                     <li class="nav-item" v-if="identityStore.$state.jwt?.role == 'admin'">
                        <RouterLink to="/coordinate" class="nav-link text-dark" active-class="active">
                            Table
                        </RouterLink>
                    </li>
                    <li class="nav-item" v-if="identityStore.$state.jwt?.role == 'admin'">
                        <RouterLink to="/location" class="nav-link text-dark" active-class="active">
                            Caffe
                        </RouterLink>
                    </li>
                    <li class="nav-item" v-if="identityStore.$state.jwt?.role == 'user'">
                        <RouterLink to="/order" class="nav-link text-dark" active-class="active">Cart
                        </RouterLink>
                    </li>
                </ul>

                <ul class="navbar-nav">
                    <template v-if="identityStore.$state.jwt == null">
                        <li>
                            <RouterLink to="/register" class="nav-link text-dark" active-class="active">Register
                            </RouterLink>
                        </li>
                        <li>
                            <RouterLink to="/login" class="nav-link text-dark" active-class="active">Login
                            </RouterLink>
                        </li>
                    </template>

                    <template v-else>
                        <li class="nav-item dropdown">
                            <a class="nav-link text-dark dropdown-toggle" data-bs-toggle="dropdown" role="button"
                                aria-haspopup="true" aria-expanded="false" href="#">Hello,
                                {{ identityStore.$state.jwt?.firstName }}<span class="caret"></span></a>
                            <ul class="dropdown-menu" >
                                <li>
                                    <RouterLink to="/account" class="nav-link text-dark" active-class="active">My
                                        account
                                    </RouterLink>
                                </li>
                                <div  v-if="identityStore.$state.jwt?.role == 'user'">
                                <li>
                                    <RouterLink to="/orders" class="nav-link text-dark" active-class="active">Orders
                                    </RouterLink>
                                </li>
                                <li>
                                    <RouterLink to="/cards" class="nav-link text-dark" active-class="active">Cards
                                    </RouterLink>
                                </li>
                                <li>
                                    <RouterLink to="/ticketsInOrder" class="nav-link text-dark" active-class="active">
                                        Tickets
                                    </RouterLink>
                                </li>
                                <li>
                                    <RouterLink to="/usersCoupon" class="nav-link text-dark" active-class="active">
                                        Coupons
                                    </RouterLink>
                                </li>
                                </div>
                                <li>
                                    <RouterLink to="/login" class="nav-link text-dark" active-class="active" @click="handleLogout">
                                        Logout
                                    </RouterLink>
                                </li>
                            </ul>
                        </li>
                    </template>

                </ul>
            </div>

        </nav>
    </header>
</template>


<script lang="ts">
import { useIdentityStore } from "@/stores/identity";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    }
})
export default class Header extends Vue {
    identityStore = useIdentityStore();


    handleLogout(): void {
        this.identityStore.$state.jwt = null;
    }
}


</script>
