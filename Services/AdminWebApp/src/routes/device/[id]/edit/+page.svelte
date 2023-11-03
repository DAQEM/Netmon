<script lang="ts">
	import DeviceConnectionTable from '$lib/components/DeviceConnectionTable.svelte';
	import { Button } from 'flowbite-svelte';
	import { PenSolid, PlusSolid } from 'flowbite-svelte-icons';
	import type { ActionData, PageData } from './$types';
	import FormErrorChecker from '$lib/components/FormErrorChecker.svelte';

	export let data: PageData;
	export let form: ActionData;

	const device: Device = data.device;
</script>

<div class="flex justify-center mt-12">
	<div class="max-w-3xl w-full p-8 bg-white rounded-xl">
		<form method="post">
			<div class="grid grid-cols-2 grid-rows-[max-content,max-content,max-content] gap-8">
				<div class="col-span-2">
					<h1 class="text-xl font-bold">Edit device</h1>
					<h2 class="text-sm">
						You can only edit the device details that are needed to connect to the device. All of
						the other information about the device will be polled from the device automatically.
					</h2>
					<div class="mt-4">
						<FormErrorChecker name="device" errors={form?.errors} />
					</div>
				</div>
				<DeviceConnectionTable {device} errors={form?.errors} />
				<Button type="submit" class="col-span-2">
					<PenSolid class="w-3.5 h-3.5 mr-2" /> Edit
				</Button>
			</div>
		</form>
	</div>
</div>
