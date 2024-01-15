import DeviceAPI from '$lib/api/device_api';
import type { Device } from '$lib/types';
import { redirect, type Actions } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

export const load = (async ({ fetch, locals, url: { searchParams } }) => {
	const value: string | undefined = searchParams.get('q') ?? undefined;

	const devices: Device[] = await new DeviceAPI(fetch, locals.token).getDevices();
	return {
		devices,
		value
	};
}) satisfies PageServerLoad;

export const actions: Actions = {
	default: async ({ request }) => {
		const data: FormData = await request.formData();

		const value = data.get('value') as string;

		throw redirect(302, `/device?q=${value}`);
	}
};
