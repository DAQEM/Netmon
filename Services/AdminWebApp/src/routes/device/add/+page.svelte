<script lang="ts">
	import { Button, Input, Label, Popover, Select } from 'flowbite-svelte';
	import { PlusSolid, QuestionCircleSolid } from 'flowbite-svelte-icons';

	let versionGroup: number = 2;
</script>

<div class="flex justify-center mt-12">
	<div class="max-w-md w-full p-8 bg-white rounded-xl">
		<div class="mb-6">
			<h1 class="text-xl font-bold">Add device</h1>
			<h2 class="text-sm">
				We only require the login details of the device. All of the other information about the
				device will be polled from the device automatically.
			</h2>
		</div>

		<form method="post">
			<div class="mb-6">
				<Label for="version" class="mb-1 font-bold"
					>SNMP Version
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
				<Select name="version" required={true} id="test" bind:value={versionGroup}>
					<option value={2}>v2c</option>
					<option value={3}>v3</option>
				</Select>
			</div>
			<div class="mb-6">
				<Label for="ip_address" class="mb-1 font-bold">IP Address</Label>
				<Input name="ip_address" placeholder="127.0.0.1" required={true} />
			</div>
			<div class="mb-6">
				<Label for="port" class="mb-1 font-bold">Port</Label>
				<Input type="number" name="port" placeholder="161" required={true} />
			</div>
			{#if versionGroup === 2}
				<div class="mb-6">
					<Label for="community" class="mb-1 font-bold">Community</Label>
					<Input name="community" placeholder="public" required={true} />
				</div>
			{:else if versionGroup === 3}
				<div class="mb-6">
					<Label for="username" class="mb-1 font-bold">Username</Label>
					<Input name="username" placeholder="admin" required={true} />
				</div>
				<div class="mb-6">
					<Label for="authPassword" class="mb-1 font-bold">Auth Password</Label>
					<Input name="authPassword" placeholder="*****" required={true} />
				</div>
				<div class="mb-6">
					<Label for="privacyPassword" class="mb-1 font-bold">Privacy Password</Label>
					<Input name="privacyPassword" placeholder="*****" required={true} />
				</div>
				<div class="mb-6">
					<Label for="authProtocol" class="mb-1 font-bold">Auth Protocol</Label>
					<Select name="authProtocol" required={true}>
						<option value="SHA256">SHA256</option>
						<option value="SHA384">SHA384</option>
						<option value="SHA512">SHA512</option>
					</Select>
				</div>
				<div class="mb-6">
					<Label for="privacyProtocol" class="mb-1 font-bold">Privacy Protocol</Label>
					<Select name="privacyProtocol" required={true}>
						<option value="AES">AES</option>
						<option value="AES192">AES192</option>
						<option value="AES256">AES256</option>
					</Select>
				</div>
				<div class="mb-6">
					<Label for="context" class="mb-1 font-bold">Context Name</Label>
					<Input name="context" placeholder="optional" required={false} />
				</div>
			{/if}
			<Button type="submit" class="w-full">
				<PlusSolid class="w-3.5 h-3.5 mr-2" /> Add
			</Button>
		</form>
	</div>
</div>
