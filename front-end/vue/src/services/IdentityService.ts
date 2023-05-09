import type { IServiceResult } from "./IServiceResult";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";
import type { AxiosError } from "axios";
import type { IJWTResponse } from "@/domain/IJWTResponse";
import { useIdentityStore } from "@/stores/identity";


export class IdentityService {
    identityStore = useIdentityStore();

    async login(email: string, password: string): Promise<IServiceResult<IJWTResponse>> {
        try {
            let loginInfo = {
                email,
                password
            }
            let response = await httpClient.post("/identity/account/login", loginInfo);
            return {
                status: response.status,
                data: response.data as IJWTResponse
            };

        } catch (e) {
            let response = {
                status: (e as AxiosError).response!.status,
                errorMsg: (e as AxiosError).response!.data.error,
                data: (e as AxiosError).response!.data
            }

            console.log((e as AxiosError).response!.data);


            return response;
        }

    }

    async register(firstName: string, lastName: string, email: string, password: string): Promise<IServiceResult<IJWTResponse>> {
        try {
            let registerInfo = {
                firstName,
                lastName,
                email,
                password
            }
            let response = await httpClient.post("/identity/account/register", registerInfo);
            return {
                status: response.status,
                data: response.data as IJWTResponse
            };

        } catch (e) {
            let response = {
                status: (e as AxiosError).response!.status,
                errorMsg: (e as AxiosError).response!.data.errors.email[0],
            }
    
            

            return response;
        }

    }

    async refreshIdentity(): Promise<IServiceResult<IJWTResponse>> {
        try {
            console.log(this.identityStore.$state.jwt);

            let response = await httpClient.post("/identity/account/refreshtoken",
                {
                    jwt: this.identityStore.$state.jwt?.token,
                    refreshToken: this.identityStore.$state.jwt?.refreshToken
                }
            );
            return {
                status: response.status,
                data: response.data as IJWTResponse
            };

        } catch (e) {
            let response = {
                status: (e as AxiosError).response!.status,
                errorMsg: (e as AxiosError).response!.data.error,
            }

            console.log(response);

            console.log((e as AxiosError).response);

            return response;
        }
    }

}
