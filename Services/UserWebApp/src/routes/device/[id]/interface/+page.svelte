<script lang="ts">
	import InterfaceStatistics from '$lib/components/statistics/InterfaceStatistics.svelte';
	import type { PageData } from './$types';
	import type { InterfaceStatisticsList } from '$lib/types';

	export let data: PageData;
	const statistics: InterfaceStatisticsList | null = data.statistics;
</script>

<InterfaceStatistics {statistics} />