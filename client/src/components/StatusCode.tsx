import styles from "@/styles/components/StatusCode.module.css";

const StatusCode = ({
  statusCode,
  message,
}: {
  statusCode: number;
  message: string;
}) => {
  return (
    <div className={styles["container"]}>
      <div>
        <p>{statusCode}</p>
        <p>{message}</p>
      </div>
    </div>
  );
};

export default StatusCode;