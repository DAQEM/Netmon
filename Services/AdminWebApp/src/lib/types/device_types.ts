//eslint-disable-next-line
interface Device {
	id?: string | undefined;
	name?: string | undefined;
	ip_address?: string | undefined;
	location?: string | undefined;
	contact?: string | undefined;
	connection?: DeviceConnectionInfo | undefined;
}

//eslint-disable-next-line
interface DeviceConnectionInfo {
	port: number | undefined;
	community: string | undefined;
	version: number | undefined;
	auth_password?: string | undefined;
	privacy_password?: string | undefined;
	auth_protocol?: number | undefined;
	privacy_protocol?: number | undefined;
	context_name?: string | undefined;
}
