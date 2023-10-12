<script lang="ts">
	import { Alert } from "flowbite-svelte";

    export let errors: Record<string, string> | undefined;
    export let name: string;
</script>

{#if errors && errors[name]}
    <Alert type="error" class="mb-4">
        {errors[name]}
    </Alert>
{/if}