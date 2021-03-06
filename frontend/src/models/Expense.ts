export interface Expense {
  id: number;
  organizationId: number;
  serviceId: number;
  currencyId: number;
  amount: number;
  createdAt?: Date;
  updatedAt?: Date;
}

export interface ExpenseCategory {
  id: number;
  name: string;
}
