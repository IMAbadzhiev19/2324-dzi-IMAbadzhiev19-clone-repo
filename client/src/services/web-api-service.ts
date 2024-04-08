import storageService from "@/services/storage-service";

export abstract class WebApiService {
  protected generateHeader(): object {
    return {
      headers: {
        Authorization: `Bearer ${storageService.retrieveAccessToken()}`,
      },
    };
  }
}

export interface ResponseData {
  accessToken: string;
  refreshToken: string;
  expiration: string;
}
