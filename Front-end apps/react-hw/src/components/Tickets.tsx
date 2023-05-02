import { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { ITicket } from '../domain/ITicket';
import { TicketService } from '../services/TicketService';
import { AppContext } from '../state/AppContext';

const initialState: ITicket[] = [];

const Tickets = () => {
    const userRole = useContext(AppContext).jwt.role;
    const ticketService = new TicketService(useContext(AppContext));
    const [tickets, setTickets] = useState(initialState);
    useEffect(() => {
        ticketService.getAll().then(data => setTickets(data));
    }, []);
    return (
        <>
            <h3>Tickets</h3>
            {userRole == "admin" &&
                <Link to={`/tickets/create`}>Create new</Link>
            }
            <div className="h-100 row align-items-center">
                <table>
                    {tickets.map(item => {
                        return (
                            <tr key={item.id}>
                                <div className="card">
                                    <div className="card-body">
                                        <h5 className="card-title">{item.name}</h5>
                                        <p className="card-text"> Price: {item.price}</p>
                                        <p className="card-text"> Duration: {item.dayRange}</p>
                                        {userRole == "admin" &&
                                            <Link to={`/tickets/edit/${item.id}`}>Edit</Link>
                                        }
                                    </div>
                                </div>
                            </tr>
                        );
                    })}
                </table>
            </div>
        </>
    );
}
export default Tickets;