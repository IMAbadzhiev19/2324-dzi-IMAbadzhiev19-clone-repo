import { Form, Formik } from "formik";
import * as Yup from "yup";

import { toast } from "sonner";

import { useNavigate } from "react-router-dom";

import FormField from "@/components/FormField";
import { Button } from "@/components/styled/Button";

import styles from "@/styles/pages/SignUpPage.module.css";
import authService from "@/services/auth-service";

const schema = Yup.object().shape({
  first_name: Yup.string().max(256, " Твърде дълго").required("*"),
  last_name: Yup.string().max(256, " Твърде дълго").required("*"),
  phone: Yup.string().required("*"),
  email: Yup.string().required("*"),
  password: Yup.string()
    .min(7, "Поне 7 символа")
    .matches(/(?=.*\d)/, "Поне 1 цифра")
    .matches(/(?=.*[a-z])/, "Поне 1 малка буква")
    .matches(/(?=.*[A-Z])/, "Поне 1 главна буква")
    .matches(/(?=.*\W)/, "Поне 1 специален символ")
    .required("*"),
});

const SignUpPage = () => {
  const navigate = useNavigate();

  return (
    <div className={styles["container"]}>
      <h2>Регистрация</h2>
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
            .then(() => {
              toast.success("Успешна регистрация!");
              navigate("/sign-in");
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
                label="Име"
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
            <div className={styles["buttons"]}>
              <Button $primary $py={1} type="submit">
                Регистрация
              </Button>
              <Button onClick={() => navigate(-1)} type="button">
                Назад
              </Button>
            </div>
          </Form>
        )}
      </Formik>
      <footer>
        <p>
          Вече имаш профил?{" "}
          <span onClick={() => navigate("/sign-in")}>Вход</span>
        </p>
      </footer>
    </div>
  );
};

export default SignUpPage;
