import { Button } from "@/components/styled/Button";
import StatusCode from "@/components/StatusCode";

import styles from "@/styles/pages/AuthErrorPage.module.css";
import { useNavigate } from "react-router-dom";

const AuthErrorPage = ({
  statusCode,
  message,
  handleOnLogin,
  handleOnBack,
}: {
  statusCode: number;
  message: string;
  handleOnLogin: (() => void) | null;
  handleOnBack: boolean;
}) => {
  const navigate = useNavigate();

  return (
    <main className={styles["container"]}>
      <div className={styles["wrapper"]}>
        <StatusCode statusCode={statusCode} message={message} />
        <div className={styles["buttons-wrapper"]}>
          {handleOnLogin !== null && (
            <Button $primary $px={4} onClick={handleOnLogin}>
              Вход
            </Button>
          )}

          {handleOnBack === true && (
            <Button $px={3} onClick={() => navigate(-1)}>
              Назад
            </Button>
          )}
        </div>
      </div>
    </main>
  );
};

export default AuthErrorPage;
