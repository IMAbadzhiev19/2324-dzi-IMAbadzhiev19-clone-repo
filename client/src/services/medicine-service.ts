import {
  BasicMedicineRequest,
  BasicMedicineVM,
  BuildingType,
  MedicineApi,
  MedicineIM,
  MedicineVM,
  RefillIM,
} from "@/api";
import { WebApiService } from "./web-api-service";
import { AxiosResponse } from "axios";

export class MedicineService extends WebApiService {
  medicineApi: MedicineApi;

  constructor() {
    super();
    this.medicineApi = new MedicineApi();
  }

  public async makeBasicMedicinesGetRequest(): Promise<
    AxiosResponse<BasicMedicineVM[], any>
  > {
    return await this.medicineApi.apiMedicineBasicMedicinesGet(
      this.generateHeader()
    );
  }

  public async makeMedicineCreateRequest(
    basicMedicineId: string,
    price: number,
    quantity: number,
    expirationDate: string | null,
    buildingId: string,
    buildingType: BuildingType
  ): Promise<AxiosResponse<void, any>> {
    const medicineIM: MedicineIM = {
      basicMedicineId: basicMedicineId,
      price: price,
      quantity: quantity,
      expirationDate: expirationDate,
    };

    return await this.medicineApi.apiMedicineCreatePost(
      medicineIM,
      buildingId,
      buildingType,
      this.generateHeader()
    );
  }

  public async makeMedicineDeleteRequest(
    id: string
  ): Promise<AxiosResponse<void, any>> {
    return await this.medicineApi.apiMedicineDeleteIdDelete(
      id,
      this.generateHeader()
    );
  }

  public async makeMedicineGetByIdRequest(
    id: string
  ): Promise<AxiosResponse<MedicineVM, any>> {
    return await this.medicineApi.apiMedicineMedicineIdGet(
      id,
      this.generateHeader()
    );
  }

  public async makeGetMedicinesByBuilding(
    buildingId: string,
    type: BuildingType
  ): Promise<AxiosResponse<MedicineVM[], any>> {
    return await this.medicineApi.apiMedicineMedicinesBuildingIdGet(
      buildingId,
      type,
      this.generateHeader()
    );
  }

  public async makeRequestBasicMedicine(
    name: string,
    email: string
  ): Promise<AxiosResponse<void, any>> {
    const basicMedicineRequest: BasicMedicineRequest = {
      name: name,
      email: email,
    };

    return await this.medicineApi.apiMedicineRequestBasicMedicinePost(
      basicMedicineRequest,
      this.generateHeader()
    );
  }

  public async makeMedicineUpdateRequest(
    id: string,
    price: number | undefined,
    count: number | undefined,
    image: Blob | undefined
  ): Promise<AxiosResponse<void, any>> {
    return await this.medicineApi.apiMedicineUpdatePutForm(
      id,
      price,
      count,
      image,
      this.generateHeader()
    );
  }

  public async makeMedicineRefillRequest(medicineId: string, pharmacyId: string, quantity: number): Promise<AxiosResponse<void, any>> {
    const refillIM: RefillIM = ({
      pharmacyId: pharmacyId,
      quantity: quantity,
    });

    return await this.medicineApi.apiMedicineRefillIdPost(medicineId, refillIM, this.generateHeader());
  }
}

const medicineService = new MedicineService();
export default medicineService;
