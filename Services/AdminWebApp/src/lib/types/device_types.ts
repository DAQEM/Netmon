interface Device {
    id: string,
    name: string,
    ipAddress: string,
    location: string,
    contact: string
}

interface DeviceConnectionInfo {
	connection: {
		ip_address: string;
		port: number;
		community: string;
		version: number;
		auth_password?: string;
		privacy_password?: string;
		auth_protocol?: number;
		privacy_protocol?: number;
		context?: string;
	};
}