import type { DiskStatisticsList } from "$lib/types";

export default class DiskStatisticsAPI {
	fetch: typeof fetch;

	constructor(customFetch: typeof fetch) {
		this.fetch = customFetch;
	}

	getUrl(url: string, deviceId: string): string {
		return process.env.NODE_ENV === 'development'
			? `http://localhost:5000/api/device/device/${deviceId}/statistics/disk${url}`
			: `http://netmon-api-gateway:80/api/device/device/${deviceId}/statistics/disk${url}`;
	}

	async getStatistics(deviceId: string, from: Date, to: Date): Promise<DiskStatisticsList> {
		const url = this.getUrl(`?fromDate=${from.toISOString()}&toDate=${to.toISOString()}`, deviceId);
		const response = await this.fetch(url);
		return await response.json();
	}
}
