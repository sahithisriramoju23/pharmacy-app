import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import type { CreateMedicineRequest } from '../types/medicine';
import { useCreateMedicineMutation } from '../redux/medicineSlice';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router';

const schema: yup.ObjectSchema<CreateMedicineRequest> = yup.object().shape({
  name: yup.string().required('Name is required'),
  brand: yup.string().required('Brand is required'),
  expiryDate: yup.string().required('Expiry date is required'),
  quantity: yup.number().min(0, 'Quantity cannot be negative').required('Quantity is required'),
  price: yup.number().min(0, 'Price cannot be negative').required('Price is required'),
  notes: yup.string().optional(),
});

export function MedicineCreate() {
  const navigate = useNavigate();
  const [createMedicine, { data: medicine, isLoading, isSuccess, isError }] = useCreateMedicineMutation();
  const [error, setError] = useState<string | null>(null);
  const { register, handleSubmit, reset, formState: { errors } } = useForm<CreateMedicineRequest>({
    resolver: yupResolver(schema),
  });

  // Redirect to home page after successful creation
  useEffect(() => {
    if (isSuccess) {
      console.log('Medicine created successfully:', medicine?.id);
      navigate('/');
    }
  }, [isSuccess, medicine, navigate]);

  const onSubmit = async (data: CreateMedicineRequest) => {
    setError(null);
    try {
      await createMedicine(data);
      reset();
    } catch (e: any) {
      setError(e?.data?.message || 'Failed to create medicine');
    }
  };

  if (isError && !error) {
    <div className="text-red-600 mb-2">{error}</div>;
  }
  return (
    <form onSubmit={handleSubmit(onSubmit)} className="bg-white p-6 rounded-lg shadow-md border border-gray-200 max-w-lg mx-auto mt-8 space-y-5">
      <h2 className="text-2xl font-bold text-blue-700 mb-4">Add New Medicine</h2>
      {error && <div className="text-red-600 mb-2">{error}</div>}
      <div>
        <label className="block text-gray-600 font-semibold mb-1">Name</label>
        <input {...register('name')} className="w-full px-3 py-2 border rounded" />
        {errors.name && <p className="text-red-600 text-sm mt-1">{errors.name.message}</p>}
      </div>
      <div>
        <label className="block text-gray-600 font-semibold mb-1">Brand</label>
        <input {...register('brand')} className="w-full px-3 py-2 border rounded" />
        {errors.brand && <p className="text-red-600 text-sm mt-1">{errors.brand.message}</p>}
      </div>
      <div>
        <label className="block text-gray-600 font-semibold mb-1">Expiry Date</label>
        <input type="date" {...register('expiryDate')} className="w-full px-3 py-2 border rounded" />
        {errors.expiryDate && <p className="text-red-600 text-sm mt-1">{errors.expiryDate.message}</p>}
      </div>
      <div>
        <label className="block text-gray-600 font-semibold mb-1">Quantity</label>
        <input type="number" {...register('quantity')} className="w-full px-3 py-2 border rounded" />
        {errors.quantity && <p className="text-red-600 text-sm mt-1">{errors.quantity.message}</p>}
      </div>
      <div>
        <label className="block text-gray-600 font-semibold mb-1">Price</label>
        <input type="number" step="0.01" {...register('price')} className="w-full px-3 py-2 border rounded" />
        {errors.price && <p className="text-red-600 text-sm mt-1">{errors.price.message}</p>}
      </div>
      <div>
        <label className="block text-gray-600 font-semibold mb-1">Notes</label>
        <textarea {...register('notes')} className="w-full px-3 py-2 border rounded" />
        {errors.notes && <p className="text-red-600 text-sm mt-1">{errors.notes.message}</p>}
      </div>
      <button type="submit" disabled={isLoading} className="w-full py-2 bg-blue-600 text-white font-semibold rounded-lg shadow hover:bg-blue-700 transition">
        {isLoading ? 'Creating...' : 'Create Medicine'}
      </button>
    </form>
  );
}
