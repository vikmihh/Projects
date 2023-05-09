import type { ICard } from "@/domain/ICard";
import httpClient from "@/http-client";
import { BaseService } from "./BaseService";

export class CardService extends BaseService<ICard> {
    constructor() {
        super("cards");
        
    }
}