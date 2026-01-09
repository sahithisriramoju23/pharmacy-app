
import { useParams, useNavigate } from 'react-router';
import { useLazyGetMedicineByIdQuery } from '../redux/medicineSlice';
import { useDeleteMedicineByIdMutation } from '../redux/medicineSlice';
import { useEffect } from 'react';
import { LoaderComponent } from '../components/LoaderComponent';
import { ErrorComponent } from '../components/ErrorComponent';

export function MedicineDetails() {
	const { id } = useParams<{ id: string }>();
	const navigate = useNavigate();
	const [trigger, { data: medicine, isLoading, isError }] = useLazyGetMedicineByIdQuery();
	const [deleteMedicine, { isLoading: isDeleting }] = useDeleteMedicineByIdMutation();
    
    useEffect(() => {
        if (id) {
            trigger(id);
        }
    }, [id, trigger]);

    const handleDelete = async () => {
		if (window.confirm('Are you sure you want to delete this medicine?')) {
			await deleteMedicine(medicine?.id!);
			navigate('/'); // Optionally, navigate to home or list after delete
		}
	}
	// In real app, fetch from API or Redux store
	// const medicine: Medicine | undefined = mockMedicines.find(med => med.id === id);
    if(isLoading)
        return <LoaderComponent loadingMessage='Loading Medicine Details'/>;

    if(isError)
        return <ErrorComponent errorMessage='Failed to load medicine details.'/>;
	
    if (!medicine) {
		return (
			<div className="flex flex-col items-center justify-center min-h-[400px]">
				<div className="text-2xl text-red-600 font-semibold mb-2">Medicine Not Found</div>
				<button
					className="mt-4 px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700 transition"
					onClick={() => navigate(-1)}
				>
					Go Back
				</button>
			</div>
		);
	}

	return (
		<div className="max-w-xl mx-auto bg-white rounded-lg shadow-md border border-gray-200 mt-10 p-8">
			<h2 className="text-3xl font-bold text-blue-700 mb-4">{medicine.name}</h2>
			<div className="space-y-3 text-gray-800">
				<div><span className="font-semibold text-gray-600">Brand:</span> {medicine.brand}</div>
				<div><span className="font-semibold text-gray-600">Expiry Date:</span> {new Date(medicine.expiryDate).toLocaleDateString('en-IN')}</div>
				<div><span className="font-semibold text-gray-600">Quantity:</span> {medicine.quantity} units</div>
				<div><span className="font-semibold text-gray-600">Price:</span> <span className="text-green-700 font-bold">${medicine.price.toFixed(2)}</span></div>
				{medicine.notes && (
					<div className="bg-blue-50 border-l-4 border-blue-400 p-3 rounded text-blue-900">
						<span className="font-semibold">Notes:</span> {medicine.notes}
					</div>
				)}
			</div>
			<div className="flex gap-4 mt-8">
				<button
					className="flex-1 py-2 bg-blue-600 text-white font-semibold rounded-lg shadow hover:bg-blue-700 transition"
					onClick={() => navigate(`/medicine/${medicine.id}/update`)}
				>
					Update
				</button>
				<button
					className="flex-1 py-2 bg-red-600 text-white font-semibold rounded-lg shadow hover:bg-red-700 transition"
					onClick={handleDelete}
					disabled={isDeleting}
				>
					{isDeleting ? 'Deleting...' : 'Delete'}
				</button>
			</div>
		</div>
	);
}
	;
