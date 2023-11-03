<script lang="ts">
	import DeviceInfo from '$lib/components/DeviceInfo.svelte';
	import type { PageData } from './$types';
	import { Button, Input } from 'flowbite-svelte';
	import { CloseSolid, TrashBinSolid } from 'flowbite-svelte-icons';

	export let data: PageData;

	const device: Device = data.device;
</script>

<div class="flex justify-center items-center mt-12">
	<div class="bg-white rounded-xl p-8 max-w-md">
		<h1 class="text-lg font-bold">Are you sure you want to delete this device?</h1>
		<h2 class="text-sm mt-2 text-red-600 bg-red-100 py-2 px-4 rounded-lg w-max">
			This action cannot be undone.
		</h2>

		<div class="my-4">
			<DeviceInfo {device} />
		</div>

		<form method="post" class="mt-2">
			<Input type="hidden" name="id" value={device?.id} />
			<div class="flex gap-4">
				<Button color="light" href="/device" class="w-full">
					<CloseSolid class="w-3.5 h-3.5 mr-2" />
					Cancel
				</Button>
				<Button type="submit" class="w-full">
					<TrashBinSolid class="w-3.5 h-3.5 mr-2" /> Delete
				</Button>
			</div>
		</form>
	</div>
</div>
