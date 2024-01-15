<script lang="ts">
	import type { User } from '$lib/types/account_types';
	import { Button, Input } from 'flowbite-svelte';
	import { CloseSolid, TrashBinSolid } from 'flowbite-svelte-icons';
	import type { PageData } from './$types';

	export let data: PageData;

	const user: User = data.user!;
</script>

<div class="flex justify-center items-center mt-12">
	<div class="bg-white rounded-xl p-8 max-w-md">
		<h1 class="text-lg font-bold">Are you sure you want to delete this user?</h1>
		<h2 class="text-sm mt-2 text-red-600 bg-red-100 py-2 px-4 rounded-lg w-max">
			This action cannot be undone.
		</h2>

		<div class="my-8">
			<div class="grid grid-rows-[max-content,max-content] gap-2 rounded-lg">
				<a class="flex gap-2 items-center" href="/user/{user.id}">
					<img src={user.profileImageName} alt="Profile" class="w-12 h-12 rounded-full" />
					<div>
						<h1 class="text-lg font-bold">{user.fullName}</h1>
						<p class="text-gray-600">{user.userName}</p>
					</div>
				</a>
				<div class="my-4">
					<div class="grid grid-cols-2 gap-4">
						<div class="grid gap-2">
							<div>
								<h2 class="text-sm font-bold">Email</h2>
								<p>{user.email}</p>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>

		<form method="post" class="mt-2">
			<Input type="hidden" name="id" value={user?.id} />
			<div class="flex gap-4">
				<Button color="light" href="/user" class="w-full">
					<CloseSolid class="w-3.5 h-3.5 mr-2" />
					Cancel
				</Button>
				<Button type="submit" class="w-full">
					<TrashBinSolid class="w-3.5 h-3.5 mr-2" /> Delete
				</Button>
			</div>
		</form>
	</div>
</div>
