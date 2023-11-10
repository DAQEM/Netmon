<script>
	import {
		Navbar,
		NavBrand,
		NavLi,
		NavUl,
		NavHamburger,
		Avatar,
		Dropdown,
		DropdownItem,
		DropdownHeader,
		Input,
		DarkMode
	} from 'flowbite-svelte';
	import logo_wide from '$lib/images/logo_wide.png';
	import logo_wide_white from '$lib/images/logo_wide_white.png';
	import { SearchOutline } from 'flowbite-svelte-icons';
</script>

<Navbar class="px-2 md:px-16">
	<NavBrand href="/">
		<img src={logo_wide} class="dark:hidden block mr-3 h-6 sm:h-9" alt="Netmon Logo" />
		<img src={logo_wide_white} class="hidden dark:block mr-3 h-6 sm:h-9" alt="Netmon Logo" />
	</NavBrand>
	<div class="flex items-center md:order-2">
		<div class="hidden relative lg:block mr-8">
			<div class="flex absolute inset-y-0 left-0 items-center pl-3 pointer-events-none">
				<SearchOutline class="w-4 h-4" />
			</div>
			<Input id="search-navbar" class="pl-10" placeholder="Search..." />
		</div>
		<DarkMode
			btnClass="mr-8"
			class="bg-primary-500 dark:bg-primary-600 w-10 h-10 rounded-xl text-white flex justify-center items-center"
		/>
		<Avatar id="avatar-menu" src="https://applashsolutions.ca/assets/images/Charlie.jpg" />
		<NavHamburger class1="w-full md:flex md:w-auto md:order-1" />
	</div>
	<Dropdown placement="bottom" triggeredBy="#avatar-menu">
		<DropdownHeader>
			<span class="block text-sm">Bonnie Green</span>
			<span class="block truncate text-sm font-medium">name@netmon.com</span>
		</DropdownHeader>
		<DropdownItem>Sign out</DropdownItem>
	</Dropdown>
	<NavUl>
		<NavLi href="/device">Devices</NavLi>
	</NavUl>
</Navbar>
