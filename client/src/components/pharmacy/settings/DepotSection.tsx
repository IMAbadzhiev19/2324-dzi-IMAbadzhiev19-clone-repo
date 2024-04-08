import { DepotVM, PharmacyVM } from "@/api";
import { Button } from "@/components/styled/Button";
import depotService from "@/services/depot-service";
import pharmacyService from "@/services/pharmacy-service";
import styles from "@/styles/components/pharmacy/settings/DepotSection.module.css";
import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "sonner";

const DepotSection = ({ pharmacy }: { pharmacy: PharmacyVM }) => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [choice, setChoice] = useState<string>("");
  const [depots, setDepots] = useState<DepotVM[]>([]);

  function handleChange(event: SelectChangeEvent): void {
    setChoice(event.target.value as string);
  }

  useEffect(() => {
    try {
      (async () => {
        const response =
          (await depotService.makeDepotGetAllRequest()) as AxiosResponse<
            DepotVM[]
          >;
        setDepots(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }, []);

  return (
    <div className={styles["wrapper"]}>
      <div>
        <div className={styles["container"]}>
          <h3>
            <span>Настоящ склад: </span>
            <span>
              {pharmacy.depot !== null ? pharmacy.depot?.name : "Not found"}
            </span>
          </h3>
          <FormControl
            sx={{
              width: "400px",
            }}
          >
            <InputLabel sx={{ color: "#93c5fd" }} id="choice-label">
              Име
            </InputLabel>
            <Select
              labelId="choice-label"
              value={choice}
              label="Име"
              onChange={handleChange}
            >
              {depots
                .filter((d) => d.id !== pharmacy.depot?.id)
                .map((depot) => (
                  <MenuItem key={depot.id} value={depot.id!}>
                    {depot.name}
                  </MenuItem>
                ))}
            </Select>
          </FormControl>
          <Button
            $primary
            $animation
            onClick={async () => {
              const depotId = choice;

              await pharmacyService
                .makePharmacyRequestDepotAssignRequest(id!, depotId)
                .then(function () {
                  toast.success("Успешно подадена заяка към склада");
                  navigate(`/pharmacies/${id}/dashboard`);
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
            Заяви
          </Button>
        </div>
      </div>
      <footer className={styles["depot-section-footer"]}>
        <img src="/DepotIcon.png" className={styles["img"]}></img>
        <img src="/DepotIcon1.png" className={styles["img"]}></img>
      </footer>
    </div>
  );
};

export default DepotSection;
