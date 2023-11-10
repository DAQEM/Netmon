/** @type {import('tailwindcss').Config} */
export default {
  content: ['./src/**/*.{html,js,svelte,ts}', './node_modules/flowbite-svelte/**/*.{html,js,svelte,ts}'],

  plugins: [require('flowbite/plugin')],

  darkMode: 'class',

  theme: {
    extend: {
      colors: {
        'primary': {
          '50': '#f4fbea',
          '100': '#e6f6d1',
          '200': '#ceeda9',
          '300': '#aee076',
          '400': '#92d050',
          '500': '#71b42e',
          '600': '#568f21',
          '700': '#426e1d',
          '800': '#38581c',
          '900': '#304b1c',
          '950': '#17290a',
        }
      }
    }
  },
  plugins: [],
}

