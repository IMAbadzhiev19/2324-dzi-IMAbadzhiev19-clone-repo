import { UserVM } from "@/api";
import Navbar from "@/components/Navbar";
import Sidebar from "@/components/Sidebar";
import userService from "@/services/user-service";

import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";

import styles from "@/styles/layouts/DashboardLayout.module.css";

const DashboardLayout = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<UserVM | null>(null);

  useEffect(() => {
    try {
      (async () => {
        const userResponse =
          (await userService.makeUserCurrentUserRequest()) as AxiosResponse<UserVM>;

        setUser(userResponse.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }, []);

  return (
    <>
      {user && (
        <div className={styles["wrapper"]}>
          <div className={styles["sidebar-wrapper"]}>
            <Sidebar />
          </div>
          <main className={styles["container"]}>
            <Navbar userVM={user} dark={true} />
            {children}
          </main>
        </div>
      )}
    </>
  );
};

export default DashboardLayout;
