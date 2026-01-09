//import './App.css'
import { Provider } from 'react-redux';
import { Home } from './pages/Home';
import { appStore } from './redux/appStore';
import { BrowserRouter, Route, Routes } from 'react-router';
import { MedicineDetails } from './pages/MedicineDetails';
import { MedicineUpdate } from './pages/MedicineUpdate';
import { MedicineCreate } from './pages/MedicineCreate';

function App() {
  return (
    <Provider store={appStore}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/medicine/create" element={<MedicineCreate />} />
          <Route path="/medicine/:id" element={<MedicineDetails />} />
          <Route path="/medicine/:id/update" element={<MedicineUpdate />} />
        </Routes>
      </BrowserRouter>
    </Provider>
  );
}


export default App;
