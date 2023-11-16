<script lang="ts">
	import { page } from '$app/stores';
	import type { DiskStatistics } from '$lib/types';
	import colors from '$lib/util/colors';
	import type { ApexOptions } from 'apexcharts';
	import { Button, Card, Chart, Dropdown, DropdownItem, Input, Label } from 'flowbite-svelte';
	import { ChevronDownSolid } from 'flowbite-svelte-icons';

	export let statistics: DiskStatistics | null;

	const from =
		$page.url.searchParams.get('from')?.replaceAll('%3A', ':') ??
		new Date(new Date().setDate(new Date().getDate() - 1)).toISOString();
	const to = $page.url.searchParams.get('to')?.replaceAll('%3A', ':') ?? new Date().toISOString();
	const fromDate = new Date(from);
	const toDate = new Date(to);

	let totalSpace: { x: number; y: number }[] =
		statistics?.metrics.map((metric) => {
			const result = {
				x: metric.timestamp.getTime(),
				y: metric.totalSpace * metric.allocationUnits
			};
			return result;
		}) ?? [];
	let usedSpace: { x: number; y: number }[] =
		statistics?.metrics.map((metric) => {
			const result = {
				x: metric.timestamp.getTime(),
				y: metric.usedSpace * metric.allocationUnits
			};
			return result;
		}) ?? [];

	usedSpace.sort((a, b) => a.x - b.x);

	if (from && to) {
		const fromTimestamp = Math.round(fromDate.getTime() / 300000) * 300000;
		const toTimestamp = Math.round(toDate.getTime() / 300000) * 300000 - 3600000;
		if (totalSpace.length > 0 && totalSpace[0].x > fromTimestamp) {
			totalSpace.unshift({
				x: fromTimestamp,
				y: 0
			});
		}
		if (totalSpace.length > 0 && totalSpace[totalSpace.length - 1].x < toTimestamp) {
			totalSpace.push({
				x: toTimestamp,
				y: 0
			});
		}
		if (usedSpace.length > 0 && usedSpace[0].x > fromTimestamp) {
			usedSpace.unshift({
				x: fromTimestamp,
				y: 0
			});
		}
		if (usedSpace.length > 0 && usedSpace[usedSpace.length - 1].x < toTimestamp) {
			usedSpace.push({
				x: toTimestamp,
				y: 0
			});
		}
		//if the gap between 2 timestamps is bigger than 20 minutes, add a new timestamp with 0 value
		for (let i = 0; i < totalSpace.length - 1; i++) {
			if (totalSpace[i + 1].x - totalSpace[i].x > 1200000) {
				totalSpace.splice(i + 1, 0, {
					x: totalSpace[i].x + 1200000,
					y: 0
				});
			}
		}
		for (let i = 0; i < usedSpace.length - 1; i++) {
			if (usedSpace[i + 1].x - usedSpace[i].x > 1200000) {
				usedSpace.splice(i + 1, 0, {
					x: usedSpace[i].x + 1200000,
					y: 0
				});
			}
		}

		if (totalSpace.length > 3) {
			for (let i = 1; i < totalSpace.length - 1; i++) {
				if (totalSpace[i - 1].y === 0 && totalSpace[i + 1].y === 0) {
					totalSpace.splice(i, 1);
					i--;
				}
			}
		}
		if (usedSpace.length > 3) {
			for (let i = 1; i < usedSpace.length - 1; i++) {
				if (usedSpace[i - 1].y === 0 && usedSpace[i + 1].y === 0) {
					usedSpace.splice(i, 1);
					i--;
				}
			}
		}
	}

	let options: ApexOptions = {
		chart: {
			height: '400px',
			type: 'area',
			fontFamily: 'Inter, sans-serif',
			dropShadow: {
				enabled: false
			},
			toolbar: {
				show: true
			}
		},
		tooltip: {
			enabled: true,
			cssClass: 'text-black',
			x: {
				show: false
			}
		},
		fill: {
			type: 'gradient',
			gradient: {
				opacityFrom: 0.55,
				opacityTo: 0,
				shade: colors.primary['500'],
				gradientToColors: [colors.primary['500']]
			}
		},
		dataLabels: {
			enabled: false
		},
		stroke: {
			width: 6
		},
		grid: {
			show: false,
			strokeDashArray: 4,
			padding: {
				left: 2,
				right: 2,
				top: 0
			}
		},
		series: [
			{
				name: 'Used Space',
				data: usedSpace,
				color: colors.primary['500']
			},
			{
				name: 'Total Space',
				data: totalSpace,
				color: colors.primary['600']
			}
		],
		xaxis: {
			floating: false,
			// categories: timestamps,
			labels: {
				show: true,
				style: {
					cssClass: 'text-xs font-normal fill-neutral-800 dark:fill-neutral-200'
				},
				formatter: function (value: string) {
					return new Date(value).toLocaleString('nl-nl', {
						hour: '2-digit',
						minute: '2-digit',
						day: '2-digit',
						month: '2-digit',
						year: 'numeric'
					});
				}
			},
			axisBorder: {
				show: false
			},
			axisTicks: {
				show: false
			}
		},
		yaxis: {
			show: true,
			min: (() => {
				const filteredUsedSpace = usedSpace.filter((stat) => stat.y != 0);
				let min =
					filteredUsedSpace.length > 0 ? Math.min(...filteredUsedSpace.map((stat) => stat.y)) : 0;
				let max =
					filteredUsedSpace.length > 0 ? Math.max(...filteredUsedSpace.map((stat) => stat.y)) : 0;
				let difference = max - min;
				return Math.max(max - difference - difference * 0.2, 0);
			})(),
			labels: {
				show: true,
				style: {
					cssClass: 'text-xs font-normal fill-neutral-800 dark:fill-neutral-200'
				},
				formatter: function (value: number) {
					return bytesToString(value, true);
				}
			}
		}
	};

	function bytesToString(value: number, addPerSecond: boolean = false): string {
		if (value < 1000) {
			return value.toFixed(2) + ' B';
		} else if (value < 1000000) {
			return (value / 1000).toFixed(2) + ' KB';
		} else if (value < 1000000000) {
			return (value / 1000000).toFixed(2) + ' MB';
		} else if (value < 1000000000000) {
			return (value / 1000000000).toFixed(2) + ' GB';
		} else {
			return (value / 1000000000000).toFixed(2) + ' TB';
		}
	}

	function applyRange(fromDays: number, toDays: number) {
		const fromDate = new Date(new Date().setDate(new Date().getDate() - fromDays));
		const toDate = new Date(new Date().setDate(new Date().getDate() - toDays));
		console.log(fromDate, toDate);
		replaceStateWithQuery({
			from: fromDate.toISOString(),
			to: toDate.toISOString()
		});
		location.reload();
	}

	function applyCustomRange() {
		const fromDate = document.getElementById('fromDateInput') as HTMLInputElement;
		const toDate = document.getElementById('toDateInput') as HTMLInputElement;
		if (fromDate.value === '' || toDate.value === '') return;
		replaceStateWithQuery({
			from: fromDate.value,
			to: toDate.value
		});
		location.reload();
	}

	let replaceStateWithQuery = (values: Record<string, string>) => {
		const url = new URL(window.location.toString());
		for (let [k, v] of Object.entries(values)) {
			if (!!v) {
				url.searchParams.set(encodeURIComponent(k), encodeURIComponent(v));
			} else {
				url.searchParams.delete(k);
			}
		}
		history.replaceState({}, '', url);
	};

	function getFilterName() {
		if (from && to) {
			const fromDate = new Date(from);
			const toDate = new Date(to);
			const now = new Date();
			const today = new Date();
			today.setDate(today.getDate() - 1);
			const yesterday = new Date();
			yesterday.setDate(yesterday.getDate() - 2);
			const lastWeek = new Date();
			lastWeek.setDate(lastWeek.getDate() - 7);
			const lastMonth = new Date();
			lastMonth.setDate(lastMonth.getDate() - 30);
			const lastThreeMonths = new Date();
			lastThreeMonths.setDate(lastThreeMonths.getDate() - 90);
			if (
				fromDate.getDate() === yesterday.getDate() &&
				fromDate.getMonth() === yesterday.getMonth() &&
				fromDate.getFullYear() === yesterday.getFullYear() &&
				toDate.getDate() === today.getDate() &&
				toDate.getMonth() === today.getMonth() &&
				toDate.getFullYear() === today.getFullYear()
			) {
				return 'Yesterday';
			} else if (
				fromDate.getDate() === today.getDate() &&
				fromDate.getMonth() === today.getMonth() &&
				fromDate.getFullYear() === today.getFullYear() &&
				toDate.getDate() === now.getDate() &&
				toDate.getMonth() === now.getMonth() &&
				toDate.getFullYear() === now.getFullYear()
			) {
				return 'Today';
			} else if (
				fromDate.getDate() === lastWeek.getDate() &&
				fromDate.getMonth() === lastWeek.getMonth() &&
				fromDate.getFullYear() === lastWeek.getFullYear() &&
				toDate.getDate() === now.getDate() &&
				toDate.getMonth() === now.getMonth() &&
				toDate.getFullYear() === now.getFullYear()
			) {
				return 'Last 7 days';
			} else if (
				fromDate.getDate() === lastMonth.getDate() &&
				fromDate.getMonth() === lastMonth.getMonth() &&
				fromDate.getFullYear() === lastMonth.getFullYear() &&
				toDate.getDate() === now.getDate() &&
				toDate.getMonth() === now.getMonth() &&
				toDate.getFullYear() === now.getFullYear()
			) {
				return 'Last 30 days';
			} else if (
				fromDate.getDate() === lastThreeMonths.getDate() &&
				fromDate.getMonth() === lastThreeMonths.getMonth() &&
				fromDate.getFullYear() === lastThreeMonths.getFullYear() &&
				toDate.getDate() === now.getDate() &&
				toDate.getMonth() === now.getMonth() &&
				toDate.getFullYear() === now.getFullYear()
			) {
				return 'Last 90 days';
			} else {
				return (
					fromDate.toLocaleString('nl-nl', {
						day: '2-digit',
						month: '2-digit',
						year: 'numeric'
					}) +
					' - ' +
					toDate.toLocaleString('nl-nl', {
						day: '2-digit',
						month: '2-digit',
						year: 'numeric'
					})
				);
			}
		} else {
			return 'Last 7 days';
		}
	}
