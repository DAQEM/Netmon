<script lang="ts">
	import Button from '$lib/components/Button.svelte';
	import DevicesTable from '$lib/components/DevicesTable.svelte';
	import type { Device } from '$lib/types';
	import { PlusSolid, SearchOutline } from 'flowbite-svelte-icons';
	import type { PageData } from './$types';
	import { Input } from 'flowbite-svelte';
	import Heading1 from '$lib/components/heading/Heading1.svelte';

	export let data: PageData;

	const devices: Device[] = data.devices;
	let search: string = '';
</script>

<Heading1>Devices</Heading1>
<div class="flex gap-4 justify-end mb-4 h-fit">
	<div class="relative">
		<div class="flex absolute inset-y-0 left-0 items-center pl-3 pointer-events-none">
			<SearchOutline class="w-4 h-4" />
		</div>
		<Input
			bind:value={search}
			id="search-navbar"
			class="pl-10 bg-white focus:outline-primary-600 border-2 border-primary-500"
			placeholder="Search..."
		/>
	</div>
</div>
<DevicesTable {devices} {search} />
