import type { ITicket } from "@/domain/ITicket";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class TicketService extends BaseService<ITicket> {
    constructor() {
        super("tickets");
        
    }
}