import type { IJWTResponse } from "@/domain/IJWTResponse";
import type { IUser } from "@/domain/IUser";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class UserService extends BaseService<IJWTResponse> {
    constructor() {
        super("jwtResponse");
        
    }
}