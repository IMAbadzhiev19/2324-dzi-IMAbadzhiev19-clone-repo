import { AuthApi, LoginIM, RegisterIM, TokensIM } from "@/api";
import { WebApiService } from "./web-api-service";
import { AxiosResponse } from "axios";

export class AuthService extends WebApiService {
    authApi: AuthApi;

    constructor() {
        super();
        this.authApi = new AuthApi();
    }

    public async makeLoginRequest(email: string, password: string): Promise<AxiosResponse<void, any>> {
        const loginIM: LoginIM = ({
            email: email,
            password: password,
        });

        return await this.authApi.apiAuthLoginPost(loginIM);
    }

    public async makeRegisterRequest(firstName: string, lastName: string, email: string, password: string, phoneNumber: string): Promise<AxiosResponse<void, any>> {
        const registerIM: RegisterIM = ({
            firstName: firstName,
            lastName: lastName,
            email: email,
            password: password,
            phoneNumber: phoneNumber,
        });

        return await this.authApi.apiAuthRegisterPost(registerIM);
    }

    public async makeRenewTokensRequest(accessToken: string | null, refreshToken: string | null): Promise<AxiosResponse<void, any>> {
        const tokensIM: TokensIM = ({
            accessToken: accessToken,
            refreshToken: refreshToken,
        });

        return await this.authApi.apiAuthRenewTokensPost(tokensIM);
    }

    public async makeLogoutRequest(): Promise<AxiosResponse<void, any>> {
        return await this.authApi.apiAuthLogoutGet(this.generateHeader());
    }
}

const authService = new AuthService();
export default authService;