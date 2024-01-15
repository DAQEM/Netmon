<script lang="ts">
	import { Alert, Button, Input, Label } from 'flowbite-svelte';
	import type { ActionData } from './$types';

	export let form: ActionData;
</script>

<div class="w-full flex justify-center mt-20 p-2">
	<form class="max-w-xs w-full bg-white p-6 rounded-xl" method="POST">
		{#if form && !form.success}
			<Alert type="error" class="mb-4">The combination of email and password is incorrect.</Alert>
		{/if}
		<div class="flex flex-col gap-6">
			<h1 class="font-bold text-xl capitalize">Login</h1>
			<div>
				<Label for="username" class="mb-1 font-bold">Username</Label>
				<Input type="text" name="username" placeholder="Username" required id="username" />
			</div>
			<div>
				<Label for="password" class="mb-1 font-bold">Password</Label>
				<Input type="password" name="password" placeholder="Password" required id="password" />
			</div>
			<div>
				<Button type="submit" class="w-full" id="submit">Login</Button>
			</div>
		</div>
	</form>
</div>
