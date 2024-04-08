import { Form, Formik } from "formik";
import * as Yup from "yup";

import FormField from "@/components/FormField";

import styles from "@/styles/components/pharmacy/settings/AssignEmployeeSection.module.css";
import { Button } from "@/components/styled/Button";
import { toast } from "sonner";
import authService from "@/services/auth-service";
import pharmacyService from "@/services/pharmacy-service";

const schema = Yup.object().shape({
  first_name: Yup.string().max(256, " Твърде дълго").required("*"),
  last_name: Yup.string().max(256, " Твърде дълго").required("*"),
  phone: Yup.string().required("*"),
  email: Yup.string().required("*"),
  password: Yup.string()
    .min(7, "Поне 7 символа")
    .matches(/(?=.*\d)/, "Поне една цифра")
    .matches(/(?=.*[a-z])/, "Поне една малка буква")
    .matches(/(?=.*[A-Z])/, "Поне една главна буква")
    .matches(/(?=.*\W)/, "Поне един специален символ")
    .required("*"),
});

const AssignEmployeeSection = ({
  pharmacyId,
  setChanged,
}: {
  pharmacyId: string;
  setChanged: (value: boolean) => void;
}) => {
  return (
    <div className={styles["container"]}>
      <h2>Добави работник</h2>
      <Formik
        initialValues={{
          first_name: "",
          last_name: "",
          phone: "",
          email: "",
          password: "",
        }}
        validationSchema={schema}
        onSubmit={async (userData) => {
          await authService
            .makeRegisterRequest(
              userData.first_name,
              userData.last_name,
              userData.email,
              userData.password,
              userData.phone
            )
            .then(async function () {
              toast.warning("Изпращане..");
              await pharmacyService
                .makePharmacyAssignEmployeeRequest(
                  pharmacyId,
                  userData.email,
                  userData.password
                )
                .then(function () {
                  setChanged(true);
                  toast.success("Успешно добавен нов работник");
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
                label="Първо име"
                name="first_name"
                placeholder="John ..."
                type="text"
                error={errors.first_name && touched.first_name ? true : false}
                errorMessage={errors.first_name}
              />
              <FormField
                label="Фамилия"
                name="last_name"
                placeholder="Doe ..."
                type="text"
                error={errors.last_name && touched.last_name ? true : false}
                errorMessage={errors.last_name}
              />
            </div>
            <div className={styles["field-group"]}>
              <FormField
                label="Имейл"
                name="email"
                placeholder="john@doe.com ..."
                type="text"
                error={errors.email && touched.email ? true : false}
                errorMessage={errors.email}
              />
              <FormField
                label="Телефон"
                name="phone"
                placeholder=""
                type="text"
                error={errors.phone && touched.phone ? true : false}
                errorMessage={errors.phone}
              />
            </div>
            <FormField
              label="Парола"
              name="password"
              placeholder=""
              type="password"
              error={errors.password && touched.password ? true : false}
              errorMessage={errors.password}
            />
            <Button $primary $py={1} type="submit" className={styles["btn"]}>
              Добави
            </Button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default AssignEmployeeSection;
