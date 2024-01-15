<script lang="ts">
	import type { Device } from '$lib/types/device_types';

	export let device: Device;
</script>

<div class="grid grid-rows-[max-content,max-content] gap-2 rounded-lg">
	<div class="flex gap-2 items-center">
		<img
			src="https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/UbuntuCoF.svg/768px-UbuntuCoF.svg.png"
			alt="Logo"
			class="w-12 h-12"
		/>
		<h1 class="text-lg font-bold">{device.name}</h1>
	</div>
	<div>
		<div class="grid grid-cols-2 gap-4">
			<div class="grid gap-2">
				<div>
					<h2 class="text-sm font-bold">Name</h2>
					<p>{device.name}</p>
				</div>
				<div>
					<h2 class="text-sm font-bold">IP Address</h2>
					<p>{device.ipAddress}</p>
				</div>
			</div>
			<div class="grid gap-2">
				<div>
					<h2 class="text-sm font-bold">Location</h2>
					<p class="break-all line-clamp-1">
						{device.location}
					</p>
				</div>
				<div>
					<h2 class="text-sm font-bold">Contact</h2>
					<p class="break-all line-clamp-1">
						{device.contact}
					</p>
				</div>
			</div>
		</div>
	</div>
</div>
