import AccountAPI from '$lib/api/account_api';
import type { User } from '$lib/types/account_types';
import { error, redirect, type Actions } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

export const load = (async ({ params, locals, fetch }) => {
	const user: User | undefined = await new AccountAPI(fetch).getUser(locals.token, params.id);

	if (user) {
		return {
			props: {
				user
			}
		};
	} else {
		throw error(404, 'User not found');
	}
}) satisfies PageServerLoad;

export const actions: Actions = {
	//delete user
	default: async ({ request, fetch, locals }) => {
		const data: FormData = await request.formData();

		const id = data.get('id') as string;

		const accountApi: AccountAPI = new AccountAPI(fetch);
		await accountApi.deleteUser(locals.token, id);

		throw redirect(302, '/user');
	}
};
