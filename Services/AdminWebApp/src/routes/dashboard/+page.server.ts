import AccountAPI from '$lib/api/account_api';
import deviceApi from '$lib/api/device_api';
import urlHandler from '$lib/api/url_handler';
import type { Device } from '$lib/types/device_types';
import type { PageServerLoad } from './$types';

export const load = (async ({ locals, fetch }) => {
	const devices: Device[] = await deviceApi.getAllDevices(locals.token);
	const users = await new AccountAPI(fetch).getAllUsers(locals.token);

	return {
		props: {
			devices,
			users,
			baseUrl: urlHandler.getUserWebAppUrl('')
		}
	};
}) satisfies PageServerLoad;
