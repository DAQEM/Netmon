<script lang="ts">
	import type { InterfaceStatisticsList } from '$lib/types';
	import { GridSolid, ListSolid } from 'flowbite-svelte-icons';
	import Button from '../Button.svelte';
	import ChartInterfaceUsage from '../chart/ChartInterfaceUsage.svelte';
	import Heading2 from '../heading/Heading2.svelte';

	export let title: string = 'Interface Statistics';
	export let statistics: InterfaceStatisticsList | null;

	let grid_type: 'grid-cols-1' | 'grid-cols-2' = 'grid-cols-1';
</script>

<div class="flex justify-between gap-4 mb-4">
	<Heading2>{title}</Heading2>
	<div class="flex gap-2">
		<Button color="alternative" on:click={() => (grid_type = 'grid-cols-1')} class="mr-2">
			<ListSolid class="w-4 h-4" />
		</Button>
		<Button color="alternative" on:click={() => (grid_type = 'grid-cols-2')}>
			<GridSolid class="w-4 h-4" />
		</Button>
	</div>
</div>

{#if statistics}
	<div class="grid gap-8 {grid_type}">
		{#each statistics.interfaces as i}
			<ChartInterfaceUsage statistics={i} />
		{/each}
	</div>
{/if}
