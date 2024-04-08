import { BuildingType, MedicineVM, PharmacyVM } from "@/api";
import { Button } from "@/components/styled/Button";
import DashboardLayout from "@/layouts/DashboardLayout";
import medicineService from "@/services/medicine-service";

import { Modal } from "@mui/material";
import {
  DataGrid,
  GridColDef,
  GridRowClassNameParams,
  GridRowSelectionModel,
  GridValueGetterParams,
  bgBG,
} from "@mui/x-data-grid";
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { toast } from "sonner";

import pharmacyService from "@/services/pharmacy-service";
import MedicineMoreSection from "@/components/pharmacy/MedicineMoreSection";
import MedicineAddSection from "@/components/pharmacy/MedicineAddSection";

import styles from "@/styles/pages/PharmacyDashboardPage.module.css";
import MedicineRefillSection from "@/components/pharmacy/MedicineRefillSection";
import MedicineSellSection from "@/components/pharmacy/MedicineSellSection";
import Agenda from "@/components/Agenda";

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
    width: 150,
    valueGetter: (params: GridValueGetterParams) => {
      return new Date(params.row.updatedOn).toLocaleDateString();
    },
  },
];

const PharmacyDashboardPage = () => {
  const { id } = useParams();

  const [open, setOpen] = useState<boolean>(false);

  const [isMore, setIsMore] = useState<boolean>(false);
  const [isRefill, setIsRefill] = useState<boolean>(false);
  const [isAdd, setIsAdd] = useState<boolean>(false);
  const [isSell, setIsSell] = useState<boolean>(false);

  const [medicineChanged, setMedicineChanged] = useState<boolean>(false);

  const [pharmacy, setPharmacy] = useState<PharmacyVM | null>(null);
  const [medicines, setMedicines] = useState<MedicineVM[]>([]);
  const [selectedIds, setSelectedIds] = useState<GridRowSelectionModel>([]);

  async function refreshMedicines(): Promise<void> {
    try {
      (async () => {
        const pharmacyResponse =
          (await pharmacyService.makePharmacyGetByIdRequest(
            id!
          )) as AxiosResponse<PharmacyVM>;

        setPharmacy(pharmacyResponse.data);

        if (pharmacyResponse.data.depot !== null) {
          const medicineResponse =
            (await medicineService.makeGetMedicinesByBuilding(
              id!,
              BuildingType.NUMBER_0
            )) as AxiosResponse<MedicineVM[]>;

          setMedicines(medicineResponse.data);
        }
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
    <DashboardLayout>
      {(pharmacy && pharmacy.depot) !== null ? (
        <div className={styles["wrapper"]}>
          <DataGrid
            localeText={bgBG.components.MuiDataGrid.defaultProps.localeText}
            sx={{ color: "#000" }}
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
              onClick={() => {
                setOpen(true);
                setIsAdd(true);
              }}
            >
              Заяви от склада
            </Button>
            {selectedIds.length > 0 && (
              <Button
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
                Изтрий
              </Button>
            )}
            {selectedIds.length > 0 && (
              <Button
                onClick={() => {
                  setIsSell(true);
                  setOpen(true);
                }}
              >
                Продай
              </Button>
            )}
            {selectedIds.length == 1 && (
              <Button
                onClick={() => {
                  setIsRefill(true);
                  setOpen(true);
                }}
              >
                Презареди
              </Button>
            )}
            {selectedIds.length == 1 && (
              <Button
                onClick={() => {
                  setIsMore(true);
                  setOpen(true);
                }}
              >
                Повече
              </Button>
            )}
          </div>
          <div className={styles["agenda"]}>
            <Agenda />
          </div>
          <Modal
            open={open}
            onClose={async () => {
              if (medicineChanged) {
                await refreshMedicines();
                setMedicineChanged(false);
              }

              setIsAdd(false);
              setIsMore(false);
              setIsRefill(false);
              setIsSell(false);

              setOpen(false);
            }}
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <div className={styles["modal-container"]}>
              {isAdd && (
                <MedicineAddSection id={id!} setChanged={setMedicineChanged} />
              )}

              {isSell && (
                <MedicineSellSection
                  medicineVMs={medicines.filter((m) =>
                    selectedIds.map((id) => id.toString()).includes(m.id!)
                  )}
                />
              )}

              {isMore && (
                <MedicineMoreSection
                  medicineVM={
                    medicines.filter(
                      (m) => m.id == selectedIds.map((id) => id.toString())[0]
                    )[0]
                  }
                  setChanged={setMedicineChanged}
                />
              )}

              {isRefill && (
                <MedicineRefillSection
                  pharmacyId={id!}
                  medicineVM={
                    medicines.filter(
                      (m) => m.id == selectedIds.map((id) => id.toString())[0]
                    )[0]
                  }
                  setChanged={setMedicineChanged}
                />
              )}
            </div>
          </Modal>
        </div>
      ) : (
        <div className={styles["error"]}>
          <h2 className={styles["error-text"]}>
            Трябва първо да работите със склад !
          </h2>
        </div>
      )}
    </DashboardLayout>
  );
};

export default PharmacyDashboardPage;
