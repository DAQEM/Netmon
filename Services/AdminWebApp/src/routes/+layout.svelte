<script lang="ts">
	import NavBar from '$lib/components/NavBar.svelte';
	import type { User } from '@auth/core/types';
	import '../app.css';
	import type { LayoutData } from './$types';

	export let data: LayoutData;
	let user: User = data.user;
</script>

<NavBar {user} />
<slot />
