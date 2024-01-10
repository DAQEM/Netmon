import InterfaceStatisticsAPI from '$lib/api/interface_statistics_api';
import type { InterfaceStatisticsList } from '$lib/types';
import type { PageServerLoad } from './$types';

export const load = (async ({ fetch, params: { id }, url: { searchParams } }) => {
	const from: string | null = searchParams.get('from')?.replaceAll('%3A', ':') ?? null;
	const to: string | null = searchParams.get('to')?.replaceAll('%3A', ':') ?? null;

	const fromDate = from ? new Date(from) : new Date(new Date().setDate(new Date().getDate() - 1));
	const toDate = to ? new Date(to) : new Date();

	const statisticsList: InterfaceStatisticsList = await new InterfaceStatisticsAPI(fetch).getInOut(
		id,
		fromDate,
		toDate
	);
	if (statisticsList.interfaces.length === 0) return { statistics: null };

	statisticsList.interfaces.forEach((i) => {
		i.metrics = i.metrics.map((s) => ({
			timestamp: new Date(s.timestamp),
			inOctets: s.inOctets,
			outOctets: s.outOctets
		}));
	});

	return {
		statistics: structuredClone(statisticsList)
	};
}) satisfies PageServerLoad;
