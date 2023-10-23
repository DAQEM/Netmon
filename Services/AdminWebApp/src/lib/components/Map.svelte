<script lang="ts">
	import { browser } from '$app/environment';
	import { onDestroy, onMount } from 'svelte';

	export let height: number;
	export let width: number;

	export let latitude: number;
	export let longitude: number;

	let mapElement: HTMLDivElement;
	let map: any;

	onMount(async () => {
		if (browser) {
			mapElement.style.width = `${width}px`;
			mapElement.style.height = `${height}px`;

			//@ts-ignore
			const leaflet = await import('leaflet');

			map = leaflet.map(mapElement, {
				center: [latitude, longitude],
				zoom: 10
			});

			map.zoomControl.remove();

			leaflet
				.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
					attribution:
						'&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
				})
				.addTo(map);

			leaflet.marker([latitude, longitude]).addTo(map);

			map.on('click', () => {
				window.open(`https://www.google.com/maps/search/${latitude},${longitude}`, '_blank');
			});

			map.panTo([latitude, longitude]);
		}
	});

	onDestroy(async () => {
		if (map) {
			console.log('Unloading Leaflet map.');
			map.remove();
		}
	});
</script>

<main>
	<div bind:this={mapElement} />
</main>

<style>
	@import 'leaflet/dist/leaflet.css';
	main div {
		height: 800px;
	}
</style>
