import React, { useContext, useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import { ITicket } from '../../domain/ITicket';
import { TicketService } from '../../services/TicketService';
import { AppContext, AppContextProvider } from '../../state/AppContext';


const TicketEdit = () => {
    let { id } = useParams();
    const appState = useContext(AppContext);
    console.log("a[s");
    console.log(appState.jwt);
    const ticketService = new TicketService(appState);
    const [jwt, setJwt] = useState(appState.jwt);
    let [ticket, setTicket] = useState(appState.ticket);


    useEffect(() => {
        ticketService.getById(id!).then(data => setTicket(data));
    }, []);

    const handleDelete = () => {
        console.log("appstat");
        console.log(appState.jwt);
        ticketService.delete(id!);
    }

    const handleSubmit = () => {
        console.log("appState.jwt");
        ticketService.put(id!, ticket).then(data => setTicket(data));
    }

    return (
        <>
            <h3>Edit ticket</h3>
            <div className="row">
                <div className="col-md-4">

                    <div className="form-group">
                        <label className="control-label">Ticket Name</label>
                        <input type="text" value={ticket.name} onChange={(e) => {
                            ticket.name = e.target.value;
                            appState.setJwt(jwt);
                            setTicket(ticket);

                        }} className="form-control"
                        />

                    </div>
                    <div className="form-group">
                        <label className="control-label">Day Range</label>
                        <input type="number" value={ticket.dayRange} onChange={(e) => {
                            ticket.dayRange = e.target.value;
                            appState.setJwt(jwt);
                            setTicket(ticket);
                        }} className="form-control"
                        />
                    </div>
                    <div className="form-group">
                        <label className="control-label">Price</label>
                        <input type="number" value={ticket.price} onChange={(e) => {
                            appState.setJwt(jwt);
                            ticket.price = e.target.value;
                            setTicket(ticket);
                        }} className="form-control"
                        />

                    </div>
                    <div className="form-group">
                        <Link to="/tickets" className="btn btn-primary" onClick={handleSubmit}>Save</Link>
                    </div>
                    <div className="form-group">

                        <Link to="/tickets" className="btn btn-danger" onClick={handleDelete}>Delete</Link>
                    </div>

                    <td />
                    <Link to="/tickets">Back to main</Link>
                </div>
            </div>
        </>
    )
}

export default TicketEdit;