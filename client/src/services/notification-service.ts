import { BuildingType, NotificationApi, NotificationVM } from "@/api";
import { WebApiService } from "./web-api-service";
import { AxiosResponse } from "axios";

export class NotificationsService extends WebApiService {
  notificationApi: NotificationApi;

  constructor() {
    super();
    this.notificationApi = new NotificationApi();
  }

  public async makeNotificationsGetAllRequest(
    pharmacyId: string,
    depotId: string
  ): Promise<AxiosResponse<NotificationVM[], any>> {
    return await this.notificationApi.apiNotificationGetGet(
      pharmacyId,
      depotId,
      this.generateHeader()
    );
  }

  public async makeNotificationsGetAllAssignRequests(
    depotId: string
  ): Promise<AxiosResponse<NotificationVM[], any>> {
    return await this.notificationApi.apiNotificationAssignRequestsGet(
      depotId,
      this.generateHeader()
    );
  }

  public async makeNotificationsGetForDepotRequest(
    depotId: string
  ): Promise<AxiosResponse<NotificationVM[], any>> {
    return await this.notificationApi.apiNotificationGetDepotsDepotIdGet(
      depotId,
      this.generateHeader()
    );
  }

  public async makeNotificationsGetForPharmacyRequest(
    pharmacyId: string
  ): Promise<AxiosResponse<NotificationVM[], any>> {
    return await this.notificationApi.apiNotificationGetPharmaciesPharmacyIdGet(
      pharmacyId,
      this.generateHeader()
    );
  }

  public async makeNotificationsGetWarningsRequest(
    buildingId: string,
    buildingType: BuildingType
  ): Promise<AxiosResponse<NotificationVM[], any>> {
    return await this.notificationApi.apiNotificationWarningsGet(
      buildingId,
      buildingType,
      this.generateHeader()
    );
  }
}

const notificationService = new NotificationsService();
export default notificationService;
