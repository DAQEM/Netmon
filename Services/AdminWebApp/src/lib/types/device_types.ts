export interface Device {
	id?: string | undefined;
	name?: string | undefined;
	ipAddress?: string | undefined;
	location?: string | undefined;
	contact?: string | undefined;
	connection?: DeviceConnectionInfo | undefined;
}

export interface DeviceConnectionInfo {
	port: number | undefined;
	community: string | undefined;
	version: number | undefined;
	authPassword?: string | undefined;
	privacyPassword?: string | undefined;
	authProtocol?: number | undefined;
	privacyProtocol?: number | undefined;
	contextName?: string | undefined;
}
