// src/types/lead.ts

// Corresponde ao InvitedLeadDto do backend
export interface InvitedLead {
    id: number;
    firstName: string;
    dateCreated: string; // Datas geralmente v�m como strings ISO 8601 da API
    suburb: string;
    category: string;
    jobId: number; // Mantendo o JobId que vem da API
    description: string;
    price: number;
}

// Corresponde ao AcceptedLeadDto do backend
export interface AcceptedLead {
    id: number;
    fullName: string;
    phone?: string | null; // Pode ser nulo ou indefinido
    email?: string | null;
    suburb: string;
    category: string;
    jobId: number;
    description: string;
    price: number; // Pre�o atual (possivelmente com desconto)
    dateCreated: string;
}

// Tipo para diferenciar os cards, se necess�rio
export type Lead = InvitedLead | AcceptedLead;