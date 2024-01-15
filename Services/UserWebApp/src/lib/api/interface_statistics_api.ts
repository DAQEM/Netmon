import type { InterfaceStatisticsList } from '$lib/types';
import UrlHandler from './url_handler';

export default class InterfaceStatisticsAPI {
	fetch: typeof fetch;
	authToken: string;

	constructor(customFetch: typeof fetch, authToken: string) {
		this.fetch = customFetch;
		this.authToken = authToken;
	}

	getUrl(url: string, deviceId: string): string {
		return UrlHandler.getUrl(`/device/device/${deviceId}/statistics/interface${url}`);
	}

	async getInOut(deviceId: string, from: Date, to: Date): Promise<InterfaceStatisticsList> {
		const url = this.getUrl(
			`/inout?fromDate=${from.toISOString()}&toDate=${to.toISOString()}`,
			deviceId
		);
		return await this.fetch(url, {
			method: 'GET',
			headers: {
				Authorization: `Bearer ${this.authToken}`
			}
		})
			.then((response) => {
				if (!response.ok) {
					console.error('error response: ', response);
				}
				return response.json();
			})
			.catch((error) => {
				console.error('error: ', error);
				return { interfaces: [] } as InterfaceStatisticsList;
			});
	}
}
