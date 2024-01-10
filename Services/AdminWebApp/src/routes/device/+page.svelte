<script lang="ts">
	import DevicesTable from '$lib/components/DevicesTable.svelte';
	import type { Device } from '$lib/types/device_types';
	import { Button, Input, Popover } from 'flowbite-svelte';
	import { PlusSolid, QuestionCircleSolid, SearchOutline } from 'flowbite-svelte-icons';
	import type { PageData } from './$types';

	export let data: PageData;

	let devices: Device[] = data.props.devices;

	let searchValue: string = '';

	function search(e: Event) {
		searchValue = (e.target as HTMLInputElement).value;
	}

	$: devices = data.props.devices.filter((device: Device) => {
		return (
			device.name?.toLowerCase().includes(searchValue.toLowerCase()) ||
			device.ipAddress?.toLowerCase().includes(searchValue.toLowerCase()) ||
			device.location?.toLowerCase().includes(searchValue.toLowerCase()) ||
			device.contact?.toLowerCase().includes(searchValue.toLowerCase())
		);
	});
</script>

<div class="flex justify-center mx-2 mt-10">
	<div class="max-w-7xl w-full">
		<div class="flex justify-between items-center">
			<h1 class="text-3xl font-semibold mx-6 my-3">All Devices</h1>
			<div class="flex gap-4">
				<div class="relative">
					<div class="flex absolute inset-y-0 left-0 items-center pl-3 pointer-events-none">
						<SearchOutline class="w-4 h-4" />
					</div>
					<Input class="px-10" placeholder="Search..." on:input={(e) => search(e)} />
					<Popover
						class="w-72 text-sm font-light z-50"
						title="Search Parameters"
						triggeredBy="#searchInfo"
					>
						<div class="p-3 space-y-2">
							<h3 class="font-semibold text-gray-900 dark:text-white">Name</h3>
							The name of the device.
							<h3 class="font-semibold text-gray-900 dark:text-white">IP Address</h3>
							The IP address of the device.
							<h3 class="font-semibold text-gray-900 dark:text-white">Location</h3>
							The location of the device.
							<h3 class="font-semibold text-gray-900 dark:text-white">Contact</h3>
							The contact of the device.
						</div>
					</Popover>
					<button id="searchInfo" type="button" class="pr-3 absolute inset-y-0 right-0">
						<QuestionCircleSolid class="w-4 h-4 text-gray-800" />
						<span class="sr-only">Show information</span>
					</button>
				</div>

				<Button href="/device/add">
					<PlusSolid class="w-3.5 h-3.5 mr-2" />
					Add device
				</Button>
			</div>
		</div>
		<DevicesTable {devices} id={''} />
	</div>
</div>
