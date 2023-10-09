<script lang="ts">
	import Map from '$lib/components/Map.svelte';
	import {
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell
	} from 'flowbite-svelte';
	import { slide } from 'svelte/transition';
	import type { PageData } from './$types';

	export let data: PageData;

	const devices: any[] = data.props.data;

	let openRow: number | null = null;
	let details: any = null;

	const toggleRow = (i: number) => {
		openRow = openRow === i ? null : i;
	};
</script>

<div class="flex justify-center mx-2 mt-10">
	<div class="max-w-7xl w-full">
		<h1 class="text-3xl font-semibold ml-6">All devices</h1>
		<Table noborder={true} hoverable={true}>
			<TableHead>
				<TableHeadCell>Name</TableHeadCell>
				<TableHeadCell>IP Address</TableHeadCell>
				<TableHeadCell>
					<span class="sr-only">Edit</span>
				</TableHeadCell>
			</TableHead>
			<TableBody tableBodyClass="divide-y">
				{#each devices as device, i}
					<TableBodyRow on:click={() => toggleRow(i)}>
						<TableBodyCell>{device.name}</TableBodyCell>
						<TableBodyCell>{device.ipAddress}</TableBodyCell>
						<TableBodyCell>
							<a
								href="/device/{device.id}/edit"
								class="font-medium text-primary-600 hover:underline dark:text-primary-500">Edit</a
							>
						</TableBodyCell>
					</TableBodyRow>
					{#if openRow === i}
						<TableBodyRow on:dblclick={() => (details = device)}>
							<TableBodyCell colspan="4" class="p-0">
								<div class="px-2 py-3" transition:slide={{ duration: 300, axis: 'y' }}>
									<div class="flex gap-4">
										<div class="h-64 w-64 rounded-2xl overflow-hidden">
											<Map id={device.id} height={256} width={256} />
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
														<h3>{device.ipAddress}</h3>
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
			</TableBody>
		</Table>
	</div>
</div>
