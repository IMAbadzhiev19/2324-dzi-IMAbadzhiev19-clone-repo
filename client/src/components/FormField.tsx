import { Field } from "formik";

import styles from "@/styles/components/FormField.module.css";

const FormField = ({
  label,
  name,
  placeholder,
  type,
  error,
  errorMessage,
}: {
  label?: string;
  name?: string;
  placeholder?: string;
  type?: string;
  error?: boolean;
  errorMessage?: string;
  defaultValue?: string;
  setChanged?: (value: boolean) => void;
}) => {
  return (
    <div className={styles["wrapper"]}>
      <label className={error ? styles["error"] : ""}>
        {label} <span>{error ? `${errorMessage}` : ""}</span>
      </label>
      <Field name={name} placeholder={placeholder} type={type} />
    </div>
  );
};

export default FormField;
