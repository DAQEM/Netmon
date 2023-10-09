import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async () => {
	const url = 'http://localhost:5000/api/device/device'
	const res = await fetch(url);
	const data = await res.json();
	return {
		props: {
			data
		}
	};
};
