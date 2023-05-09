import httpClient from "@/http-client";
import { useIdentityStore } from "@/stores/identity";
import type { AxiosError } from "axios";
import { IdentityService } from "./IdentityService";
import type { IServiceResult } from "./IServiceResult";

export class BaseService<TEntity> {
    identityStore = useIdentityStore();

    constructor(protected path: string) {
    }

    async getAll(): Promise<TEntity[]> {
        console.log("called");
        try {
            console.log(this.identityStore.$state.jwt?.token);
            let response = await httpClient.get(`/${this.path}`, {
                headers: {
                    "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                }

            }
            );
            console.log(response);
            let res = response.data as TEntity[];
            return res;
        } catch (e) {
            let response = (e as AxiosError).response!;
            if (response.status == 401 && this.identityStore.jwt) {
                let identityService = new IdentityService();
                let refreshResponse = await identityService.refreshIdentity();
                this.identityStore.$state.jwt = refreshResponse.data!;

                if (!this.identityStore.$state.jwt) return [];


                let response = await httpClient.get(`/${this.path}`, {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                });
                console.log(response);

                let res = response.data as TEntity[];
                return res;

            }
        }
        return [];
    }

    async get(id: string): Promise<TEntity> {
        console.log("get");
        let response = await httpClient.get(`/${this.path}/${id}`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        }
        
        );
        console.log(response);
        let res = response.data as TEntity;
        return res;
    }

    async delete(id: string): Promise<TEntity> {
        console.log("delete");
        let response = await httpClient.delete(`/${this.path}/${id}`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as TEntity;
        return res;
    }

    async add(entity: TEntity): Promise<IServiceResult<void>> {
        console.log("add");
        let response;
        try {
            response = await httpClient.post(`/${this.path}`, entity,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                }
            );
        } catch (e) {
            let res = {
                status: (e as AxiosError).response!.status,
                errorMsg: (e as AxiosError).response!.data.error

            }
            console.log(res);
            return res;

        }
        return { status: response.status };
    }


    async put(id: string, entity: TEntity): Promise<IServiceResult<void>> {
        console.log("add");
        let response;
        try {
            response = await httpClient.put(`/${this.path}/${id}`, entity,
                {
                    headers: {
                        "Authorization": "bearer " + this.identityStore.$state.jwt?.token
                    }
                }
            );
        } catch (e) {
            let res = {
                status: (e as AxiosError).response!.status,
                errorMessage: (e as AxiosError).response.data.error

            }
            console.log(res);
            return res;

        }
        return { status: response.status };
    }



}