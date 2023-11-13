<script lang="ts">
	import DiskStatistics from '$lib/components/DiskStatistics.svelte';
import type { PageData } from './$types';

	export let data: PageData;
</script>

<DiskStatistics />