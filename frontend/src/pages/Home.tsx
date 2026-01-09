import { useEffect, useState } from 'react';
import type { Medicine }  from '../types/medicine';
import { MedicineCard } from '../components/MedicineCard';
import { useLazyGetMedicinesQuery } from '../redux/medicineSlice';
import { mockMedicines } from '../data/mockData';
import { LoaderComponent } from '../components/LoaderComponent';
import { ErrorComponent } from '../components/ErrorComponent';
import { useNavigate } from 'react-router';

const SearchIcon = () => (
  <svg className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
  </svg>
);

export function Home() {
  const [medicines, setMedicines] = useState<Medicine[]>([]);
  const [filteredMedicines, setFilteredMedicines] = useState<Medicine[]>([]);
  const [searchQuery, setSearchQuery] = useState('');
  const navigate = useNavigate();
  //const [loading, setLoading] = useState(true);
  //const [error, setError] = useState<string | null>(null);
  const [trigger,{isLoading,isError}] = useLazyGetMedicinesQuery();

  useEffect(() => {
      //setLoading(true);

      trigger().then((result) => {
        if ('data' in result) {
          setMedicines(result.data?.data?.items || []);
          setFilteredMedicines(result.data?.data?.items || []);
            //setLoading(false);
        } else if ('error' in result) {
          //uncomment to enable error handling
          //setError('Failed to fetch medicines');

          /* Using mock data in case of error 
          Comment the following two lines to disable mock data */
          setMedicines(mockMedicines)
          setFilteredMedicines(mockMedicines)
          /* End of mock data usage */
         // setLoading(false);
        }
      }).catch(() => {
         /* Using mock data in case of error 
          Comment the following two lines to disable mock data */
          setMedicines(mockMedicines)
          setFilteredMedicines(mockMedicines)
          /* End of mock data usage */
          //setLoading(false);
          //uncomment to enable error handling
        //setError('Failed to fetch medicines');
      });
   
  }, [trigger]);

  useEffect(() => {
    if (searchQuery.trim() === '') {
      setFilteredMedicines(medicines);
    } else {
      const filtered = medicines.filter(medicine =>
        medicine.name.toLowerCase().includes(searchQuery.toLowerCase())
      );
      setFilteredMedicines(filtered);
    }
  }, [searchQuery, medicines]);


  if (isLoading) {
    return <LoaderComponent loadingMessage="Loading medicines..." />;
  }

  if (isError) {
    return <ErrorComponent errorMessage="Failed to load medicines." />;
  }

  return (
    <div className="w-full max-w-7xl mx-auto p-6">
      <div className="mb-6">
        <h1 className="text-3xl font-bold text-gray-900 mb-2">Medicine Inventory</h1>
        <p className="text-gray-600 mb-4">Manage and track your medicine stock</p>

        <div className="flex justify-between items-center mb-4">
          <div className="relative w-full max-w-md">
            <SearchIcon />
            <input
              type="text"
              placeholder="Search medicines by name..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none"
            />
          </div>
          <button
            className="ml-4 px-4 py-2 bg-green-600 text-white font-semibold rounded-lg shadow hover:bg-green-700 transition"
            onClick={() => navigate('/medicine/create')}
          >
            Add Medicine
          </button>
        </div>

        <div className="flex gap-4 mt-4 text-sm">
          <div className="flex items-center gap-2">
            <div className="w-4 h-4 bg-red-100 border-2 border-red-300 rounded"></div>
            <span className="text-gray-700">Expires in less than 30 days</span>
          </div>
          <div className="flex items-center gap-2">
            <div className="w-4 h-4 bg-yellow-100 border-2 border-yellow-300 rounded"></div>
            <span className="text-gray-700">Low stock (less than 10 units)</span>
          </div>
        </div>
      </div>

      {filteredMedicines.length === 0 ? (
        <div className="text-center py-12">
          <p className="text-gray-500 text-lg">
            {searchQuery ? 'No medicines found matching your search' : 'No medicines available'}
          </p>
        </div>
      ) : (
        <>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
            {filteredMedicines.map((medicine) => (
              <MedicineCard key={medicine.id} medicine={medicine} />
            ))}
          </div>

          <div className="mt-6 text-center text-gray-600">
            Showing {filteredMedicines.length} of {medicines.length} medicines
          </div>
        </>
      )}
    </div>
  );
}
