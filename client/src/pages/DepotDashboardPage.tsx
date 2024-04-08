import { DepotVM, UserVM } from "@/api";
import Navbar from "@/components/Navbar";
import { CustomTabPanel, allyProps } from "@/components/TabPanel";
import AssignRequestsSection from "@/components/depot/AssignRequestsSection";
import CustomerSection from "@/components/depot/CustomerSection";
import MedicineSection from "@/components/depot/MedicineSection";
import RequestsHistorySection from "@/components/depot/RequestsHistorySection";
import depotService from "@/services/depot-service";
import userService from "@/services/user-service";
import styles from "@/styles/pages/DepotDashboardPage.module.css";
import { CircularProgress, Tab, Tabs } from "@mui/material";
import { AxiosResponse } from "axios";
import { LogOut } from "lucide-react";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

const DepotDashboardPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [user, setUser] = useState<UserVM | null>(null);
  const [depot, setDepot] = useState<DepotVM | null>(null);
  const [value, setValue] = useState<number>(0);

  function handleTabChange(event: React.SyntheticEvent, newValue: number) {
    setValue(newValue);
  }

  useEffect(() => {
    try {
      (async () => {
        const depotResponse = (await depotService.makeDepotGetByIdRequest(
          id!
        )) as AxiosResponse<DepotVM>;

        setDepot(depotResponse.data);

        const userResponse =
          (await userService.makeUserCurrentUserRequest()) as AxiosResponse<UserVM>;

        setUser(userResponse.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }, [id]);

  return (
    <>
      {depot !== null && user !== null ? (
        <div className={styles["wrapper"]}>
          <header>
            <h2 onClick={() => navigate("/depots")}>
              <LogOut className={styles["exit-icon"]} />
            </h2>
            <Navbar userVM={user} dark={false} />
          </header>
          <main>
            <div className={styles["tabs"]}>
              <Tabs value={value} onChange={handleTabChange}>
                <Tab
                  label="Лекарства"
                  {...allyProps(0)}
                  sx={{
                    fontWeight: "bold",
                    color: "#FFF",
                    fontFamily: "Montserrat sans-serif",
                  }}
                />
                <Tab
                  label="Клиенти"
                  {...allyProps(1)}
                  sx={{
                    fontWeight: "bold",
                    color: "#FFF",
                    fontFamily: "Montserrat sans-serif",
                  }}
                />
                <Tab
                  label="Заявки за работа"
                  {...allyProps(2)}
                  sx={{
                    fontWeight: "bold",
                    color: "#FFF",
                    fontFamily: "Montserrat sans-serif",
                  }}
                />
                <Tab
                  label="История на зареждане"
                  {...allyProps(3)}
                  sx={{
                    fontWeight: "bold",
                    color: "#FFF",
                    fontFamily: "Montserrat sans-serif",
                  }}
                />
              </Tabs>
            </div>
            <CustomTabPanel value={value} index={0}>
              <MedicineSection depotId={id!} />
            </CustomTabPanel>
            <CustomTabPanel value={value} index={1}>
              <CustomerSection depotId={id!} />
            </CustomTabPanel>
            <CustomTabPanel value={value} index={2}>
              <AssignRequestsSection />
            </CustomTabPanel>
            <CustomTabPanel value={value} index={3}>
              <RequestsHistorySection />
            </CustomTabPanel>
          </main>
        </div>
      ) : (
        <div className={styles["loader"]}>
          <CircularProgress />
        </div>
      )}
    </>
  );
};

export default DepotDashboardPage;
