<script lang="ts">
	import logo_wide from '$lib/images/logo_wide.png';
	import logo_wide_white from '$lib/images/logo_wide_white.png';
	import type { User } from '$lib/types';
	import {
		Avatar,
		DarkMode,
		Dropdown,
		DropdownHeader,
		DropdownItem,
		Input,
		NavBrand,
		NavHamburger,
		NavLi,
		NavUl,
		Navbar
	} from 'flowbite-svelte';
	import { SearchOutline } from 'flowbite-svelte-icons';
	import Button from './Button.svelte';

	export let user: User | undefined;
</script>

<Navbar class="px-2 md:px-16">
	<NavBrand href="/">
		<img src={logo_wide} class="dark:hidden block mr-3 h-6 sm:h-9" alt="Netmon Logo" />
		<img src={logo_wide_white} class="hidden dark:block mr-3 h-6 sm:h-9" alt="Netmon Logo" />
	</NavBrand>
	{#if user}
		<div class="flex items-center md:order-2">
			<div class="hidden relative lg:block mr-8">
				<form method="POST" action="/device">
					<div class="flex absolute inset-y-0 left-0 items-center pl-3 pointer-events-none">
						<SearchOutline class="w-4 h-4" />
					</div>
					<Input id="search-navbar" class="pl-10" placeholder="Search..." name="value" />
				</form>
			</div>
			<DarkMode
				btnClass="mr-8"
				class="bg-primary-500 dark:bg-primary-600 w-10 h-10 rounded-xl text-white flex justify-center items-center"
			/>
			<Avatar id="avatar-menu" src={user.profileImageName ?? ''} />
			<NavHamburger class1="w-full md:flex md:w-auto md:order-1" />
		</div>
		<Dropdown placement="bottom" triggeredBy="#avatar-menu">
			<DropdownHeader>
				<span class="block text-sm">{user.fullName}</span>
				<span class="block truncate text-sm font-medium">{user.email}</span>
			</DropdownHeader>
			<DropdownItem href="/logout">Sign out</DropdownItem>
		</Dropdown>
		<NavUl>
			<NavLi href="/device">Devices</NavLi>
		</NavUl>
	{:else}
		<Button href="/login">Login</Button>
	{/if}
</Navbar>
