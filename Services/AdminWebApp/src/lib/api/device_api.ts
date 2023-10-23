import urlHandler from './url_handler';

const deviceApi = {
	getUrl(url: string) {
		return urlHandler.getDeviceManagerUrl('/device' + url);
	},
	async getAllDevices(): Promise<Device[]> {
		return await fetch(this.getUrl('/')).then((res) => res.json());
	},
	async getDevice(id: string): Promise<Device> {
		return await fetch(this.getUrl('/' + id)).then((res) => res.json());
	},
	async getDeviceWithConnection(id: string): Promise<Device> {
		return await fetch(this.getUrl('/' + id + '?includeConnection=true')).then((res) => res.json());
	},
	async addDevice(data: Device) {
		const json = JSON.stringify(data);
		console.log('json',json);
		return await fetch(this.getUrl('/'), {
			method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
			body: json
		}).then((res) => (res.ok ? res.json() : res.json().then((err) => Promise.reject(err))));
	}
};

export default deviceApi;
