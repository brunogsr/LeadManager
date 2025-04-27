// src/components/LeadCard.tsx
import React from 'react';
import { InvitedLead, AcceptedLead } from '../types/lead';
import './LeadCard.css'; // Importa o CSS do card

// Props que o componente LeadCard receberá
interface LeadCardProps {
    lead: InvitedLead | AcceptedLead;
    type: 'invited' | 'accepted'; // Para saber qual layout renderizar
    onAccept?: (id: number) => void; // Função opcional, passada apenas para 'invited'
    onDecline?: (id: number) => void; // Função opcional, passada apenas para 'invited'
}

// --- IMPLEMENTAÇÃO DAS FUNÇÕES DE FORMATAÇÃO ---
const formatDate = (dateString: string): string => {
    if (!dateString || isNaN(Date.parse(dateString))) { return "Invalid date"; }
    try {
        const date = new Date(dateString);
        return new Intl.DateTimeFormat('en-US', {
            month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric', hour12: true
        }).format(date).replace(',', ' @');
    } catch (e) { return dateString; }
};

const formatPrice = (price: number): string => {
    if (typeof price !== 'number' || isNaN(price)) { return "N/A"; }
    try {
        return new Intl.NumberFormat('en-AU', {
            style: 'currency', currency: 'AUD', minimumFractionDigits: 2
        }).format(price);
    } catch (e) { return price.toString(); }
}

// --- COMPONENTE PRINCIPAL ---
const LeadCard: React.FC<LeadCardProps> = ({ lead, type, onAccept, onDecline }) => {
    const isInvited = type === 'invited' && 'firstName' in lead && !!onAccept && !!onDecline;
    const isAccepted = type === 'accepted' && 'fullName' in lead;

    return (
        <div className="lead-card">
            {/* --- CABEÇALHO (Comum a ambos os tipos) --- */}
            <div className="card-header">
                <span className="card-initial">
                    {isInvited ? (lead as InvitedLead).firstName?.charAt(0)?.toUpperCase() ?? '?' : ''}
                    {isAccepted ? (lead as AcceptedLead).fullName?.charAt(0)?.toUpperCase() ?? '?' : ''}
                </span>
                <div className="header-details">
                    <span className="contact-name">
                        {isInvited ? (lead as InvitedLead).firstName : ''}
                        {isAccepted ? (lead as AcceptedLead).fullName : ''}
                    </span>
                    <span className="date-created">{formatDate(lead.dateCreated)}</span>
                </div>
            </div>

            {/* --- CORPO (Layout comum com variações) --- */}
            <div className="card-body">
                {/* Informações Lado a Lado com Separadores */}
                <p className="card-meta-info">
                    {/* Localização */}
                    <span className="meta-item">
                        <span className="meta-icon">📍</span>
                        {lead.suburb}
                    </span>

                    {/* Separador */}
                    <span className="meta-separator">|</span>

                    {/* Categoria */}
                    <span className="meta-item">
                        <span className="meta-icon">💼</span>
                        {lead.category}
                    </span>

                    {/* Separador */}
                    <span className="meta-separator">|</span>

                    {/* Job ID */}
                    <span className="meta-item">
                        {/* Sem ícone, apenas texto */}
                        Job ID: {lead.jobId}
                    </span>
                </p>

                {/* Separador e Informações Adicionais (Contatos só para Aceitos) */}
                {isAccepted && (
                    <>
                        <hr className="card-divider" />
                        <p className="contact-info">
                            {(lead as AcceptedLead).phone && <span>📞{(lead as AcceptedLead).phone}</span>}
                            {(lead as AcceptedLead).email && <span>✉️{(lead as AcceptedLead).email}</span>}
                        </p>
                    </>
                )}

                {/* Separador e Descrição (Comum a ambos) */}
                <hr className="card-divider" />
                <p className="description">{lead.description}</p>
            </div>

            {/* --- FOOTER (Layout específico para cada tipo) --- */}
            {isInvited ? (
                <div className="card-footer invited-footer">
                    <div> {/* Div para agrupar botões */}
                        <button className="button accept-button" onClick={() => onAccept(lead.id)}>Accept</button>
                        <button className="button decline-button" onClick={() => onDecline(lead.id)}>Decline</button>
                    </div>
                    <span className="price">{formatPrice(lead.price)} Lead Invitation</span>
                </div>
            ) : (
                <div className="card-footer accepted-footer">
                    <span className="price">{formatPrice(lead.price)} Accepted Price</span>
                </div>
            )}
        </div>
    );
};

export default LeadCard;