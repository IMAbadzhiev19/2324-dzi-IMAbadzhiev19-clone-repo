import * as Yup from "yup";

import { Form, Formik } from "formik";

import { useState } from "react";

import FormField from "@/components/FormField";

import { MedicineVM } from "@/api";

import styles from "@/styles/components/depot/MedicineMoreSection.module.css";
import { Button } from "../styled/Button";
import { toast } from "sonner";
import medicineService from "@/services/medicine-service";

const schema = Yup.object().shape({
  price: Yup.number()
    .typeError(" Невалидно")
    .min(0.1, " Твърде малко")
    .required("*"),
  quantity: Yup.number()
    .integer("Само цели стойности!")
    .typeError(" Невалидно")
    .min(0.1, " Твърде малко")
    .required("*"),
});

const MedicineMoreSection = ({
  medicineVM,
  setChanged,
}: {
  medicineVM: MedicineVM;
  setChanged: (value: boolean) => void;
}) => {
  const [selectedImage, setSelectedImage] = useState<Blob | undefined>(
    undefined
  );
  const [previewImage, setPreviewImage] = useState<string | undefined>(
    undefined
  );

  function handleImageChange(event: React.ChangeEvent<HTMLInputElement>) {
    const file = event.target.files && event.target.files[0];
    if (file) {
      setSelectedImage(file);
      const imageUrl = URL.createObjectURL(file);
      setPreviewImage(imageUrl);
    }
  }

  return (
    <div className={styles["container"]}>
      <header>
        <h2>{medicineVM.basicMedicine?.name}</h2>
        <label htmlFor="avatar-input">
          <img
            src={
              previewImage
                ? previewImage
                : medicineVM.imageUrl
                ? medicineVM.imageUrl
                : "/../NotFound.jpg"
              // : `/${medicineVM.basicMedicine?.name}.jpg`
            }
            alt={"Medicine Image"}
            className={styles["medicine-img"]}
          />
          <input
            id="avatar-input"
            type="file"
            accept="image/*"
            hidden
            onChange={handleImageChange}
          />
        </label>
      </header>
      <main>
        <Formik
          initialValues={{
            price: medicineVM.price,
            quantity: medicineVM.quantity,
          }}
          validationSchema={schema}
          onSubmit={async (medicineData) => {
            await medicineService
              .makeMedicineUpdateRequest(
                medicineVM.id!,
                medicineData.price,
                medicineData.quantity,
                selectedImage
              )
              .then(async function () {
                setChanged(true);
                toast.success("Успешна промяна");
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
                Промени
              </Button>
            </Form>
          )}
        </Formik>
      </main>
    </div>
  );
};

export default MedicineMoreSection;
