<script lang="ts">
	import DevicesTable from '$lib/components/DevicesTable.svelte';
	import UserTable from '$lib/components/UserTable.svelte';
	import type { User } from '$lib/types/account_types';
	import type { Device } from '$lib/types/device_types';
	import { Button, TableBodyCell, TableBodyRow } from 'flowbite-svelte';
	import { ArrowRightSolid } from 'flowbite-svelte-icons';
	import type { PageData } from './$types';

	export let data: PageData;

	const devices: Device[] = data.props.devices;
	const users: User[] = data.props.users;
</script>

<div class="flex justify-center">
	<div class="m-5 max-w-7xl w-full">
		<h1 class="text-2xl font-bold mb-5">Dashboard</h1>
		<div class="w-full grid grid-cols-5 grid-rows-[max-content,max-content,max-content] gap-8">
			<div class="col-span-3 row-span-2">
				<DevicesTable {devices} id="device-table">
					{#if devices.length > 0}
						<TableBodyRow>
							<TableBodyCell colspan="5">
								<div class="flex justify-end">
									<Button color="primary" href="/device">
										View More
										<ArrowRightSolid class="w-4 h-4 ml-2" />
									</Button>
								</div>
							</TableBodyCell>
						</TableBodyRow>
					{/if}
				</DevicesTable>
			</div>
			<div class="col-span-2 row-span-2">
				<UserTable {users} id="user-table">
					<TableBodyRow>
						<TableBodyCell colspan="4">
							<div class="flex justify-end">
								<Button color="primary" href="/user">
									View More
									<ArrowRightSolid class="w-4 h-4 ml-2" />
								</Button>
							</div>
						</TableBodyCell>
					</TableBodyRow>
				</UserTable>
			</div>
			<div class="col-span-2 row-span-1">
				<div class="bg-white h-full text-white rounded-xl overflow-hidden">
					<div class="bg-primary-700 px-6 py-3">
						<h1 class="text-xs font-bold uppercase">Statistics</h1>
					</div>
					<div class="flex text-black p-4">
						<div class="text-center px-6 py-2 border-r-2 border-gray-100">
							<h1 class="text-2xl font-bold">{devices.length}</h1>
							<h2 class="text-xs font-bold">Devices</h2>
						</div>
						<div class="text-center px-6 py-2 border-r-2 border-gray-100">
							<h1 class="text-2xl font-bold">{users.length}</h1>
							<h2 class="text-xs font-bold">Users</h2>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
</div>
