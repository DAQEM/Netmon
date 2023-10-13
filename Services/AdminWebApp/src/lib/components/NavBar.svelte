<script lang="ts">
	import logo from '$lib/images/logo_wide.png';
	import type { User } from '@auth/core/types';
	import {
		Avatar,
		Dropdown,
		DropdownDivider,
		DropdownHeader,
		DropdownItem,
		NavBrand,
		NavHamburger,
		NavLi,
		NavUl,
		Navbar
	} from 'flowbite-svelte';
	import { ChevronDownOutline, ListSolid, PlusSolid } from 'flowbite-svelte-icons';

	export let user: User;
</script>

<Navbar>
	<NavBrand href="/">
		<img src={logo} class="h-6 sm:h-9" alt="Netmon Logo" />
	</NavBrand>
	<div class="flex items-center md:order-2">
		<Avatar id="avatar-menu" src={user.image ?? ''} />
		<NavHamburger class1="w-full md:flex md:w-auto md:order-1" />
	</div>
	<Dropdown placement="bottom" triggeredBy="#avatar-menu">
		<DropdownHeader>
			<span class="block text-sm">{user.name}</span>
			<span class="block truncate text-sm font-medium">{user.email}</span>
		</DropdownHeader>
		<DropdownItem>Dashboard</DropdownItem>
		<DropdownItem>Settings</DropdownItem>
		<DropdownItem>Earnings</DropdownItem>
		<DropdownDivider />
		<DropdownItem>Sign out</DropdownItem>
	</Dropdown>
	<NavUl>
		<NavLi href="/dashboard" active={true}>Dashboard</NavLi>
		<NavLi class="cursor-pointer">
			Device<ChevronDownOutline
				class="w-3 h-3 ml-2 text-primary-800 dark:text-white inline focus:outline-none"
			/>
		</NavLi>
		<Dropdown class="w-44 z-20">
			<DropdownItem href="/device">
				<ListSolid
					class="mr-3 w-3 h-3 ml-2 text-primary-800 dark:text-white inline focus:outline-none"
				/>Devices
			</DropdownItem>
			<DropdownItem href="/device/add">
				<PlusSolid
					class="mr-3 w-3 h-3 ml-2 text-primary-800 dark:text-white inline focus:outline-none"
				/>Add New Device
			</DropdownItem>
		</Dropdown>

		<NavLi class="cursor-pointer">
			Users<ChevronDownOutline
				class="w-3 h-3 ml-2 text-primary-800 dark:text-white inline focus:outline-none"
			/>
		</NavLi>
		<Dropdown class="w-44 z-20">
			<DropdownItem href="/user">
				<ListSolid
					class="mr-3 w-3 h-3 ml-2 text-primary-800 dark:text-white inline focus:outline-none"
				/>Users
			</DropdownItem>
			<DropdownItem href="/user/add">
				<PlusSolid
					class="mr-3 w-3 h-3 ml-2 text-primary-800 dark:text-white inline focus:outline-none"
				/>Add New User
			</DropdownItem>
		</Dropdown>
	</NavUl>
</Navbar>
