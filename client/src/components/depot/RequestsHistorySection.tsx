import {
  DataGrid,
  GridColDef,
  GridRowClassNameParams,
  GridRowSelectionModel,
  GridValueGetterParams,
  bgBG,
} from "@mui/x-data-grid";

import styles from "@/styles/components/depot/RequestsHistorySection.module.css";
import { useEffect, useState } from "react";
import { NotificationVM } from "@/api";
import notificationService from "@/services/notification-service";
import { AxiosResponse } from "axios";
import { useParams } from "react-router-dom";
import { Button } from "../styled/Button";

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

const RequestsHistorySection = () => {
  const { id } = useParams();

  const [requests, setRequests] = useState<NotificationVM[]>([]);
  const [selectedIds, setSelectedIds] = useState<GridRowSelectionModel>([]);

  async function refreshRequests(): Promise<void> {
    try {
      (async () => {
        const response =
          (await notificationService.makeNotificationsGetForDepotRequest(
            id!
          )) as AxiosResponse<NotificationVM[]>;

        setRequests(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    refreshRequests();
  }, []);

  const getRowClassName = () => {
    return styles["column"];
  };

  return (
    <div className={styles["wrapper"]}>
      <DataGrid
        localeText={bgBG.components.MuiDataGrid.defaultProps.localeText}
        sx={{ backgroundColor: "#FFF", color: "#000" }}
        rows={requests}
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
      {selectedIds.length < 0 && <Button>Accept</Button>}
    </div>
  );
};

export default RequestsHistorySection;