</script>

<Card class="w-full max-w-none">
	{#if statistics}
		<div class="flex justify-center text-xl font-bold !text-white">
			<p class="bg-primary-500 rounded-xl px-4 py-2 w-max">
				{statistics.mountingPoint}
			</p>
		</div>
	{/if}
	<div class="grid grid-cols-2">
		<dl class="flex items-center">
			<dt class="text-gray-500 dark:text-gray-400 text-sm font-normal mr-1">Total:</dt>
			<dd class="text-gray-900 text-sm dark:text-white font-semibold">
				{bytesToString(totalSpace.at(-1)?.y ?? 0)}
			</dd>
		</dl>
		<dl class="flex items-center justify-end">
			<dt class="text-gray-500 dark:text-gray-400 text-sm font-normal mr-1">Used:</dt>
			<dd class="text-gray-900 text-sm dark:text-white font-semibold">
				{(((usedSpace.at(-1)?.y ?? 0) / (totalSpace.at(-1)?.y ?? 0)) * 100).toFixed(2)}%
			</dd>
		</dl>
	</div>
	<Chart {options} />
	<div
		class="grid grid-cols-1 items-center border-gray-200 border-t dark:border-gray-700 justify-between"
	>
		<div class="flex justify-between items-center pt-5">
			<Button
				class="text-sm font-medium text-gray-500 dark:text-gray-400 hover:text-gray-900 text-center inline-flex items-center dark:hover:text-white bg-transparent hover:bg-transparent dark:bg-transparent dark:hover:bg-transparent focus:ring-transparent dark:focus:ring-transparent py-0"
				>{getFilterName()}<ChevronDownSolid class="w-2.5 m-2.5 ml-1.5" /></Button
			>
			<Dropdown class="w-96">
				<DropdownItem on:click={() => applyRange(2, 1)}>Yesterday</DropdownItem>
				<DropdownItem on:click={() => applyRange(1, 0)}>Today</DropdownItem>
				<DropdownItem on:click={() => applyRange(7, 0)}>Last 7 days</DropdownItem>
				<DropdownItem on:click={() => applyRange(30, 0)}>Last 30 days</DropdownItem>
				<DropdownItem on:click={() => applyRange(90, 0)}>Last 90 days</DropdownItem>
				<DropdownItem defaultClass="bg-gray-100 p-4 border-t-2 border-gray-300 mt-4">
					<p class="text-sm font-medium text-gray-500 dark:text-gray-400 mb-2">Custom range</p>
					<div class="flex gap-4 mb-4 w-full">
						<div class="flex-1">
							<Label for="fromDateInput">From</Label>
							<Input id="fromDateInput" type="date" class="w-full" />
						</div>
						<div class="flex-1">
							<Label for="fromDateInput">To</Label>
							<Input id="toDateInput" type="date" class="w-full" />
						</div>
					</div>
					<Button class="w-full" on:click={applyCustomRange}>Apply</Button>
				</DropdownItem>
			</Dropdown>
		</div>
	</div>
</Card>
