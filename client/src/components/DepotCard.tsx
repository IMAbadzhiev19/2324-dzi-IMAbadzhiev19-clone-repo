import { DepotVM } from "@/api";
import { Avatar, Card, CardContent, CardHeader, Modal } from "@mui/material";
import { useState } from "react";
import { MousePointer2, Trash2 } from "lucide-react";
import { toast } from "sonner";
import { Button } from "./styled/Button";

import styles from "@/styles/components/Card.module.css";

const DepotCard = ({
  depotVM,
  handleDelete,
  handleClick,
}: {
  depotVM: DepotVM;
  handleDelete: (id: string) => void;
  handleClick: (id: string) => void;
}) => {
  const [open, setOpen] = useState<boolean>(false);

  return (
    <Card
      sx={{
        boxShadow: "0.4em 0.4em 0 0 #93c5fd",
        borderRadius: "0.7em",
        border: "3px solid #93c5fd",
        backgroundColor: "#FFF",
      }}
      className={styles["card"]}
    >
      <CardHeader
        avatar={
          <Avatar sx={{ backgroundColor: "#E0AFA0"}} className={styles["avatar"]}>
            {depotVM.name![0]}
          </Avatar>
        }
        title={depotVM.name}
        subheader={`${depotVM.address?.street} ${depotVM.address?.number}, ${depotVM.address?.city} ${depotVM.address?.country}`}
        subheaderTypographyProps={{color: "#000"}}
      />
      <CardContent className={styles["content"]}>
        <div>
          <p>
            Основател:{" "}
            {`${depotVM.manager?.firstName} ${depotVM.manager?.lastName![0]}.`}
          </p>
        </div>
        <div className={styles["buttons-wrapper"]}>
          <MousePointer2
            onClick={() => {
              handleClick(depotVM.id!);
              toast.success("Успешно влизане");
            }}
          />
          <Trash2
            className={styles["delete-btn"]}
            onClick={() => {
              setOpen(true);
            }}
          />
        </div>
      </CardContent>
      <Modal
        open={open}
        onClose={() => {
          setOpen(false);
        }}
        sx={{ display: "flex", alignItems: "center", justifyContent: "center" }}
      >
        <div className={styles["confirmation"]}>
          <p>Сигурен ли си, че искаш да изтриеш склада?</p>
          <Button
            $danger
            onClick={() => {
              handleDelete(depotVM.id!);
              toast.success("Успешно изтриване");
            }}
          >
            Потвърди
          </Button>
        </div>
      </Modal>
    </Card>
  );
};

export default DepotCard;
