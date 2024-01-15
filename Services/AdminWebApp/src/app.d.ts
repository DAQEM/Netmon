// See https://kit.svelte.dev/docs/types#app

import type { User } from '$lib/types/account_types';

// for information about these interfaces
declare global {
	namespace App {
		// interface Error {}
		interface Locals {
			token: string;
			user: User | undefined;
		}
		// interface PageData {}
		// interface Platform {}
	}
}

export {};
