<script lang="ts">
	import type { User } from '$lib/types/account_types';
	import { Button } from 'flowbite-svelte';
	import type { PageData } from './$types';

	export let data: PageData;
	const user: User = data.user!;
</script>

<div class="flex items-center justify-center">
	<div class="bg-white p-8 rounded-xl max-w-7xl w-full mt-20">
		<!-- User Profile Image and Name -->
		<div class="flex justify-between items-end">
			<div class="flex">
				<img src={user.profileImageName} alt="Profile" class="w-16 h-16 rounded-full mr-8" />
				<div>
					<h2 class="text-2xl font-semibold">{user.fullName}</h2>
					<p class="text-gray-600">{user.userName}</p>
				</div>
			</div>

			<div class="flex gap-2">
				<Button color="blue" href="/user/{user.id}/edit">Edit</Button>
				<Button color="red" href="/user/{user.id}/delete">Delete</Button>
			</div>
		</div>
		<hr class="my-4" />
		<div>
			<h3 class="text-lg font-semibold mb-2">User Details</h3>
			<p><strong>Email:</strong> {user.email}</p>
			<!-- Add more user details as needed -->
		</div>
	</div>
</div>
