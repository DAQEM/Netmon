import DeviceAPI from '$lib/api/device_api';
import type { Device } from '$lib/types';
import type { LayoutServerLoad } from './$types';

export const load = (async ({ params, locals }) => {
	const device: Device = await new DeviceAPI(fetch, locals.token).getDeviceById(params.id);
	return {
		device
	};
}) satisfies LayoutServerLoad;
