<script lang="ts">
	import { page } from '$app/stores';
	import type { Device } from '$lib/types';
	import {
		Sidebar,
		SidebarDropdownItem,
		SidebarDropdownWrapper,
		SidebarGroup,
		SidebarItem,
		SidebarWrapper
	} from 'flowbite-svelte';
	import {
		ChartPieSolid,
		FileEditSolid,
		GridSolid,
		MailBoxSolid,
		TrashBinSolid
	} from 'flowbite-svelte-icons';

	export let device: Device;

	let spanClass = 'flex-1 ml-3 whitespace-nowrap';
	$: activeUrl = $page.url.pathname;
</script>

<Sidebar {activeUrl}>
	<SidebarWrapper class="bg-white rounded-xl">
		<SidebarGroup>
			<h1 class="text-sm font-bold uppercase">Device</h1>
			<SidebarItem label="Details" href={'/device/' + device.id}>
				<svelte:fragment slot="icon">
					<ChartPieSolid class="w-5 h-5" />
				</svelte:fragment>
			</SidebarItem>
			<SidebarDropdownWrapper label="Components" isOpen>
				<svelte:fragment slot="icon">
					<GridSolid class="w-5 h-5" />
				</svelte:fragment>
				<SidebarDropdownItem label="CPUs" />
				<SidebarDropdownItem label="Disks" />
				<SidebarDropdownItem label="Interfaces" />
				<SidebarDropdownItem label="Memory" />
			</SidebarDropdownWrapper>
			<SidebarItem label="Alerts" {spanClass} href={'/device/' + device.id + '/alerts'}>
				<svelte:fragment slot="icon">
					<MailBoxSolid class="w-5 h-5" />
				</svelte:fragment>
				<svelte:fragment slot="subtext">
					<span
						class="inline-flex justify-center items-center p-3 ml-3 w-3 h-3 text-sm font-medium text-red-600 bg-red-200 rounded-full dark:bg-red-900 dark:text-red-200"
					>
						3
					</span>
				</svelte:fragment>
			</SidebarItem>
		</SidebarGroup>
	</SidebarWrapper>
</Sidebar>
