<script lang="ts">
	import DeviceConnectionTable from '$lib/components/DeviceConnectionTable.svelte';
	import { Button, Toast } from 'flowbite-svelte';
	import { CheckCircleSolid, PlusSolid } from 'flowbite-svelte-icons';
	import { slide } from 'svelte/transition';
	import type { ActionData } from './$types';

	export let form: ActionData;

	const device: Device | undefined = form?.device;
</script>

<div class="flex flex-col items-center mt-12">
	{#if form?.success}
		<Toast
			transition={slide}
			color="green"
			class="max-w-3xl w-full mb-4 shadow-none px-8"
			contentClass="w-full text-sm flex items-center justify-between text-green-700"
		>
			<CheckCircleSolid slot="icon" class="w-5 h-5" />
			<p class="">Device added successfully.</p>
			<Button href="/device/{form?.device?.id}" color="green" class="ml-auto">View device</Button>
		</Toast>
	{/if}
	<div class="max-w-3xl w-full p-8 bg-white rounded-xl">
		<form method="post">
			<div
				class="grid grid-cols-1 sm:grid-cols-2 sm:grid-rows-[max-content,max-content,max-content] gap-8"
			>
				<div class="sm:col-span-2">
					<h1 class="text-xl font-bold">Add device</h1>
					<h2 class="text-sm">
						We only require the login details of the device. All of the other information about the
						device will be polled from the device automatically.
					</h2>
				</div>
				<DeviceConnectionTable {device} errors={form?.errors} />
				<Button type="submit" class="sm:col-span-2">
					<PlusSolid class="w-3.5 h-3.5 mr-2" /> Add
				</Button>
			</div>
		</form>
	</div>
</div>
