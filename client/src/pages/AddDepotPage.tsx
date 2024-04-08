import { useNavigate } from "react-router-dom";

import * as Yup from "yup";

import authService from "@/services/auth-service";
import storageService from "@/services/storage-service";
import { ResponseData } from "@/services/web-api-service";

import styles from "@/styles/pages/AddDepotPage.module.css";
import { Form, Formik } from "formik";
import depotService from "@/services/depot-service";
import { toast } from "sonner";
import FormField from "@/components/FormField";
import { Button } from "@/components/styled/Button";

const schema = Yup.object().shape({
  name: Yup.string().min(2, " Too short").max(15, " Too long").required("*"),
  address_number: Yup.number().typeError(" Invalid"),
  address_street: Yup.string().min(1, " Too short").required("*"),
  address_city: Yup.string().min(1, " Too short").required("*"),
  address_country: Yup.string().min(1, " Too short").required("*"),
});

const AddDepotPage = () => {
  const navigate = useNavigate();

  async function refreshTokens(): Promise<void> {
    try {
      const refreshToken = storageService.retrieveRefreshToken();
      if (!refreshToken) {
        console.log("Refresh token-а липсва. Моля влезте в системата наново!");
        storageService.deleteUserData();
        return;
      }

      const response = await authService.makeRenewTokensRequest(
        storageService.retrieveAccessToken(),
        storageService.retrieveRefreshToken()
      );

      const responseData = response.data as unknown as ResponseData;
      storageService.saveAccessToken(responseData.accessToken);
      storageService.saveRefreshToken(responseData.refreshToken);
      storageService.saveTokenExpiresDate(responseData.expiration);
    } catch (error) {
      console.error("Грешка при подновяването на access token-а: ", error);
      storageService.deleteUserData();
    }
  }

  return (
    <div className={styles["container"]}>
      <h2>Добави склад</h2>
      <Formik
        initialValues={{
          name: "",
          address_number: 0,
          address_street: "",
          address_city: "",
          address_country: "",
        }}
        validationSchema={schema}
        onSubmit={async (depotData) => {
          await depotService
            .makeDepotCreateRequest(
              depotData.name,
              depotData.address_number,
              depotData.address_street,
              depotData.address_city,
              depotData.address_country
            )
            .then(async function () {
              toast.success("Успешно създаден нов склад");
              await refreshTokens();
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
              label="Име"
              name="name"
              placeholder="Име ..."
              type="text"
              error={errors.name && touched.name ? true : false}
              errorMessage={errors.name}
            />
            <div className={styles["field-group"]}>
              <FormField
                label="Улица"
                name="address_street"
                placeholder="Улица ..."
                type="text"
                error={
                  errors.address_street && touched.address_street ? true : false
                }
                errorMessage={errors.address_street}
              />
              <FormField
                label="№"
                name="address_number"
                placeholder="№ ..."
                type="text"
                error={
                  errors.address_number && touched.address_number ? true : false
                }
                errorMessage={errors.address_number}
              />
            </div>
            <div className={styles["field-group"]}>
              <FormField
                label="Град"
                name="address_city"
                placeholder="Град ..."
                type="text"
                error={
                  errors.address_city && touched.address_city ? true : false
                }
                errorMessage={errors.address_city}
              />
              <FormField
                label="Държава"
                name="address_country"
                placeholder="Държава ..."
                type="text"
                error={
                  errors.address_country && touched.address_country
                    ? true
                    : false
                }
                errorMessage={errors.address_country}
              />
            </div>
            <div className={styles["buttons"]}>
              <Button $primary $py={1} type="submit">
                Добави
              </Button>
              <Button onClick={() => navigate(-1)} type="button">
                Назад
              </Button>
            </div>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default AddDepotPage;
