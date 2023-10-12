import deviceApi from '$lib/api/device_api';
import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async () => {
	return {
		props: {
			data: await deviceApi.getAllDevices()
		}
	};
};
