import AccountAPI from '$lib/api/account_api';
import type { User } from '$lib/types/account_types';
import type { PageServerLoad } from './$types';

export const load = (async ({ fetch, locals }) => {
	const users: User[] = await new AccountAPI(fetch).getAllUsers(locals.token);

	return {
		props: {
			users
		}
	};
}) satisfies PageServerLoad;
