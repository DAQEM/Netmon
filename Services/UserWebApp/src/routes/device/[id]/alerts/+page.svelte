<script lang="ts">
	import Heading1 from '$lib/components/heading/Heading1.svelte';
	import Heading2 from '$lib/components/heading/Heading2.svelte';
	import { Alert, DropdownDivider } from 'flowbite-svelte';
	import type { PageData } from './$types';

	export let data: PageData;
</script>

<Heading1>
	<div class="w-max">
		Alerts
		<DropdownDivider class="bg-primary-500 dark:bg-primary-600 rounded-full h-[2px] -mr-5 -ml-1" />
	</div>
</Heading1>
<div class="mt-6 grid gap-4">
	<div>
		<Heading2>Critical</Heading2>
		<div class="mt-2 grid gap-2">
			<Alert color="red" class="dark:bg-red-600 dark:text-white bg-red-400 text-red-900">
				Lorem ipsum dolor sit amet consectetur adipisicing elit. Libero, voluptatum.
			</Alert>
		</div>
	</div>
	<div>
		<Heading2>Warning</Heading2>
		<div class="mt-2 grid gap-2">
			<Alert
				color="yellow"
				class="dark:bg-yellow-600 dark:text-white bg-yellow-300 text-yellow-900"
			>
				Lorem ipsum, dolor sit amet consectetur adipisicing elit. Amet eius iusto tempore obcaecati.
				Illo officia, cupiditate accusantium aut culpa omnis.
			</Alert>
			<Alert
				color="yellow"
				class="dark:bg-yellow-600 dark:text-white bg-yellow-300 text-yellow-900"
			>
				Lorem ipsum dolor sit amet consectetur adipisicing elit. Quisquam, voluptatum.
			</Alert>
		</div>
	</div>
</div>
