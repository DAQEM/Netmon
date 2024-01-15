<script lang="ts">
	import UserTable from '$lib/components/UserTable.svelte';
	import type { User } from '$lib/types/account_types';
	import { Button, Input, Popover } from 'flowbite-svelte';
	import { PlusSolid, QuestionCircleSolid, SearchOutline } from 'flowbite-svelte-icons';
	import type { PageData } from './$types';

	export let data: PageData;

	let users: User[] = data.props.users;

	let value: string = '';

	$: users = data.props.users.filter((user: User) => {
		return (
			user.email?.toLowerCase().includes(value.toLowerCase()) ||
			user.fullName?.toLowerCase().includes(value.toLowerCase()) ||
			user.userName?.toLowerCase().includes(value.toLowerCase())
		);
	});
</script>

<div class="flex justify-center mx-2 mt-10">
	<div class="max-w-7xl w-full">
		<div class="flex justify-between items-center">
			<h1 class="text-3xl font-semibold mx-6 my-3">All Users</h1>
			<div class="flex gap-4">
				<div class="relative">
					<div class="flex absolute inset-y-0 left-0 items-center pl-3 pointer-events-none">
						<SearchOutline class="w-4 h-4" />
					</div>
					<Input class="px-10" placeholder="Search..." bind:value />
					<Popover
						class="w-72 text-sm font-light z-50"
						title="Search Parameters"
						triggeredBy="#searchInfo"
					>
						<div class="p-3 space-y-2">
							<h3 class="font-semibold text-gray-900 dark:text-white">Username</h3>
							The username of the user.
							<h3 class="font-semibold text-gray-900 dark:text-white">Full name</h3>
							The full name of the user.
							<h3 class="font-semibold text-gray-900 dark:text-white">Email address</h3>
							The email address of the user.
						</div>
					</Popover>
					<button id="searchInfo" type="button" class="pr-3 absolute inset-y-0 right-0">
						<QuestionCircleSolid class="w-4 h-4 text-gray-800" />
						<span class="sr-only">Show information</span>
					</button>
				</div>

				<Button href="/user/add">
					<PlusSolid class="w-3.5 h-3.5 mr-2" />
					Add user
				</Button>
			</div>
		</div>
		<UserTable {users} id={''} showButtons />
	</div>
</div>
