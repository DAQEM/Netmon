<script lang="ts">
	import type { User } from '$lib/types/account_types';
	import {
		Button,
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell
	} from 'flowbite-svelte';
	import { slide } from 'svelte/transition';

	export let users: User[];
	export let id: string;
	export let showButtons: boolean = false;

	let openRow: number | null = null;
	let details: User | null = null;

	const toggleRow = (i: number) => {
		openRow = openRow === i ? null : i;
	};
</script>

<Table noborder={true} hoverable={true} class="rounded-xl overflow-hidden" {id}>
	<TableHead class="bg-primary-700 text-white">
		<TableHeadCell>
			<span class="sr-only">Image</span>
		</TableHeadCell>
		<TableHeadCell>Name</TableHeadCell>
		{#if showButtons}
			<TableHeadCell>
				<span class="sr-only">Edit</span>
			</TableHeadCell>
			<TableHeadCell>
				<span class="sr-only">Delete</span>
			</TableHeadCell>
		{/if}
	</TableHead>
	<TableBody tableBodyClass="divide-y">
		{#each users as user, i}
			<TableBodyRow on:click={() => toggleRow(i)} class="cursor-pointer">
				<TableBodyCell>
					<img src={user.profileImageName} alt={user.fullName} class="w-8 h-8 rounded-full" />
				</TableBodyCell>
				<TableBodyCell>{user.fullName}</TableBodyCell>
				{#if showButtons}
					<TableBodyCell tdClass="max-w-[8rem] w-[8rem]">
						<Button color="blue" href="/user/{user.id}/edit">Edit</Button>
					</TableBodyCell>
					<TableBodyCell tdClass="max-w-[8rem] w-[8rem]">
						<Button color="red" href="/user/{user.id}/delete">Delete</Button>
					</TableBodyCell>
				{/if}
			</TableBodyRow>
			{#if openRow === i}
				<TableBodyRow on:dblclick={() => (details = user)}>
					<TableBodyCell colspan="4" class="p-0">
						<div class="px-6 py-6" transition:slide={{ duration: 300, axis: 'y' }}>
							<p><strong>Username: </strong>{user.userName}</p>
							<p><strong>Full Name: </strong>{user.fullName}</p>
							<p><strong>Email: </strong>{user.email}</p>
							<Button class="mt-3	" href="/user/{user.id}/">View profile</Button>
						</div>
					</TableBodyCell>
				</TableBodyRow>
			{/if}
		{/each}
		<slot />
	</TableBody>
</Table>
