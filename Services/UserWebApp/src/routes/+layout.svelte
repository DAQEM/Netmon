<script lang="ts">
	import Footer from '$lib/components/Footer.svelte';
	import Navbar from '$lib/components/Navbar.svelte';
	import type { User } from '$lib/types';
	import '../app.css';
	import type { LayoutData } from './$types';

	export let data: LayoutData;
	const user: User | undefined = data.user;
</script>

<Navbar {user} />
<main class="content-grid my-8 min-h-screen">
	<slot />
</main>
<Footer />
