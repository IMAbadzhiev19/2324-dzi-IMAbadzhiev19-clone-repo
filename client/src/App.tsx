import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";

import LandingPage from "@/pages/LandingPage";
import SignUpPage from "@/pages/SignUpPage";
import SignInPage from "@/pages/SignInPage";
import HomePage from "@/pages/HomePage";
import AuthErrorPage from "@/pages/AuthErrorPage";
import DepotsPage from "@/pages/DepotsPage";
import AuthGuard from "@/components/AuthGuard";
import AddDepotPage from "@/pages/AddDepotPage";
import DepotDashboardPage from "./pages/DepotDashboardPage";
import AddMedicinePage from "./pages/AddMedicinePage";
import ProfilePage from "./pages/ProfilePage";
import PharmaciesPage from "./pages/PharmaciesPage";
import AddPharmacyPage from "./pages/AddPharmacyPage";
import PharmacyDashboardPage from "./pages/PharmacyDashboardPage";
import PharmacySettingsPage from "./pages/PharmacySettingsPage";
import InvoicePage from "./pages/InvoicePage";

const App = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route index path="/" element={<LandingPage />} />
      <Route path="/sign-up" element={<SignUpPage />} />
      <Route path="/sign-in" element={<SignInPage />} />
      <Route path="/profile" element={<AuthGuard element={<ProfilePage />} />} />
      <Route path="/invoice" element={<AuthGuard element={<InvoicePage />} />} />
      <Route path="/home" element={<AuthGuard element={<HomePage />} />} />
      <Route path="/pharmacies" element={<AuthGuard element={<PharmaciesPage />} />} />
      <Route path="/pharmacies/add" element={<AuthGuard element={<AddPharmacyPage />} />} />
      <Route
        path="/pharmacies/:id/dashboard"
        element={
          <AuthGuard element={<PharmacyDashboardPage />} />
        }
      />
      <Route
        path="/pharmacies/:id/settings"
        element={
          <AuthGuard element={<PharmacySettingsPage />} requiresFounder={true} />
        }
      />
      <Route path="/depots" element={<AuthGuard element={<DepotsPage />} />} />
      <Route path="/depots/add" element={<AuthGuard element={<AddDepotPage />} />} />
      <Route
        path="/depots/:id/dashboard"
        element={
          <AuthGuard element={<DepotDashboardPage />} requiresDepotM={true} />
        }
      />
      <Route path="/medicines/add" element={<AuthGuard element={<AddMedicinePage />} />} />
      <Route
        path="*"
        element={
          <AuthErrorPage
            statusCode={404}
            message="Page not found."
            handleOnLogin={null}
            handleOnBack={true}
          />
        }
      />
    </>
  )
);

export default App;
