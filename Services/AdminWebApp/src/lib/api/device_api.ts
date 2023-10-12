import urlHandler from './url_handler';

interface ConnectionInfo {
	connection: {
		ip_address: string;
		port: number;
		community: string;
		version: number;
		auth_password?: string;
		privacy_password?: string;
		auth_protocol?: number;
		privacy_protocol?: number;
		context?: string;
	};
}

const deviceApi = {
	getUrl(url: string) {
		return urlHandler.getDeviceManagerUrl('/device' + url);
	},
	async getAllDevices() {
		return await fetch(this.getUrl('/')).then((res) => res.json());
	},
	async getDevice(id: string) {
		return await fetch(this.getUrl('/' + id)).then((res) => res.json());
	},
	async addDevice(data: ConnectionInfo) {
		return await fetch(this.getUrl('/'), {
			method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
			body: JSON.stringify(data)
		}).then((res) => (res.ok ? res.json() : res.json().then((err) => Promise.reject(err))));
	}
};

export default deviceApi;
