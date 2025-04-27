// src/services/apiService.ts
import axios from 'axios';
import { InvitedLead, AcceptedLead } from '../types/lead';

// !!! ATUALIZADO PARA USAR A URL HTTP REAL DA SUA API !!!
const API_BASE_URL = 'http://localhost:5150/api/lead'; // <-- USE ESTA URL

const apiClient = axios.create({
    baseURL: API_BASE_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

// ... (resto do arquivo apiService.ts permanece o mesmo) ...

// Função para buscar leads convidados
export const getInvitedLeads = async (): Promise<InvitedLead[]> => {
    try {
        const response = await apiClient.get<InvitedLead[]>('/invited');
        return response.data;
    } catch (error) {
        console.error("Error fetching invited leads:", error);
        throw error;
    }
};

// Função para buscar leads aceitos
export const getAcceptedLeads = async (): Promise<AcceptedLead[]> => {
    try {
        const response = await apiClient.get<AcceptedLead[]>('/accepted');
        return response.data;
    } catch (error) {
        console.error("Error fetching accepted leads:", error);
        throw error;
    }
};

// Função para aceitar um lead
export const acceptLead = async (id: number): Promise<void> => {
    try {
        await apiClient.post(`/${id}/accept`);
    } catch (error) {
        console.error(`Error accepting lead ${id}:`, error);
        throw error;
    }
};

// Função para recusar um lead
export const declineLead = async (id: number): Promise<void> => {
    try {
        await apiClient.post(`/${id}/decline`);
    } catch (error) {
        console.error(`Error declining lead ${id}:`, error);
        throw error;
    }
};