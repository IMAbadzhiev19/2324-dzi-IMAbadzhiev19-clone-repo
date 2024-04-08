import storageService from "@/services/storage-service";

const useAuth = () => {
  const accessToken = storageService.retrieveAccessToken();
  const tokenPayload = accessToken
    ? JSON.parse(window.atob(accessToken.split(".")[1]))
    : null;
  const roles: string[] = tokenPayload
    ? tokenPayload[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ]
    : [];

  return {
    isLoggedIn: !!accessToken,
    isFounder: roles.includes("Founder"),
    isDepotManager: roles.includes("DepotManager"),
    isPharmacist: roles.includes("Pharmacist"),
  };
};

export default useAuth;
