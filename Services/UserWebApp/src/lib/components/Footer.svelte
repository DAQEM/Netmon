<script>
	import logo_wide from '$lib/images/logo_wide.png';
	import logo_wide_white from '$lib/images/logo_wide_white.png';
	import { Footer, FooterBrand, FooterCopyright } from 'flowbite-svelte';
</script>

<Footer footerType="socialmedia">
	<div class="md:flex md:justify-between">
		<div class="mb-6 md:mb-0">
			<FooterBrand href="/" src={logo_wide} alt="Netmon Logo" aClass="dark:hidden block" />
			<FooterBrand href="/" src={logo_wide_white} alt="Netmon Logo" aClass="hidden dark:block" />
		</div>
	</div>
	<hr class="my-6 border-gray-200 sm:mx-auto dark:border-gray-700 lg:my-8" />
	<div class="sm:flex sm:items-center sm:justify-between">
		<FooterCopyright href="/" by="Netmon" />
	</div>
</Footer>
