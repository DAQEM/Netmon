import deviceApi from '$lib/api/device_api';
import { redirect } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

export const load = (async ({ params }) => {
    const device: Device = await deviceApi.getDeviceWithConnection(params.id);
    return {
        device
    };
}) satisfies PageServerLoad;

export const actions = {
    default: async (event) => {
        const data: FormData = await event.request.formData();
        const id: string = data.get('id') as string;

        await deviceApi.deleteDevice(id);

        throw redirect(302, '/device');
    }
}