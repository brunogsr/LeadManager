// src/App.tsx
import { useState, useEffect, useCallback } from 'react';
import { InvitedLead, AcceptedLead } from './types/lead';
import * as apiService from './services/apiService';
import LeadCard from './components/LeadCard'; // Criaremos este componente a seguir
import './App.css'; // Criaremos este CSS a seguir

type Tab = 'invited' | 'accepted';

function App() {
    const [activeTab, setActiveTab] = useState<Tab>('invited');
    const [invitedLeads, setInvitedLeads] = useState<InvitedLead[]>([]);
    const [acceptedLeads, setAcceptedLeads] = useState<AcceptedLead[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    // Função para buscar todos os dados
    const fetchData = useCallback(async () => {
        setIsLoading(true);
        setError(null);
        try {
            // Busca ambos os tipos de leads em paralelo
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
    }, []); // useCallback com array de dependências vazio para não recriar a função a cada render

    // Busca dados ao montar o componente
    useEffect(() => {
        fetchData();
    }, [fetchData]); // Executa quando fetchData muda (que só acontece na montagem)

    // Handler para aceitar um lead
    const handleAccept = async (id: number) => {
        // Opcional: Mostrar um estado de loading específico para o card
        try {
            await apiService.acceptLead(id);
            // Refresca os dados para refletir a mudança
            // Alternativa mais simples para o prazo: buscar tudo de novo
            fetchData();
            // Alternativa otimizada (mais complexa):
            // 1. Remover o lead da lista `invitedLeads`
            // 2. Buscar *apenas* o lead aceito (precisaria de um endpoint GET /api/lead/{id})
            // 3. Adicionar o lead buscado à lista `acceptedLeads`
            // setInvitedLeads(prev => prev.filter(lead => lead.id !== id));
            // // ... buscar lead aceito e adicionar a acceptedLeads ...
        } catch (err) {
            console.error(`Failed to accept lead ${id}:`, err);
            // Mostrar erro específico para o usuário se desejado
            setError(`Failed to accept lead ${id}. Please try again.`);
        }
    };

    // Handler para recusar um lead
    const handleDecline = async (id: number) => {
        try {
            await apiService.declineLead(id);
            // Refresca os dados (abordagem simples)
            fetchData();
            // Alternativa otimizada: apenas remover da lista invitedLeads
            // setInvitedLeads(prev => prev.filter(lead => lead.id !== id));
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
                        key={lead.id} // Chave única para React
                        lead={lead}
                        type={activeTab} // Passa o tipo da aba para o card
                        onAccept={activeTab === 'invited' ? handleAccept : undefined} // Passa handlers apenas para leads convidados
                        onDecline={activeTab === 'invited' ? handleDecline : undefined}
                    />
                ))}
            </div>
        </div>
    );
}

export default App;