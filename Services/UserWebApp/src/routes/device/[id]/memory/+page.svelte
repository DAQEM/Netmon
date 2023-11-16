<script lang="ts">
	import MemoryStatistics from '$lib/components/statistics/MemoryStatistics.svelte';
	import type { PageData } from './$types';

	export let data: PageData;
</script>

<MemoryStatistics />
