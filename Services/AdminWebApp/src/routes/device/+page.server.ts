import deviceApi from '$lib/api/device_api';
import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async () => {
	const devices: Device[] = await deviceApi.getAllDevices();

	return {
		props: {
			devices
		}
	};
};
