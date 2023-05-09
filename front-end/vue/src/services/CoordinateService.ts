import type { ICoordinate } from "@/domain/ICoordinate";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class CoordinateService extends BaseService<ICoordinate>{
    constructor() {
        super("coordinates");
        
    }
    async getCoordinatesByLocationId(locationId:string): Promise<ICoordinate[]> {
        console.log("getCoordinateByLocationId");
        let response = await httpClient.get(`/coordinates/locationby/${locationId}`, {
            headers: {
                "Authorization": "bearer " + this.identityStore.$state.jwt?.token
            }
        });
        console.log(response);
        let res = response.data as ICoordinate[];
        return res;
    }
}