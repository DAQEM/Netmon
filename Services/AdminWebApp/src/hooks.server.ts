import AccountAPI from '$lib/api/account_api';
import type { LoginResponse } from '$lib/types/account_types';
import { redirect, type Handle, type RequestEvent } from '@sveltejs/kit';

const DEMO_USER = {
	userName: 'demo',
	fullName: 'Demo User',
	profileImageName: 'https://i.pravatar.cc/100',
	email: 'demo@demo.com',
	password: 'P@ssw0rd!'
};

const API_PATHS = {
	REGISTER: '/register',
	LOGIN: '/login',
	DASHBOARD: '/dashboard'
};

let initiated: boolean = false;

const authenticateUser = async (
	accountApi: AccountAPI,
	event: RequestEvent<Partial<Record<string, string>>, string | null>,
	authToken?: string
): Promise<string | undefined> => {
	let user = undefined;

	if (authToken) {
		user = await accountApi.authenticate(authToken);
		if (!user) {
			event.cookies.delete('authToken');
			event.cookies.delete('refreshToken');
		} else {
			event.locals.user = user;
		}
	} else {
		const refreshToken: string | undefined = event.cookies.get('refreshToken');
		if (refreshToken) {
			const refreshResponse: LoginResponse = await accountApi.refresh(refreshToken);
			authToken = refreshResponse.accessToken;
			event.cookies.set('authToken', refreshResponse.accessToken);
			event.cookies.set('refreshToken', refreshResponse.refreshToken);

			user = await accountApi.authenticate(refreshResponse.accessToken);
			if (!user) {
				event.cookies.delete('authToken');
				event.cookies.delete('refreshToken');
			} else {
				event.locals.user = user;
			}
		}
	}

	return user ? authToken : undefined;
};

export const handle: Handle = async ({ event, resolve }) => {
	const accountApi: AccountAPI = new AccountAPI(fetch);
	const { pathname } = event.url;

	if (!initiated) {
		await accountApi.register(
			DEMO_USER.userName,
			DEMO_USER.fullName,
			DEMO_USER.profileImageName,
			DEMO_USER.email,
			DEMO_USER.password
		);
		initiated = true;
	}

	let authToken: string | undefined = event.cookies.get('authToken');

	if (pathname !== API_PATHS.LOGIN) {
		authToken = await authenticateUser(accountApi, event, authToken);

		if (authToken) {
			event.locals.token = authToken;
		}
	} else if (authToken) {
		throw redirect(302, API_PATHS.DASHBOARD);
	}

	if (authToken === undefined && pathname !== API_PATHS.LOGIN) {
		throw redirect(302, API_PATHS.LOGIN);
	}

	const response = await resolve(event);
	return response;
};
