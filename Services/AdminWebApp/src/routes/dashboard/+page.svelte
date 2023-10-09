<script lang="ts">
	import type { PageData } from './$types';

	export let data: PageData;
</script>

<h1>Admin Dashboard</h1>

<p>Welcome, {data.user.name}!</p>
