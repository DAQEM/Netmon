<script lang="ts">
	import type { Device } from '$lib/types';
	import {
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell
	} from 'flowbite-svelte';

	export let devices: Device[];
	export let search: string = '';

	$: filteredDevices = devices.filter((device) =>
		Object.values(device).some((value) => value.includes(search))
	);
</script>

<Table hoverable class="rounded-xl overflow-hidden">
	<TableHead class="bg-primary-500 text-white">
		<TableHeadCell>Hostname / Location</TableHeadCell>
		<TableHeadCell>IP Address</TableHeadCell>
	</TableHead>
	<TableBody>
		{#each filteredDevices as device}
			<TableBodyRow>
				<TableBodyCell tdClass="p-0">
					<a class="px-6 py-4 block" href="/device/{device.id}">
						<div class="grid grid-cols-1 grid-rows-2">
							<div class="text-base">
								{device.name}
							</div>
							<div class="text-xs">
								{device.location}
							</div>
						</div>
					</a>
				</TableBodyCell>
				<TableBodyCell tdClass="p-0">
					<a class="px-6 py-4 block" href="/device/{device.id}">
						{device.ip_address}
					</a>
				</TableBodyCell>
			</TableBodyRow>
		{/each}
	</TableBody>
</Table>
