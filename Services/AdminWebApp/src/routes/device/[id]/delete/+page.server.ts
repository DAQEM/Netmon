import deviceApi from '$lib/api/device_api';
import { error, redirect } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';
import type { Error } from '$lib/types/error';
import type { Device } from '$lib/types/device_types';

export const load = (async ({ params, locals}) => {
    const device: Device | Error = await deviceApi.getDeviceWithConnection(locals.token, params.id);

    if ('code' in device) {
		throw error(device.code, device.message);
	}

    return {
        device
    };
}) satisfies PageServerLoad;

export const actions = {
    default: async (event) => {
        const data: FormData = await event.request.formData();
        const id: string = data.get('id') as string;

        await deviceApi.deleteDevice(event.locals.token, id);

        throw redirect(302, '/device');
    }
}