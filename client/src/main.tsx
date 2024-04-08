import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";

import storageService from "@/services/storage-service";
import { ACCESS_TOKEN_CHECKING_INTERVAL } from "@/shared/auth-constants";
import authService from "@/services/auth-service";
import { RouterProvider } from "react-router-dom";
import { Toaster } from "sonner";
import { ResponseData } from "@/services/web-api-service";

const checkTokens = async () => {
  if (!storageService.retrieveAccessToken()) {
    return;
  }

  const expiration = storageService.retrieveTokenExpiresDate();
  const now = new Date(Date.now() + ACCESS_TOKEN_CHECKING_INTERVAL);

  if (expiration! < now) {
    try {
      const refreshToken = storageService.retrieveRefreshToken();
      if (!refreshToken) {
        console.log("Refresh token is missing. Please sign in.");
        storageService.deleteUserData();
        return;
      }

      const response = await authService.makeRenewTokensRequest(
        storageService.retrieveAccessToken(),
        storageService.retrieveRefreshToken()
      );

      const responseData = response.data as unknown as ResponseData;
      storageService.saveAccessToken(responseData.accessToken);
      storageService.saveRefreshToken(responseData.refreshToken);
      storageService.saveTokenExpiresDate(responseData.expiration);
    } catch (error) {
      console.error("Error refreshing access token: ", error);
      storageService.deleteUserData();
    }
  }
};

await checkTokens();

setInterval(checkTokens, ACCESS_TOKEN_CHECKING_INTERVAL);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={App} />
    <Toaster richColors />
  </React.StrictMode>
);
