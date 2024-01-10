<script lang="ts">
	import InterfaceStatistics from '$lib/components/statistics/InterfaceStatistics.svelte';
	import type { InterfaceStatisticsList } from '$lib/types';
	import { Alert, Toast } from 'flowbite-svelte';
	import { onDestroy, onMount } from 'svelte';
	import type { PageData } from './$types';
	import Button from '$lib/components/Button.svelte';

	export let data: PageData;
	const statistics: InterfaceStatisticsList | null = data.statistics;

	let newData: boolean = false;
	let ws: WebSocket | null = null;

	onMount(() => {
		ws = new WebSocket('ws://localhost:5002/api/ws');

		ws.onopen = function () {
			console.log('WebSocket Client Connected');
		};

		ws.addEventListener('message', function (event) {
			newData = true;
			console.log('Message from server ', event.data);
		});
	});

	onDestroy(() => {
		if (ws) {
			ws.close();
		}
	});
</script>

{#if newData}
	<Toast class="max-w-none mb-4" on:close={() => (newData = false)}>
		<Alert type="info">
			The statistics have been updated. <Button
				color="primary"
				class="m-0 px-2 py-1"
				on:click={() => location.reload()}>Click here to refresh.</Button
			>
		</Alert>
	</Toast>
{/if}
<InterfaceStatistics {statistics} />
