// src/types/lead.ts

// Corresponde ao InvitedLeadDto do backend
export interface InvitedLead {
    id: number;
    firstName: string;
    dateCreated: string; // Datas geralmente vêm como strings ISO 8601 da API
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
    price: number; // Preço atual (possivelmente com desconto)
    dateCreated: string;
}

// Tipo para diferenciar os cards, se necessário
export type Lead = InvitedLead | AcceptedLead;