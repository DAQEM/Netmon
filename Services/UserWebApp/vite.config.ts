import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vitest/config';

export default defineConfig({
	plugins: [sveltekit()],
	test: {
		include: ['src/**/*.{test,spec}.{js,ts}']
	},
	server: {
		port: 80,
		proxy: {
			'/admin': {
				target: 'http://localhost:81',
				changeOrigin: true,
				rewrite: (path) => path.replace(/^\/admin/, '')
			}
		},
		fs: {
			strict: false
		},
		cors: true
	}
});
