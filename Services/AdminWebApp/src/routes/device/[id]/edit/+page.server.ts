import deviceApi from '$lib/api/device_api';
import type { PageServerLoad } from '../$types';
import type { Actions } from './$types';

export const load: PageServerLoad = async ({ params }) => {
	const result = await deviceApi.getDeviceWithConnection(params.id);
	const device: Device = JSON.parse(JSON.stringify(result));
	console.log(device);
	return {};
};

export const actions = {
	default: async (event) => {
		const data: FormData = await event.request.formData();

		const errors: Record<string, string> = {};

		if (
			Number.parseInt(data.get('version') as string) < 1 ||
			Number.parseInt(data.get('version') as string) > 2
		) {
			errors['version'] = 'Version must be 1 or 2';
		}

		if (
			!data
				.get('ip_address')
				?.toString()
				.match(/^(\d{1,3}\.){3}\d{1,3}$/)
		) {
			errors['ip_address'] = 'IP Address is invalid';
		}

		if (
			Number.parseInt(data.get('port') as string) < 1 ||
			Number.parseInt(data.get('port') as string) > 65535
		) {
			errors['port'] = 'Port must be between 1 and 65535';
		}

		if (Object.keys(errors).length === 0) {
			const version = Number.parseInt(data.get('version') as string);
			const community =
				version === 1 ? (data.get('community') as string) : (data.get('username') as string);

			const result = await deviceApi.addDevice({
				ip_address: data.get('ip_address') as string,
				connection: {
					port: Number.parseInt(data.get('port') as string),
					community,
					version,
					auth_password: data.get('auth_password') as string,
					privacy_password: data.get('privacy_password') as string,
					auth_protocol: Number.parseInt(data.get('auth_protocol') as string),
					privacy_protocol: Number.parseInt(data.get('privacy_protocol') as string),
					context_name: data.get('context_name') as string
				}
			});

			console.log(result);
		}

		return {
			errors,
			version: Number.parseInt(data.get('version') as string),
			ip_address: data.get('ip_address') as string,
			port: Number.parseInt(data.get('port') as string),
			community: data.get('community') as string,
			username: data.get('username') as string,
			auth_password: data.get('auth_password') as string,
			privacy_password: data.get('privacy_password') as string,
			auth_protocol: Number.parseInt(data.get('auth_protocol') as string),
			privacy_protocol: Number.parseInt(data.get('privacy_protocol') as string),
			context_name: data.get('context_name') as string
		};
	}
} satisfies Actions;