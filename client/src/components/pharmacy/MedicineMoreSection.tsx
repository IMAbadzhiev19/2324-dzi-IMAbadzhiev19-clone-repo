import { useState } from "react";

import { MedicineVM } from "@/api";

import { Button } from "../styled/Button";
import { toast } from "sonner";
import medicineService from "@/services/medicine-service";

import styles from "@/styles/components/pharmacy/MedicineMoreSection.module.css";

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
        <div className={styles["description"]}>
          <p>{medicineVM.basicMedicine?.description}</p>
        </div>
        <Button
          $primary
          $animation
          $px={5}
          onClick={async () => {
            if (selectedImage === undefined) {
              toast.warning("Select an image first ;)");
            }

            await medicineService
              .makeMedicineUpdateRequest(
                medicineVM.id!,
                undefined,
                undefined,
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
          Промени
        </Button>
      </main>
    </div>
  );
};

export default MedicineMoreSection;
