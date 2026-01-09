import { fetchBaseQuery } from "@reduxjs/toolkit/query";
import { createApi } from "@reduxjs/toolkit/query/react";

export const apiSlice =  createApi({
    reducerPath: 'api',
    baseQuery: fetchBaseQuery  ({
        baseUrl: import.meta.env.VITE_API_BASE_URL,
    }),
    endpoints: () => ({
        // Define your endpoints here
    }),
    tagTypes: ['Medicine'],
    keepUnusedDataFor: 86400, //24 hours
})