export interface IJWTResponse{
    token?: string,
    refreshToken?: string,
    firstName?:string,
    lastName?: string,
    email?: string,
    role: string
}