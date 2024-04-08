import { Form, Formik } from "formik";
import * as Yup from "yup";

import { useNavigate } from "react-router-dom";
import { toast } from "sonner";

import { Button } from "@/components/styled/Button";
import FormField from "@/components/FormField";

import styles from "@/styles/pages/SignInPage.module.css";
import authService from "@/services/auth-service";
import { ResponseData } from "@/services/web-api-service";
import storageService from "@/services/storage-service";

const schema = Yup.object().shape({
  email: Yup.string().required("*"),
  password: Yup.string().required("*"),
});

const SignInPage = () => {
  const navigate = useNavigate();

  return (
    <div className={styles["container"]}>
      <h2>Вход</h2>
      <Formik
        initialValues={{
          email: "",
          password: "",
        }}
        validationSchema={schema}
        onSubmit={async (userData) => {
          await authService
            .makeLoginRequest(userData.email, userData.password)
            .then(function (response) {
              const responseData: ResponseData =
                response.data as unknown as ResponseData;

              storageService.saveAccessToken(responseData.accessToken);
              storageService.saveRefreshToken(responseData.refreshToken);
              storageService.saveTokenExpiresDate(responseData.expiration);

              toast.success("Успешен вход в системата!");
              navigate("/home");
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
            <FormField
              label="Имейл"
              name="email"
              placeholder="john@doe.com ..."
              type="text"
              error={errors.email && touched.email ? true : false}
              errorMessage={errors.email}
            />
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
                Вход
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
          Нямаш профил?{" "}
          <span onClick={() => navigate("/sign-up")}>Регистрация</span>
        </p>
      </footer>
    </div>
  );
};

export default SignInPage;
