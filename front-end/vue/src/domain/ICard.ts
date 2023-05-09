export interface ICard{
    id?: string,
    firstName: string,
    lastName: string,
    cardNumber: string,
    securityCode: string,
    expiryMonth: string,
    expiryYear: string
    appUserId?:string
}