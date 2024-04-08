import useAuth from "@/hooks/useAuth";
import AuthErrorPage from "@/pages/AuthErrorPage";
import React, { ReactNode } from "react";
import { Navigate } from "react-router-dom";

const AuthGuard = ({
  element,
  requiresFounder,
  requiresDepotM,
  ...rest
}: {
  element: ReactNode;
  requiresFounder?: boolean;
  requiresDepotM?: boolean;
}) => {
    const { isLoggedIn, isDepotManager, isFounder } = useAuth();

    if (isLoggedIn) {
        if (requiresFounder) {
            if (isFounder) {
                return <React.Fragment {...rest}>{element}</React.Fragment>
            }
            else {
                return (
                    <AuthErrorPage
                      statusCode={403}
                      message="Unauthorized."
                      handleOnLogin={null}
                      handleOnBack={true}
                    />
                  );
            }
        }
        else if (requiresDepotM) {
            if (isDepotManager) {
                return <React.Fragment {...rest}>{element}</React.Fragment>
            }
            else {
                return (
                    <AuthErrorPage
                      statusCode={403}
                      message="Unauthorized."
                      handleOnLogin={null}
                      handleOnBack={true}
                    />
                  );
            }
        }
        else return <React.Fragment {...rest}>{element}</React.Fragment>
    } else return <Navigate to="/sign-in" replace />
};

export default AuthGuard;
