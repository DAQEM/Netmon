<script lang="ts">
	import Button from '$lib/components/Button.svelte';
	import { CheckSolid } from 'flowbite-svelte-icons';
	import { onMount } from 'svelte';

	onMount(() => {
		const title = document.getElementById('title');
		const circles = document.getElementById('circles');
		if (title && circles) {
			title.addEventListener('click', () => onClick(circles));
		}

		const onClick = (circles: HTMLElement) => {
			const randomChild = circles.children[Math.floor(Math.random() * circles.children.length)];
			if (randomChild) {
				const newChild = randomChild.cloneNode(true) as HTMLElement;
				newChild.style.animationDuration = `${Math.floor(Math.random() * 5) + 5}s`;
				const newCircles = document.getElementById('new_circles');
				if (newCircles) {
					newCircles.appendChild(newChild);
					console.log(newCircles.children.length);
				}
			}
		};

		function createSVG(data: number[], id: string) {
			const chartWidth = document.getElementById('test')!.clientWidth;
			let barWidth = chartWidth / data.length;
			data = data.slice(0, Math.floor(chartWidth / 5));
			barWidth = chartWidth / data.length;
			const chartHeight = 300;

			const svg = document.createElementNS('http://www.w3.org/2000/svg', 'svg');
			svg.classList.add('full-width');
			svg.setAttribute('width', chartWidth + '');
			svg.setAttribute('height', chartHeight + '');

			// Create bars
			data.forEach((value, index) => {
				const barHeight = (value / Math.max(...data)) * chartHeight;
				const x = index * barWidth;
				const y = chartHeight - barHeight;

				const rect = document.createElementNS('http://www.w3.org/2000/svg', 'rect');
				rect.setAttribute('x', x + '');
				rect.setAttribute('y', y + '');
				rect.setAttribute('width', barWidth + 1 + '');
				rect.setAttribute('height', barHeight + '');
				rect.setAttribute('fill', 'inherit');

				svg.appendChild(rect);
			});

			// Append the SVG to the chart container
			const chartContainer = document.getElementById(id);
			if (chartContainer) {
				while (chartContainer.firstChild) {
					chartContainer.removeChild(chartContainer.firstChild);
				}
				chartContainer.appendChild(svg);
			}
		}

		createSVG(
			Array.from({ length: 200 }, () => Math.floor(Math.random() * 10) + 20),
			'chart-container-1'
		);
		createSVG(
			Array.from({ length: 200 }, () => Math.floor(Math.random() * 50) + 50),
			'chart-container-2'
		);

		//on resize
		window.addEventListener('resize', () => {
			createSVG(
				Array.from({ length: 200 }, () => Math.floor(Math.random() * 10) + 20),
				'chart-container-1'
			);
			createSVG(
				Array.from({ length: 200 }, () => Math.floor(Math.random() * 50) + 50),
				'chart-container-2'
			);
		});
	});
</script>

<div class="w-full h-[80vh] full-width">
	<div class="text-[4rem] lg:text-[10rem]">
		<div class="circle-container h-full select-none">
			<div class="absolute h-full flex items-center -mt-24 lg-mt-0" id="title">
				<h1 class="font-bold text-center cool-text">Netmon</h1>
			</div>
			<div
				id="subtitle"
				class="mt-8 lg:mt-64 px-3 pb-2 pt-1 rounded-xl bg-primary-500 dark:bg-primary-600 bg-opacity-75"
			>
				<h2 class="font-bold text-center text-base lg:text-4xl">Network Monitoring made simple</h2>
			</div>
			<div id="circles">
				<div
					class="absolute circle border-primary-900 w-[200px] h-[150px] left-[calc(50%-100px)] top-[calc(50vh-75px-100px)] lg:w-[550px] lg:h-[300px] lg:left-[calc(50%-275px)] lg:top-[calc(50vh-150px-100px)]"
					id="circle-1"
				/>
				<div
					class="absolute circle border-primary-800 w-[250px] h-[150px] left-[calc(50%-125px)] top-[calc(50vh-75px-100px)] lg:w-[750px] lg:h-[350px] lg:left-[calc(50%-375px)] lg:top-[calc(50vh-175px-100px)]"
					id="circle-2"
				/>
				<div
					class="absolute circle border-primary-700 w-[250px] h-[150px] left-[calc(50%-125px)] top-[calc(50vh-75px-100px)] lg:w-[600px] lg:h-[400px] lg:left-[calc(50%-300px)] lg:top-[calc(50vh-200px-100px)]"
					id="circle-3"
				/>
				<div
					class="absolute circle border-primary-600 w-[150px] h-[150px] left-[calc(50%-75px)] top-[calc(50vh-75px-100px)] lg:w-[500px] lg:h-[300px] lg:left-[calc(50%-250px)] lg:top-[calc(50vh-150px-100px)]"
					id="circle-4"
				/>
				<div
					class="absolute circle border-primary-500 w-[200px] h-[150px] left-[calc(50%-100px)] top-[calc(50vh-75px-100px)] lg:w-[700px] lg:h-[350px] lg:left-[calc(50%-350px)] lg:top-[calc(50vh-175px-100px)]"
					id="circle-5"
				/>
				<div
					class="absolute circle border-primary-400 w-[250px] h-[150px] left-[calc(50%-125px)] top-[calc(50vh-75px-100px)] lg:w-[650px] lg:h-[400px] lg:left-[calc(50%-325px)] lg:top-[calc(50vh-200px-100px)]"
					id="circle-6"
				/>
			</div>
			<div id="new_circles" />
		</div>
	</div>
