<script lang="ts">
	import { browser } from '$app/environment';
	import { onDestroy, onMount } from 'svelte';

	export let id: string;
	export let height: number;
	export let width: number;

	let mapElement: HTMLDivElement;
	let map: any;

	onMount(async () => {
		if (browser) {
			//@ts-ignore
			const leaflet = await import('leaflet');

			map = leaflet.map(mapElement, {
				center: [51.714 + 0.013, 5.273],
				zoom: 13
			});

			map.zoomControl.remove();
			// map.dragging.disable();

			leaflet
				.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
					attribution:
						'&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
				})
				.addTo(map);

			leaflet.marker([51.714, 5.273]).addTo(map);

			mapElement.style.width = `${width}px`;
			mapElement.style.height = `${height + 20}px`;
		}
	});

	onDestroy(async () => {
		if (map) {
			console.log('Unloading Leaflet map.');
			map.remove();
		}
	});
</script>

<div {id}>
	<div bind:this={mapElement} />
</div>

<style>
	@import 'leaflet/dist/leaflet.css';
</style>
