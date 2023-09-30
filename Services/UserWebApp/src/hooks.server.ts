import { redirect, type Handle } from "@sveltejs/kit";

export const handle: Handle = async ({ event, resolve }) => {
    const response = await resolve(event);

    if (event.url.pathname.startsWith('/admin')) {
        throw redirect(302, 'http://localhost:81/');
    }

    return response;
};