import DeviceAPI from '$lib/api/device_api';
import type { Device } from '$lib/types';
import type { PageServerLoad } from './$types';

export const load = (async ({ fetch, params }) => {
    const device: Device = await new DeviceAPI(fetch).getDeviceById(params.id);
    return {
        device
    };
}) satisfies PageServerLoad;