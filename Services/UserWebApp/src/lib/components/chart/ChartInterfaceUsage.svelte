<script lang="ts">
	import { page } from '$app/stores';
	import type { InterfaceMetrics, InterfaceStatistics } from '$lib/types';
	import colors from '$lib/util/colors';
	import type { ApexOptions } from 'apexcharts';
	import { Button, Card, Chart, Dropdown, DropdownItem, Input, Label } from 'flowbite-svelte';
	import { ChevronDownSolid } from 'flowbite-svelte-icons';

	export let statistics: InterfaceStatistics | null;

	let options: ApexOptions = {};

	const from =
		$page.url.searchParams.get('from')?.replaceAll('%3A', ':') ??
		new Date(new Date().setDate(new Date().getDate() - 1)).toISOString();
	const to = $page.url.searchParams.get('to')?.replaceAll('%3A', ':') ?? new Date().toISOString();
	const fromDate = new Date(from);
	const toDate = new Date(to);
	let total: string = '0';
	let average: string = '0';

	if (statistics && statistics.metrics.length > 0) {
		let metrics: InterfaceMetrics[] = statistics.metrics;

		//Round to 5 minutes and deduct previous value
		let lastInOctets = metrics[0].inOctets;
		let inStats = metrics.map((metric: InterfaceMetrics) => {
			const result = {
				x: new Date(Math.round(metric.timestamp.getTime() / 300000) * 300000).getTime(),
				y: metric.inOctets - lastInOctets
			};
			lastInOctets = metric.inOctets;
			return result;
		});
		let lastOutOctets = metrics[0].outOctets;
		let outStats = metrics.map((metric: InterfaceMetrics) => {
			const result = {
				x: new Date(Math.round(metric.timestamp.getTime() / 300000) * 300000).getTime(),
				y: metric.outOctets - lastOutOctets
			};
			lastOutOctets = metric.outOctets;
			return result;
		});

		//Convert to Bytes/s
		inStats = inStats.map((stat) => {
			return {
				x: stat.x,
				y: stat.y / 300
			};
		});
		outStats = outStats.map((stat) => {
			return {
				x: stat.x,
				y: stat.y / 300
			};
		});

		//Remove duplicates
		inStats = inStats.filter((stat, index, self) => {
			return index === self.findIndex((s) => s.x === stat.x);
		});
		outStats = outStats.filter((stat, index, self) => {
			return index === self.findIndex((s) => s.x === stat.x);
		});

		//Add missing values
		if (from && to) {
			const fromTimestamp = Math.round(fromDate.getTime() / 300000) * 300000;
			const toTimestamp = Math.round(toDate.getTime() / 300000) * 300000 - 3600000;
			if (inStats.length > 0 && inStats[0].x > fromTimestamp) {
				inStats.unshift({
					x: fromTimestamp,
					y: 0
				});
			}
			if (inStats.length > 0 && inStats[inStats.length - 1].x < toTimestamp) {
				inStats.push({
					x: toTimestamp,
					y: 0
				});
			}
			if (outStats.length > 0 && outStats[0].x > fromTimestamp) {
				outStats.unshift({
					x: fromTimestamp,
					y: 0
				});
			}
			if (outStats.length > 0 && outStats[outStats.length - 1].x < toTimestamp) {
				outStats.push({
					x: toTimestamp,
					y: 0
				});
			}
		}

		options = {
			series: [
				{
					name: 'In',

					color: colors.primary['500'],
					data: inStats
				},
				{
					name: 'Out',
					color: colors.primary['600'],
					data: outStats
				}
			],
			chart: {
				type: 'bar',
				height: '320px',
				fontFamily: 'Inter, sans-serif',
				stacked: true,
				toolbar: {
					show: true
				}
			},
			plotOptions: {
				bar: {
					horizontal: false,
					columnWidth: '110%',
					borderRadiusApplication: 'around',
					borderRadius: -1
				}
			},
			tooltip: {
				shared: true,
				intersect: false,
				style: {
					fontFamily: 'Inter, sans-serif'
				},
				onDatasetHover: {
					highlightDataSeries: false
				}
			},
			states: {
				hover: {
					filter: {
						type: 'darken',
						value: 1
					}
				}
			},
			stroke: {
				show: false,
				width: 0,
				colors: ['transparent'],
				curve: 'straight'
			},
			grid: {
				show: false
			},
			dataLabels: {
				enabled: false
			},
			legend: {
				show: true,
				fontSize: '22px',
				fontWeight: 600
			},
			xaxis: {
				floating: false,
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
					let inMin = inStats.length > 0 ? Math.min(...inStats.map((stat) => stat.y)) : 0;
					let inMax = inStats.length > 0 ? Math.max(...inStats.map((stat) => stat.y)) : 0;
					let difference = inMax - inMin;
					return Math.max(inMin - difference * 0.2, 0);
				})(),
				labels: {
					show: true,
					style: {
						cssClass: 'text-xs font-normal fill-neutral-800 dark:fill-neutral-200'
					},
					formatter: function (value: number) {
						return bytesToBitsString(value, true);
					}
				}
			},
			fill: {
				opacity: 1
			}
		};

		total = bytesToBitsString(
			inStats.reduce((a, b) => a + b.y, 0) + outStats.reduce((a, b) => a + b.y, 0)
		);
		average = bytesToBitsString(Number.parseInt(total) / (inStats.length + outStats.length));

		console.log(inStats.reduce((a, b) => a + b.y, 0) + outStats.reduce((a, b) => a + b.y, 0));
	}

	function bytesToBitsString(value: number, addPerSecond: boolean = false): string {
		value = value * 8;
		if (value < 1000) {
			return value.toFixed(2) + ' b' + (addPerSecond ? '/s' : 'it');
		} else if (value < 1000000) {
			return (value / 1000).toFixed(2) + ' Kb' + (addPerSecond ? '/s' : 'it');
		} else if (value < 1000000000) {
			return (value / 1000000).toFixed(2) + ' Mb' + (addPerSecond ? '/s' : 'it');
		} else if (value < 1000000000000) {
			return (value / 1000000000).toFixed(2) + ' Gb' + (addPerSecond ? '/s' : 'it');
		} else {
			return (value / 1000000000000).toFixed(2) + ' Tb' + (addPerSecond ? '/s' : 'it');
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
				{statistics.name}
			</p>
		</div>
	{/if}
	<div class="grid grid-cols-2">
		<dl class="flex items-center">
			<dt class="text-gray-500 dark:text-gray-400 text-sm font-normal mr-1">Total:</dt>
			<dd class="text-gray-900 text-sm dark:text-white font-semibold">{total}</dd>
		</dl>
		<dl class="flex items-center justify-end">
			<dt class="text-gray-500 dark:text-gray-400 text-sm font-normal mr-1">Average:</dt>
			<dd class="text-gray-900 text-sm dark:text-white font-semibold">{average}</dd>
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
