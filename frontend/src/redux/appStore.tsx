import {configureStore}  from "@reduxjs/toolkit";
import { apiSlice } from "./apiSlice";
import medicineReducer from "./medicineSlice";

export const appStore = configureStore({
  reducer: {
    medicine: medicineReducer,
    [apiSlice.reducerPath]: apiSlice.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(apiSlice.middleware),
});

export type RootState = ReturnType<typeof appStore.getState>;
export type AppDispatch = typeof appStore.dispatch;