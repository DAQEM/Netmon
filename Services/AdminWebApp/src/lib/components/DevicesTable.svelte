<script lang="ts">
	import Map from '$lib/components/Map.svelte';
	import {
		Button,
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell
	} from 'flowbite-svelte';
	import { slide } from 'svelte/transition';

	export let devices: Device[];
	export let id: string;

	let openRow: number | null = null;
	let details: Device | null = null;

	const toggleRow = (i: number) => {
		openRow = openRow === i ? null : i;
	};
</script>

<Table noborder={true} hoverable={true} class="rounded-xl overflow-hidden" {id}>
	<TableHead class="bg-primary-700 text-white">
		<TableHeadCell>
			<span class="sr-only">Image</span>
		</TableHeadCell>
		<TableHeadCell>Name</TableHeadCell>
		<TableHeadCell>IP Address</TableHeadCell>
		<TableHeadCell>
			<span class="sr-only">Edit</span>
		</TableHeadCell>
		<TableHeadCell>
			<span class="sr-only">View</span>
		</TableHeadCell>
	</TableHead>
	<TableBody tableBodyClass="divide-y">
		{#if devices.length === 0}
			<TableBodyRow>
				<TableBodyCell colspan="5" class="text-center">
					No devices found
				</TableBodyCell>
			</TableBodyRow>
		{/if} 
		{#each devices as device, i}
			<TableBodyRow on:click={() => toggleRow(i)} class="cursor-pointer">
				<TableBodyCell>
					<img
						src={'https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/UbuntuCoF.svg/768px-UbuntuCoF.svg.png'}
						alt={device.name}
						class="w-8 h-8 rounded-full"
					/>
				</TableBodyCell>
				<TableBodyCell>{device.name}</TableBodyCell>
				<TableBodyCell>{device.ip_address}</TableBodyCell>
				<TableBodyCell>
					<Button color="none" class="text-primary-700" href="/device/{device.id}/edit">Edit</Button
					>
				</TableBodyCell>
				<TableBodyCell>
					<Button color="none" class="text-primary-700" href="/device/{device.id}">View</Button>
				</TableBodyCell>
			</TableBodyRow>
			{#if openRow === i}
				<TableBodyRow on:dblclick={() => (details = device)}>
					<TableBodyCell colspan="5" class="p-0">
						<div class="px-2 py-3" transition:slide={{ duration: 300, axis: 'y' }}>
							<div class="flex gap-4">
								<div class="h-64 w-64 rounded-2xl overflow-hidden">
									<Map height={256} width={256} latitude={52.0} longitude={5.0} />
								</div>
								<div class="flex items-center">
									<div class="flex gap-16">
										<div class="flex flex-col gap-4">
											<div>
												<h2 class="text-xs font-bold">Name</h2>
												<h3>{device.name}</h3>
											</div>
											<div>
												<h2 class="text-xs font-bold">IP Address</h2>
												<h3>{device.ip_address}</h3>
											</div>
											<div>
												<h2 class="text-xs font-bold">Location</h2>
												<h3>{device.location}</h3>
											</div>
											<div>
												<h2 class="text-xs font-bold">Contact</h2>
												<h3>{device.contact}</h3>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</TableBodyCell>
				</TableBodyRow>
			{/if}
		{/each}
		<slot />
	</TableBody>
</Table>
