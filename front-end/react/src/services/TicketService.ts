import { ITicket } from "../domain/ITicket";
import { IAppState } from "../state/IAppState";
import { BaseService } from "./BaseService";

export class TicketService extends BaseService<ITicket> {
    constructor(appState:IAppState) {
        super("tickets", appState);
        console.log(appState.jwt);
    }

   async create(name:string, price: string, dayRange:string) {
       
        let ticket: ITicket = {
            name:name,
            price:price,
            dayRange:dayRange
        };
        this.add(ticket);
   }
}