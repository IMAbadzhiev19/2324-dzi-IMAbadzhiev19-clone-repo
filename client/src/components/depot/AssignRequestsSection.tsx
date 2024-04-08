import { NotificationVM } from "@/api";
import notificationService from "@/services/notification-service";
import {
  DataGrid,
  GridColDef,
  GridRowSelectionModel,
  GridValueGetterParams,
  bgBG,
} from "@mui/x-data-grid";
import { useEffect, useState } from "react";

import { AxiosResponse } from "axios";
import { Button } from "../styled/Button";

import styles from "@/styles/components/depot/AssignRequestsSection.module.css";
import depotService from "@/services/depot-service";
import { toast } from "sonner";
import { useParams } from "react-router-dom";

const columns: GridColDef[] = [
  {
    field: "text",
    headerName: "Текст",
    width: 890,
  },
  {
    field: "sentOn",
    headerName: "Изпратено на",
    width: 120,
    valueGetter: (params: GridValueGetterParams) => {
      return new Date(params.row.sentOn).toLocaleDateString();
    },
  },
];

const AssignRequestsSection = () => {
  const { id } = useParams();

  const [assignRequests, setAssignRequests] = useState<NotificationVM[]>([]);
  const [selectedIds, setSelectedIds] = useState<GridRowSelectionModel>([]);

  async function refreshAssignRequests(): Promise<void> {
    try {
      (async () => {
        const response =
          (await notificationService.makeNotificationsGetAllAssignRequests(
            id!
          )) as AxiosResponse<NotificationVM[]>;

        setAssignRequests(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    refreshAssignRequests();
  }, []);

  const getRowClassName = () => {
    return styles["column"];
  };

  return (
    <div className={styles["wrapper"]}>
      <DataGrid
        localeText={bgBG.components.MuiDataGrid.defaultProps.localeText}
        sx={{
          backgroundColor: "#FFF",
          color: "#000",
        }}
        rows={assignRequests}
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
      {selectedIds.length == 1 && (
        <Button
          $transparent
          onClick={async () => {
            const id = selectedIds.map((id) => id.toString())[0];
            const notification = assignRequests.filter((ar) => ar.id === id)[0];

            await depotService
              .makeDepotAssignToPharmacyRequest(
                notification.pharmacyId!,
                notification.depotId!
              )
              .then(async function () {
                await refreshAssignRequests();
                toast.success("Успешно приета заявка за работа");
              })
              .catch((error) => {
                const message =
                  error.response.data.message ||
                  error.response.data.errors[
                    Object.keys(error.response?.data.errors)[0]
                  ];
                toast.error(`Излезе грешка: ${message}`);
              });
          }}
        >
          Accept
        </Button>
      )}
    </div>
  );
};

export default AssignRequestsSection;
