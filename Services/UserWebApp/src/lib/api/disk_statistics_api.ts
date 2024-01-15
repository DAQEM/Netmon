import type { DiskStatisticsList } from '$lib/types';
import UrlHandler from './url_handler';

export default class DiskStatisticsAPI {
	fetch: typeof fetch;
	authToken: string;

	constructor(customFetch: typeof fetch, authToken: string) {
		this.fetch = customFetch;
		this.authToken = authToken;
	}

	getUrl(url: string, deviceId: string): string {
		return UrlHandler.getUrl(`/device/device/${deviceId}/statistics/disk${url}`);
	}

	async getStatistics(deviceId: string, from: Date, to: Date): Promise<DiskStatisticsList> {
		return await this.fetch(
			this.getUrl(`?fromDate=${from.toISOString()}&toDate=${to.toISOString()}`, deviceId),
			{
				method: 'GET',
				headers: {
					Authorization: `Bearer ${this.authToken}`
				}
			}
		)
			.then((response) => {
				if (!response.ok) {
					console.error('error response: ', response);
				}
				return response.json();
			})
			.catch((error) => {
				console.error('error: ', error);
				return { disks: [] } as DiskStatisticsList;
			});
	}
}
