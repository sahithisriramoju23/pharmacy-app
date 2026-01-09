import type { Maybe } from "yup";

export interface Medicine {
  id: string;
  name: string;
  quantity: number;
  expiryDate: string;
  brand: string;
  price: number;
  notes?: string;
}

export interface CreateMedicineRequest{
  name: string;
  quantity: number;
  expiryDate: string;
  brand: string;
  price: number;
  notes?: string;
}
export interface CreateMedicineResponse{
  id: string;
}
export interface UpdateMedicineRequest{
  name: string;
  quantity: number;
  expiryDate: string;
  brand: string;
  price: number;
  notes?: string;
}

export interface PaginationResponse<T>{
    pageIndex: number;
    pageSize: number;
    totalCount: number; 
    items: T[];
}
export interface GetAllMedicinesResponse{
  data: PaginationResponse<Medicine>;
}

export interface MedicineState {
  loading: boolean;
  error?: string;
  medicine?: Medicine;
}
