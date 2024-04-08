import { useEffect, useState } from "react";

import styles from "@/styles/pages/InvoicePage.module.css";
import { Button } from "@/components/styled/Button";
import { useNavigate } from "react-router-dom";
import { Modal } from "@mui/material";
import { Input } from "@/components/styled/Input";
import { toast } from "sonner";
import invoiceService from "@/services/invoice-service";

const InvoicePage = () => {
  const navigate = useNavigate();

  const [invoiceBase64, setInvoiceBase64] = useState<string | null>(null);
  const [pdfUrl, setPdfUrl] = useState<string | null>(null);

  const [email, setEmail] = useState<string | null>(null);
  const [fileName, setFileName] = useState<string | null>(null);

  const [open, setOpen] = useState<boolean>(false);

  useEffect(() => {
    (async () => {
      const item = localStorage.getItem("_tempInvoice");
      setInvoiceBase64(item);

      const byteCharacters = atob(item!);
      const byteNumbers = new Array(byteCharacters.length);
      for (let i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] = byteCharacters.charCodeAt(i);
      }
      const byteArray = new Uint8Array(byteNumbers);
      const blob = new Blob([byteArray], { type: "application/pdf" });

      // Create URL for the Blob
      const url = URL.createObjectURL(blob);
      setPdfUrl(url);
    })();
  }, []);

  return (
    <div className={styles["wrapper"]}>
      {invoiceBase64 !== null && pdfUrl !== null ? (
        <>
          <div>
            <div className={styles["buttons"]}>
              <Button
                $animation
                $primary
                $px={3}
                onClick={() => {
                  setOpen(true);
                }}
              >
                Share
              </Button>
              <Button
                $animation
                $px={1}
                onClick={() => {
                  localStorage.removeItem("_tempInvoice");
                  navigate(-1);
                }}
              >
                Back
              </Button>
            </div>
            <iframe src={pdfUrl} className={styles["frame"]} />
          </div>
          <Modal
            open={open}
            onClose={() => {
              setOpen(false);
            }}
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <div className={styles["modal-container"]}>
              <h2>Share Request</h2>
              <div>
                <Input
                  placeholder="Email"
                  onChange={(e) => setEmail(e.target.value)}
                />
                <Input
                  placeholder="Filename"
                  onChange={(e) => setFileName(e.target.value)}
                />
              </div>
              <Button
                $primary
                $animation
                $px={10}
                onClick={async () => {
                  if (fileName?.includes(".")) {
                    toast.warning(
                      "No file extensions allowed. Just the name of the file."
                    );
                    return;
                  }

                  toast.warning("Sending...");

                  await invoiceService
                    .makeInvoiceShareRequest(email!, invoiceBase64, fileName!)
                    .then(function () {
                      toast.success("Successfully shared the invoice");
                      toast.warning(
                        "Click anywhere outside the modal to continue"
                      );
                    })
                    .catch((error) => {
                      const message =
                        error.response.data.message ||
                        error.response.data.errors[
                          Object.keys(error.response?.data.errors)[0]
                        ];
                      toast.error(`Error occurred: ${message}`);
                    });
                }}
              >
                Share
              </Button>
            </div>
          </Modal>
        </>
      ) : (
        <p className={styles["warning"]}>No invoice found</p>
      )}
    </div>
  );
};

export default InvoicePage;
