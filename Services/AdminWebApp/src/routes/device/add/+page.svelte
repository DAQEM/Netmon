<script lang="ts">
	import FormErrorChecker from '$lib/components/FormErrorChecker.svelte';
	import { Button, Input, Label, Popover, Select } from 'flowbite-svelte';
	import { PlusSolid, QuestionCircleSolid } from 'flowbite-svelte-icons';
	import type { ActionData } from './$types';

	export let form: ActionData;

	let versionGroup: number = form?.version || 1;
</script>

<div class="flex justify-center mt-12">
	<div class="max-w-3xl w-full p-8 bg-white rounded-xl">
		<form method="post">
			<div class="grid grid-cols-2 grid-rows-[max-content,max-content,max-content] gap-8">
				<div class="col-span-2">
					<h1 class="text-xl font-bold">Add device</h1>
					<h2 class="text-sm">
						We only require the login details of the device. All of the other information about the
						device will be polled from the device automatically.
					</h2>
				</div>
				<div>
					<h1 class="mb-4 font-bold">Connection Info</h1>
					<div class="mb-6">
						<FormErrorChecker name="version" errors={form?.errors} />
						<Label for="version" class="mb-1 font-bold"
							>SNMP Version*
							<button id="versionInfo" type="button">
								<QuestionCircleSolid class="w-4 h-4 text-gray-800" />
								<span class="sr-only">Show information</span>
							</button>
						</Label>
						<Popover
							class="w-72 text-sm font-light "
							title="SNMP Versions Information"
							triggeredBy="#versionInfo"
						>
							<div class="p-3 space-y-2">
								<h3 class="font-semibold text-gray-900 dark:text-white">SNMP Version 2c</h3>
								Limited security, uses clear-text community strings.
								<h3 class="font-semibold text-gray-900 dark:text-white">SNMP Version 3</h3>
								Strongest security, supports encryption, authentication, access control.
							</div></Popover
						>
						<Select name="version" required={true} bind:value={versionGroup}>
							<option value={1}>v2c</option>
							<option value={2}>v3</option>
						</Select>
					</div>
					<div class="mb-6">
						<FormErrorChecker name="ip_address" errors={form?.errors} />
						<Label for="ip_address" class="mb-1 font-bold">IP Address*</Label>
						<Input
							name="ip_address"
							placeholder="127.0.0.1"
							required={true}
							value={form?.ip_address}
						/>
					</div>
					<div class="mb-6">
						<FormErrorChecker name="port" errors={form?.errors} />
						<Label for="port" class="mb-1 font-bold">Port*</Label>
						<Input type="number" name="port" placeholder="161" required={true} value={form?.port} />
					</div>
				</div>
				<div>
					<h1 class="mb-4 font-bold">Security Info</h1>
					{#if versionGroup === 1}
						<div class="mb-6">
							<Label for="community" class="mb-1 font-bold">Community*</Label>
							<Input
								name="community"
								placeholder="public"
								required={true}
								value={form?.community}
							/>
						</div>
					{:else if versionGroup === 2}
						<div class="mb-6">
							<Label for="username" class="mb-1 font-bold">Username*</Label>
							<Input name="username" placeholder="admin" required={true} value={form?.username} />
						</div>
						<div class="mb-6">
							<Label for="auth_password" class="mb-1 font-bold">Auth Password*</Label>
							<Input
								name="auth_password"
								type="password"
								placeholder="*****"
								required={true}
								value={form?.auth_password}
							/>
						</div>
						<div class="mb-6">
							<Label for="privacy_password" class="mb-1 font-bold">Privacy Password*</Label>
							<Input
								name="privacy_password"
								type="password"
								placeholder="*****"
								required={true}
								value={form?.privacy_password}
							/>
						</div>
						<div class="mb-6">
							<Label for="auth_protocol" class="mb-1 font-bold">Auth Protocol*</Label>
							<Select name="auth_protocol" required={true} value={form?.auth_protocol}>
								<option value={0}>SHA256</option>
								<option value={1}>SHA384</option>
								<option value={2}>SHA512</option>
							</Select>
						</div>
						<div class="mb-6">
							<Label for="privacy_protocol" class="mb-1 font-bold">Privacy Protocol*</Label>
							<Select name="privacy_protocol" required={true} value={form?.privacy_protocol}>
								<option value={0}>AES</option>
								<option value={1}>AES192</option>
								<option value={2}>AES256</option>
							</Select>
						</div>
						<div class="mb-6">
							<Label for="context_name" class="mb-1 font-bold">Context Name</Label>
							<Input
								name="context_name"
								placeholder="optional"
								required={false}
								value={form?.context_name}
							/>
						</div>
					{/if}
				</div>
				<Button type="submit" class="col-span-2">
					<PlusSolid class="w-3.5 h-3.5 mr-2" /> Add
				</Button>
			</div>
		</form>
	</div>
</div>
