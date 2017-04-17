export class ServiceProviderInfo {
    name: string;
    description: string;
    webSite: string;
    serviceTypeId: number;
}

export class UserServiceInfo {
    activationDateTime: Date;
    status: number;
    serviceType: string;
    provider: ServiceProviderInfo;
    requireActivationData: boolean;
}

export enum UserServiceStatus {
    Available = 1,
    Activated,
    Unavailable
}