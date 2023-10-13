<script lang="ts">
	import DevicesTable from '$lib/components/DevicesTable.svelte';
	import type { PageData } from './$types';

	export let data: PageData;

	const devices: Device[] = data.props.devices;
</script>

<div class="flex justify-center mx-2 mt-10">
	<div class="max-w-7xl w-full">
		<h1 class="text-3xl font-semibold mx-6 my-3">All devices</h1>
		<DevicesTable {devices} />
	</div>
</div>
