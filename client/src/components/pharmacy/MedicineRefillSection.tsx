import * as Yup from "yup";

import { Form, Formik } from "formik";

import FormField from "@/components/FormField";

import { MedicineVM } from "@/api";

import styles from "@/styles/components/pharmacy/MedicineRefillSection.module.css";
import { Button } from "../styled/Button";
import medicineService from "@/services/medicine-service";
import { toast } from "sonner";

const schema = Yup.object().shape({
  quantity: Yup.number()
    .integer("Само цели числа!")
    .typeError(" Невалидно")
    .min(0.1, " Твърде малко")
    .required("*"),
});

const MedicineRefillSection = ({
  pharmacyId,
  medicineVM,
  setChanged,
}: {
  pharmacyId: string;
  medicineVM: MedicineVM;
  setChanged: (value: boolean) => void;
}) => {
  return (
    <div className={styles["container"]}>
      <h2>Презареждане</h2>
      <Formik
        initialValues={{
          quantity: 0,
        }}
        validationSchema={schema}
        onSubmit={async (medicineData) => {
          await medicineService
            .makeMedicineRefillRequest(
              medicineVM.id!,
              pharmacyId,
              medicineData.quantity!
            )
            .then(async function () {
              setChanged(true);
              toast.success("Успешно презареждане");
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
                label="Брой"
                name="quantity"
                type="text"
                error={errors.quantity && touched.quantity ? true : false}
                errorMessage={errors.quantity}
              />
            </div>
            <Button $primary $py={1} type="submit">
              Презареди
            </Button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default MedicineRefillSection;
