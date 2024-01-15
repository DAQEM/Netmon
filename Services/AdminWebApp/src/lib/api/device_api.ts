import type { Device } from '$lib/types/device_types';
import { Error } from '$lib/types/error';
import urlHandler from './url_handler';

const deviceApi = {
	getUrl(url: string) {
		return urlHandler.getDeviceManagerUrl('/device' + url);
	},
	async getAllDevices(token: string): Promise<Device[]> {
		return await fetch(this.getUrl('/'),
			{
				method: 'GET',
				headers: {
					Authorization: 'Bearer ' + token
				}
			})
			.then((res) => {
				return res.ok ? res.json() : Error.fromResponse(res);
			})
			.catch(() => [] as Device[]);
	},
	async getDevice(token: string, id: string): Promise<Device | Error> {
		return await fetch(this.getUrl('/' + id),
			{
				method: 'GET',
				headers: {
					Authorization: 'Bearer ' + token
				}
			})
			.then((res) => res.ok ? res.json() : Error.fromResponse(res))
			.catch(() => ({} as Device));
	},
	async getDeviceWithConnection(token: string, id: string): Promise<Device | Error> {
		return await fetch(this.getUrl('/' + id + '?includeConnection=true'),
			{
				method: 'GET',
				headers: {
					Authorization: 'Bearer ' + token
				}
			})
			.then((res) => res.ok ? res.json() : Error.fromResponse(res))
			.catch(() => ({} as Device));
	},
	async addDevice(token: string, data: Device): Promise<Device | Error> {
		return await fetch(this.getUrl('/'), {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json',
				Authorization: 'Bearer ' + token
			},
			body: JSON.stringify(data)
		})
			.then((res) => {
				return res.ok ? res.json() : Error.fromResponse(res);
			})
			.catch(() => Error.unknown());
	},
	async editDevice(token: string, id: string, data: Device): Promise<Device | Error> {
		return await fetch(this.getUrl('/' + id), {
			method: 'PUT',
			headers: {
				'Content-Type': 'application/json',
				Authorization: 'Bearer ' + token
			},
			body: JSON.stringify(data)
		})
			.then((res) => {
				return res.ok ? res.json() : Error.fromResponse(res);
			})
			.catch(() => Error.unknown());
	},
	async deleteDevice(token: string, id: string): Promise<void> {
		await fetch(this.getUrl('/' + id), {
			method: 'DELETE',
			headers: {
				Authorization: 'Bearer ' + token
			}
		});
	}
};

export default deviceApi;
