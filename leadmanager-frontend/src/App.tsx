// src/App.tsx
import { useState, useEffect, useCallback } from 'react';
import { InvitedLead, AcceptedLead } from './types/lead';
import * as apiService from './services/apiService';
import LeadCard from './components/LeadCard';
import './App.css'; // Criaremos este CSS a seguir

type Tab = 'invited' | 'accepted';

function App() {
    const [activeTab, setActiveTab] = useState<Tab>('invited');
    const [invitedLeads, setInvitedLeads] = useState<InvitedLead[]>([]);
    const [acceptedLeads, setAcceptedLeads] = useState<AcceptedLead[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const fetchData = useCallback(async () => {
        setIsLoading(true);
        setError(null);
        try {
            const [invited, accepted] = await Promise.all([
                apiService.getInvitedLeads(),
                apiService.getAcceptedLeads()
            ]);
            setInvitedLeads(invited);
            setAcceptedLeads(accepted);
        } catch (err) {
            console.error("Failed to fetch leads:", err);
            setError("Failed to load leads. Please try again later.");
        } finally {
            setIsLoading(false);
        }
    }, []);

    useEffect(() => {
        fetchData();
    }, [fetchData]);

    const handleAccept = async (id: number) => {
        try {
            await apiService.acceptLead(id);

            fetchData();
        } catch (err) {
            console.error(`Failed to accept lead ${id}:`, err);
            setError(`Failed to accept lead ${id}. Please try again.`);
        }
    };

    // Handler para recusar um lead
    const handleDecline = async (id: number) => {
        try {
            await apiService.declineLead(id);
            fetchData();
        } catch (err) {
            console.error(`Failed to decline lead ${id}:`, err);
            setError(`Failed to decline lead ${id}. Please try again.`);
        }
    };

    // Determina quais leads mostrar com base na aba ativa
    const leadsToShow = activeTab === 'invited' ? invitedLeads : acceptedLeads;

    return (
        <div className="app-container">
            <h1>Lead Management</h1>

            <div className="tabs">
                <button
                    className={`tab-button ${activeTab === 'invited' ? 'active' : ''}`}
                    onClick={() => setActiveTab('invited')}
                >
                    Invited ({invitedLeads.length})
                </button>
                <button
                    className={`tab-button ${activeTab === 'accepted' ? 'active' : ''}`}
                    onClick={() => setActiveTab('accepted')}
                >
                    Accepted ({acceptedLeads.length})
                </button>
            </div>

            {isLoading && <p className="loading-message">Loading leads...</p>}
            {error && <p className="error-message">{error}</p>}

            <div className="lead-list">
                {!isLoading && !error && leadsToShow.length === 0 && (
                    <p className="no-leads-message">No leads to display in this category.</p>
                )}
                {!isLoading && !error && leadsToShow.map(lead => (
                    <LeadCard
                        key={lead.id}
                        lead={lead}
                        type={activeTab}
                        onAccept={activeTab === 'invited' ? handleAccept : undefined}
                        onDecline={activeTab === 'invited' ? handleDecline : undefined}
                    />
                ))}
            </div>
        </div>
    );
}

export default App;