<script lang="ts">
	import MemoryStatistics from '$lib/components/MemoryStatistics.svelte';
import type { PageData } from './$types';

	export let data: PageData;
</script>

<MemoryStatistics />