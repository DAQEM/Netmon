import type { Device } from "$lib/types";

export default class DeviceAPI {

    fetch: typeof fetch;

    constructor(customFetch: typeof fetch) {
        this.fetch = customFetch;
    }

    getUrl(url: string): string {
        return process.env.NODE_ENV === 'development'
            ? `http://localhost:5000/api/device/${url}`
            : `http://netmon-device-manager-service/api/device/${url}`;
    }

    async getDevices(): Promise<Device[]> {
        const response = await this.fetch(this.getUrl('device'));
        return await response.json();
    }

    async getDeviceById(id: string): Promise<Device> {
        const response = await this.fetch(this.getUrl(`device/${id}`));
        return await response.json();
    }
}