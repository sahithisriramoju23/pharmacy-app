import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useParams, useNavigate } from 'react-router';
import { useLazyGetMedicineByIdQuery, useUpdateMedicineByIdMutation } from '../redux/medicineSlice';
import { useEffect } from 'react';
import { LoaderComponent } from '../components/LoaderComponent';
import { ErrorComponent } from '../components/ErrorComponent';
import type { UpdateMedicineRequest } from '../types/medicine';

const schema: yup.ObjectSchema<UpdateMedicineRequest> = yup.object().shape({
  name: yup.string().required('Name is required'),
  brand: yup.string().required('Brand is required'),
  expiryDate: yup.string().required('Expiry date is required'),
  quantity: yup.number().min(0, 'Quantity cannot be negative').required('Quantity is required'),
  price: yup.number().min(0, 'Price cannot be negative').required('Price is required'),
  notes: yup.string().optional(),
});

export function MedicineUpdate() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [trigger, { data: medicine, isLoading, isError }] = useLazyGetMedicineByIdQuery();
  const [updateMedicine, { isLoading: isUpdating }] = useUpdateMedicineByIdMutation();

  const { register, handleSubmit, reset, formState: { errors } } = useForm<UpdateMedicineRequest>({
    resolver: yupResolver(schema),
  });

  useEffect(() => {
    if (id) {
      trigger(id);
    }
  }, [id, trigger]);

  useEffect(() => {
    if (medicine) {
      reset(medicine);
    }
  }, [medicine, reset]);

  if (isLoading) return <LoaderComponent loadingMessage="Loading Medicine..." />;
  if (isError) return <ErrorComponent errorMessage="Failed to load medicine." />;
  if (!medicine) return null;

  const onSubmit = async (data: UpdateMedicineRequest) => {
    await updateMedicine({ id: medicine.id, payload: data });
    navigate(`/medicine/${id}`);
  };

  return (
    <div className="max-w-xl mx-auto bg-white rounded-lg shadow-md border border-gray-200 mt-10 p-8">
      <h2 className="text-2xl font-bold text-blue-700 mb-6">Update Medicine</h2>
      <form onSubmit={handleSubmit(onSubmit)} className="space-y-5">
        <div>
          <label className="block text-gray-600 font-semibold mb-1">ID</label>
          <input value={medicine.id} disabled className="w-full px-3 py-2 border rounded bg-gray-100 text-gray-700" />
        </div>
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
        <button type="submit" disabled={isUpdating} className="w-full py-2 bg-blue-600 text-white font-semibold rounded-lg shadow hover:bg-blue-700 transition">
          {isUpdating ? 'Updating...' : 'Update'}
        </button>
      </form>
    </div>
  );
}
