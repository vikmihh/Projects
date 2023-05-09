import React, { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { ITicket } from '../../domain/ITicket';
import { TicketService } from '../../services/TicketService';
import { AppContext } from '../../state/AppContext';

let initialTicket:ITicket ={
    id: "",
    name: "",
    dayRange: "",
    price: ""
};
const TicketCreate = () => {
    let appState = useContext(AppContext);
    const [ticket,setTicket] = useState(initialTicket);
    const ticketService = new TicketService(appState);
    const [jwt,setJwt] = useState(appState.jwt);

    const handleSubmit = () => {
        ticketService.create(ticket.name, ticket.dayRange, ticket.price);
    }

    return (
        <>
            <h3>Create new ticket</h3>
            <div className="row">
                <div className="col-md-12">
                        <div className="form-group">
                            <label className="control-label">Ticket Name</label>
                            <input type="text" className="form-control" value={ticket.name} onChange={(e) => {
                                ticket.name = e.target.value;
                                appState.setJwt(jwt);
                                setTicket(ticket);
                            }}
                            />
                        </div>
                        <div className="form-group">
                            <label className="control-label">Day Range</label>
                            <input type="number" className="form-control" value={ticket.dayRange} onChange={(e) => {
                                ticket.dayRange = e.target.value;
                                appState.setJwt(jwt);
                                setTicket(ticket);
                            }} />
                        </div>
                        <div className="form-group">
                            <label className="control-label">Price</label>
                            <input type="number" className="form-control" value={ticket.price} onChange={(e) => {
                                ticket.price = e.target.value;
                                appState.setJwt(jwt);
                                setTicket(ticket);
                            }} />
                        </div>
                        <div className="form-group">
                            
                        <Link to="/tickets" className="btn btn-primary" onClick={handleSubmit}>Create</Link>
                        </div>
                        <Link to="/tickets">Back to main</Link>
                    
                </div>
            </div>

        </>
    )
}

export default TicketCreate;