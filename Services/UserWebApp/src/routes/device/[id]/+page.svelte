<script lang="ts">
	import Heading1 from '$lib/components/heading/Heading1.svelte';
	import type { Device } from '$lib/types';
	import { DropdownDivider } from 'flowbite-svelte';
	import type { PageData } from './$types';

	export let data: PageData;

	const device: Device = data.device;
</script>

<div class="grid gap-8">
	<Heading1>
		<div class="w-max">
			{device.name}
			<DropdownDivider
				class="bg-primary-500 dark:bg-primary-600 rounded-full h-[2px] -mr-5 -ml-1"
			/>
		</div>
	</Heading1>

	<p>
		Lorem ipsum dolor, sit amet consectetur adipisicing elit. Culpa, veritatis dignissimos. Sed sint
		quia quae veritatis magni, harum numquam voluptas explicabo, mollitia amet pariatur est.
		Repudiandae placeat maxime dolor sequi aliquid, voluptatibus corporis culpa est.
	</p>
</div>
