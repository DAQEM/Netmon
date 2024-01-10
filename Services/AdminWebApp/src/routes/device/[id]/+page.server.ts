import deviceApi from '$lib/api/device_api';
import type { Device } from '$lib/types/device_types';
import type { PageServerLoad } from './$types';

export const load = (async ({params}) => {
	const device: Device = await deviceApi.getDeviceWithConnection(params.id);
	return {
		device
	};
}) satisfies PageServerLoad;
