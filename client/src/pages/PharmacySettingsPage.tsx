import { CustomTabPanel, allyProps } from "@/components/TabPanel";
import DashboardLayout from "@/layouts/DashboardLayout";
import { Tab, Tabs } from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import DepotSection from "@/components/pharmacy/settings/DepotSection";

import styles from "@/styles/pages/PharmacySettingsPage.module.css";
import { PharmacyVM } from "@/api";
import { AxiosResponse } from "axios";
import pharmacyService from "@/services/pharmacy-service";
import ImportantSection from "@/components/pharmacy/settings/ImportantSection";
import AddressSection from "@/components/pharmacy/settings/AddressSection";
import EmployeesSection from "@/components/pharmacy/settings/EmployeesSection";

const PharmacySettingsPage = () => {
  const { id } = useParams();

  const [value, setValue] = useState<number>(0);
  const [pharmacy, setPharmacy] = useState<PharmacyVM | null>(null);

  function handleTabChange(event: React.SyntheticEvent, newValue: number) {
    setValue(newValue);
  }

  useEffect(() => {
    try {
      (async () => {
        const response = (await pharmacyService.makePharmacyGetByIdRequest(
          id!
        )) as AxiosResponse<PharmacyVM>;

        setPharmacy(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }, [id]);

  return (
    <DashboardLayout>
      {pharmacy !== null && (
        <div className={styles["wrapper"]}>
          <div className={styles["tabs"]}>
            <Tabs value={value} onChange={handleTabChange}>
              <Tab
                label="Общо"
                {...allyProps(0)}
                sx={{
                  fontWeight: "bold",
                  color: "#000",
                  fontFamily: "Montserrat sans-serif",
                }}
              />
              <Tab
                label="Работници"
                {...allyProps(1)}
                sx={{
                  fontWeight: "bold",
                  color: "#000",
                  fontFamily: "Montserrat sans-serif",
                }}
              />
              <Tab
                label="Склад"
                {...allyProps(2)}
                sx={{
                  fontWeight: "bold",
                  color: "#000",
                  fontFamily: "Montserrat sans-serif",
                }}
              />
            </Tabs>
          </div>
          <CustomTabPanel value={value} index={0}>
            <section className={styles["section"]}>
              <div className={styles["form"]}>
                <p>Важно</p>
                <ImportantSection pharmacy={pharmacy} />
              </div>
              <div className={styles["form"]}>
                <p>Адрес</p>
                <AddressSection pharmacy={pharmacy} />
              </div>
            </section>
          </CustomTabPanel>
          <CustomTabPanel value={value} index={1}>
            <EmployeesSection pharmacyId={id!} />
          </CustomTabPanel>
          <CustomTabPanel value={value} index={2}>
            <DepotSection pharmacy={pharmacy} />
          </CustomTabPanel>
        </div>
      )}
    </DashboardLayout>
  );
};

export default PharmacySettingsPage;
