<script lang="ts">
	import DeviceSidebar from '$lib/components/DeviceSidebar.svelte';
	import Heading1 from '$lib/components/Heading1.svelte';
	import type { Device } from '$lib/types';
	import { DropdownDivider } from 'flowbite-svelte';
	import type { PageData } from './$types';

	export let data: PageData;

	const device: Device = data.device;
</script>

<div class="breakout grid grid-cols-[max-content,1fr] gap-8">
	<DeviceSidebar {device} />
	<div class="p-8 bg-white dark:bg-gray-800 rounded-xl h-max">
		<Heading1>
			<div class="w-max">
				{device.name}
				<DropdownDivider
					class="bg-primary-500 dark:bg-primary-600 rounded-full h-[2px] -mr-5 -ml-1"
				/>
			</div>
		</Heading1>
	</div>
</div>
