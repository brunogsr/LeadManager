import React from 'react';
import { InvitedLead, AcceptedLead } from '../types/lead';
import './LeadCard.css'; // Criaremos este CSS a seguir

interface LeadCardProps {
    lead: InvitedLead | AcceptedLead;
    type: 'invited' | 'accepted';
    onAccept?: (id: number) => void;
    onDecline?: (id: number) => void;
}

const formatDate = (dateString: string): string => {
    try {
        const date = new Date(dateString);
        return new Intl.DateTimeFormat('en-US', {
            month: 'long',
            day: 'numeric',
            hour: 'numeric',
            minute: 'numeric',
            hour12: true,
        }).format(date);
    } catch (e) {
        return dateString;
    }
};

const formatPrice = (price: number): string => {
    return new Intl.NumberFormat('en-AU', {
        style: 'currency',
        currency: 'AUD',
        minimumFractionDigits: 2,
    }).format(price);
}

const LeadCard: React.FC<LeadCardProps> = ({ lead, type, onAccept, onDecline }) => {
    const isInvited = type === 'invited' && 'firstName' in lead && onAccept && onDecline;

    return (
        <div className="lead-card">
            {isInvited ? (
                <>
                    <div className="card-header">
                        <span className="card-initial">{(lead as InvitedLead).firstName.charAt(0).toUpperCase()}</span>
                        <div className="header-details">
                            <span className="contact-name">{(lead as InvitedLead).firstName}</span>
                            <span className="date-created">{formatDate(lead.dateCreated)}</span>
                        </div>
                    </div>
                    <div className="card-body">
                        <p className="location-info">
                            <span className="suburb">{lead.suburb}</span>
                            <span className="category">{lead.category}</span>
                            <span className="job-id">Job ID: {lead.jobId}</span>
                        </p>
                        <p className="description">{lead.description}</p>
                    </div>
                    <div className="card-footer invited-footer">                        
                        <button className="button accept-button" onClick={() => onAccept(lead.id)}>Accept</button>
                        <button className="button decline-button" onClick={() => onDecline(lead.id)}>Decline</button>
                        <span className="price">{formatPrice(lead.price)} Lead Invitation</span>
                    </div>
                </>
            ) : (                
                <>
                    <div className="card-header">
                        <span className="card-initial">{(lead as AcceptedLead).fullName.charAt(0).toUpperCase()}</span>
                        <div className="header-details">
                            <span className="contact-name">{(lead as AcceptedLead).fullName}</span>
                            <span className="date-created">{formatDate(lead.dateCreated)}</span>
                        </div>
                    </div>
                    <div className="card-body accepted-body">
                        <p className="location-info">
                            <span className="suburb">{lead.suburb}</span>
                            <span className="category">{lead.category}</span>
                            <span className="job-id">Job ID: {lead.jobId}</span>
                        </p>
                        <p className="contact-info">
                            {(lead as AcceptedLead).phone && <span>📞 {(lead as AcceptedLead).phone}</span>}
                            {(lead as AcceptedLead).email && <span>✉️ {(lead as AcceptedLead).email}</span>}
                        </p>
                        <p className="description">{lead.description}</p>
                    </div>
                    <div className="card-footer accepted-footer">
                        <span className="price">{formatPrice(lead.price)} Accepted Price</span>
                    </div>
                </>
            )}
        </div>
    );
};

export default LeadCard;