import type {
	LoginErrors,
	LoginResponse,
	RegisterErrors,
	RegisterResponse,
	User
} from '$lib/types/account_types';
import urlHandler from './url_handler';

export default class AccountAPI {
	fetch: typeof fetch;

	constructor(customFetch: typeof fetch) {
		this.fetch = customFetch;
	}

	getUrl(url: string): string {
		return urlHandler.getAccountServiceUrl(`${url}`);
	}

	async register(
		userName: string,
		fullName: string,
		profileImageName: string,
		email: string,
		password: string
	): Promise<RegisterResponse | RegisterErrors> {
		return await this.fetch(this.getUrl('/myregister'), {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ userName, fullName, profileImageName, email, password })
		})
			.then((res) => {
				if (res.ok) return { success: true };
				else return res.json();
			})
			.catch(() => {
				console.error('Error in register');
				return { success: false };
			});
	}

	async login(username: string, password: string): Promise<LoginResponse | LoginErrors> {
		return await this.fetch(this.getUrl('/login'), {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ email: username, password })
		})
			.then((res) => res.json())
			.catch(() => {
				console.error('Error in login');
				return { success: false };
			});
	}

	async authenticate(token: string): Promise<User | undefined> {
		return await this.fetch(this.getUrl('/authenticate'), {
			method: 'GET',
			headers: {
				'Content-Type': 'application/json',
				Authorization: 'Bearer ' + token
			}
		})
			.then((res) => {
				if (res.ok) return res.json();
				else return undefined;
			})
			.catch(() => {
				console.error('Error in authenticate');
				return false;
			});
	}

	async refresh(refreshToken: string): Promise<LoginResponse> {
		return await this.fetch(this.getUrl('/refresh'), {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ refreshToken })
		})
			.then((res) => res.json())
			.catch(() => {
				console.error('Error in refresh');
				return { success: false };
			});
	}

	async getUser(token: string, id: string): Promise<User | undefined> {
		return await this.fetch(this.getUrl('/users/' + id), {
			method: 'GET',
			headers: {
				Authorization: 'Bearer ' + token
			}
		})
			.then((res) => {
				if (res.ok) return res.json();
				else return undefined;
			})
			.catch((err) => {
				console.error('Error in users', err);
				return false;
			});
	}

    async getUserByUserName(token: string, userName: string): Promise<User | undefined> {
		return await this.fetch(this.getUrl('/users/userName/' + userName), {
			method: 'GET',
			headers: {
				Authorization: 'Bearer ' + token
			}
		})
			.then((res) => {
				if (res.ok) return res.json();
				else return undefined;
			})
			.catch((err) => {
				console.error('Error in users', err);
				return false;
			});
	}

	async getAllUsers(token: string): Promise<User[]> {
		return await this.fetch(this.getUrl('/users'), {
			method: 'GET',
			headers: {
				Authorization: 'Bearer ' + token
			}
		})
			.then((res) => res.json())
			.catch(() => {
				console.error('Error in users');
				return [] as User[];
			});
	}

	async deleteUser(token: string, id: string): Promise<void> {
		await this.fetch(this.getUrl('/users/' + id), {
			method: 'DELETE',
			headers: {
				Authorization: 'Bearer ' + token
			}
		}).catch(() => {
			console.error('Error in delete user');
		});
	}
}
