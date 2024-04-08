import { DepotVM } from "@/api";
import depotService from "@/services/depot-service";
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import { Button } from "@/components/styled/Button";
import DepotCard from "@/components/DepotCard";
import { Pagination } from "@mui/material";

import styles from "@/styles/pages/DepotsPage.module.css";

const DepotsPage = () => {
  const navigate = useNavigate();

  const [depots, setDepots] = useState<DepotVM[]>([]);

  async function refreshDepots(): Promise<void> {
    try {
      (async () => {
        const response =
          (await depotService.makeDepotGetByUserRequest()) as AxiosResponse<
            DepotVM[]
          >;
        setDepots(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    refreshDepots();
  }, []);

  const [page, setPage] = useState<number>(1);
  const [itemsPerPage, setItemsPerPage] = useState<number>(
    window.innerWidth <= 768 ? 1 : 3
  );

  const start = (page - 1) * itemsPerPage;
  const end = start + itemsPerPage;

  function handleClick(id: string): void {
    navigate(`/depots/${id}/dashboard`);
  }

  async function handleDelete(id: string): Promise<void> {
    await depotService.makeDepotDeleteRequest(id);
    await refreshDepots();
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
        <Button
          $primary
          $animation
          $px={3}
          onClick={() => {
            navigate("/depots/add");
          }}
        >
          Добави склад
        </Button>
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
        {depots.length > 0 ? (
          depots
            .slice(start, end)
            .map(
              (depot) =>
                depot && (
                  <DepotCard
                    key={depot.id}
                    depotVM={depot}
                    handleClick={handleClick}
                    handleDelete={handleDelete}
                  />
                )
            )
        ) : (
          <div className={styles["empty-list"]}>Няма налични складове !</div>
        )}
      </main>
      <footer>
        <Pagination
          shape="rounded"
          defaultPage={1}
          page={page}
          count={Math.ceil(depots.length / itemsPerPage)}
          color="primary"
          onChange={(e, p) => setPage(p)}
        />
      </footer>
    </div>
  );
};

export default DepotsPage;
