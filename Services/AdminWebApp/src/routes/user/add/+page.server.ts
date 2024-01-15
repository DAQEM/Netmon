import AccountAPI from '$lib/api/account_api';
import type { RegisterErrors, RegisterResponse, User } from '$lib/types/account_types';
import type { Actions } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

export const load = (async () => {
	return {};
}) satisfies PageServerLoad;

export const actions: Actions = {
	default: async ({ request, fetch, locals }) => {
		const data: FormData = await request.formData();

		const userName = data.get('userName') as string;
		const fullName = data.get('fullName') as string;
		const email = data.get('email') as string;
		const profileImageName = data.get('profileImageName') as string;
		const password = data.get('password') as string;
		const confirmPassword = data.get('confirmPassword') as string;

		if (password !== confirmPassword) {
			const errors: { [key: string]: string[] } = {
				confirmPassword: ['Password and Confirm Password do not match.']
			};

			return {
				success: false,
				errors,
				user: {
					userName,
					fullName,
					email,
					profileImageName
				}
			};
		}

		const accountApi: AccountAPI = new AccountAPI(fetch);
		const registerResponse: RegisterResponse | RegisterErrors = await accountApi.register(
			userName,
			fullName,
			profileImageName,
			email,
			password
		);

		if ('errors' in registerResponse) {
			return {
				success: false,
				errors: registerResponse.errors,
				user: {
					userName,
					fullName,
					email,
					profileImageName
				}
			};
		}

		const userResponse: User | undefined = await accountApi.getUserByUserName(
			locals.token,
			userName
		);

		if (userResponse) {
			return {
				success: true,
				user: {
					id: userResponse.id,
					userName: userResponse.userName,
					fullName: userResponse.fullName,
					email: userResponse.email,
					profileImageName: userResponse.profileImageName
				}
			};
		}

		return {
			success: true,
			user: {
				userName,
				fullName,
				email,
				profileImageName
			}
		};
	}
};
