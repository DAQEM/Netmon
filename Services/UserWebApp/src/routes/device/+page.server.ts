import DeviceAPI from '$lib/api/device_api';
import type { Device } from '$lib/types';
import type { PageServerLoad } from './$types';

export const load = (async ({ fetch }) => {
    const devices: Device[] = await new DeviceAPI(fetch).getDevices(); 
    return {
        devices
    };
}) satisfies PageServerLoad;