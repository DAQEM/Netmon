import { Error } from '$lib/types/error';
import urlHandler from './url_handler';

const deviceApi = {
	getUrl(url: string) {
		return urlHandler.getDeviceManagerUrl('/device' + url);
	},
	async getAllDevices(): Promise<Device[]> {
		return await fetch(this.getUrl('/'))
			.then((res) => res.json())
			.catch(() => [] as Device[]);
	},
	async getDevice(id: string): Promise<Device> {
		return await fetch(this.getUrl('/' + id))
			.then((res) => res.json())
			.catch(() => ({} as Device));
	},
	async getDeviceWithConnection(id: string): Promise<Device> {
		return await fetch(this.getUrl('/' + id + '?includeConnection=true'))
			.then((res) => res.json())
			.catch(() => ({} as Device));
	},
	async addDevice(data: Device): Promise<Device | Error> {
		return await fetch(this.getUrl('/'), {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(data)
		})
			.then((res) => {
				return res.ok ? res.json() : Error.fromResponse(res);
			})
			.catch((res) => {
				console.log('An error occurred while adding a device: ', res);
				return Error.unknown();
			});
	},
	async editDevice(id: string, data: Device): Promise<Device | Error> {
		return await fetch(this.getUrl('/' + id), {
			method: 'PUT',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(data)
		})
			.then((res) => {
				return res.ok ? res.json() : Error.fromResponse(res);
			})
			.catch(() => Error.unknown());
	},
	async deleteDevice(id: string): Promise<void> {
		await fetch(this.getUrl('/' + id), {
			method: 'DELETE'
		});
	}
};

export default deviceApi;
