import DeviceAPI from '$lib/api/device_api';
import type { Device } from '$lib/types';
import type { LayoutServerLoad } from './$types';

export const load = (async ({ params }) => {
	const device: Device = await new DeviceAPI(fetch).getDeviceById(params.id);
	return {
		device
	};
}) satisfies LayoutServerLoad;
