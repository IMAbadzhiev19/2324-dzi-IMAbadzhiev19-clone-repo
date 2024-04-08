import * as Yup from "yup";
import { Formik, Form } from "formik";
import { Button } from "@/components/styled/Button";
import FormField from "@/components/FormField";

import styles from "@/styles/pages/ProfilePage.module.css";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { CircularProgress, Tab, Tabs } from "@mui/material";
import { CustomTabPanel, allyProps } from "@/components/TabPanel";
import { toast } from "sonner";
import userService from "@/services/user-service";
import { AxiosResponse } from "axios";
import { UserVM } from "@/api";

const first_tab_schema = Yup.object().shape({
  first_name: Yup.string().max(256, " Too long").required("*"),
  last_name: Yup.string().max(256, " Too long").required("*"),
  phone: Yup.string().required("*"),
  email: Yup.string().required("*"),
});

const second_tab_schema = Yup.object().shape({
  old_password: Yup.string().required("*"),
  new_password: Yup.string()
    .min(7, "Поне 7 символа")
    .matches(/(?=.*\d)/, "Поне 1 цифра")
    .matches(/(?=.*[a-z])/, "Поне една малка буква")
    .matches(/(?=.*[A-Z])/, "Поне една главна буква")
    .matches(/(?=.*\W)/, "Поне един специален символ")
    .required("*"),
  confirm_password: Yup.string().required("*"),
});

const ProfilePage = () => {
  const navigate = useNavigate();

  const [value, setValue] = useState<number>(0);
  const [currentUser, setCurrentUser] = useState<UserVM | null>(null);

  function handleTabChange(event: React.SyntheticEvent, newValue: number) {
    setValue(newValue);
  }

  useEffect(() => {
    try {
      (async () => {
        const response =
          (await userService.makeUserCurrentUserRequest()) as AxiosResponse<UserVM>;
        setCurrentUser(response.data);
      })();
    } catch (error) {
      console.log(error);
    }
  }, []);

  return (
    <main className={styles["wrapper"]}>
      {currentUser !== null ? (
        <div className={styles["container"]}>
          <header>
            <p>Профил</p>
          </header>
          <div className={styles["content"]}>
            <Tabs value={value} onChange={handleTabChange}>
              <Tab label="Информация" {...allyProps(0)} />
              <Tab label="Парола" {...allyProps(1)} />
            </Tabs>
          </div>
          <div>
            <CustomTabPanel value={value} index={0}>
              <Formik
                initialValues={{
                  first_name: currentUser.firstName,
                  last_name: currentUser.lastName,
                  phone: currentUser.phoneNumber,
                  email: currentUser.email,
                }}
                validationSchema={first_tab_schema}
                onSubmit={async (userData) => {
                  await userService
                    .makeUserUpdateRequest(
                      userData.first_name!,
                      userData.last_name!,
                      userData.email!,
                      userData.phone!
                    )
                    .then(function () {
                      toast.success("Успешна промяна!");
                      navigate(-1);
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
                        type="text"
                        error={
                          errors.first_name && touched.first_name ? true : false
                        }
                        errorMessage={errors.first_name}
                      />
                      <FormField
                        label="Фамилия"
                        name="last_name"
                        type="text"
                        error={
                          errors.last_name && touched.last_name ? true : false
                        }
                        errorMessage={errors.last_name}
                      />
                    </div>
                    <FormField
                      label="Имейл"
                      name="email"
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
                    <footer>
                      <Button $primary type="submit">
                        Запази
                      </Button>
                      <Button
                        $px={2}
                        type="button"
                        onClick={() => navigate(-1)}
                      >
                        Назад
                      </Button>
                    </footer>
                  </Form>
                )}
              </Formik>
            </CustomTabPanel>
            <CustomTabPanel value={value} index={1}>
              <Formik
                initialValues={{
                  old_password: "",
                  new_password: "",
                  confirm_password: "",
                }}
                validationSchema={second_tab_schema}
                onSubmit={async (userData) => {
                  if (userData.new_password !== userData.confirm_password) {
                    toast.error("Паролите не съвпадат!");
                    return;
                  }

                  await userService
                    .makeChangePasswordRequest(
                      userData.old_password,
                      userData.new_password
                    )
                    .then(function () {
                      toast.success("Успешна промяна на парола");
                      navigate(-1);
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
                      label="Стара парола"
                      name="old_password"
                      placeholder=""
                      type="password"
                      error={
                        errors.old_password && touched.old_password
                          ? true
                          : false
                      }
                      errorMessage={errors.old_password}
                    />
                    <FormField
                      label="Нова парола"
                      name="new_password"
                      placeholder=""
                      type="password"
                      error={
                        errors.new_password && touched.new_password
                          ? true
                          : false
                      }
                      errorMessage={errors.new_password}
                    />
                    <FormField
                      label="Потвърди новата паролата"
                      name="confirm_password"
                      placeholder=""
                      type="password"
                      error={
                        errors.confirm_password && touched.confirm_password
                          ? true
                          : false
                      }
                      errorMessage={errors.confirm_password}
                    />
                    <footer>
                      <Button $primary type="submit">
                        Запази
                      </Button>
                      <Button
                        $px={2}
                        type="button"
                        onClick={() => navigate(-1)}
                      >
                        Назад
                      </Button>
                    </footer>
                  </Form>
                )}
              </Formik>
            </CustomTabPanel>
          </div>
        </div>
      ) : (
        <CircularProgress />
      )}
    </main>
  );
};

export default ProfilePage;
