<script lang="ts">
	import {
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell
	} from 'flowbite-svelte';
	import { slide } from 'svelte/transition';
	import type { User } from '@auth/core/types';

	export let users: User[];
	export let id: string;

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
		<TableHeadCell>Email</TableHeadCell>
		<TableHeadCell>
			<span class="sr-only">Edit</span>
		</TableHeadCell>
	</TableHead>
	<TableBody tableBodyClass="divide-y">
		{#each users as user, i}
			<TableBodyRow on:click={() => toggleRow(i)} class="cursor-pointer">
				<TableBodyCell>
					<img src={user.image} alt={user.name} class="w-8 h-8 rounded-full" />
				</TableBodyCell>
				<TableBodyCell>{user.name}</TableBodyCell>
				<TableBodyCell>{user.email}</TableBodyCell>
				<TableBodyCell>
					<a
						href="/user/{user.id}/edit"
						class="font-medium text-primary-600 hover:underline dark:text-primary-500">Edit</a
					>
				</TableBodyCell>
			</TableBodyRow>
			{#if openRow === i}
				<TableBodyRow on:dblclick={() => (details = user)}>
					<TableBodyCell colspan="4" class="p-0">
						<div class="px-2 py-3" transition:slide={{ duration: 300, axis: 'y' }}>
							<div>test</div>
						</div>
					</TableBodyCell>
				</TableBodyRow>
			{/if}
		{/each}
		<slot />
	</TableBody>
</Table>
