import { redirect } from '@sveltejs/kit';
import type { RequestHandler } from './$types';

export const GET: RequestHandler = async ({cookies}) => {
	cookies.delete('authToken');
	cookies.delete('refreshToken');

	throw redirect(302, '/login');
};
