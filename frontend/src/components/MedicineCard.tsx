import { useNavigate } from 'react-router';
import { useDeleteMedicineByIdMutation } from '../redux/medicineSlice';
import type { Medicine } from '../types/medicine';

interface MedicineCardProps {
  medicine: Medicine;
}

const PackageIcon = () => (
  <svg className="w-4 h-4 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4" />
  </svg>
);

const CalendarIcon = () => (
  <svg className="w-4 h-4 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
  </svg>
);

const BuildingIcon = () => (
  <svg className="w-4 h-4 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 21H5a2 2 0 01-2-2V5a2 2 0 012-2h4l2-3h2l2 3h4a2 2 0 012 2v14a2 2 0 01-2 2z" />
  </svg>
);

const DollarIcon = () => (
  <svg className="w-4 h-4 flex-shrink-0" fill="none" stroke="currentColor" viewBox="0 0 24 24">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
  </svg>
);

export function MedicineCard({ medicine }: MedicineCardProps) {
  const navigate = useNavigate();
  const [deleteMedicine, { isLoading: isDeleting }] = useDeleteMedicineByIdMutation();
  const getBackgroundColor = () => {
    const today = new Date();
    const expiryDate = new Date(medicine.expiryDate);
    const daysUntilExpiry = Math.ceil((expiryDate.getTime() - today.getTime()) / (1000 * 60 * 60 * 24));

    if (daysUntilExpiry < 30) {
      return 'bg-red-100 border-red-300';
    }

    if (medicine.quantity < 10) {
      return 'bg-yellow-100 border-yellow-300';
    }

    return 'bg-white border-gray-200';
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-IN', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  };

  return (
    <div className={`rounded-lg border-2 p-4 shadow-sm transition-all hover:shadow-md ${getBackgroundColor()}`}
      onClick={() => navigate(`/medicine/${medicine.id}`)}
      role='button'
      tabIndex={0}
      onKeyDown={(e) => {
        if (e.key === 'Enter' || e.key === ' ') {
          navigate(`/medicine/${medicine.id}`);
        }
      }}
      aria-label='View medicine details'
    >
      <h3 className="text-lg font-semibold text-gray-800 mb-3">{medicine.name}</h3>

      <div className="space-y-2">
        <div className="flex items-center gap-2 text-sm text-gray-700">
          <PackageIcon />
          <span className="font-medium">Quantity:</span>
          <span className={medicine.quantity < 10 ? 'font-bold text-orange-700' : ''}>
            {medicine.quantity} units
          </span>
        </div>

        <div className="flex items-center gap-2 text-sm text-gray-700">
          <CalendarIcon />
          <span className="font-medium">Expiry:</span>
          <span>{formatDate(medicine.expiryDate)}</span>
        </div>

        <div className="flex items-center gap-2 text-sm text-gray-700">
          <BuildingIcon />
          <span className="font-medium">Brand:</span>
          <span>{medicine.brand}</span>
        </div>

        <div className="flex items-center gap-2 text-sm text-gray-700">
          <DollarIcon />
          <span className="font-medium">Price:</span>
          <span>${medicine.price.toFixed(2)}</span>
        </div>
      </div>
    </div>
  );
}
