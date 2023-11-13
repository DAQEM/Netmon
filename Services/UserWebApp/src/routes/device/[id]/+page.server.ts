import type { Device } from '$lib/types';
import type { PageServerLoad, PageServerParentData } from './$types';

export const load = (async ({ parent }) => {
	const data: PageServerParentData = await parent();
	const device: Device = data.device;
	return {
		device
	};
}) satisfies PageServerLoad;
