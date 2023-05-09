import httpClient from "./HttpClient";
import type { AxiosError } from "axios";
import { IServiceResult } from './IServiceResult';
import { IJWTResponse } from '../domain/IJWTResponse';


export class IdentityService {

    async login(email: string, password: string): Promise<IServiceResult<IJWTResponse>> {
        try {
            let loginInfo = {
                email,
                password
            }
            console.log("gotit");
            let response = await httpClient.post("/identity/account/login", loginInfo);
            console.log("gotit");
            console.log(response)
            return {
                status: response.status,
                data: response.data as IJWTResponse
            };

        } catch (e) {
            let response = {
                status: (e as AxiosError).response!.status,
            }
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
            }
            return response;
        }

    }

}
