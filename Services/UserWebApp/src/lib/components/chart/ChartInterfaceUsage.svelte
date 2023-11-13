<script lang="ts">
	import colors from '$lib/util/colors';
	import type { ApexOptions } from 'apexcharts';
	import { Button, Card, Chart, Dropdown, DropdownItem } from 'flowbite-svelte';
	import { ChevronDownSolid } from 'flowbite-svelte-icons';

	const options: ApexOptions = {
		series: [
			{
				name: 'Organic',
				color: colors.primary['500'],
				data: [
					{ x: 1, y: 231 },
					{ x: 2, y: 122 },
					{ x: 3, y: 63 },
					{ x: 4, y: 421 },
					{ x: 5, y: 122 },
					{ x: 6, y: 323 },
					{ x: 7, y: 111 },
					{ x: 8, y: 111 },
					{ x: 9, y: 111 },
					{ x: 10, y: 111 },
					{ x: 11, y: 111 },
					{ x: 12, y: 111 },
					{ x: 13, y: 111 },
					{ x: 14, y: 111 },
					{ x: 15, y: 111 },
					{ x: 16, y: 111 },
					{ x: 17, y: 111 },
					{ x: 18, y: 111 },
					{ x: 19, y: 111 },
					{ x: 20, y: 111 },
					{ x: 21, y: 111 },
					{ x: 22, y: 111 }
				]
			},
			{
				name: 'Not Organic',
				color: colors.primary['600'],
				data: [
					{ x: 1, y: 231 },
					{ x: 2, y: 122 },
					{ x: 3, y: 63 },
					{ x: 4, y: 421 },
					{ x: 5, y: 122 },
					{ x: 6, y: 323 },
					{ x: 7, y: 111 },
					{ x: 8, y: 111 },
					{ x: 9, y: 111 },
					{ x: 10, y: 111 },
					{ x: 11, y: 111 },
					{ x: 12, y: 111 },
					{ x: 13, y: 111 },
					{ x: 14, y: 111 },
					{ x: 15, y: 111 },
					{ x: 16, y: 111 },
					{ x: 17, y: 111 },
					{ x: 18, y: 111 },
					{ x: 19, y: 111 },
					{ x: 20, y: 111 },
					{ x: 21, y: 111 },
					{ x: 22, y: 111 }
				]
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
				columnWidth: '100%',
				borderRadiusApplication: 'around',
				borderRadius: 0
			}
		},
		tooltip: {
			shared: true,
			intersect: false,
			style: {
				fontFamily: 'Inter, sans-serif'
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
			colors: ['transparent']
		},
		grid: {
			show: false
			// strokeDashArray: 4
			// padding: {
			// 	left: 2,
			// 	right: 2,
			// 	top: -14
			// }
		},
		dataLabels: {
			enabled: false
		},
		legend: {
			show: false
		},
		xaxis: {
			floating: false,
			labels: {
				show: true,
				style: {
					fontFamily: 'Inter, sans-serif',
					cssClass: 'text-xs font-normal fill-gray-500 dark:fill-gray-400'
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
			show: false
		},
		fill: {
			opacity: 1
		}
	};

	$: total = 0;
	$: average = 0;
</script>

<Card class="w-full max-w-none">
	<!-- <div class="flex justify-between pb-4 mb-4 border-b border-gray-200 dark:border-gray-700">
		<div class="flex items-center">
			<div
				class="w-12 h-12 rounded-lg bg-gray-100 dark:bg-gray-700 flex items-center justify-center mr-3"
			>
				<UserGroupSolid class="w-6 h-6 text-gray-500 dark:text-gray-400" />
			</div>
			<div>
				<h5 class="leading-none text-2xl font-bold text-gray-900 dark:text-white pb-1">3.4k</h5>
				<p class="text-sm font-normal text-gray-500 dark:text-gray-400">Leads generated per week</p>
			</div>
		</div>
		<div>
			<span
				class="bg-green-100 text-green-800 text-xs font-medium inline-flex items-center px-2.5 py-1 rounded-md dark:bg-green-900 dark:text-green-300"
			>
				<ArrowUpSolid class="w-2.5 h-2.5 mr-1.5" />
				42.5%
			</span>
		</div>
	</div> -->
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
				>Last 7 days<ChevronDownSolid class="w-2.5 m-2.5 ml-1.5" /></Button
			>
			<Dropdown class="w-40">
				<DropdownItem>Yesterday</DropdownItem>
				<DropdownItem>Today</DropdownItem>
				<DropdownItem>Last 7 days</DropdownItem>
				<DropdownItem>Last 30 days</DropdownItem>
				<DropdownItem>Last 90 days</DropdownItem>
			</Dropdown>
			<!-- <A
				href="/"
				class="uppercase text-sm font-semibold hover:text-primary-700 dark:hover:text-primary-500 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-700 dark:focus:ring-gray-700 dark:border-gray-700 px-3 py-2 hover:no-underline"
			>
				Leads Report
				<ChevronRightSolid class="w-2.5 h-2.5 ml-1.5" />
			</A> -->
		</div>
	</div>
</Card>
