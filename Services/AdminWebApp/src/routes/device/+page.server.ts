import deviceApi from '$lib/api/device_api';
import type { Device } from '$lib/types/device_types';
import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async ({locals}) => {
	const devices: Device[] = await deviceApi.getAllDevices(locals.token);

	return {
		props: {
			devices
		}
	};
};
