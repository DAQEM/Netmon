<script lang="ts">
	import Button from '$lib/components/Button.svelte';
	import DiskStatistics from '$lib/components/statistics/DiskStatistics.svelte';
	import type { DiskStatisticsList } from '$lib/types';
	import { Alert, Toast } from 'flowbite-svelte';
	import { onDestroy, onMount } from 'svelte';
	import type { PageData } from './$types';

	export let data: PageData;

	const statistics: DiskStatisticsList | null = data.statistics;

	let newData: boolean = false;
	let ws: WebSocket | null = null;

	onMount(() => {
		ws = new WebSocket('ws://localhost:5002/api/ws');

		ws.addEventListener('message', function () {
			newData = true;
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
<DiskStatistics {statistics} />
