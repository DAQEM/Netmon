<script lang="ts">
	import type { Device } from '$lib/types/device_types';
	import {
		Button,
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell
	} from 'flowbite-svelte';

	export let devices: Device[];
	export let id: string;
	export let showButtons: boolean = false;
	export let baseUrl: string;
	const deviceUrl: (device: Device) => string = (device: Device) => `${baseUrl}/device/${device.id}`;
</script>

<Table noborder={true} class="rounded-xl overflow-hidden" {id}>
	<TableHead class="bg-primary-700 text-white">
		<TableHeadCell>
			<span class="sr-only">Image</span>
		</TableHeadCell>
		<TableHeadCell>Name</TableHeadCell>
		<TableHeadCell>IP Address</TableHeadCell>
		{#if showButtons}
			<TableHeadCell>
				<span class="sr-only">Edit</span>
			</TableHeadCell>
			<TableHeadCell>
				<span class="sr-only">View</span>
			</TableHeadCell>
		{/if}
	</TableHead>
	<TableBody tableBodyClass="divide-y">
		{#if devices.length === 0}
			<TableBodyRow>
				<TableBodyCell colspan="5" class="text-center">No devices found</TableBodyCell>
			</TableBodyRow>
		{/if}
		{#each devices as device}
			<TableBodyRow id={`device-${device.ipAddress?.replace(/\./g, '-')}`}>
				<TableBodyCell>
					<a href={deviceUrl(device)}>
						<img
							src={'https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/UbuntuCoF.svg/768px-UbuntuCoF.svg.png'}
							alt={device.name}
							class="w-8 h-8 rounded-full"
						/>
					</a>
				</TableBodyCell>
				<TableBodyCell>
					<a href={deviceUrl(device)}>{device.name}</a></TableBodyCell
				>
				<TableBodyCell><a href={deviceUrl(device)}>{device.ipAddress}</a></TableBodyCell>
				{#if showButtons}
					<TableBodyCell tdClass="max-w-[8rem] w-[8rem]">
						<Button color="blue" href="/device/{device.id}/edit">Edit</Button>
					</TableBodyCell>
					<TableBodyCell tdClass="max-w-[8rem] w-[8rem]">
						<Button color="red" href="/device/{device.id}/delete">Delete</Button>
					</TableBodyCell>
				{/if}
			</TableBodyRow>
		{/each}
		<slot />
	</TableBody>
</Table>