</div>
<div class="w-full full-width fill-primary-500 -z-10 overflow-hidden">
	<div>
		<div id="chart-container-2" class="left-0 absolute fill-primary-700" />
		<div id="chart-container-1" class="left-0 absolute mt-20" />
	</div>
</div>
<div class="bg-primary-500 mt-80 full-width h-60 lg:pb-64 pb-10" id="test">
	<div class="grid lg:grid-cols-[40%,60%] grid-cols-1">
		<div class="flex flex-col justify-center">
			<h1
				class="text-4xl cool-text text-transparent bg-clip-text bg-gradient-to-r from-white to-transparent"
			>
				See the Invisible
			</h1>
			<p class="text-lg text-white">
				Netmon, a revolutionary network monitoring tool, breathes magic into your SNMP devices.
				Stand back as stunning graphs spring to life, framed by the raw data from your devices.
			</p>
		</div>
		<div class="hidden lg:flex justify-center items-center">
			<div class="w-1 h-1">
				<div
					class="!z-10 circle border-primary-700 w-[400px] h-[200px] lg:w-[400px] lg:h-[100px] -rotate-[12deg]"
				/>
			</div>
			<div class="w-1 h-1">
				<div
					class="!z-10 circle border-primary-800 w-[400px] h-[200px] lg:w-[400px] lg:h-[100px] -rotate-[2654deg]"
				/>
			</div>
			<div class="w-1 h-1">
				<div
					class="!z-10 circle border-primary-600 w-[400px] h-[200px] lg:w-[400px] lg:h-[100px] -rotate-[32deg]"
				/>
			</div>
		</div>
	</div>
</div>
<div class="bg-primary-500 full-width lg:py-32">
	<div class="grid lg:grid-cols-[60%,40%] grid-cols-1 lg:gap-20">
		<div class="">
			<img src="https://i.imgur.com/yuNm6cX.png" class="w-full rounded-xl" alt="user interface" />
		</div>
		<div class="flex flex-col justify-center mt-10 mb-20 lg:m-0">
			<h1 class="text-3xl cool-text text-white">Become a Network Whisperer</h1>
			<p class="text-lg text-white">
				Let Netmon's orchestration shine a torch into the shadows of your network. Embrace the
				light, make your strategy proactive not reactive!
			</p>
		</div>
	</div>
