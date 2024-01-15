import type { User } from '$lib/types/account_types';
import { redirect } from '@sveltejs/kit';
import type { LayoutServerLoad } from './$types';

export const load: LayoutServerLoad = async ({ locals, url }) => {
	if (locals.user) {
		return {
			user: locals.user as User
		};
	}

	if (url.pathname !== '/login') {
		throw redirect(302, '/login');
	}
};
