<script lang="ts">
	import { Alert, Button, Input, Label, Toast } from 'flowbite-svelte';
	import { CheckCircleSolid, PlusSolid } from 'flowbite-svelte-icons';
	import { slide } from 'svelte/transition';
	import type { ActionData } from './$types';

	export let form: ActionData;
</script>

<div class="flex flex-col items-center mt-12">
	{#if form}
		{#if form?.success}
			<Toast
				transition={slide}
				color="green"
				class="max-w-3xl w-full mb-4 shadow-none px-8"
				contentClass="w-full text-sm flex items-center justify-between text-green-700"
			>
				<CheckCircleSolid slot="icon" class="w-5 h-5" />
				<p id="success-message">User added successfully.</p>
				<Button href="/user/{form?.user?.id}" color="green" class="ml-auto">View user</Button>
			</Toast>
		{:else if form?.errors}
			{#each Object.values(form.errors) as error}
				<Alert color="red" class="max-w-3xl w-full mb-4 shadow-none px-8">
					<p>{error}</p>
				</Alert>
			{/each}
		{/if}
	{/if}
	<div class="max-w-3xl w-full p-8 bg-white rounded-xl">
		<form method="post">
			<div class="">
				<div class="mb-4">
					<h1 class="text-xl font-bold">Add User</h1>
				</div>
				<div class="grid grid-cols-2 gap-6">
					<div>
						<div class="mb-4">
							<Label for="userName" class="mb-1 font-bold">Username*</Label>
							<Input name="userName" required value={form?.user?.userName} id="userName" />
						</div>
						<div class="mb-4">
							<Label for="fullName" class="mb-1 font-bold">Full name*</Label>
							<Input name="fullName" required value={form?.user?.fullName} id="fullName" />
						</div>
						<div class="mb-4">
							<Label for="email" class="mb-1 font-bold">Email*</Label>
							<Input name="email" required value={form?.user?.email} id="email" type="email" />
						</div>
					</div>
					<div>
						<div class="mb-4">
							<Label for="profileImageName" class="mb-1 font-bold">Image URL*</Label>
							<Input
								name="profileImageName"
								required
								value={form?.user?.profileImageName}
								id="fullName"
							/>
						</div>
						<div class="mb-4">
							<Label for="password" class="mb-1 font-bold">Password*</Label>
							<Input name="password" required id="fullName" type="password" />
						</div>
						<div class="mb-4">
							<Label for="confirmPassword" class="mb-1 font-bold">Confirm Password*</Label>
							<Input name="confirmPassword" required id="confirmPassword" type="password" />
						</div>
					</div>
				</div>
				<Button type="submit" class="sm:col-span-2" id="submit">
					<PlusSolid class="w-3.5 h-3.5 mr-2" /> Add
				</Button>
			</div>
		</form>
	</div>
</div>
