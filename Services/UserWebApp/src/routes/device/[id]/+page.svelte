<script lang="ts">
	import Heading1 from '$lib/components/heading/Heading1.svelte';
	import CpuStatistics from '$lib/components/statistics/CpuStatistics.svelte';
	import InterfaceStatistics from '$lib/components/statistics/InterfaceStatistics.svelte';
	import MemoryStatistics from '$lib/components/statistics/MemoryStatistics.svelte';
	import DiskStatistics from '$lib/components/statistics/DiskStatistics.svelte';
	import type { Device } from '$lib/types';
	import { Accordion, AccordionItem, DropdownDivider } from 'flowbite-svelte';
	import type { PageData } from './$types';

	export let data: PageData;

	const device: Device = data.device;
</script>

<div class="grid gap-8">
	<Heading1>
		<div class="w-max">
			{device.name}
			<DropdownDivider
				class="bg-primary-500 dark:bg-primary-600 rounded-full h-[2px] -mr-5 -ml-1"
			/>
		</div>
	</Heading1>
</div>
