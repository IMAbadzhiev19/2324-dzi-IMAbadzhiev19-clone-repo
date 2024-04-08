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

import styles from "@/styles/pages/AddMedicinePage.module.css";
import { BasicMedicineVM, BuildingType } from "@/api";
import medicineService from "@/services/medicine-service";
import { AxiosResponse } from "axios";
import { Button } from "@/components/styled/Button";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";

const schema = Yup.object().shape({
  price: Yup.number()
    .typeError(" Invalid")
    .min(0.1, " Too small")
    .required("*"),
  quantity: Yup.number()
    .integer("Whole values only!")
    .typeError(" Invalid")
    .min(0.1, " Too small")
    .required("*"),
});

const AddMedicinePage = () => {
  const navigate = useNavigate();

  const [choice, setChoice] = useState<string>("");
  const [expirationDate, setExpirationDate] = useState<string>("");

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
      <h2>Добави лекарство</h2>
      <div className={styles["field-group"]}>
        <FormControl
          sx={{
            width: "250px",
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
        <input
          type="date"
          className={styles["date-input"]}
          onChange={(e) => setExpirationDate(e.target.value)}
        />
      </div>
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
          } else if (expirationDate == "") {
            toast.warning("Трябва да избереш срок на годност");
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
              expirationDate,
              localStorage.getItem("_buildingId")!,
              BuildingType.NUMBER_1
            )
            .then(async function () {
              localStorage.removeItem("_buildingId");
              toast.success("Успешно създаване на лекарство");
              navigate(-1);
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
                label="Price"
                name="price"
                type="text"
                error={errors.price && touched.price ? true : false}
                errorMessage={errors.price}
              />
              <FormField
                label="Quantity"
                name="quantity"
                type="text"
                error={errors.quantity && touched.quantity ? true : false}
                errorMessage={errors.quantity}
              />
            </div>
            <div className={styles["buttons"]}>
              <Button $primary $py={1} type="submit">
                Добави
              </Button>
              <Button onClick={() => navigate(-1)} type="button">
                Назад
              </Button>
            </div>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default AddMedicinePage;
