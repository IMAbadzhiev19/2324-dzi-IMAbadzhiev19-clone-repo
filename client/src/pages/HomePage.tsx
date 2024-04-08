import { useState } from "react";
import { useNavigate } from "react-router-dom";

import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";

import { Button } from "@/components/styled/Button";

import styles from "@/styles/pages/HomePage.module.css";
import { toast } from "sonner";

const HomePage = () => {
  const navigate = useNavigate();
  const [choice, setChoice] = useState<string>("");

  function handleChange(event: SelectChangeEvent): void {
    setChoice(event.target.value as string);
  }

  return (
    <div className={styles["wrapper"]}>
      <div className={styles["select"]}>
        <FormControl fullWidth>
          <InputLabel sx={{ color: "#f4f3ee" }} id="choice-label">
            Избор
          </InputLabel>
          <Select
            sx={{ color: "#f4f3ee" }}
            labelId="choice-label"
            value={choice}
            label="Choice"
            onChange={handleChange}
          >
            <MenuItem value="pharmacies">Аптеки</MenuItem>
            <MenuItem value="depots">Складове</MenuItem>
          </Select>
        </FormControl>
        <Button
          $primary
          $animation
          $line
          $px={4.5}
          onClick={() => {
            choice !== ""
              ? navigate(`/${choice}`)
              : toast.warning("Choose an option first 😜");
          }}
        >
          Продължи
        </Button>
      </div>
    </div>
  );
};

export default HomePage;
