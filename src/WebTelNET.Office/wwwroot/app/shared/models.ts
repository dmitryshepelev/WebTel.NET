export class ServiceProviderInfo {
    name: string;
    description: string;
    webSite: string;
    serviceTypeId: number;
}

export interface IServiceStatus {
    status: number;
    serviceType: string;
}

export class UserServiceInfo implements IServiceStatus {
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

export enum DynamicComponentMode {
    NEW,
    EDIT,
    FREE
}

export class IDynamicComponentSettings {
    component: string;
    mode: DynamicComponentMode;

    model?: any;
}

export interface IDynamicComponent {
    init(settings: IDynamicComponentSettings): void;
    destroy?(): void;
}