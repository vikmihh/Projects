import httpClient from "./HttpClient";
import { IAppState } from "../state/IAppState";

export class BaseService<TEntity> {

    constructor(private path: string, private appState: IAppState) {
    }

    async getAll(): Promise<TEntity[]> {
        console.log("get");
        let response = await httpClient.get(`/${this.path}`, {
            headers: {
                "Authorization": "bearer " + this.appState.jwt!.token
            }

        });
        console.log(response);
        let res = response.data as TEntity[];
        return res;
    }

    async add(entity: TEntity): Promise<void> {
        console.log("create");
        let response = await httpClient.post(`/${this.path}`, entity, {
            headers: {
                "Authorization": "bearer " + this.appState.jwt!.token
            }

        })
            ;
    }

    async getById(id: string): Promise<TEntity> {
        console.log("getById");
        let response = await httpClient.get(`/${this.path}/${id}`, {
            headers: {
                "Authorization": "bearer " + this.appState.jwt!.token
            }

        });
        console.log(response);
        let res = response.data as TEntity;
        return res;
    }

    async delete(id: string): Promise<TEntity> {
        console.log("delete!");
        console.log(this.appState.jwt!);
        let response = await httpClient.delete(`/${this.path}/${id}`, {
            headers: {
                "Authorization": "bearer " + this.appState.jwt!.token
            }

        });
        console.log(response);
        let res = response.data as TEntity;
        return res;
    }

    async put(id: string, entity: TEntity): Promise<TEntity> {
        console.log("put");
        let response = await httpClient.put(`/${this.path}/${id}`, entity,
            {
                headers: {
                    "Authorization": "bearer " + this.appState.jwt!.token
                }
            });
        console.log(response);
        let res = response.data as TEntity;
        return res;
    }
}
