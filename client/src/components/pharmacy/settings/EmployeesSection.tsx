import { DataGrid, GridColDef, GridRowSelectionModel, bgBG } from "@mui/x-data-grid";
import { UserVM } from "@/api";

import styles from "@/styles/components/pharmacy/settings/EmployeesSection.module.css";
import { useEffect, useState } from "react";
import pharmacyService from "@/services/pharmacy-service";
import { AxiosResponse } from "axios";
import { Button } from "@/components/styled/Button";
import { Modal } from "@mui/material";
import AssignEmployeeSection from "./AssignEmployeeSection";
import { toast } from "sonner";

const columns: GridColDef[] = [
  {
    field: "firstName",
    headerName: "Име",
    width: 150,
  },
  {
    field: "lastName",
    headerName: "Фамилия",
    width: 150,
  },
  {
    field: "email",
    headerName: "Имейл",
    width: 250,
  },
  {
    field: "phoneNumber",
    headerName: "Телефон",
    width: 150,
  },
  {
    field: "workedHours",
    headerName: "Отработени часове",
    width: 130,
  },
];

const EmployeesSection = ({ pharmacyId }: { pharmacyId: string }) => {
  const [selectedIds, setSelectedIds] = useState<GridRowSelectionModel>([]);

  const [open, setOpen] = useState<boolean>(false);
  const [changed, setChanged] = useState<boolean>(false);

  const [pharmacists, setPharmacists] = useState<UserVM[]>([]);

  async function refreshPharmacists(): Promise<void> {
    try {
      (async () => {
        const response =
          (await pharmacyService.makePharmacyGetPharmacistsRequest(
            pharmacyId
          )) as AxiosResponse<UserVM[]>;

        setPharmacists(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    refreshPharmacists();
  }, []);

  return (
    <div className={styles["wrapper"]}>
      <p>Отработени часове на фармацевта се актуализират веднъж седмично !</p>
      <DataGrid
        localeText={bgBG.components.MuiDataGrid.defaultProps.localeText}
        sx={{ maxHeight: "290px" }}
        rows={pharmacists}
        columns={columns}
        initialState={{
          pagination: {
            paginationModel: { page: 0, pageSize: 5 },
          },
        }}
        pageSizeOptions={[3]}
        checkboxSelection
        onRowSelectionModelChange={(ids) => {
          setSelectedIds(ids);
        }}
      />
      <Button
        onClick={() => {
          setOpen(true);
        }}
      >
        Добави работник
      </Button>
      {selectedIds.length === 1 && (
        <Button
          onClick={async () => {
            await pharmacyService
              .makePharmacyRemoveEmployeeRequest(
                pharmacyId,
                selectedIds[0].toString()
              )
              .then(async function () {
                await refreshPharmacists();
                toast.success("Успешно премахване на работник от аптеката");
              })
              .catch(function (error) {
                const message =
                  error.response.data.message ||
                  error.response.data.errors[
                    Object.keys(error.response?.data.errors)[0]
                  ];
                toast.error(`Излезе грешка: ${message}`);
              });
          }}
        >
          Премахни работник
        </Button>
      )}
      <div className={styles["employee-icon-container"]}>
        <img src="/EmployeeIcon.png" alt="" />
      </div>
      <Modal
        open={open}
        onClose={async () => {
          if (changed) {
            await refreshPharmacists();
            setChanged(false);
          }
          setOpen(false);
        }}
        sx={{ display: "flex", alignItems: "center", justifyContent: "center" }}
      >
        <div className={styles["modal-container"]}>
          <AssignEmployeeSection
            pharmacyId={pharmacyId}
            setChanged={setChanged}
          />
        </div>
      </Modal>
    </div>
  );
};

export default EmployeesSection;
