import { BuildingType, NotificationVM, UserVM } from "@/api";
import { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { Avatar, Badge, Modal, Popover } from "@mui/material";
import { Button } from "./styled/Button";
import authService from "@/services/auth-service";
import storageService from "@/services/storage-service";
import { toast } from "sonner";
import MailIcon from "@mui/icons-material/Mail";

import styles from "@/styles/components/Navbar.module.css";
import notificationService from "@/services/notification-service";
import { DataGrid, GridColDef, GridValueGetterParams } from "@mui/x-data-grid";

const columns: GridColDef[] = [
  {
    field: "text",
    headerName: "Текст",
    width: 690,
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

const Navbar = ({ userVM, dark }: { userVM: UserVM; dark: boolean }) => {
  const { id } = useParams();

  const pathname = useLocation().pathname;
  const navigate = useNavigate();

  const [notifications, setNotifications] = useState<NotificationVM[]>([]);
  const [modalOpen, setModalOpen] = useState<boolean>(false);

  async function refreshNotifications(): Promise<void> {
    try {
      if (pathname.includes("depots")) {
        const notificationResponse =
          await notificationService.makeNotificationsGetWarningsRequest(
            id!,
            BuildingType.NUMBER_1
          );

        setNotifications(notificationResponse.data);
      } else if (pathname.includes("pharmacies")) {
        const notificationResponse =
          await notificationService.makeNotificationsGetWarningsRequest(
            id!,
            BuildingType.NUMBER_0
          );

        setNotifications(notificationResponse.data);
      }
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    refreshNotifications();
    const intervalId = setInterval(refreshNotifications, 1 * 60 * 1000);

    return () => clearInterval(intervalId);
  }, []);

  const getRowClassName = () => {
    return styles["column"];
  };

  const [anchorEl, setAnchorEl] = useState<HTMLDivElement | null>(null);
  const open = Boolean(anchorEl);

  return (
    <div className={styles["wrapper"]}>
      <div className={styles["container"]}>
        <div className={styles["group"]}>
          <Badge badgeContent={notifications?.length} color="primary">
            <MailIcon
              className={dark ? styles["mail-icon-dark"] : styles["mail-icon"]}
              onClick={() => {
                setModalOpen(true);
              }}
            />
          </Badge>
          <Avatar
            onClick={(event: React.MouseEvent<HTMLDivElement>) => {
              setAnchorEl(event.currentTarget);
            }}
            alt={userVM.firstName![0] + userVM.lastName![0]}
          />
        </div>
        <Popover
          open={open}
          anchorEl={anchorEl}
          onClose={() => {
            setAnchorEl(null);
          }}
          anchorOrigin={{
            vertical: "bottom",
            horizontal: "left",
          }}
          sx={{ marginTop: "0.3em" }}
        >
          <div className={styles["buttons"]}>
            <Button $primary $px={4} onClick={() => navigate("/profile")}>
              Профил
            </Button>
            <Button
              $line
              $px={4}
              onClick={async () => {
                await authService.makeLogoutRequest();
                storageService.deleteUserData();
                toast.success("Logged out successfully");
                navigate("/");
              }}
            >
              Изход
            </Button>
          </div>
        </Popover>
      </div>
      <Modal
        open={modalOpen}
        onClose={() => {
          setModalOpen(false);
        }}
        sx={{ display: "flex", alignItems: "center", justifyContent: "center" }}
      >
        <div className={styles["modal-container"]}>
          <DataGrid
            rows={notifications}
            columns={columns}
            initialState={{
              pagination: {
                paginationModel: { page: 0, pageSize: 5 },
              },
            }}
            pageSizeOptions={[5]}
            getRowClassName={getRowClassName}
          />
        </div>
      </Modal>
    </div>
  );
};

export default Navbar;
