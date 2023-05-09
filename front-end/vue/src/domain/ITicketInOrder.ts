export interface ITicketInOrder{
    id: string,
    validFromStr: string,
    ticketName: string,
    ticketPrice: number,
    ticketDayRange: string
    validUntilStr: string,
    activated: boolean,
    orderId: string,
    ticketId: string,
    appUserId: string
}