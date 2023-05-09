import type { ICoordinateLocation } from "@/domain/ICoordinateLocation";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class CoordinateLocationService extends BaseService<ICoordinateLocation>{
    constructor() {
        super("coordinatesLocation");
        
    }
    
}