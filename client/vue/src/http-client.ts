import axios from "axios";

export const httpClient = axios.create({
    baseURL: "https://localhost:7016/api/v1",
    headers: {
        "Content-type": "application/json"
    }
});

export default httpClient;