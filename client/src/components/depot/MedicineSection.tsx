import { BuildingType, MedicineVM } from "@/api";
import styles from "@/styles/components/depot/MedicineSection.module.css";
import {
  DataGrid,
  GridColDef,
  GridRowClassNameParams,
  GridRowSelectionModel,
  GridValueGetterParams,
  bgBG,
} from "@mui/x-data-grid";
import { Button } from "../styled/Button";
import { useEffect, useState } from "react";
import medicineService from "@/services/medicine-service";
import { useNavigate } from "react-router-dom";
import { AxiosResponse } from "axios";
import { toast } from "sonner";
import { Modal } from "@mui/material";
import MedicineMoreSection from "./MedicineMoreSection";
import Agenda from "../Agenda";

const columns: GridColDef[] = [
  {
    field: "name",
    headerName: "Име",
    width: 150,
    valueGetter: (params: GridValueGetterParams) => {
      return params.row.basicMedicine.name;
    },
  },
  { field: "price", headerName: "Цена", width: 120 },
  { field: "quantity", headerName: "Брой", width: 120 },
  {
    field: "expiration_date",
    headerName: "Срок на годност",
    width: 170,
    valueGetter: (params: GridValueGetterParams) => {
      return new Date(params.row.expirationDate).toLocaleDateString();
    },
  },
  {
    field: "expired",
    headerName: "Изтекло?",
    width: 120,
    valueGetter: (params: GridValueGetterParams) => {
      return params.row.isExpired ? "Да" : "Не";
    },
  },
  {
    field: "received_date",
    headerName: "Получено на",
    width: 150,
    valueGetter: (params: GridValueGetterParams) => {
      return new Date(params.row.createdOn).toLocaleDateString();
    },
  },
  {
    field: "last_modified",
    headerName: "Последна промяна",
    width: 170,
    valueGetter: (params: GridValueGetterParams) => {
      return new Date(params.row.updatedOn).toLocaleDateString();
    },
  },
];

const MedicineSection = ({ depotId }: { depotId: string }) => {
  const navigate = useNavigate();

  const [open, setOpen] = useState<boolean>(false);
  const [medicineChanged, setMedicineChanged] = useState<boolean>(false);

  const [medicines, setMedicines] = useState<MedicineVM[]>([]);
  const [selectedIds, setSelectedIds] = useState<GridRowSelectionModel>([]);

  async function refreshMedicines(): Promise<void> {
    try {
      (async () => {
        const response = (await medicineService.makeGetMedicinesByBuilding(
          depotId!,
          BuildingType.NUMBER_1
        )) as AxiosResponse<MedicineVM[]>;

        setMedicines(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    refreshMedicines();
  }, []);

  const getRowClassName = (params: GridRowClassNameParams) => {
    const medicine = params.row;
    if (
      medicine.quantity == 0 &&
      new Date(medicine.expirationDate) < new Date()
    )
      return styles["both-warnings"];
    else if (medicine.quantity == 0) return styles["no-quantity"];
    else if (new Date(medicine.expirationDate) < new Date())
      return styles["expired-row"];

    return styles["in-stock"];
  };

  return (
    <div className={styles["wrapper"]}>
      <DataGrid
        localeText={bgBG.components.MuiDataGrid.defaultProps.localeText}
        sx={{
          backgroundColor: "#FFF",
          color: "#000",
        }}
        rows={medicines}
        columns={columns}
        initialState={{
          pagination: {
            paginationModel: { page: 0, pageSize: 5 },
          },
        }}
        pageSizeOptions={[5, 10]}
        checkboxSelection
        onRowSelectionModelChange={(ids) => {
          setSelectedIds(ids);
        }}
        getRowClassName={getRowClassName}
      />
      <div className={styles["buttons"]}>
        <Button
          $transparent
          onClick={() => {
            localStorage.setItem("_buildingId", depotId!);
            navigate("/medicines/add");
          }}
        >
          Добави лекарство
        </Button>
        {selectedIds.length > 0 && (
          <Button
            $transparent
            onClick={() => {
              selectedIds.map(async (id) => {
                await medicineService
                  .makeMedicineDeleteRequest(id.toString())
                  .then(async function () {
                    await refreshMedicines();
                    toast.success("Успешно изтриване");
                  })
                  .catch((error) => {
                    const message =
                      error.response.data.message ||
                      error.response.data.errors[
                        Object.keys(error.response?.data.errors)[0]
                      ];
                    toast.error(`Излезе грешка: ${message}`);
                  });
              });
            }}
          >
            Изтриване
          </Button>
        )}
        {selectedIds.length == 1 && (
          <Button $transparent onClick={() => setOpen(true)}>
            Повече
          </Button>
        )}
      </div>
      <Modal
        open={open}
        onClose={async () => {
          if (medicineChanged) {
            await refreshMedicines();
            setMedicineChanged(false);
          }
          setOpen(false);
        }}
        sx={{ display: "flex", alignItems: "center", justifyContent: "center" }}
      >
        <div className={styles["more-info"]}>
          <MedicineMoreSection
            medicineVM={
              medicines.filter(
                (m) => m.id == selectedIds.map((id) => id.toString())[0]
              )[0]
            }
            setChanged={setMedicineChanged}
          />
        </div>
      </Modal>
      <Agenda />
    </div>
  );
};

export default MedicineSection;
