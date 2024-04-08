import { PharmacyVM } from "@/api";
import { Button } from "@/components/styled/Button";
import pharmacyService from "@/services/pharmacy-service";
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import styles from "@/styles/pages/PharmaciesPage.module.css";
import PharmacyCard from "@/components/PharmacyCard";
import { Pagination } from "@mui/material";
import useAuth from "@/hooks/useAuth";

const PharmaciesPage = () => {
  const navigate = useNavigate();
  const { isPharmacist } = useAuth();

  const [pharmacies, setPharmacies] = useState<PharmacyVM[]>([]);

  async function refreshPharmacies(): Promise<void> {
    try {
      (async () => {
        const response =
          (await pharmacyService.makePharmacyGetPharmaciesRequest()) as AxiosResponse<
            PharmacyVM[]
          >;
        setPharmacies(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    refreshPharmacies();
  }, []);

  const [page, setPage] = useState<number>(1);
  const [itemsPerPage, setItemsPerPage] = useState<number>(
    window.innerWidth <= 768 ? 1 : 3
  );

  const start = (page - 1) * itemsPerPage;
  const end = Math.min(start + itemsPerPage, pharmacies.length);

  function handleClick(id: string): void {
    navigate(`/pharmacies/${id}/dashboard`);
  }

  async function handleDelete(id: string): Promise<void> {
    await pharmacyService.makePharmacyDeleteRequest(id);
    await refreshPharmacies();
  }

  useEffect(() => {
    const handleResize = () => {
      const innerWidth = window.innerWidth;
      setItemsPerPage(innerWidth > 768 ? 3 : 1);
      setPage(1);
    };

    window.addEventListener("resize", handleResize);

    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  return (
    <div className={styles["wrapper"]}>
      <header>
        {!isPharmacist && (
          <Button
            $primary
            $animation
            $px={3}
            onClick={() => {
              navigate("/pharmacies/add");
            }}
          >
            Добави аптека
          </Button>
        )}
        <Button
          $animation
          $px={2}
          onClick={() => {
            navigate("/home");
          }}
        >
          Назад
        </Button>
      </header>
      <main>
        {pharmacies.length > 0 ? (
          pharmacies
            .slice(start, end)
            .map(
              (pharmacy) =>
                pharmacy && (
                  <PharmacyCard
                    key={pharmacy.id}
                    pharmacyVM={pharmacy}
                    handleClick={handleClick}
                    handleDelete={handleDelete}
                  />
                )
            )
        ) : (
          <div className={styles["empty-list"]}>
            Няма налични аптеки !
          </div>
        )}
      </main>
      <footer>
        <Pagination
          shape="rounded"
          defaultPage={1}
          color="primary"
          page={page}
          count={Math.ceil(pharmacies.length / itemsPerPage)}
          onChange={(e, p) => setPage(p)}
        />
      </footer>
    </div>
  );
};

export default PharmaciesPage;
