import {
  Address,
  AssignEmployeeIM,
  PharmacyApi,
  PharmacyIM,
  PharmacyUM,
  PharmacyVM,
  UserVM,
} from "@/api";
import { WebApiService } from "./web-api-service";
import { AxiosResponse } from "axios";

export class PharmacyService extends WebApiService {
  pharmacyApi: PharmacyApi;

  constructor() {
    super();
    this.pharmacyApi = new PharmacyApi();
  }

  public async makePharmacyCreateRequest(
    name: string,
    description: string,
    address_number: number,
    address_street: string,
    address_city: string,
    address_country: string,
    depotId: string | null
  ): Promise<AxiosResponse<void, any>> {
    const address: Address = {
      number: address_number,
      street: address_street,
      city: address_city,
      country: address_country,
    };

    const pharmacyIM: PharmacyIM = {
      name: name,
      description: description,
      address: address,
      depotId: depotId,
    };

    return await this.pharmacyApi.apiPharmacyCreatePost(
      pharmacyIM,
      this.generateHeader()
    );
  }

  public async makePharmacyDeleteRequest(
    id: string
  ): Promise<AxiosResponse<void, any>> {
    return await this.pharmacyApi.apiPharmacyDeleteIdDelete(
      id,
      this.generateHeader()
    );
  }

  public async makePharmacyGetPharmaciesRequest(): Promise<
    AxiosResponse<PharmacyVM[], any>
  > {
    return await this.pharmacyApi.apiPharmacyPharmaciesGet(
      this.generateHeader()
    );
  }

  public async makePharmacyGetByIdRequest(
    id: string
  ): Promise<AxiosResponse<PharmacyVM, any>> {
    return await this.pharmacyApi.apiPharmacyPharmacyIdGet(
      id,
      this.generateHeader()
    );
  }

  public async makePharmacyUpdateRequest(
    id: string,
    name: string | null,
    description: string | null,
    address_number: number | null,
    address_street: string | null,
    address_city: string | null,
    address_country: string | null,
    founderId: string | null,
    depotId: string | null
  ): Promise<AxiosResponse<void, any>> {
    const address: Address = {
      number: address_number,
      street: address_street,
      city: address_city,
      country: address_country,
    };

    const pharmacyUM: PharmacyUM = {
      id: id,
      name: name,
      description: description,
      address: address,
      founderId: founderId,
      depotId: depotId,
    };

    return await this.pharmacyApi.apiPharmacyUpdatePut(
      pharmacyUM,
      this.generateHeader()
    );
  }

  public async makePharmacyGetByDepotIdRequest(
    depotId: string
  ): Promise<AxiosResponse<PharmacyVM[], any>> {
    return await this.pharmacyApi.apiPharmacyPharmaciesDepotIdGet(
      depotId,
      this.generateHeader()
    );
  }

  public async makePharmacyRequestDepotAssignRequest(
    pharmacyId: string,
    depotId: string
  ): Promise<AxiosResponse<void, any>> {
    return await this.pharmacyApi.apiPharmacyRequestDepotAssignPost(
      pharmacyId,
      depotId,
      this.generateHeader()
    );
  }

  public async makePharmacyAssignEmployeeRequest(pharmacyId: string, email: string, password: string): Promise<AxiosResponse<void, any>> {
    const assignEmployeeIM: AssignEmployeeIM = ({
      email: email,
      password: password,
    });

    return await this.pharmacyApi.apiPharmacyAssignEmployeePharmacyIdPost(pharmacyId, assignEmployeeIM, this.generateHeader());
  }

  public async makePharmacyRemoveEmployeeRequest(pharmacyId: string, employeeId: string): Promise<AxiosResponse<void, any>> {
    return await this.pharmacyApi.apiPharmacyRemoveEmployeePharmacyIdPost(pharmacyId, employeeId, this.generateHeader());
  }

  public async makePharmacyGetPharmacistsRequest(pharmacyId: string): Promise<AxiosResponse<UserVM[], any>> {
    return await this.pharmacyApi.apiPharmacyPharmacistsPharmacyIdGet(pharmacyId, this.generateHeader());
  }
}

const pharmacyService = new PharmacyService();
export default pharmacyService;
