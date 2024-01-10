import DiskStatisticsAPI from '$lib/api/disk_statistics_api';
import type { DiskStatisticsList } from '$lib/types';
import type { PageServerLoad } from './$types';

export const load = (async ({ params: { id }, url: { searchParams } }) => {
	const from: string | null = searchParams.get('from')?.replaceAll('%3A', ':') ?? null;
	const to: string | null = searchParams.get('to')?.replaceAll('%3A', ':') ?? null;

	const fromDate = from ? new Date(from) : new Date(new Date().setDate(new Date().getDate() - 1));
	const toDate = to ? new Date(to) : new Date();

	const statisticsList: DiskStatisticsList = await new DiskStatisticsAPI(fetch).getStatistics(
		id,
		fromDate,
		toDate
	);
	if (statisticsList.disks.length === 0) return { statistics: null };

	statisticsList.disks.forEach((i) => {
		i.metrics = i.metrics.map((s) => ({
			timestamp: new Date(s.timestamp),
			allocationUnits: s.allocationUnits,
			totalSpace: s.totalSpace,
			usedSpace: s.usedSpace
		}));
	});

	return {
		statistics: structuredClone(statisticsList)
	};
}) satisfies PageServerLoad;
