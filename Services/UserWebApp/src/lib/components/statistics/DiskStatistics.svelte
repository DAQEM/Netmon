<script lang="ts">
	import Heading2 from '$lib/components/heading/Heading2.svelte';
	import type { DiskStatisticsList } from '$lib/types';
	import ChartDiskUsage from '../chart/ChartDiskUsage.svelte';

	export let title: string = 'Disk Statistics';
	export let statistics: DiskStatisticsList | null;
</script>

<Heading2>{title}</Heading2>
{#if statistics}
	{#each statistics.disks.sort((a, b) => a.mountingPoint.localeCompare(b.mountingPoint)) as statistic}
		<ChartDiskUsage statistics={statistic} />
	{:else}
		<p>No data available.</p>
	{/each}
{/if}
