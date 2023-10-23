<script lang="ts">
	import DeviceConnectionTable from '$lib/components/DeviceConnectionTable.svelte';
	import { Button } from 'flowbite-svelte';
	import { PlusSolid } from 'flowbite-svelte-icons';
	import type { ActionData, PageData } from './$types';

	export let data: PageData;
	export let form: ActionData;
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
				</div>
				<DeviceConnectionTable {device} errors={form?.errors} />
				<Button type="submit" class="col-span-2">
					<PlusSolid class="w-3.5 h-3.5 mr-2" /> Add
				</Button>
			</div>
		</form>
	</div>
</div>
