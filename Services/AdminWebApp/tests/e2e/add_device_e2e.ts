import test, { expect, type BrowserContext } from '@playwright/test';

test.describe.serial('Add Device', () => {
	const ipAddress = '1.1.1.1';
	let context: BrowserContext;

	test.beforeAll(async ({ browser }) => {
		context = await browser.newContext();
	});

	test.afterAll(async () => {
		await context.close();
	});

	test('login', async () => {
		const page = await context.newPage();

		await page.goto('/login');

		await page.fill('#username', 'demo');
		await page.fill('#password', 'P@ssw0rd!');
		await page.click('#submit');

		const url = page.url();
		expect(url).toBe('http://localhost:4173/dashboard');
	});

	test('Add device to database', async () => {
		const page = await context.newPage();

		await page.goto('/device/add');

		await page.selectOption('#version', '2');
		await page.fill('#ip_address', ipAddress);
		await page.fill('#port', '161');
		await page.fill('#community', 'public');

		await page.click('#submit');

		const success = await page.innerText('#success-message');
		expect(success).toBe('Device added successfully.');
	});

	test('Device should be added to table', async () => {
		const page = await context.newPage();

		await page.goto('/device');

		const device = await page.innerText(
			`#device-${ipAddress.replace(/\./g, '-')} > td:nth-child(3)`
		);
		expect(device).toBe(ipAddress);
	});
});
