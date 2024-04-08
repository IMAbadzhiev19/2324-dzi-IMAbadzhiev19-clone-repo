import { PharmacyVM } from "@/api";
import { useState } from "react";

import { Avatar, Card, CardContent, CardHeader, Modal } from "@mui/material";

import styles from "@/styles/components/Card.module.css";
import { Button } from "./styled/Button";
import { MousePointer2, Trash2 } from "lucide-react";
import { toast } from "sonner";
import useAuth from "@/hooks/useAuth";

const PharmacyCard = ({
  pharmacyVM,
  handleClick,
  handleDelete,
}: {
  pharmacyVM: PharmacyVM;
  handleClick: (id: string) => void;
  handleDelete: (id: string) => void;
}) => {
  const { isPharmacist, isFounder } = useAuth();
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
          <Avatar sx={{ backgroundColor: "#E0AFA0" }}>
            {pharmacyVM.name![0]}
          </Avatar>
        }
        title={pharmacyVM.name}
        subheader={`${pharmacyVM.address?.street} ${pharmacyVM.address?.number}, ${pharmacyVM.address?.city} ${pharmacyVM.address?.country}`}
        subheaderTypographyProps={{color: "#000"}}
      />
      <CardContent className={styles["content"]}>
        <div>
          <p>
            Основател:{" "}
            {`${pharmacyVM.founder?.firstName} ${
              pharmacyVM.founder?.lastName![0]
            }.`}
          </p>
          <p>
            Тоята роля:{" "}
            {isPharmacist ? "Pharmacist" : isFounder ? "Founder" : "None"}
          </p>
        </div>
        <div className={styles["buttons-wrapper"]}>
          <MousePointer2
            onClick={() => {
              handleClick(pharmacyVM.id!);
              toast.success("Successfully enrolled into pharmacy");
            }}
          />
          {isFounder && (
            <Trash2
              className={styles["delete-btn"]}
              onClick={() => {
                setOpen(true);
              }}
            />
          )}
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
          <p>Сигурен ли си, че искаш да изтриеш аптеката?</p>
          <Button
            $danger
            onClick={() => {
              handleDelete(pharmacyVM.id!);
              toast.success("Successfully deleted");
            }}
          >
            Confirm
          </Button>
        </div>
      </Modal>
    </Card>
  );
};

export default PharmacyCard;
