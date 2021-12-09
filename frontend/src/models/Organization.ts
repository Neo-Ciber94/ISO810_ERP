
export interface Organization {
    id: number;
    accountId: number;
    name: string;
    alias: string;
    createdAt: Date;
    updatedAt: Date;
}

export interface OrganizationCreate {
    name: string;
    alias: string;
}

export type OrganizationUpdate = Partial<OrganizationCreate>;