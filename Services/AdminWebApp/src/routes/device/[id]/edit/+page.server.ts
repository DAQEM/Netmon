import deviceApi from '$lib/api/device_api';
import { Error } from '$lib/types/error';
import type { PageServerLoad } from '../$types';
import type { Actions } from './$types';

export const load: PageServerLoad = async ({ params }) => {
	const device = await deviceApi.getDeviceWithConnection(params.id);
	return {
		device
	};
};

export const actions = {
	default: async (event) => {
		const data: FormData = await event.request.formData();

		const errors: Record<string, string> = {};

		if (
			Number.parseInt(data.get('version') as string) < 2 ||
			Number.parseInt(data.get('version') as string) > 3
		) {
			errors['version'] = 'Version must be 2 or 3';
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
				version === 2 ? (data.get('community') as string) : (data.get('username') as string);

			const result = await deviceApi.editDevice(data.get('id') as string, {
				ipAddress: data.get('ip_address') as string,
				connection: {
					port: Number.parseInt(data.get('port') as string),
					community,
					version,
					authPassword: data.get('auth_password') as string,
					privacyPassword: data.get('privacy_password') as string,
					authProtocol: Number.parseInt(data.get('auth_protocol') as string),
					privacyProtocol: Number.parseInt(data.get('privacy_protocol') as string),
					contextName: data.get('context_name') as string
				}
			});

			if (result instanceof Error) {
				errors['device'] = result.message;
			} else {
				return {
					success: true,
					device: structuredClone(result)
				};
			}
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
