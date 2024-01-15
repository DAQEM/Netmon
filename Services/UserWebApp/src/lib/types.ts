enum AuthProtocol {
	SHA256 = 0,
	SHA384 = 1,
	SHA512 = 2
}

enum PrivacyProtocol {
	AES = 0,
	AES192 = 1,
	AES256 = 2
}

interface Device {
	id: string;
	name: string;
	ipAddress: string;
	location: string;
	contact: string;
}

interface InterfaceStatisticsList {
	interfaces: InterfaceStatistics[];
}

interface InterfaceStatistics {
	index: number;
	name: string;
	type: number;
	physicalAddress: string;
	metrics: InterfaceMetrics[];
}

interface InterfaceMetrics {
	timestamp: Date;
	inOctets: number;
	outOctets: number;
}

interface DiskStatisticsList {
	disks: DiskStatistics[];
}

interface DiskStatistics {
	index: number;
	mountingPoint: string;
	metrics: DiskMetrics[];
}

interface DiskMetrics {
	timestamp: Date;
	allocationUnits: number;
	totalSpace: number;
	usedSpace: number;
}

interface RegisterResponse {
	success: boolean;
}

interface RegisterErrors {
	type: string;
	title: string;
	status: number;
	detail: string;
	instance: string;
	errors: {
		[key: string]: string[];
	};
}

interface LoginResponse {
	tokenType: string;
	accessToken: string;
	expiresIn: number;
	refreshToken: string;
}

interface LoginErrors {
	type: string;
	title: string;
	status: number;
	detail: string;
}

interface User {
	id: string;
	userName: string;
	fullName: string;
	profileImageName: string;
	email: string;
}

export type {
	AuthProtocol,
	Device,
	DiskMetrics,
	DiskStatistics,
	DiskStatisticsList,
	InterfaceMetrics,
	InterfaceStatistics,
	InterfaceStatisticsList,
	LoginErrors,
	LoginResponse,
	PrivacyProtocol,
	RegisterErrors,
	RegisterResponse,
	User
};
