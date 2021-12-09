
export interface Account {
    id: number;
    name: string;
    email: string;
    createdAt: Date;
    updatedAt: Date;
}

export interface AccountSignup {
    name: string;
    email: string;
    password: string;
}

export interface AccountLogin {
    email: string;
    password: string;
}

export interface AccountUpdate {
    name?: string;
    email?: string;
    password?: string;
}

