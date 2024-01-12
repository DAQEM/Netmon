import type { Device } from '$lib/types';
import UrlHandler from './url_handler';

export default class DeviceAPI {
	fetch: typeof fetch;

	constructor(customFetch: typeof fetch) {
		this.fetch = customFetch;
	}

	getUrl(url: string): string {
		return UrlHandler.getUrl(`/device/device/${url}`);
	}

	async getDevices(): Promise<Device[]> {
		return await this.fetch(this.getUrl(''))
			.then((response) => {
				if (!response.ok) {
					console.error('error response: ', response);
				}
				return response.json();
			})
			.catch((error) => {
				console.error('error: ', error);
				return [];
			});
	}

	async getDeviceById(id: string): Promise<Device> {
		const response = await this.fetch(this.getUrl(`${id}`));
		return await response.json();
	}
}
