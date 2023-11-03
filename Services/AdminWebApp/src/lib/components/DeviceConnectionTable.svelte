<script lang="ts">
	import { Input, Label, Popover, Select } from 'flowbite-svelte';
	import { QuestionCircleSolid } from 'flowbite-svelte-icons';
	import FormErrorChecker from './FormErrorChecker.svelte';

	export let device: Device | undefined;
	export let errors: Record<string, string> | undefined;

	let versionGroup: number = device?.connection?.version || 2;
</script>

<div>
	<h1 class="mb-4 font-bold">Connection Info</h1>
	<div class="mb-6">
		<Input type="hidden" name="id" value={device?.id} />

		<FormErrorChecker name="version" {errors} />
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
			<option value={2}>v2c</option>
			<option value={3}>v3</option>
		</Select>
	</div>
	<div class="mb-6">
		<FormErrorChecker name="ip_address" {errors} />
		<Label for="ip_address" class="mb-1 font-bold">IP Address*</Label>
		<Input name="ip_address" placeholder="127.0.0.1" required={true} value={device?.ip_address} />
	</div>
	<div class="mb-6">
		<FormErrorChecker name="port" {errors} />
		<Label for="port" class="mb-1 font-bold">Port*</Label>
		<Input
			type="number"
			name="port"
			placeholder="161"
			required={true}
			value={device?.connection?.port}
		/>
	</div>
</div>
<div>
	<h1 class="mb-4 font-bold">Security Info</h1>
	{#if versionGroup === 2}
		<div class="mb-6">
			<Label for="community" class="mb-1 font-bold">Community*</Label>
			<Input
				name="community"
				placeholder="public"
				required={true}
				value={device?.connection?.community}
			/>
		</div>
	{:else if versionGroup === 3}
		<div class="mb-6">
			<Label for="username" class="mb-1 font-bold">Username*</Label>
			<Input
				name="username"
				placeholder="admin"
				required={true}
				value={device?.connection?.community}
			/>
		</div>
		<div class="mb-6">
			<Label for="auth_password" class="mb-1 font-bold">Auth Password*</Label>
			<Input
				name="auth_password"
				type="password"
				placeholder="*****"
				required={true}
				value={device?.connection?.auth_password}
			/>
		</div>
		<div class="mb-6">
			<Label for="privacy_password" class="mb-1 font-bold">Privacy Password*</Label>
			<Input
				name="privacy_password"
				type="password"
				placeholder="*****"
				required={true}
				value={device?.connection?.privacy_password}
			/>
		</div>
		<div class="mb-6">
			<Label for="auth_protocol" class="mb-1 font-bold">Auth Protocol*</Label>
			<Select name="auth_protocol" required={true} value={device?.connection?.auth_protocol}>
				<option value={0}>SHA256</option>
				<option value={1}>SHA384</option>
				<option value={2}>SHA512</option>
			</Select>
		</div>
		<div class="mb-6">
			<Label for="privacy_protocol" class="mb-1 font-bold">Privacy Protocol*</Label>
			<Select name="privacy_protocol" required={true} value={device?.connection?.privacy_protocol}>
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
				value={device?.connection?.context_name}
			/>
		</div>
	{/if}
</div>
