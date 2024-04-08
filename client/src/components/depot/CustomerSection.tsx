import { PharmacyVM } from "@/api";
import pharmacyService from "@/services/pharmacy-service";
import { DataGrid, GridColDef, GridValueGetterParams, bgBG } from "@mui/x-data-grid";
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";

import styles from "@/styles/components/depot/CustomerSection.module.css";

const columns: GridColDef[] = [
  {
    field: "name",
    headerName: "Име на аптеката",
    width: 220,
  },
  {
    field: "address",
    headerName: "Адрес",
    width: 250,
    valueGetter(params: GridValueGetterParams) {
      return `${params.row.address.street} ${params.row.address.number}, ${params.row.address.city} ${params.row.address.country}`;
    },
  },
  {
    field: "founder",
    headerName: "Собственик",
    width: 220,
    valueGetter(params: GridValueGetterParams) {
      return `${params.row.founder.firstName} ${params.row.founder.lastName}`;
    },
  },
  {
    field: "medicines_count",
    headerName: "Брой на лекарства",
    width: 100,
    valueGetter(params: GridValueGetterParams) {
      return params.row.medicines.length;
    },
  },
];

const CustomerSection = ({ depotId }: { depotId: string }) => {
  const [pharmacies, setPharmacies] = useState<PharmacyVM[]>([]);

  useEffect(() => {
    try {
      (async () => {
        const response = (await pharmacyService.makePharmacyGetByDepotIdRequest(
          depotId
        )) as AxiosResponse<PharmacyVM[]>;

        setPharmacies(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }, [depotId]);

  const getRowClassName = () => {
    return styles["column"];
  };

  return (
    <div className={styles["wrapper"]}>
      <DataGrid
        localeText={bgBG.components.MuiDataGrid.defaultProps.localeText}
        sx={{ backgroundColor: "#FFF", color: "#000" }}
        rows={pharmacies}
        columns={columns}
        initialState={{
          pagination: {
            paginationModel: { page: 0, pageSize: 5 },
          },
        }}
        pageSizeOptions={[5, 10]}
        getRowClassName={getRowClassName}
      />
      {/* <div className={styles["media"]}>
        <img src="/Customers.jpg" />
      </div> */}
    </div>
  );
};

export default CustomerSection;
