<script lang="ts">
	import CpuStatistics from '$lib/components/statistics/CpuStatistics.svelte';
	import type { PageData } from './$types';

	export let data: PageData;
</script>

<CpuStatistics />