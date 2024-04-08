import { Form, Formik } from "formik";
import * as Yup from "yup";

import FormField from "@/components/FormField";
import { Button } from "@/components/styled/Button";

import styles from "@/styles/components/pharmacy/settings/ImporantSection.module.css";
import { PharmacyVM } from "@/api";
import pharmacyService from "@/services/pharmacy-service";
import { toast } from "sonner";
import { useNavigate, useParams } from "react-router-dom";

const schema = Yup.object().shape({
  name: Yup.string().min(2, " Too short").max(15, " Too long").required("*"),
});

const ImportantSection = ({ pharmacy }: { pharmacy: PharmacyVM }) => {
  const { id } = useParams();
  const navigate = useNavigate();

  return (
    <div>
      <Formik
        initialValues={{
          name: pharmacy.name,
        }}
        validationSchema={schema}
        onSubmit={async (pharamcyData) => {
          if (pharamcyData.name === pharmacy.name) {
            return;
          }

          await pharmacyService
            .makePharmacyUpdateRequest(
              pharmacy.id!,
              pharamcyData.name!,
              null,
              pharmacy.address?.number as number,
              pharmacy.address?.street as string,
              pharmacy.address?.city as string,
              pharmacy.address?.country as string,
              null,
              null
            )
            .then(function () {
              toast.success("Успешна промяна");
              navigate(`/pharmacies/${id}/dashboard`);
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
              label="Име на аптеката"
              name="name"
              placeholder=""
              type="text"
              error={errors.name && touched.name ? true : false}
              errorMessage={errors.name}
            />
            <Button $animation $primary type="submit">
              Промени
            </Button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default ImportantSection;
