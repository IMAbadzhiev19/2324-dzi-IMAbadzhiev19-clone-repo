import * as Yup from "yup";

import { Form, Formik } from "formik";

import FormField from "@/components/FormField";

import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import { useEffect, useState } from "react";
import medicineService from "@/services/medicine-service";
import { AxiosResponse } from "axios";
import { BasicMedicineVM, BuildingType } from "@/api";

import { Button } from "../styled/Button";
import { toast } from "sonner";

import styles from "@/styles/components/pharmacy/MedicineAddSection.module.css";

const schema = Yup.object().shape({
  price: Yup.number()
    .typeError(" Невалидно")
    .min(0.1, " Твърде малко")
    .required("*"),
  quantity: Yup.number()
    .integer("Само цели числа!")
    .typeError(" Невалидно")
    .min(0.1, " Твърде малко")
    .required("*"),
});

const MedicineAddSection = ({
  id,
  setChanged,
}: {
  id: string;
  setChanged: (value: boolean) => void;
}) => {
  const [choice, setChoice] = useState<string>("");
  const [basicMedicines, setBasicMedicines] = useState<BasicMedicineVM[]>([]);

  function handleChange(event: SelectChangeEvent): void {
    setChoice(event.target.value as string);
  }

  useEffect(() => {
    try {
      (async () => {
        const response =
          (await medicineService.makeBasicMedicinesGetRequest()) as AxiosResponse<
            BasicMedicineVM[]
          >;

        setBasicMedicines(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }, []);

  return (
    <div className={styles["container"]}>
      <h2>Request medicine</h2>
      <FormControl
        sx={{
          width: "420px",
        }}
      >
        <InputLabel sx={{ color: "#FFF" }} id="choice-label">
          Име
        </InputLabel>
        <Select
          sx={{ color: "#FFF" }}
          labelId="choice-label"
          value={choice}
          label="Име"
          onChange={handleChange}
        >
          {basicMedicines.map((bm) => (
            <MenuItem key={bm.id} value={bm.name?.toLocaleLowerCase()}>
              {bm.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>

      <Formik
        initialValues={{
          price: 0,
          quantity: 0,
        }}
        validationSchema={schema}
        onSubmit={async (medicineData) => {
          if (choice === "") {
            toast.warning("Трябва да избереш име");
            return;
          }

          const basicMedicineId = basicMedicines.filter(
            (bm) => bm.name?.toLocaleLowerCase() == choice
          )[0].id;

          await medicineService
            .makeMedicineCreateRequest(
              basicMedicineId!,
              medicineData.price,
              medicineData.quantity,
              null,
              id!,
              BuildingType.NUMBER_0
            )
            .then(async function () {
              setChanged(true);
              toast.success("Успешно добавено лекарство");
              toast.warning("Натисни някъде извън модала за да продължиш");
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
        {({ errors, touched }) => (
          <Form className={styles["form"]}>
            <div className={styles["field-group"]}>
              <FormField
                label="Цена"
                name="price"
                type="text"
                error={errors.price && touched.price ? true : false}
                errorMessage={errors.price}
              />
              <FormField
                label="Брой"
                name="quantity"
                type="text"
                error={errors.quantity && touched.quantity ? true : false}
                errorMessage={errors.quantity}
              />
            </div>
            <Button $primary $py={1} type="submit">
              Заяви
            </Button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default MedicineAddSection;
