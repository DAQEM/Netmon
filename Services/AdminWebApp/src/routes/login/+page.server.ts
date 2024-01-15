import AccountAPI from '$lib/api/account_api';
import { redirect, type Actions } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

export const load = (async () => {
	return {};
}) satisfies PageServerLoad;

export const actions: Actions = {
	default: async ({ request, cookies }) => {
		const data: FormData = await request.formData();

		const username = data.get('username') as string;
		const password = data.get('password') as string;

		const accountApi: AccountAPI = new AccountAPI(fetch);
		const loginResponse = await accountApi.login(username, password);

		if ('accessToken' in loginResponse && 'refreshToken' in loginResponse) {
			cookies.set('authToken', loginResponse.accessToken);
			cookies.set('refreshToken', loginResponse.refreshToken);
			throw redirect(302, '/dashboard');
		}

		return {
			success: false
		};
	}
};
