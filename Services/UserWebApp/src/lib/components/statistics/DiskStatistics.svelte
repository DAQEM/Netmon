<script lang="ts">
	import Heading2 from '$lib/components/heading/Heading2.svelte';
	import ChartInterfaceUsage from '../chart/ChartInterfaceUsage.svelte';

	export let title: string = 'Disk Statistics';
</script>

<Heading2>{title}</Heading2>
<ChartInterfaceUsage />