<script>
	import Footer from '$lib/components/Footer.svelte';
	import Navbar from '$lib/components/Navbar.svelte';
	import '../app.css';
</script>

<Navbar />
<main class="content-grid my-8 min-h-screen">
	<slot />
</main>
<Footer />
