import type { User } from '@auth/core/types';
import type { LayoutServerLoad } from './$types';

export const load: LayoutServerLoad = async () => {
	const user: User = {
		id: '1',
		name: 'John Doe',
		email: 'johndoe@gmail.com',
		image: 'https://i.pravatar.cc/100'
	};

	return {
		user
	};
};
