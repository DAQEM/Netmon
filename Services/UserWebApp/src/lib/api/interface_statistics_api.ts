import type { InterfaceStatisticsList } from '$lib/types';
import UrlHandler from './url_handler';

export default class InterfaceStatisticsAPI {
	fetch: typeof fetch;

	constructor(customFetch: typeof fetch) {
		this.fetch = customFetch;
	}

	getUrl(url: string, deviceId: string): string {
		return UrlHandler.getUrl(`/device/device/${deviceId}/statistics/interface${url}`);
	}

	async getInOut(deviceId: string, from: Date, to: Date): Promise<InterfaceStatisticsList> {
		const url = this.getUrl(
			`/inout?fromDate=${from.toISOString()}&toDate=${to.toISOString()}`,
			deviceId
		);
		const response = await this.fetch(url);
		return await response.json();
	}
}
