/** @type {import('tailwindcss').Config} */
export default {
	content: [
		'./src/**/*.{html,js,svelte,ts}',
		'./node_modules/flowbite-svelte/**/*.{html,js,svelte,ts}'
	],

	plugins: [require('flowbite/plugin')],

	darkMode: 'class',

	theme: {
		extend: {
			colors: {
				primary: {
					50: '#FFF5F2',
					100: '#FFF1EE',
					200: '#FFE4DE',
					300: '#FFD5CC',
					400: '#FFBCAD',
					500: '#FE795D',
					600: '#EF562F',
					700: '#EB4F27',
					800: '#CC4522',
					900: '#A5371B'
				},
				blue: {
					DEFAULT: '#3B94CB',
					50: '#CCE3F1',
					100: '#BCDAED',
					200: '#9CC9E5',
					300: '#7BB7DC',
					400: '#5BA6D4',
					500: '#3B94CB',
					600: '#3B94CB',
					700: '#3B94CB',
					800: '#3B94CB',
					900: '#3B94CB',
					950: '#3B94CB'
				},
				red: {
					DEFAULT: '#DF4033',
					50: '#F2B4AE',
					100: '#EFA39D',
					200: '#EA827A',
					300: '#E46156',
					400: '#DF4033',
					500: '#DF4033',
					600: '#992218',
					700: '#DF4033',
					800: '#DF4033',
					900: '#DF4033',
					950: '#DF4033'
				}
			}
		}
	}
};
