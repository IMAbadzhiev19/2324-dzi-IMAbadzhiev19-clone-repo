import { MedicineInvoiceModel, MedicineVM, PharmacyVM, UserVM } from "@/api";
import { useEffect, useState } from "react";

import styles from "@/styles/components/pharmacy/MedicineSellSection.module.css";
import { ChevronLeft, ChevronRight } from "lucide-react";
import { Button } from "../styled/Button";
import { toast } from "sonner";
import invoiceService from "@/services/invoice-service";
import { useNavigate, useParams } from "react-router-dom";
import userService from "@/services/user-service";
import { AxiosResponse } from "axios";
import pharmacyService from "@/services/pharmacy-service";

const MedicineSellSection = ({
  medicineVMs,
}: {
  medicineVMs: MedicineVM[];
}) => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [finalData, setFinalData] = useState<MedicineInvoiceModel[]>([]);

  const [page, setPage] = useState<number>(0);
  const [tempQuantity, setTempQuantity] = useState<number | undefined>(
    undefined
  );

  async function handleSell(): Promise<void> {
    if (tempQuantity === undefined) {
      toast.warning("Полето е празно");
      return;
    }

    if (!Number.isInteger(tempQuantity)) {
      toast.warning("Само цели числа се приемат");
      return;
    }

    if (tempQuantity === 0) {
      toast.warning("Стойността трябва да е по-голяма от 0");
      return;
    }

    if (tempQuantity > medicineVMs[page].quantity!) {
      toast.warning(
        "Стойността не може да е по-голяма от броя в склада"
      );
      return;
    }

    const updatedFinalData = [...finalData];
    if (updatedFinalData.length - 1 >= page) {
      updatedFinalData[page].quantity = tempQuantity;
    } else {
      const medicineIM: MedicineInvoiceModel = {
        id: medicineVMs[page].id!,
        quantity: tempQuantity,
        name: medicineVMs[page].basicMedicine?.name,
        price: medicineVMs[page].price! * tempQuantity,
      };
      updatedFinalData.push(medicineIM);
    }

    const totalPrice = updatedFinalData.reduce((total, medicine) => {
      return (
        total +
        medicineVMs.filter((m) => m.id === medicine.id)[0].price! *
          medicine.quantity!
      );
    }, 0);

    try {
      const currentUser =
        (await userService.makeUserCurrentUserRequest()) as AxiosResponse<UserVM>;
      const pharmacy = (await pharmacyService.makePharmacyGetByIdRequest(
        id!
      )) as AxiosResponse<PharmacyVM>;

      await invoiceService
        .makeInvoiceGenerateRequest(
          totalPrice,
          currentUser.data.id!,
          pharmacy.data.depot?.id as string,
          pharmacy.data.id!,
          updatedFinalData
        )
        .then((response) => {
          localStorage.setItem(
            "_tempInvoice",
            response.data as unknown as string
          );
          toast.success("Успешно направена продажба");
          navigate("/invoice");
        })
        .catch((error) => {
          const message =
            error.response.data.message ||
            error.response.data.errors[
              Object.keys(error.response?.data.errors)[0]
            ];
          toast.error(`Излезе грешка: ${message}`);
        });
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    setTempQuantity(
      finalData.length - 1 >= page ? finalData[page].quantity : undefined
    );
  }, [page, finalData]);

  return (
    <div className={styles["wrapper"]}>
      <div className={styles["carousel-wrapper"]}>
        <ChevronLeft
          className={page === 0 ? styles["end"] : styles["icon"]}
          onClick={() => {
            setPage(page === 0 ? page : page - 1);
          }}
        />

        <div className={styles["container"]}>
          <h2>{medicineVMs[page].basicMedicine?.name}</h2>
          <input
            type="number"
            placeholder="Брой"
            value={tempQuantity !== undefined ? tempQuantity : ""}
            onChange={(e) => setTempQuantity(Number(e.target.value))}
          />
        </div>

        <ChevronRight
          className={
            page === medicineVMs.length - 1 ? styles["end"] : styles["icon"]
          }
          onClick={() => {
            if (tempQuantity === undefined) {
              toast.warning("Полето е празно");
              return;
            }

            if (!Number.isInteger(tempQuantity)) {
              toast.warning("Само цели числа се приемат");
              return;
            }

            if (tempQuantity === 0) {
              toast.warning("Стойността трябва да е по-голяма от 0");
              return;
            }

            if (tempQuantity > medicineVMs[page].quantity!) {
              toast.warning(
                "Стойността не може да е по-голяма от броя в склада"
              );
              return;
            }

            if (finalData.length - 1 >= page) {
              finalData[page].quantity = tempQuantity;
            } else {
              const medicineIM: MedicineInvoiceModel = {
                id: medicineVMs[page].id!,
                quantity: tempQuantity,
                name: medicineVMs[page].basicMedicine?.name,
                price: medicineVMs[page].price! * tempQuantity,
              };

              setFinalData([...finalData, medicineIM]);
            }

            setPage(page === medicineVMs.length - 1 ? page : page + 1);
          }}
        />
      </div>
      {page === medicineVMs.length - 1 && (
        <Button
          $primary
          $animation
          onClick={handleSell}
          className={styles["btn"]}
        >
          Продай
        </Button>
      )}
    </div>
  );
};

export default MedicineSellSection;
