import type { Device } from '$lib/types';
import UrlHandler from './url_handler';

export default class DeviceAPI {
	fetch: typeof fetch;
	authToken: string;


	constructor(customFetch: typeof fetch, authToken: string) {
		this.fetch = customFetch;
		this.authToken = authToken;
	}

	getUrl(url: string): string {
		return UrlHandler.getUrl(`/device/device/${url}`);
	}

	async getDevices(): Promise<Device[]> {
		return await this.fetch(this.getUrl(''), {
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
				return [];
			});
	}

	async getDeviceById(id: string): Promise<Device> {
		return await this.fetch(this.getUrl(`${id}`), {
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
				return {} as Device;
			});
	}
}
