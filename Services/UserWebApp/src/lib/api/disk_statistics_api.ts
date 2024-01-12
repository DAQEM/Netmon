import type { DiskStatisticsList } from "$lib/types";
import UrlHandler from "./url_handler";

export default class DiskStatisticsAPI {
	fetch: typeof fetch;

	constructor(customFetch: typeof fetch) {
		this.fetch = customFetch;
	}

	getUrl(url: string, deviceId: string): string {
		return UrlHandler.getUrl(`/device/device/${deviceId}/statistics/disk${url}`);
	}

	async getStatistics(deviceId: string, from: Date, to: Date): Promise<DiskStatisticsList> {
		const url = this.getUrl(`?fromDate=${from.toISOString()}&toDate=${to.toISOString()}`, deviceId);
		const response = await this.fetch(url);
		return await response.json();
	}
}
