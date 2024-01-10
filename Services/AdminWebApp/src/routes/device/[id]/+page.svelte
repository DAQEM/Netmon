<script lang="ts">
	import DeviceSidebar from '$lib/components/DeviceSidebar.svelte';
	import Map from '$lib/components/Map.svelte';
	import type { PageData } from './$types';

	export let data: PageData;
</script>

<div class="flex justify-center mt-16">
	<div class="flex max-w-[90rem] w-full gap-8 mx-4">
		<div>
			<DeviceSidebar device={data.device} />
		</div>
		<div class="flex flex-col gap-8 w-full bg-white rounded-xl p-8">
			<div class="flex items-center gap-8">
				<img
					src="https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/UbuntuCoF.svg/768px-UbuntuCoF.svg.png"
					alt="Logo"
					class="w-16 h-16"
				/>
				<h1 class="text-5xl font-bold text-gray-900">{data.device.name}</h1>
			</div>
			<div>
				<div class="rounded-xl overflow-hidden w-max h-[calc(512px-18px)]">
					<Map height={512} width={512} latitude={52.0} longitude={5.0} />
				</div>
			</div>
		</div>
	</div>
</div>
