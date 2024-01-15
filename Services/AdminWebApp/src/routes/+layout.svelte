<script lang="ts">
	import NavBar from '$lib/components/NavBar.svelte';
	import type { User } from '$lib/types/account_types';
	import '../app.css';
	import type { LayoutData } from './$types';

	export let data: LayoutData;
	let user: User | undefined = data.user;
</script>

<NavBar {user} />
<slot />
