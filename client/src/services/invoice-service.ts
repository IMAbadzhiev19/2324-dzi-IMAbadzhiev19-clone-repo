import { InvoiceApi, InvoiceIM, MedicineInvoiceModel, ShareIM } from "@/api";
import { WebApiService } from "./web-api-service";
import { AxiosResponse } from "axios";

export class InvoiceService extends WebApiService {
  invoiceApi: InvoiceApi;

  constructor() {
    super();
    this.invoiceApi = new InvoiceApi();
  }

  public async makeInvoiceGenerateRequest(
    totalPrice: number,
    pharmacistId: string,
    depotId: string,
    pharmacyId: string,
    medicines: Array<MedicineInvoiceModel>
  ): Promise<AxiosResponse<void, any>> {
    const invoiceIM: InvoiceIM = {
      totalPrice: totalPrice,
      pharmacistId: pharmacistId,
      depotId: depotId,
      pharmacyId: pharmacyId,
      medicines: medicines,
    };

    return await this.invoiceApi.apiInvoiceGeneratePost(
      invoiceIM,
      this.generateHeader()
    );
  }

  public async makeInvoiceShareRequest(
    email: string,
    base64File: string,
    fileName: string
  ): Promise<AxiosResponse<void, any>> {
    const shareIM: ShareIM = {
      email: email,
      base64File: base64File,
      fileName: fileName,
    };

    return await this.invoiceApi.apiInvoiceSharePost(
      shareIM,
      this.generateHeader()
    );
  }
}

const invoiceService = new InvoiceService();
export default invoiceService;
