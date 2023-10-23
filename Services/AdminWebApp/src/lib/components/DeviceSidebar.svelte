<script lang="ts">
	import { page } from '$app/stores';
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
	<SidebarWrapper divClass="rounded-xl bg-white p-4">
		<SidebarGroup>
			<h1 class="text-sm font-bold text-gray-500 uppercase">Device</h1>
			<SidebarItem label="Details" href={'/device/' + device.id}>
				<svelte:fragment slot="icon">
					<ChartPieSolid
						class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
					/>
				</svelte:fragment>
			</SidebarItem>
			<SidebarDropdownWrapper label="Components" isOpen>
				<svelte:fragment slot="icon">
					<GridSolid
						class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
					/>
				</svelte:fragment>
				<SidebarDropdownItem label="CPUs" />
				<SidebarDropdownItem label="Disks" />
				<SidebarDropdownItem label="Interfaces" />
				<SidebarDropdownItem label="Memory" />
			</SidebarDropdownWrapper>
			<SidebarItem label="Alerts" {spanClass} href={'/device/' + device.id + '/alerts'}>
				<svelte:fragment slot="icon">
					<MailBoxSolid
						class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
					/>
				</svelte:fragment>
				<svelte:fragment slot="subtext">
					<span
						class="inline-flex justify-center items-center p-3 ml-3 w-3 h-3 text-sm font-medium text-primary-600 bg-primary-200 rounded-full dark:bg-primary-900 dark:text-primary-200"
					>
						3
					</span>
				</svelte:fragment>
			</SidebarItem>
		</SidebarGroup>
		<SidebarGroup border>
			<h1 class="text-sm font-bold text-gray-500 uppercase">Actions</h1>
			<SidebarItem label="Edit" href={'/device/' + device.id + '/edit'}>
				<svelte:fragment slot="icon">
					<FileEditSolid
						class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
					/>
				</svelte:fragment>
			</SidebarItem>
			<SidebarItem label="Delete" href={'/device/' + device.id + '/delete'}>
				<svelte:fragment slot="icon">
					<TrashBinSolid
						class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white"
					/>
				</svelte:fragment>
			</SidebarItem>
		</SidebarGroup>
	</SidebarWrapper>
</Sidebar>
