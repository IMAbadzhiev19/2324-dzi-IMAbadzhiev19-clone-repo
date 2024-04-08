import { Address, DepotApi, DepotIM, DepotUM, DepotVM } from "@/api";
import { WebApiService } from "./web-api-service";
import { AxiosResponse } from "axios";

export class DepotService extends WebApiService {
  depotApi: DepotApi;

  constructor() {
    super();
    this.depotApi = new DepotApi();
  }

  public async makeDepotCreateRequest(
    name: string,
    address_number: number,
    address_street: string,
    address_city: string,
    address_country: string,
  ): Promise<AxiosResponse<void, any>> {
    const address: Address = {
      number: address_number,
      street: address_street,
      city: address_city,
      country: address_country,
    };

    const depotIM: DepotIM = {
      name: name,
      address: address,
    };

    return await this.depotApi.apiDepotCreatePost(
      depotIM,
      this.generateHeader()
    );
  }

  public async makeDepotUpdateRequest(
    id: string,
    name: string | null,
    managerId: string | null,
    address_number: number | null,
    address_street: string | null,
    address_city: string | null,
    address_country: string | null,
  ): Promise<AxiosResponse<void, any>> {
    const address: Address = {
      number: address_number,
      street: address_street,
      city: address_city,
      country: address_country,
    }

    const depotUM: DepotUM = {
      id: id,
      name: name,
      managerId: managerId,
      address: address,
    };

    return await this.depotApi.apiDepotUpdatePut(
      depotUM,
      this.generateHeader()
    );
  }

  public async makeDepotDeleteRequest(
    id: string
  ): Promise<AxiosResponse<void, any>> {
    return await this.depotApi.apiDepotDeleteIdDelete(
      id,
      this.generateHeader()
    );
  }

  public async makeDepotGetAllRequest(): Promise<AxiosResponse<DepotVM[], any>> {
    return await this.depotApi.apiDepotDepotsGet(this.generateHeader());
  }

  public async makeDepotGetByIdRequest(
    id: string
  ): Promise<AxiosResponse<DepotVM, any>> {
    return await this.depotApi.apiDepotDepotIdGet(id, this.generateHeader());
  }

  public async makeDepotGetByUserRequest(): Promise<AxiosResponse<DepotVM[], any>> {
    return await this.depotApi.apiDepotDepotsByUserGet(this.generateHeader());
  }

  public async makeDepotAssignToPharmacyRequest(
    pharmacyId: string,
    depotId: string
  ): Promise<AxiosResponse<void, any>> {
    return await this.depotApi.apiDepotAssignToPharmacyPost(
      pharmacyId,
      depotId,
      this.generateHeader()
    );
  }
}

const depotService = new DepotService();
export default depotService;