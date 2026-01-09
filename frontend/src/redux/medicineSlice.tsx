import { createSlice, type PayloadAction } from "@reduxjs/toolkit/react"
import type { CreateMedicineRequest, CreateMedicineResponse, GetAllMedicinesResponse, Medicine, MedicineState, UpdateMedicineRequest } from "../types/medicine"
import { apiSlice } from "./apiSlice"


export const medicineApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder) => ({
        getMedicines: builder.query<GetAllMedicinesResponse,void>({
            query: () => ({
                url: `/medicines`,
                params: {
                    pageIndex: 0,
                    pageSize: 10,
                },
                method: 'GET',
            }),
            providesTags: (result) =>
            result?.data?.items
            ? [
              ...result?.data?.items?.map(({ id }) => ({ type: 'Medicine' as const, id })),
              { type: 'Medicine', id: 'LIST' },
            ]
            : [{ type: 'Medicine', id: 'LIST' }],
        }),
        getMedicineById: builder.query<Medicine, string>({
          query: (id) => ({
              url: `/medicine/${id}`,
              method: 'GET',
          }),
          providesTags: (result) => [{ type: 'Medicine' as const, id: result?.id }],
        }),
        updateMedicineById: builder.mutation<Medicine, { id: string; payload: UpdateMedicineRequest }>({
          query: ({ id, payload }) => ({
              url: `/medicine/update/${id}`,
              method: 'PUT',
              body: payload,
          }),
          invalidatesTags:  [{ type: 'Medicine', id: 'LIST' }],
        }),
        deleteMedicineById: builder.mutation<{ isSuccess: boolean; id: string }, string>({
          query: (id) => ({
              url: `/medicine/delete/${id}`,
              method: 'DELETE',
          }),
          invalidatesTags:  [{ type: 'Medicine', id: 'LIST' }],
        }),
        createMedicine: builder.mutation<CreateMedicineResponse, CreateMedicineRequest>({
          query: (payload) => ({
              url: `/medicine/create`,
              method: 'POST',
              body: payload,
          }),
          invalidatesTags: [{ type: 'Medicine', id: 'LIST' }],
        }),
    }),
});

const medicineSlice = createSlice({
  name: 'medicine',
  initialState: {} as MedicineState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addMatcher(medicineApiSlice.endpoints.getMedicineById.matchPending, (state) => {
        state.loading = true;
      }),
      builder.addMatcher(medicineApiSlice.endpoints.getMedicineById.matchFulfilled, (state, action:PayloadAction<Medicine>) => {
        state.loading = false;
        state.medicine = action.payload;
      }),
      builder.addMatcher(medicineApiSlice.endpoints.getMedicineById.matchRejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message;
      })
},
});

export default medicineSlice.reducer;

export const { useGetMedicinesQuery, 
  useLazyGetMedicinesQuery,
  useGetMedicineByIdQuery,
  useLazyGetMedicineByIdQuery,
  useUpdateMedicineByIdMutation,
  useDeleteMedicineByIdMutation, 
useCreateMedicineMutation } = medicineApiSlice;