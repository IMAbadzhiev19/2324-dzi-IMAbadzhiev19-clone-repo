import { ChangePasswordIM, UserApi, UserUM, UserVM } from "@/api";
import { WebApiService } from "./web-api-service";
import { AxiosResponse } from "axios";

export class UserService extends WebApiService {
  userApi: UserApi;

  constructor() {
    super();
    this.userApi = new UserApi();
  }

  public async makeChangePasswordRequest(
    oldPassword: string,
    newPassword: string
  ): Promise<AxiosResponse<void, any>> {
    const changePasswordIM: ChangePasswordIM = {
      oldPassword: oldPassword,
      newPassword: newPassword,
    };

    return await this.userApi.apiUserChangePasswordPut(
      changePasswordIM,
      this.generateHeader()
    );
  }

  public async makeUserUpdateRequest(
    firstName: string | null,
    lastName: string | null,
    email: string | null,
    phoneNumber: string | null
  ): Promise<AxiosResponse<void, any>> {
    const userUM: UserUM = {
      firstName: firstName,
      lastName: lastName,
      email: email,
      phoneNumber: phoneNumber,
    };

    return await this.userApi.apiUserUpdatePut(userUM, this.generateHeader());
  }

  public async makeUserCurrentUserRequest(): Promise<AxiosResponse<UserVM, any>> {
    return await this.userApi.apiUserCurrentUserGet(this.generateHeader());
  }
}

const userService = new UserService();
export default userService;
