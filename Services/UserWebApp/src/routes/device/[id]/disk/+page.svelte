<script lang="ts">
	import DiskStatistics from '$lib/components/statistics/DiskStatistics.svelte';
	import type { DiskStatisticsList } from '$lib/types';
	import type { PageData } from './$types';

	export let data: PageData;

	const statistics: DiskStatisticsList | null = data.statistics;
</script>

<DiskStatistics {statistics} />