</div>
<div class="h-3 bg-gradient-to-b from-primary-500 to-transparent full-width" />
<div class="my-12 lg:my-32">
	<div class="grid lg:grid-cols-[1fr,min-content,1fr] grid-cols-1 gap-8">
		<div class="hidden lg:block mt-6">
			<div class="h-2 bg-primary-500 rounded-full" />
		</div>
		<h1 class="text-center text-5xl cool-text text-primary-800 mb-4">Prices</h1>
		<div class="hidden lg:block mt-6">
			<div class="h-2 bg-primary-500 rounded-full" />
		</div>
	</div>
	<div id="crads" class="grid lg:grid-cols-[32%,36%,32%]">
		<div class="card !my-6">
			<h1>Start-Up</h1>
			<h2>$19.99/mo</h2>
			<div class="mt-4">
				<ul>
					<li><CheckSolid class="mr-2" /> Single User</li>
					<li><CheckSolid class="mr-2" /> Basic Monitoring</li>
					<li><CheckSolid class="mr-2" /> Email Support</li>
					<li><CheckSolid class="mr-2" /> 30 Devices</li>
				</ul>
			</div>
			<Button class="mt-4">Start Free Trial</Button>
		</div>
		<div class="card">
			<h1>SME</h1>
			<h2 class="!text-4xl">$49.99/mo</h2>
			<div class="mt-4">
				<ul>
					<li><CheckSolid class="mr-2" /> Unlimited User</li>
					<li><CheckSolid class="mr-2" /> Advanced Monitoring</li>
					<li><CheckSolid class="mr-2" /> Phone Support</li>
					<li><CheckSolid class="mr-2" /> 100 Devices</li>
				</ul>
			</div>
			<Button class="mt-4">Start Free Trial</Button>
		</div>
		<div class="card !my-6">
			<h1>Enterprise</h1>
			<h2>$99.99/mo</h2>
			<div class="mt-4">
				<ul>
					<li><CheckSolid class="mr-2" /> Dedicated Manager</li>
					<li><CheckSolid class="mr-2" /> Premium Support</li>
					<li><CheckSolid class="mr-2" /> Unlimited Devices</li>
					<li><CheckSolid class="mr-2" /> All features</li>
				</ul>
			</div>
			<Button class="mt-4">Contact Sales</Button>
		</div>
	</div>
</div>

<style lang="postcss">
	@import url('https://fonts.googleapis.com/css2?family=Lemon&family=Open+Sans:wght@300&display=swap');

	@keyframes rotation {
		from {
			transform: rotate(0deg);
		}
		to {
			transform: rotate(360deg);
		}
	}

	@keyframes rotation-reverse {
		from {
			transform: rotate(360deg);
		}
		to {
			transform: rotate(0deg);
		}
	}

	.card {
		background-color: #fff;
		border-radius: 1rem;
		box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
		display: flex;
		flex-direction: column;
		justify-content: space-between;
		margin: 1rem;
		padding: 2rem;
		position: relative;
		transition: all 0.3s ease-in-out;
	}

	.card:hover {
		box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.25);
		transform: translateY(-0.25rem);
	}

	.card > h1 {
		font-size: 1rem;
		font-weight: 700;
		margin-bottom: 0.5rem;
	}

	.card > h2 {
		color: #999;
		font-size: 2rem;
		@apply font-bold;
		@apply text-primary-800;
		@apply cool-text;
		margin-bottom: 0.5rem;
	}

	.card li {
		align-items: center;
		display: flex;
		font-size: 0.875rem;
		margin-bottom: 0.5rem;
	}

	.circle-container {
		align-content: center;
		align-items: center;
		display: flex;
		flex: none;
		flex-direction: column;
		flex-wrap: nowrap;
		gap: 10px;
		justify-content: center;
		max-width: 1000px;
		overflow: visible;
		padding: 0;
		position: relative;
		width: 100%;
	}
	.circle {
		border-bottom-width: 6px;
		border-left-width: 10px;
		border-right-width: 13px;
		border-top-width: 8px;
		border-style: solid;
		border-radius: 100%;
		flex: none;
		overflow: hidden;
		z-index: -1;
	}
	@media (max-width: 640px) {
		.circle {
			border-bottom-width: 3px;
			border-left-width: 5px;
			border-right-width: 7px;
			border-top-width: 4px;
		}
	}

	#circle-1 {
		animation: rotation-reverse 8s linear infinite;
		animation-delay: 0s;
	}

	#circle-2 {
		animation: rotation 17s linear infinite;
		animation-delay: -4s;
	}

	#circle-3 {
		animation: rotation-reverse 32s linear infinite;
		animation-delay: -8s;
	}

	#circle-4 {
		animation: rotation 9s linear infinite;
		animation-delay: -16s;
	}

	#circle-5 {
		animation: rotation-reverse 19s linear infinite;
		animation-delay: -32s;
	}

	#circle-6 {
		animation: rotation 27s linear infinite;
		animation-delay: -64s;
	}

	.cool-text {
		font-family: 'Lemon', serif;
	}
</style>
