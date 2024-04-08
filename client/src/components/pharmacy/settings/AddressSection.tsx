import { Form, Formik } from "formik";
import * as Yup from "yup";

import FormField from "@/components/FormField";

import styles from "@/styles/components/pharmacy/settings/AddressSection.module.css";
import { PharmacyVM } from "@/api";
import { Button } from "@/components/styled/Button";
import { toast } from "sonner";
import pharmacyService from "@/services/pharmacy-service";
import { useNavigate, useParams } from "react-router-dom";

const schema = Yup.object().shape({
  address_number: Yup.number().typeError(" Невалидно"),
  address_street: Yup.string().min(2, " Твърде късо").required("*"),
  address_city: Yup.string().min(2, " Твърде късо").required("*"),
  address_country: Yup.string().min(2, " Твърде късо").required("*"),
});

const AddressSection = ({ pharmacy }: { pharmacy: PharmacyVM }) => {
  const { id } = useParams();
  const navigate = useNavigate();

  return (
    <div>
      <Formik
        initialValues={{
          address_number: pharmacy?.address?.number,
          address_street: pharmacy?.address?.street,
          address_city: pharmacy?.address?.city,
          address_country: pharmacy?.address?.country,
        }}
        validationSchema={schema}
        onSubmit={async (values) => {
          if (
            values.address_number === pharmacy.address?.number &&
            values.address_city === pharmacy.address?.city &&
            values.address_street === pharmacy.address?.street &&
            values.address_country === pharmacy.address?.country
          ) {
            return;
          }

          await pharmacyService
            .makePharmacyUpdateRequest(
              pharmacy!.id!,
              null,
              null,
              values.address_number!,
              values.address_street!,
              values.address_city!,
              values.address_country!,
              null,
              null
            )
            .then(function () {
              toast.success("Успешна промяна");
              navigate(`/pharmacies/${id}/dashboard`);
            })
            .catch(function (error) {
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
              <Button $primary $animation type="submit">
                Промени
              </Button>
            </div>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default AddressSection;
