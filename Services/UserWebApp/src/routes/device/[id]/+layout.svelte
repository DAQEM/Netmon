<script lang="ts">
	import { page } from '$app/stores';
	import DeviceSidebar from '$lib/components/DeviceSidebar.svelte';
	import type { Device } from '$lib/types';
	import type { LayoutData } from './$types';

	export let data: LayoutData;
	const device: Device = data.device;

	$: activeUrl = $page.url.pathname;
</script>

<div class="breakout">
	<div class="breakout col-span-5 grid grid-cols-[max-content,1fr] gap-8">
		<DeviceSidebar {device} {activeUrl} />
		<div class="breakout p-8 bg-white dark:bg-gray-800 rounded-xl h-max">
			<slot />
		</div>
	</div>
</div>
